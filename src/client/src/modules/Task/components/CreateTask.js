import React, { Component } from 'react';
import { Mutation } from "react-apollo";
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { mutations } from "../../Task";
import Task from "./Task";
import { TASK_STATUSES } from "../constants";

class CreateTask extends Component {
    constructor(props) {
        super(props);
    
        this.state = {
            timer: null,
            todo: {
                name: "New Task",
                description: "",
                status: TASK_STATUSES.Open,
                assignedUser: null
            }
        };
    }

    render() {
        return (
            <Task {...this.props}
                todo={this.state.todo}
                createTodo={this.props.createTodo}
                updateTodo={updates => {           
                    const exitingTodo = this.state.todo;
                    const todo = {
                        ...exitingTodo,
                        ...updates
                    };
                    this.setState({ todo });
                }}
            />
        )
    }
}   

export default withRouter(props => (
    <Mutation mutation={mutations.createTodo}>{createTodo => (
        <CreateTask {...props}
            createTodo={todo => {
                const todoToSend = _.omit(todo, "__typename");
                createTodo({ variables: {...todoToSend } });
            }
        }/>
    )}</Mutation>
));