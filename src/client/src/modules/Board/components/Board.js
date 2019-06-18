import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { Query } from "react-apollo";
import _ from "lodash";
import { StickyContainer } from 'react-sticky';
import { List, CircularProgress } from '@material-ui/core';
import ROUTES from "../../appRouter/routes";
import { queries, subscriptions } from "../../Board";
import BoardTask from './BoardTask';
import TopPanel from "./TopPanel";
import { UpdateTask, CreateTask } from "../../Task/components";

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
        maxWidth: 300,
        padding: "4px",
        margin: "20px auto 20px auto",
        borderRadius: 10
    }
};

class Board extends Component {    
    componentDidMount() {
        this.props.onLoaded();
    }

    render() {
        const { history, isCreating, id, loading, data } = this.props;

        if (loading) return (
            <div style={styles.spinnerContainer}>
                <CircularProgress />
            </div>
        );
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
                {id ? <UpdateTask id="modal" todoId={id} /> : null}
                {isCreating ? <CreateTask /> : null}
            </StickyContainer>
        );
    }
}

export default withRouter((props) => {
    const { id } = props.match.params;
    return (
        <Query query={queries.todosQuery}>
        {({ loading, error, data, subscribeToMore }) => (
            <Board
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
                            return {
                                ...prev,
                                todos: [...prev.todos, todoAdded]
                            }
                        }
                    });
                    subscribeToMore({
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
                    subscribeToMore({
                        document: subscriptions.todoUpdated
                    });
                }}
            />
        )}</Query>
    );
});