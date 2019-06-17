import React, { Component } from 'react';
import _ from "lodash";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button } from "@material-ui/core";
import { toast } from "react-toastify";
import { withRouter } from 'react-router-dom';
import NameInput from "./NameInput";
import DescriptionInput from "./DescriptionInput";
import StatusInput from "./StatusInput";
import DeleteButton from "./DeleteButton";
import ROUTES from "../../appRouter/routes";

const styles = {
    closeButton: {
        background: "#979797",
        color: "#FFF"
    },
    createButton: {
        background: "green",
        color: "#FFF"
    },
    statusInput: {
        marginRight: 20
    },
    footer: {
        paddingLeft: 24
    }
};

const texts = {
    closeButton: "Close",
    createButton: "Save",
    deleted: "Looks like someone deleted this task."    
};

class Task extends Component { 
    componentDidMount() {
        if (this.props.subscribeToRemoved) {
            this.props.subscribeToRemoved();
        }
    }

    onClose = () => {
        const { history, onClose } = this.props;
        if (onClose) {
            onClose();
        }
        history.push(ROUTES.HOME);
    }

    onCreate = () => {
        const { data, createTodo } = this.props;
        createTodo(data.todo);
        this.onClose();
    }

    onDelete = () => {
        const { data, onDelete } = this.props;
        onDelete();
        this.onClose();
    }

    handleDeletion = () => {
        toast.error(texts.deleted);
        this.onClose();
    }

    render() {
        const { loading, todoId, data, updateTodo, onDelete, createTodo } = this.props;
        if (loading) {
            return "loading brooooooo";
        }
        if (_.get(data, "todo", {}) == null) {
            this.handleDeletion();
            return null;
        }
        const todo = { ..._.get(data, "todo") };
        return (
            <Dialog open={(!loading && todoId && todo) || !!createTodo} onClose={this.onClose}>
                {((!loading && todoId && todo) || !!createTodo) ? (
                    <React.Fragment>
                        <DialogTitle disableTypography={true}>
                            <NameInput name={todo.name} onChange={updateTodo}/>
                        </DialogTitle>

                        <DialogContent>
                            <DescriptionInput description={todo.description} onChange={updateTodo}/>  
                        </DialogContent>

                        <DialogActions style={styles.footer}>
                            <StatusInput style={styles.statusInput} status={todo.status} onChange={updateTodo}/>
                            {onDelete ? (
                                <DeleteButton onDelete={this.onDelete}/>
                            ) : null}
                            {createTodo ? (
                                <Button style={styles.createButton} onClick={this.onCreate}>{texts.createButton}</Button>
                            ) : null}
                            <Button style={styles.closeButton} onClick={this.onClose}>{texts.closeButton}</Button>                            
                        </DialogActions>
                    </React.Fragment>
                ) : null}
            </Dialog>
        );
    }
}

export default withRouter(Task);