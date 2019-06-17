import React from 'react';
import _ from "lodash";
import { Mutation } from "react-apollo";
import { Sticky } from 'react-sticky';
import { IconButton } from '@material-ui/core';
import { AddComment as AddCommentIcon } from '@material-ui/icons';
import { mutations } from "../../Board";
import { TASK_STATUSES } from "../../Task/constants";

const styles = {
    container: {
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "8px 8px",
        background: "darkblue",
        zIndex: 1
    },
    title: {
        fontSize: 24,
        color: "#FFFF",
        fontWeight: "bold"
    },
    button: {
        fontSize: 18,
        color: "white",
        background: "coral"
    }
};

const texts = {
    title: "TODO GraphQL",
    create: "Create task"
}

const TopPanel = props => {
    return (        
        <Sticky>{({ style }) => (
            <div style={{...styles.container, ...style}}>
                <div />
                <span style={styles.title}>{texts.title}</span>
                <IconButton style={styles.button} edge="end" aria-label={texts.create} onClick={props.createTodo}>
                    <AddCommentIcon />
                </IconButton>
            </div>
        )}
        </Sticky>
    )
};

export default () => (
    <Mutation mutation={mutations.createTodo}>
        {createTodo => (
            <TopPanel createTodo={() => createTodo({ 
                variables: { 
                    name: "Task", 
                    description: "description", 
                    status: TASK_STATUSES.Done
                }
            })} />
        )
    }</Mutation>
);