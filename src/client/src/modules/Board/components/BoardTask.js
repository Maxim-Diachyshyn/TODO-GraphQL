import React from 'react';
import _ from "lodash";
import { ListItemSecondaryAction, ListItemText, IconButton } from '@material-ui/core';
import { Delete as DeleteIcon } from '@material-ui/icons';
import ListItem from '@material-ui/core/ListItem';
import { Mutation, Subscription, Query } from "react-apollo";
import { deleteTodo } from "../mutations";
import { TASK_STATUSES } from "../../Task/constants";

const styles = {
    link: {
        maxWidth: 500,
        borderRadius: 5,
        margin: "2px 0px"
    },
    [`link${TASK_STATUSES.Pending}`]: {
        background: "black",
        color: "white"
    },
    [`link${TASK_STATUSES["In Progress"]}`]: {
        background: "green",
    },
    [`link${TASK_STATUSES.Review}`]: {
        background: "yellow"
    },
    [`link${TASK_STATUSES.Done}`]: {
        background: "blue"
    },
    linkClosed: {
        textDecoration: "line-through"
    }
};

const texts = {
    delete: "Delete"
};

const BoardTask = props => {
    const { name, status, onSelect, onDelete } = props;
    let style = { ...styles.link, ...styles[`link${status}`] };
    return (        
        <ListItem button={true} style={style} onClick={onSelect}>
            <ListItemText primary={name} />
                <ListItemSecondaryAction>
                <IconButton edge="end" aria-label={texts.delete} onClick={onDelete}>
                    <DeleteIcon />
                </IconButton>
            </ListItemSecondaryAction>
        </ListItem>
    )
};

export default props => {
    const { id } = props;
    return (
        <Mutation mutation={deleteTodo}>{(deleteTodo) => (
            <BoardTask {...props} onDelete={() => deleteTodo({ variables: { id } })} />
        )}</Mutation>
    );
}
    