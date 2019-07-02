import React, { Component } from 'react';
import _ from "lodash";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button } from "@material-ui/core";
import { withRouter } from 'react-router-dom';
import NameInput from "./NameInput";
import DescriptionInput from "./DescriptionInput";
import StatusInput from "./StatusInput";
import AssignedUserInput from "./AssignedUserInput";
import DeleteButton from "./DeleteButton";
import ROUTES from "../../appRouter/routes";
import MySnackbar from "./MySnackbar";

const styles = {
    closeButton: {
        background: "#979797",
        color: "#FFF"
    },
    createButton: {
        background: "green",
        color: "#FFF"
    },
    selectorsContainer: {
        display: "grid",
        gridTemplateColumns: "repeat(auto-fill, minmax(200px, 1fr))",
        gridColumnGap: 16,
        width: "100%"
    },
    footer: {
        paddingLeft: 24
    }
};

const texts = {
    closeButton: "Close",
    createButton: "Save",
    deleted: "Looks like someone has deleted this task."    
};

class Task extends Component {
    constructor(props) {
        super(props);

        this.state = {
            deletedHandled: false
        };
    }

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
        const { todo, createTodo } = this.props;
        createTodo(todo);
        this.onClose();
    }

    onDelete = () => {
        const { onDelete } = this.props;
        onDelete();
        this.onClose();
    }

    handleError = () => {
        this.setState({ deletedHandled: true });
        this.onClose();
    } 

    render() {
        const { loading, todoId, todo, updateTodo, onDelete, createTodo } = this.props;
        const { deletedHandled } = this.state;
        if (loading) {
            return "loading brooooooo";
        }
        return (
            <React.Fragment>
                <MySnackbar 
                    open={todo === null && !deletedHandled} 
                    text={texts.deleted}
                    onClose={this.handleError}
                />
                <Dialog open={(!loading && todoId && todo) || !!createTodo} onClose={this.onClose} scroll="body">
                {((!loading && todoId && todo) || !!createTodo) ? (
                    <React.Fragment>
                        <DialogTitle disableTypography={true}>
                            <NameInput name={todo.name} onChange={updateTodo}/>
                        </DialogTitle>

                        <DialogContent>
                            <DescriptionInput description={todo.description} onChange={updateTodo}/>
                            <div style={styles.selectorsContainer}>
                                <StatusInput status={todo.status} onChange={updateTodo}/>
                                <AssignedUserInput status={todo.status} onChange={updateTodo} assignedUserId={todo.assignedUserId}/>
                            </div>
                        </DialogContent>

                        <DialogActions style={styles.footer}>
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
            </React.Fragment>
        );
    }
}

export default withRouter(Task);