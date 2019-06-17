import React from 'react';
import _ from "lodash";
import { Select, MenuItem } from '@material-ui/core';
import { TASK_STATUSES } from "../constants";

const styles = {
    container: {
        minWidth: 250,
        marginRight: 20,
        paddingLeft: 8
    },
    select: {
        borderRadius: 4,
    },
    [`select${TASK_STATUSES.Pending}`]: {
        background: "#b71c1c",
        color: "white"
    },
    [`select${TASK_STATUSES["In Progress"]}`]: {
        background: "green"
    },
    [`select${TASK_STATUSES.Review}`]: {
        background: "yellow"
    },
    [`select${TASK_STATUSES.Done}`]: {
        background: "blue"
    },
}

export default props => {
    const { loading, status } = props;

    const onChange = e => {
        props.onChange({ status: e.target.value });
    }
    
    return (
        <Select style={{...styles.container, ...styles.select, ...styles[`select${status}`]}} value={status} disabled={loading} onChange={onChange}>
            {_.map(TASK_STATUSES, (v, k) => <MenuItem style={{...styles.select, ...styles[`select${v}`]}} key={k} value={v}>{k}</MenuItem>)}
        </Select>     
    );                               
}