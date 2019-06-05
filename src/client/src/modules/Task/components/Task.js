import React from 'react';
import _ from "lodash";
import { Mutation, Subscription } from "react-apollo";
import { Modal, Button } from "react-bootstrap";
import { withRouter } from 'react-router-dom';
import { mutations } from "../../Board";
import { toast } from 'react-toastify';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';


import ROUTES from "../../appRouter/routes";


export default withRouter((props) => {
    const { todo } = props;        
    return (
        <Modal show={props.todo} onHide={() => props.history.push(ROUTES.HOME)}>
            <Modal.Header closeButton>
                <Modal.Header>
                    <Mutation mutation={mutations.updateTodo}>
                            {(updateTodo, { data, loading }) => (
                                <TextField
                                label="Task"
                                value={_.get(todo, "name")}
                                margin="normal"
                                variant="outlined"
                                onChange={event => 
                                    updateTodo({ 
                                        variables: { 
                                            todo: { 
                                                id: todo.id, 
                                                name: event.target.value, 
                                                description: todo.description, 
                                                status: todo.status 
                                            } 
                                        }
                                    })                                    
                                } />
                            )}                                  
                    </Mutation>          
                </Modal.Header>
            </Modal.Header>

            <Modal.Body>
                <Mutation mutation={mutations.updateTodo}>
                        {(updateTodo, { data, loading }) => (
                            <TextField
                            label="Description"
                            multiline
                            rows="4"
                            value={_.get(todo, "description")}
                            margin="normal"
                            variant="outlined"
                            onChange={event => 
                                updateTodo({ 
                                    variables: { 
                                        todo: { 
                                            id: todo.id, 
                                            name: todo.name, 
                                            description:event.target.value, 
                                            status: todo.status 
                                        } 
                                    }
                                }) 
                            } />
                        )}                                  
                </Mutation>  
            </Modal.Body>

            <Modal.Footer>
                <Mutation mutation={mutations.updateTodo}>
                    {(updateTodo, { data, loading }) => (
                        <Select value={_.get(todo, "status")} onChange={event => updateTodo({ variables: { todo: { id: todo.id, name: todo.name, description: todo.description, status: event.target.value } }})}>
                            <MenuItem value={"PENDING"}>Pending</MenuItem>
                            <MenuItem value={"IN_PROGRESS"}>In Progress</MenuItem>
                            <MenuItem value={"REVIEW"}>In Review</MenuItem>
                            <MenuItem value={"DONE"}>Done</MenuItem>
                        </Select>     
                    )}                                   
                </Mutation>                

                <Mutation mutation={mutations.deleteTodo}>
                    {(deleteTodo, { data, loading }) => (
                        <Button disabled={loading} onClick={() => deleteTodo({ variables: { id: todo.id } })}>Delete Task</Button>
                    )}
                </Mutation>
                <Button variant="secondary" onClick={() => props.history.push(ROUTES.HOME)}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
});