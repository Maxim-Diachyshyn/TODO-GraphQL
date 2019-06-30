import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { Query } from "react-apollo";
import _ from "lodash";
import { List, CircularProgress, Divider } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import ROUTES from "../../appRouter/routes";
import { queries, subscriptions } from "../../Board";
import BoardTask from './SectionTask';
import { TASK_STATUSES } from "../../Task/constants";

const styles = {
    spinnerContainer: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        background: "floralwhite"
    },
    boardTasksContainer: {
        display: "flex",
        flex: 1,
        flexDirection: "column",
        margin: "2px 2px",
        padding: 10,
        borderRadius: 10,
        background: "##f5f5f5"
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
    wrapper: {
        position: "relative",
        // height: "100%",
        overflowY: "scroll"
    },
    list: {
        overflowY: "auto",
        position: "absolute",
        top: 0,
        bottom: 0
    },
    scrollBars: {
        padding: 5
    }
};

class Section extends Component {    
    componentDidMount() {
        this.props.onLoaded();
    }

    renderView = ({ style, ...props }) =>
        <div style={{...style, ...styles.scrollBars}} {...props} />

    render() {
        const { history, loading, data, status } = this.props;
        return (
                <div style={styles.boardTasksContainer}>
                    <div style={{...styles.headerText, ...styles[`headerText${status}`]}}>
                        {_.findKey(TASK_STATUSES, x => x === status)}
                    </div>
                    {loading ? null : (
                        <Scrollbars style={styles.scrollBars}
                        renderView={this.renderView}>
                            <List>
                                {_.map(data.todos, (t, i) => (
                                    <React.Fragment>
                                        <BoardTask id={t.id}
                                            name={t.name} 
                                            status={t.status}
                                            onSelect={() => history.push(ROUTES.EDIT_FILM.build(t.id))}
                                        />
                                        {i !== data.todos.length ? (
                                            <Divider variant="inset" component="li" />
                                        ) : null}
                                    </React.Fragment>
                                ))}
                            </List>
                        </Scrollbars>
                    )}                
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
                                    todos: _.reject(prev.todos, t => t.id === todoDeleted.id)
                                }
                            }
                            return {
                                ...prev,
                            }
                        }
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
                        }
                    });
                }}
            />
        )}</Query>
    );
});