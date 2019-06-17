import React, { Component } from 'react';
import _ from "lodash";
import { Mutation, Subscription, Query } from "react-apollo";
import { Modal, Button } from "react-bootstrap";
import { withRouter } from 'react-router-dom';
import { todoByIdQuery } from "../queries";
import { updateTodo, deleteTodo } from "../../Board/mutations";
import NameInput from "./NameInput";
import DescriptionInput from "./DescriptionInput";
import StatusInput from "./StatusInput";
import DeleteButton from "./DeleteButton";
import ROUTES from "../../appRouter/routes";


class Task extends Component {
    constructor(props) {
        super(props);
        this.state = {
            timer: null,
            changes: {}
        };
    };

    onChange = newValue => {
        this.setState(prevState => {
            const { updateTodo, data: { todo } } = this.props;
            const { timer, changes } = prevState;
            if (timer) {
                clearTimeout(timer);
            }

            const newChanges = {
                ...changes,
                ...newValue
            };
            return {
                changes: newChanges,
                timer: setTimeout(() => {
                    const updatedTodo = _.pick({ ...todo, ...newChanges }, "id", "name", "description", "status");
                    updateTodo(updatedTodo);
                }, 1000)
            };
        });
    }

    onClose = () => {
        const { history } = this.props;
        this.setState(prevState => {
            const { timer } = prevState;
            if (timer) {
                clearTimeout(timer);
            }
            return { timer: null, changes: {}};
        }, () => history.push(ROUTES.HOME));
    }

    render() {
        const { loading, todoId, data, onDelete } = this.props;
        const { changes } = this.state;
        if (loading) {
            return "loading brooooooo";
        }
        const todo = { ..._.get(data, "todo"), ...changes };
        return (
            <Modal show={!loading && todoId && todo} onHide={this.onClose}>
                {(!loading && todoId && todo) ? (
                    <React.Fragment>
                        <Modal.Header style={{ width: "100%" }} closeButton>
                            <NameInput name={todo.name} onChange={this.onChange}/>
                        </Modal.Header>

                        <Modal.Body>
                            <DescriptionInput description={todo.description} onChange={this.onChange}/>  
                        </Modal.Body>

                        <Modal.Footer>
                            <StatusInput status={todo.status} onChange={this.onChange}/>
                            <DeleteButton onDelete={onDelete}/>
                            <Button variant="secondary" onClick={this.onClose}>Close</Button>
                        </Modal.Footer>
                    </React.Fragment>
                ) : null}
            </Modal>
        );
    }
}

export default withRouter((props) => {
    const { todoId } = props;
    return (
        <Mutation mutation={deleteTodo}>{(deleteTodo) => (
            <Mutation mutation={updateTodo}>{(updateTodo) => (
                <Query query={todoByIdQuery} variables={{ id: todoId }}>{({ loading, data }) => (
                    <Task {...props} 
                        data={data} 
                        updateTodo={todo => updateTodo({variables: { todo }})} 
                        loading={loading} 
                        onDelete={() => deleteTodo({ variables: { id: todoId } })}
                    />
                )}</Query>
            )}</Mutation>
        )}</Mutation>
    );
});