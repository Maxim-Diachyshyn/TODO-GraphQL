import React from 'react';
import _ from "lodash";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, useMediaQuery } from "@material-ui/core";
import { useTheme } from '@material-ui/styles';
import { withRouter } from 'react-router-dom';
import NameInput from "./NameInput";
import DescriptionInput from "./DescriptionInput";
import StatusInput from "./StatusInput";
import AssignedUserInput from "./AssignedUserInput";
import DeleteButton from "./DeleteButton";
import ROUTES from "../../appRouter/routes";
import { compose, lifecycle, withHandlers, branch } from 'recompose';

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

const Task = props => {
    const { loading, todoId, todo, updateTodo, onDelete, createTodo, onClose, onCreate } = props;
    const theme = useTheme();
    console.log(useMediaQuery);
    const fullScreen = useMediaQuery(theme.breakpoints.down('xs'));
    
    return (
        <Dialog maxWidth="sm" fullWidth={true} fullScreen={fullScreen} open={(!loading && todoId && todo) || !!createTodo} onClose={onClose} scroll="body">
        {((!loading && todoId && todo) || !!createTodo) ? (
            <React.Fragment>
                <DialogTitle disableTypography={true}>
                    <NameInput name={todo.name} onChange={updateTodo}/>
                </DialogTitle>

                <DialogContent>
                    <DescriptionInput description={todo.description} onChange={updateTodo}/>
                    <div style={styles.selectorsContainer}>
                        <StatusInput status={todo.status} onChange={updateTodo}/>
                        <AssignedUserInput status={todo.status} onChange={updateTodo} assignedUser={todo.assignedUser}/>
                    </div>
                </DialogContent>

                <DialogActions style={styles.footer}>
                    {onDelete ? (
                        <DeleteButton onDelete={onDelete}/>
                    ) : null}
                    {createTodo ? (
                        <Button style={styles.createButton} onClick={onCreate}>{texts.createButton}</Button>
                    ) : null}
                    <Button style={styles.closeButton} onClick={onClose}>{texts.closeButton}</Button>                            
                </DialogActions>
            </React.Fragment>
        ) : null}
        </Dialog>
    );
};

const onClose = props => () => {
    const { history, onClose } = props;
    if (onClose) {
        onClose();
    }
    history.push(ROUTES.HOME);
};

export default compose(
    withRouter,
    withHandlers({
        onClose: onClose,
        onCreate: props => () => {
            const { todo, createTodo } = props;
            createTodo(todo);
            onClose(props)();
        }
    }),
    branch(
        ({ onDelete }) => !!onDelete,
        withHandlers({
            onDelete: props => {
                const { onDelete } = props;
                if (onDelete){
                    return () => {
                        onDelete();
                        onClose(props)();
                    };
                }
            },
        }) 
    ),
    lifecycle({
        componentDidMount() {
            if (this.props.subscribeToRemoved) {
                this.props.subscribeToRemoved();
            }
        }
    })
)(Task);