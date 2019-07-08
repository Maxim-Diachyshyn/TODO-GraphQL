import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { Query } from "react-apollo";
import _ from "lodash";
import { List, CircularProgress, Divider, Card, CardHeader, CardContent } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import ROUTES from "../../appRouter/routes";
import { queries, subscriptions } from "../../Board";
import BoardTask from './SectionTask';
import { TASK_STATUSES } from "../../Task/constants";
import withLoader from '../../shared/withLoader';
import { compose } from 'recompose';

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
    scrollbarContainer: {
        paddingLeft: 0,
        paddingRight: 0,
        overflowY : "hidden"
    },
    list: {
        padding: "0px 8px"
    }
};

class Section extends Component {    
    componentDidMount() {
        this.props.onLoaded();
    }

    render() {
        const { history, loading, data, status } = this.props;
        return (
            <Card style={styles.boardTasksContainer}>
                <CardHeader style={styles.header} title={(
                    <div style={{...styles.headerText, ...styles[`headerText${status}`]}}>
                        {_.findKey(TASK_STATUSES, x => x === status)}
                    </div>)} 
                />
                <Divider />
                {loading ? null : (
                    <CardContent style={styles.scrollbarContainer}>
                        <Scrollbars autoHide={true}>
                            <List style={styles.list}>
                                {_.map(data.todos, (t, i) => (
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
}

export default compose(
    withRouter,
    withLoader
)(props => {
    const { id } = props.match.params;
    const { status } = props;

    return (
        <Query query={queries.todosQuery} variables={{ status }}>
        {({ loading, error, data, subscribeToMore }) => (
            <Section
                {...props}
                id={id}
                loading={loading} 
                data={data} 
                onLoaded={() => {
                    subscribeToMore({
                        document: subscriptions.todoAdded,
                        updateQuery: (prev, { subscriptionData }) => {
                            if (!subscriptionData.data) return prev;
                            const { todoAdded } = subscriptionData.data;
                            if (todoAdded.status === status) {
                                return {
                                    ...prev,
                                    todos: [...prev.todos, todoAdded]
                                }
                            }
                            return {
                                ...prev,
                            }
                        },
                    });
                    subscribeToMore({
                        document: subscriptions.deleteTodo,
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
                    subscribeToMore({
                        document: subscriptions.todoUpdated,
                        updateQuery: (prev, { subscriptionData }) => {
                            if (!subscriptionData.data) return prev;
                            const { todoUpdated } = subscriptionData.data;
                            const newTodos = _.reject(prev.todos, t => t.id === todoUpdated.id);
                            if (todoUpdated.status === status) {
                                return {
                                    ...prev,
                                    todos: [...newTodos, todoUpdated]
                                }
                            }
                            return {
                                ...prev,
                                todos: newTodos
                            };
                        },
                    });
                }}
            />
        )}</Query>
    );
});