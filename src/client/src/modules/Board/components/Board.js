import React, { Component } from 'react';
import { Query } from "react-apollo";
import _ from "lodash";
import { StickyContainer } from 'react-sticky';
import { List, CircularProgress } from '@material-ui/core';
import ROUTES from "../../appRouter/routes"
import { queries, subscriptions } from "../../Board";
import BoardTask from './BoardTask';
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components"

let unsubscribeForAdded = null;
let unsubscribeForRemoved = null;
let unsubscribeForUpdated = null;

const texts = {
    loading: "Loading TODO list.",
    error: (e) => `Error! ${e}`
};

const styles = {
    spinnerContainer: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
        background: "floralwhite"
    },
    boardTasksContainer: {
        display: "flex", 
        flexDirection: "column",
        maxWidth: 500,
        padding: "4px",
        margin: "20px auto 20px auto",
        background: "aliceblue",
        borderRadius: 10
    }
};

class Board extends Component {
    render() {
        const { history, isCreating } = this.props;

        return (
            <Query query={queries.todosQuery}>
                {({ loading, error, data, subscribeToMore }) => {
                    const { id } = this.props.match.params;
                    if (loading) return (
                        <div style={styles.spinnerContainer}>
                            <CircularProgress />
                        </div>
                    );
                    if (error) return texts.error(error.message);
                    if (!unsubscribeForAdded) {
                        unsubscribeForAdded = subscribeToMore({
                            document: subscriptions.todoAdded,
                            updateQuery: (prev, { subscriptionData }) => {
                                if (!subscriptionData.data) return prev;
                                const { todoAdded } = subscriptionData.data;
                                return {
                                    ...prev,
                                    todos: [...prev.todos, todoAdded]
                                }
                            }
                        });
                    }
                    if (!unsubscribeForRemoved) {
                        unsubscribeForRemoved = subscribeToMore({
                            document: subscriptions.deleteTodo,
                            updateQuery: (prev, { subscriptionData }) => {
                                if (!subscriptionData.data) return prev;
                                const { todoDeleted } = subscriptionData.data;
                                return {
                                    ...prev,
                                    todos: _.filter(prev.todos, t => t.id !== todoDeleted.id)
                                }
                            }
                        });
                    }
                    if (!unsubscribeForUpdated) {
                        unsubscribeForRemoved = subscribeToMore({
                            document: subscriptions.todoUpdated,
                            updateQuery: (prev, { subscriptionData }) => {
                                if (!subscriptionData.data) return prev;
                                const { todoUpdated } = subscriptionData.data;
                                return {
                                    ...prev,
                                }
                            }
                        });
                    }
                    return (
                        <StickyContainer>
                            <TopPanel />
                            <List style={styles.boardTasksContainer}>
                                {_.map(data.todos, t => (
                                    <BoardTask id={t.id}
                                        name={t.name} 
                                        status={t.status}
                                        onSelect={() => history.push(ROUTES.EDIT_FILM.build(t.id))}
                                    />
                                ))}
                            </List>
                            {id && _.some(data.todos, t => t.id === id) ? <UpdateTask id="modal" todoId={id} /> : null}
                            {isCreating ? <CreateTask /> : null}
                        </StickyContainer>
                    );
                }}
            </Query>
        );
    }
}

export default Board;