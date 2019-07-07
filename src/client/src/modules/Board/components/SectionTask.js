import React from 'react';
import _ from "lodash";
import { ListItemSecondaryAction, ListItemText, IconButton, ListItemIcon, ListSubheader, ListItemAvatar, Avatar } from '@material-ui/core';
import { Delete as DeleteIcon, PersonAdd as PersonAddIcon } from '@material-ui/icons';
import { ListItem } from '@material-ui/core';
import { Query, Mutation } from "react-apollo";
import { deleteTodo } from "../mutations";
import { usersQuery } from "../../Task/queries"
import { TASK_STATUSES } from "../../Task/constants";

const styles = {
    link: {
        // borderRadius: 8,
        // margin: "2px 0px",
        background: "#FFFF",
        minHeight: 75
    },
    circle: {
        borderRadius: "50%",
        width: 40,
        height: 40
    },
    text: {
        overflow: "hidden",
        textOverflow: "ellipsis"
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

const SectionTask = props => {
    const { name, status, picture, onSelect, onDelete } = props;
    return (
        <ListItem button={true} style={styles.link} onClick={onSelect}>
            <ListItemAvatar style={styles[`status${status}`]}>
                <Avatar src={picture}>
                    {picture ? null : (
                        <PersonAddIcon/>
                    )}                    
                </Avatar>
            </ListItemAvatar>
            <ListItemText primary={<p style={styles.text}>{name}</p>} />
        </ListItem>
    )
};

export default props => {
    const { id, assignedUser } = props;
    const assignedUserId = _.get(assignedUser, "id", null);
    return (
        <Query query={usersQuery}>
        {({ loading, error, data, subscribeToMore }) => {
            const users = _.get(data, "users", []);
            const assignedUser = _.find(users, x => x.id === assignedUserId);
            const picture = _.get(assignedUser, "picture");
            return (
                <Mutation mutation={deleteTodo}>{(deleteTodo) => (
                    <SectionTask {...props} picture={picture} onDelete={() => deleteTodo({ variables: { id } })} />
                )}</Mutation>
            );
        }}</Query>
    );
}
    