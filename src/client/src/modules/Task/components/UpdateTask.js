import React, { useState } from 'react';
import { Query, Mutation } from "react-apollo";
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import omitDeep from "omit-deep-lodash";
import { mutations } from "../../Board";
import { subscriptions } from "../../Board"
import { todoByIdQuery } from "../queries";
import Task from "./Task";
import { compose } from 'recompose';
import withLoader from '../../shared/withLoader';
import withError from '../../shared/withError';

const timeout = 500;

const UpdateTask = props => {
    const { match: { params: { id } }, loading, data, deleteTodo } = props;
   
    return (
        <Task {...props} 
            todo={_.get(data, "todo", {})}               
            loading={loading} 
            onDelete={() => deleteTodo({ variables: { id } })}
        />
    );
}

const withData = WrappedComponent => props => {
    const [timer, setTimer] = useState(null);
    return (
        <Mutation mutation={mutations.deleteTodo}>{(deleteTodo) => (
            <Mutation mutation={mutations.updateTodo}>{(updateTodo) => (
                <Query query={todoByIdQuery}
                    variables={{ id: props.todoId }}>{({ loading, data, client, error, subscribeToMore }) => (
                    <WrappedComponent 
                        {...props} 
                        loading={loading} 
                        data={data} 
                        client={client} 
                        error={error} 
                        subscribeToMore={subscribeToMore}
                        deleteTodo={deleteTodo}
                        updateTodo={updates => {
                            if (timer) {
                                clearTimeout(timer);
                            }
                
                            const exitingTodo = data.todo;

                            const newTodo = _.assign(_.cloneDeep(exitingTodo), updates);

                            let updateFunc = () => {};
                            
                            if (!_.isEqual(exitingTodo, newTodo)) {                                
                                client.writeData({ data: { todo: newTodo } });
                                const todoToSend = omitDeep(newTodo, "__typename");
                                updateFunc = () => updateTodo({ variables: { todo: todoToSend } })
                            }

                            if (exitingTodo.status !== newTodo.status) {
                                updateFunc();
                            }
                            else {
                                setTimer(setTimeout(updateFunc, timeout));
                            }                                    
                        }}
                        subscribeToRemoved={() => subscribeToMore({
                            document: subscriptions.deleteTodo,
                            updateQuery: (prev, { subscriptionData }) => {
                                if (!subscriptionData.data) return prev;
                                const { todoDeleted } = subscriptionData.data;
                                return {
                                    ...prev,
                                    todo: prev.todo.id === todoDeleted.id ? null : prev.todo
                                }
                            }})}
                        />
                )}</Query>
            )}</Mutation>
        )}</Mutation>
    )
};

export default compose(
    withRouter,
    withData,
    withLoader,
    withError
)(UpdateTask);