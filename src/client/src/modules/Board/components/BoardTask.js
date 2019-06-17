import React from 'react';
import _ from "lodash";
import { ListItemSecondaryAction, ListItemText, IconButton, ListItemIcon, ListSubheader } from '@material-ui/core';
import { Delete as DeleteIcon } from '@material-ui/icons';
import ListItem from '@material-ui/core/ListItem';
import { Mutation, Subscription, Query } from "react-apollo";
import { deleteTodo } from "../mutations";
import { TASK_STATUSES } from "../../Task/constants";

const styles = {
    link: {
        borderRadius: 8,
        margin: "2px 0px",
        background: "#eceff1",
        minHeight: 75
    },
    circle: {
        borderRadius: "50%",
        width: 25,
        height: 25
    },
    [`status${TASK_STATUSES.Open}`]: {
        background: "#bdbdbd",
        color: "white"
    },
    [`status${TASK_STATUSES["In Progress"]}`]: {
        background: "#03a9f4",
    },
    [`status${TASK_STATUSES.Review}`]: {
        background: "#9575cd"
    },
    [`status${TASK_STATUSES.Done}`]: {
        background: "#388e3c"
    },
    deleteButton: {
        padding: 5,
        color: "#f44336"
    }
};

const texts = {
    delete: "Delete"
};

const BoardTask = props => {
    const { name, status, onSelect, onDelete } = props;
    return (
        <ListItem button={true} style={styles.link} onClick={onSelect}>
            <ListItemIcon>
                <div style={{...styles.circle, ...styles[`status${status}`]}}></div>
            </ListItemIcon>
            <ListItemText primary={name} />
            <ListItemSecondaryAction>
                <IconButton style={styles.deleteButton} edge="end" aria-label={texts.delete} onClick={onDelete}>
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
    