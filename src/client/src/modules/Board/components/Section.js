import React, { Component, useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';
import { useQuery, useMutation } from "@apollo/react-hooks";
import _ from "lodash";
import { List, CircularProgress, Divider, Card, CardHeader, CardContent } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import { useDrop } from 'react-dnd'
import ROUTES from "../../appRouter/routes";
import { queries, subscriptions } from "../../Board";
import BoardTask from './SectionTask';
import { TASK_STATUSES } from "../../Task/constants";
import withLoader from '../../shared/withLoader';
import { compose, lifecycle } from 'recompose';
import { CARD_DRAG_ID } from '../constants';
import { updateTodo } from '../mutations';

const styles = {
    spinnerContainer: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        background: "floralwhite"
    },
    boardTasksContainer: {
        margin: "12px 2px",
        display: "grid",
        gridTemplateRows: "auto auto 1fr",
    },
    header: {
        background: "#f9f7f7"
    },
    headerText: {
        textAlign: "center",
    },
    [`headerText${TASK_STATUSES.Open}`]: {
        color: "#bdbdbd",
    },
    [`headerText${TASK_STATUSES["In Progress"]}`]: {
        color: "#03a9f4",
    },
    [`headerText${TASK_STATUSES.Review}`]: {
        color: "#9575cd"
    },
    [`headerText${TASK_STATUSES.Done}`]: {
        color: "#388e3c"
    },
    scrollbarContainer: isOver => ({
        paddingLeft: 0,
        paddingRight: 0,
        overflowY : "hidden",
        backgroundColor: isOver ? '#ececec' : null
    }),
    list: {
        padding: "0px 8px",
    }
};

const Section = ({ history, loading, data, status, updateTodo }) => {

    const [{ isOver }, drop] = useDrop({
        accept: CARD_DRAG_ID,
        drop: item => {
            if (item.status === status) {
                return;
            }
            const todo = {
                name: item.name,
                description: undefined,
                id: item.id,
                assignedUser: !item.assignedUser
                    ? null
                    : {
                        id: item.assignedUser.id,
                    },
                status
            };
            updateTodo({ variables: { todo } })
        },
        collect: monitor => ({
          isOver: !!monitor.isOver(),
        }),
      })

    return (
        <Card style={styles.boardTasksContainer}>
            <CardHeader style={styles.header} title={(
                <div style={{...styles.headerText, ...styles[`headerText${status}`]}}>
                    {_.findKey(TASK_STATUSES, x => x === status)}
                </div>)} 
            />
            <Divider />
            {loading ? null : (
                <CardContent ref={drop} style={styles.scrollbarContainer(isOver)}>
                    <Scrollbars autoHide={true}>
                        <List style={styles.list}>
                            {_.map(_.get(data, "todos", []), (t, i) => (
                                <React.Fragment>
                                    <BoardTask {...t}
                                        onSelect={() => history.push(ROUTES.EDIT_FILM.build(t.id))}
                                    />
                                    {i !== data.todos.length - 1 ? (
                                        <Divider variant="inset" component="li" />
                                    ) : null}
                                </React.Fragment>
                            ))}
                        </List>
                    </Scrollbars>
                </CardContent>
            )}                
        </Card>
    );
}

const SectionWithData = props => {
    const { status, searchText, searchUser, match: { params: { id } } } = props;

    const { loading, error, data, subscribeToMore } = useQuery(queries.todosQuery, { variables: { status, searchText, assignedUser: searchUser } });
    const [updateTodoMutation] = useMutation(updateTodo);

    const [unsubscribe, setUnsubscribe] = useState(null);

    useEffect(() => {
        unsubscribe && unsubscribe();
        setUnsubscribe(() => {
            const unsubscribe1 = subscribeToMore({
                document: subscriptions.todoAdded,
                variables: { searchText, assignedUser: searchUser },
                updateQuery: (prev, { subscriptionData }) => {
                    if (!subscriptionData.data) return prev;
                    const { todoAdded } = subscriptionData.data;
                    if (todoAdded.status === status) {
                        return {
                            ...prev,
                            todos: _.sortBy([...prev.todos, todoAdded], x => x.createdAt)
                        }
                    }
                    return {
                        ...prev,
                    }
                },
            });
            
            const unsubscribe2 = subscribeToMore({
                document: subscriptions.deleteTodo,
                variables: { searchText, assignedUser: searchUser },
                updateQuery: (prev, { subscriptionData }) => {
                    if (!subscriptionData.data) return prev;
                    const { todoDeleted } = subscriptionData.data;
                    if (todoDeleted.status === status) {
                        return {
                            ...prev,
                            todos: _.reject(prev.todos, t => t.id === todoDeleted.id)
                        }
                    }
                    return {
                        ...prev,
                    }
                },
            });
            const unsubscribe3 = subscribeToMore({
                document: subscriptions.todoUpdated,
                variables: { searchText, assignedUser: searchUser },
                updateQuery: (prev, { subscriptionData }) => {
                    if (!subscriptionData.data) return prev;
                    const { todoUpdated } = subscriptionData.data;
                    const newTodos = _.reject(prev.todos, { id: todoUpdated.id });
    
                    if (todoUpdated.status === status) {
                        return {
                            ...prev,
                            todos: _.orderBy([...newTodos, todoUpdated], "createdAt")
                        }
                    }
                    return {
                        ...prev,
                        todos: newTodos
                    };
                },
            });
            return () => {
                unsubscribe1();
                unsubscribe2();
                unsubscribe3();
            };
        // eslint-disable-next-line react-hooks/exhaustive-deps
        })}, [searchText, searchUser]);

    return (
        <Section
            {...props}
            id={id}
            loading={loading}
            data={data}
            searchText={searchText}
            updateTodo={updateTodoMutation}
        />
    );
}

export default compose(
    withRouter,
    // withData,
    withLoader
)(SectionWithData);