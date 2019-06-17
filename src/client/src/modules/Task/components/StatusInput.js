import React from 'react';
import _ from "lodash";
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import { TASK_STATUSES } from "../constants";

const styles = {
    container: {
        minWidth: 250
    }
}

export default props => {
    const { loading, status } = props;

    const onChange = e => {
        props.onChange({ status: e.target.value });
    }
    
    return (
        <Select style={styles.container} value={status} disabled={loading} onChange={onChange}>
            {_.map(TASK_STATUSES, (v, k) => <MenuItem key={k} value={v}>{k}</MenuItem>)}
        </Select>     
    );                               
}