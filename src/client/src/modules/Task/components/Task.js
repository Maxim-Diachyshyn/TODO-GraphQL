import React, { Component } from 'react';
import _ from "lodash";
import { Mutation, Subscription, Query, ApolloConsumer } from "react-apollo";
import { Modal, Button } from "react-bootstrap";
import { withRouter } from 'react-router-dom';
import { todoByIdQuery } from "../queries";
import { updateTodo, deleteTodo } from "../../Board/mutations";
import NameInput from "./NameInput";
import DescriptionInput from "./DescriptionInput";
import StatusInput from "./StatusInput";
import DeleteButton from "./DeleteButton";
import ROUTES from "../../appRouter/routes";

const styles = {
    header: {
        width: "100%"
    }
};

class Task extends Component { 
    onClose = () => {
        const { history } = this.props;
        history.push(ROUTES.HOME);
    }

    render() {
        const { loading, todoId, data, onDelete, updateTodo } = this.props;
        if (loading) {
            return "loading brooooooo";
        }
        const todo = { ..._.get(data, "todo") };
        return (
            <Modal show={!loading && todoId && todo} onHide={this.onClose}>
                {(!loading && todoId && todo) ? (
                    <React.Fragment>
                        <Modal.Header style={styles.header} closeButton={true}>
                            <NameInput name={todo.name} onChange={updateTodo}/>
                        </Modal.Header>

                        <Modal.Body>
                            <DescriptionInput description={todo.description} onChange={updateTodo}/>  
                        </Modal.Body>

                        <Modal.Footer>
                            <StatusInput status={todo.status} onChange={updateTodo}/>
                            <DeleteButton onDelete={onDelete}/>
                            <Button variant="secondary" onClick={this.onClose}>Close</Button>
                        </Modal.Footer>
                    </React.Fragment>
                ) : null}
            </Modal>
        );
    }
}

let timer = null;
export default withRouter((props) => {
    const { todoId } = props;

    return (
        <ApolloConsumer>{client => (
            <Mutation mutation={deleteTodo}>{(deleteTodo) => (
                <Mutation mutation={updateTodo}>{(updateTodo) => (
                    <Query query={todoByIdQuery} variables={{ id: todoId }}>{({ loading, data }) => (
                        <Task {...props} 
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
                                timer = setTimeout(() => updateTodo({ variables: { todo: todoToSend } }), 1000);
                            }}
                            loading={loading} 
                            onDelete={() => deleteTodo({ variables: { id: todoId } })}
                        />
                    )}</Query>
                )}</Mutation>
            )}</Mutation>
        )}</ApolloConsumer>
    );
});