import React from 'react';
import _ from "lodash";
import { useDrag, useDragLayer } from 'react-dnd'
import { usePreview } from 'react-dnd-preview';
import { ListItemSecondaryAction, ListItemText, IconButton, ListItemIcon, ListSubheader, ListItemAvatar, Avatar, Tooltip } from '@material-ui/core';
import { Delete as DeleteIcon, PersonAdd as PersonAddIcon } from '@material-ui/icons';
import { ListItem } from '@material-ui/core';
import { Query, Mutation } from "react-apollo";
import { deleteTodo } from "../mutations";
import { usersQuery } from "../../Task/queries"
import { TASK_STATUSES } from "../../Task/constants";
import { CARD_DRAG_ID } from '../constants';

const styles = {
    link: (isDragging) => ({
        // borderRadius: 8,
        // margin: "2px 0px",
        background: "#FFFF",
        minHeight: 75,
        opacity: isDragging ? 0.9 : 1
    }),
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
    },
    listItemAvatar: {
        minWidth: "unset",
        borderRadius: "50%",
        marginRight: 12
    }
};

const texts = {
    delete: "Delete",
    unassigned: "Unassigned"
};

export const SectionTaskPreview = () => {
    const { style, display, item } = usePreview();
    if (!display) {
        return null;
    }
    
    console.log(item);
    const { name, status, assignedUser, width } = item;

    const username = _.get(assignedUser, 'username' ,texts.unassigned);
    const picture = _.get(assignedUser, 'picture', null);
    return (
        <ListItem button={true} style={{...style, ...styles.link(false), width}}>
            <Tooltip title={username}>
                <ListItemAvatar style={{...styles.listItemAvatar, ...styles[`status${status}`]}}>
                    <Avatar src={picture}>
                        {picture ? null : (
                            <PersonAddIcon/>
                        )}                    
                    </Avatar>
                </ListItemAvatar>
            </Tooltip>
            <ListItemText primary={<p style={styles.text}>{name}</p>} />
        </ListItem>
    )
};