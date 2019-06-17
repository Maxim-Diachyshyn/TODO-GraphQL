import React, { Component } from 'react';
import { Query, Mutation } from "react-apollo";
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { mutations } from "../../Board";
import { todoByIdQuery } from "../queries";
import Task from "./Task";

class UpdateTask extends Component {
    constructor(props) {
        super(props);
    
        this.state = {
            timer: null
        };
    }

    render() {
        const { id } = this.props.match.params;
        const { timer } = this.state;
        return (
            <Mutation mutation={mutations.deleteTodo}>{(deleteTodo) => (
                <Mutation mutation={mutations.updateTodo}>{(updateTodo) => (
                    <Query query={todoByIdQuery} variables={{ id }}>{({ loading, data, client }) => (
                        <Task {...this.props} 
                            data={data} 
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
                        />
                    )}</Query>
                )}</Mutation>
            )}</Mutation>
        );
    }   
}

export default withRouter(UpdateTask);