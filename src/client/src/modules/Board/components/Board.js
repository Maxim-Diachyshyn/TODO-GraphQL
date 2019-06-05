import React from 'react';
import { Query,  Mutation } from "react-apollo";
import _ from "lodash";
import { Link } from 'react-router-dom'
import { Button } from "react-bootstrap";
import { queries, mutations, subscriptions } from "../../Board";
import ROUTES from "../../appRouter/routes";
import { components as taskComponents } from "../../Task";

let unsubscribeForAdded = null;
let unsubscribeForRemoved = null;
let unsubscribeForUpdated = null;

export default props => (
    <Query query={queries.todosQuery}>
        {({ loading, error, data, subscribeToMore }) => {
            const { id } = props.match.params;
            if (loading) return "Loading...";
            if (error) return `Error! ${error.message}`;
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
                        var todoToUpdate = _.find(prev.todos, t => t.id === todoUpdated.id);
                        todoToUpdate = {...todoUpdated};
                        return {
                            ...prev,
                        }
                    }
                });
            }
            return (
                <div style={{display: "flex", flexDirection: "column"}}>
                    {_.map(data.todos, t => (
                        <Link id={t.id} to={ROUTES.EDIT_FILM.build(t.id)}>{t.name}</Link>
                    ))}
                    <Mutation mutation={mutations.createTodo}>
                        {(createTodo, { loading }) => (
                            <Button id="addButton" disabled={loading} onClick={() => createTodo({ variables: { name: "Task", description: "description", status: "PENDING" } })}>Create task</Button>
                        )}
                    </Mutation>
                    <taskComponents.default id="modal" todo={_.find(data.todos, t => t.id === id)} />
                </div>
            );
        }}
  </Query>
);