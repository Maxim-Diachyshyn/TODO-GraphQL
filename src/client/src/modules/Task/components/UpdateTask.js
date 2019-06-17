import React, { Component } from 'react';
import { Query, Mutation } from "react-apollo";
import { withRouter } from 'react-router-dom';
import { toast } from "react-toastify";
import _ from "lodash";
import { mutations } from "../../Board";
import { subscriptions } from "../../Board"
import { todoByIdQuery } from "../queries";
import Task from "./Task";

const texts = {
    notFound: "Looks like this task doesn't exist anymore."
}

class UpdateTask extends Component {
    constructor(props) {
        super(props);
    
        this.state = {
            timer: null,
            errorShowed: false
        };
    }

    render() {
        const { id } = this.props.match.params;
        const { timer, errorShowed } = this.state;
        return (
            <Mutation mutation={mutations.deleteTodo}>{(deleteTodo) => (
                <Mutation mutation={mutations.updateTodo}>{(updateTodo) => (
                    <Query query={todoByIdQuery} 
                        variables={{ id }}>{({ loading, data, client, error, subscribeToMore }) => {
                        if (!loading && error ) {
                            if (!errorShowed) {
                                this.setState({ errorShowed: true }, () => toast.error(texts.notFound));
                            }
                            return null;
                        }
                        return (
                            <Task {...this.props} 
                                data={data}
                                subscribeToRemoved={() => subscribeToMore({
                                    document: subscriptions.deleteTodo,
                                    updateQuery: (prev, { subscriptionData }) => {
                                        if (!subscriptionData.data) return prev;
                                        const { todoDeleted } = subscriptionData.data;
                                        return {
                                            ...prev,
                                            todo: prev.todo.id === todoDeleted.id ? null : prev.todo
                                        }
                                    }
                                })}
                                updateTodo={updates => {
                                    if (timer) {
                                        clearTimeout(timer);
                                    }
                        
                                    const exitingTodo = data.todo;
                                    const newTodo = {
                                        ...exitingTodo,
                                        ...updates
                                    };
        
                                    client.writeData({ data: { todo: newTodo } });
        
                                    const todoToSend = _.omit(newTodo, "__typename");
                                    this.setState({ timer: setTimeout(() => updateTodo({ variables: { todo: todoToSend } }), 1000) });
                                }}
                                loading={loading} 
                                onDelete={() => deleteTodo({ variables: { id } })}
                                onClose={() => this.setState({ errorShowed: false })}
                            />
                        )
                    }}</Query>
                )}</Mutation>
            )}</Mutation>
        );
    }   
}

export default withRouter(UpdateTask);