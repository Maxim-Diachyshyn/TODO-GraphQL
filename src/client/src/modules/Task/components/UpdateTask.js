import React, { Component } from 'react';
import { Query, Mutation } from "react-apollo";
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { mutations } from "../../Board";
import { subscriptions } from "../../Board"
import { todoByIdQuery } from "../queries";
import Task from "./Task";
import MySnackbar from "./MySnackbar";
import ROUTES from "../../appRouter/routes";

const texts = {
    notFound: "Looks like this task doesn't exist anymore."
}

const timeout = 500;

class UpdateTask extends Component {
    constructor(props) {
        super(props);
    
        this.state = {
            timer: null,
            errorHandled: false
        };
    }

    handleError = () => {
        this.setState({ errorHandled: true }, () => this.props.history.push(ROUTES.HOME));
    }
        

    render() {
        const { id } = this.props.match.params;
        const { timer, errorHandled } = this.state;
        return (
            <Mutation mutation={mutations.deleteTodo}>{(deleteTodo) => (
                <Mutation mutation={mutations.updateTodo}>{(updateTodo) => (
                    <Query query={todoByIdQuery}
                        variables={{ id }}>{({ loading, data, client, error, subscribeToMore }) => {
                            return (
                                <React.Fragment>
                                    <MySnackbar 
                                        open={!loading && error && !errorHandled} 
                                        text={texts.notFound}
                                        onClose={this.handleError}
                                    />
                                    <Task {...this.props} 
                                        todo={_.get(data, "todo", {})}
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
                                            const updateFunc = () => updateTodo({ variables: { todo: todoToSend } });
                                            if (exitingTodo.status !== newTodo.status) {
                                                updateFunc();
                                            }
                                            else {
                                                this.setState({ timer: setTimeout(updateFunc, timeout) });
                                            }                                    
                                        }}
                                        loading={loading} 
                                        onDelete={() => deleteTodo({ variables: { id } })}
                                        onClose={() => this.setState({ errorShowed: false })}
                                    />
                                    </React.Fragment>
                                )
                            }}</Query>
                )}</Mutation>
            )}</Mutation>
        );
    }   
}

export default withRouter(UpdateTask);