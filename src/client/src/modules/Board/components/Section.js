import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { Query } from "react-apollo";
import _ from "lodash";
import { List, CircularProgress } from '@material-ui/core';
import ROUTES from "../../appRouter/routes";
import { queries, subscriptions } from "../../Board";
import BoardTask from './SectionTask';
import { TASK_STATUSES } from "../../Task/constants";

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
        flex: 1,
        flexDirection: "column",
        // maxWidth: 300,
        padding: "4px",
        margin: "2px 2px",
        borderRadius: 10
    },
    headerText: {
        fontWeight: "bold",
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
};

class Section extends Component {    
    componentDidMount() {
        this.props.onLoaded();
    }

    render() {
        const { history, loading, data, status } = this.props;

        if (loading) return (
            null
            // <div style={styles.spinnerContainer}>
            //     <CircularProgress />
            // </div>
        );
        return (
            <div style={styles.boardTasksContainer}>
                <div style={{...styles.headerText, ...styles[`headerText${status}`]}}>
                    {_.findKey(TASK_STATUSES, x => x === status)}
                </div>
                <List>
                {_.map(data.todos, t => (
                    <BoardTask id={t.id}
                        name={t.name} 
                        status={t.status}
                        onSelect={() => history.push(ROUTES.EDIT_FILM.build(t.id))}
                    />
                ))}
            </List>
            </div>
        );
    }
}

export default withRouter(props => {
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
                        }
                    });
                    subscribeToMore({
                        document: subscriptions.deleteTodo,
                        updateQuery: (prev, { subscriptionData }) => {
                            if (!subscriptionData.data) return prev;
                            const { todoDeleted } = subscriptionData.data;
                            if (todoDeleted.status === status) {
                                return {
                                    ...prev,
                                    todos: _.filter(prev.todos, t => t.id !== todoDeleted.id)
                                }
                            }
                            return {
                                ...prev,
                            }
                        }
                    });
                    subscribeToMore({
                        document: subscriptions.todoUpdated
                    });
                }}
            />
        )}</Query>
    );
});