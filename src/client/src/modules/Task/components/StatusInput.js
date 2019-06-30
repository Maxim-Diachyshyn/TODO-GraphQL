import React from 'react';
import _ from "lodash";
import { Select, MenuItem } from '@material-ui/core';
import { TASK_STATUSES } from "../constants";

const styles = {
    container: {
        minWidth: 250,
        paddingLeft: 8
    },
    select: {

    },
    [`select${TASK_STATUSES.Open}`]: {
        background: "#bdbdbd",
        color: "white"
    },
    [`select${TASK_STATUSES["In Progress"]}`]: {
        background: "#03a9f4"
    },
    [`select${TASK_STATUSES.Review}`]: {
        background: "#9575cd"
    },
    [`select${TASK_STATUSES.Done}`]: {
        background: "#388e3c"
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