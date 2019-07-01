import React from 'react';
import _ from "lodash";
import { Select, MenuItem, Avatar, Typography } from '@material-ui/core';
import { PersonAdd as PersonAddIcon } from "@material-ui/icons";
import { TASK_STATUSES } from "../constants";

const styles = {
    container: {
        // width: "100%"
        // minWidth: 250,
    },
    item: {
        display: "grid",
        gridTemplateColumns: "auto 1fr",
        gridColumnGap: 8,
        alignItems: "center"
    }
}

export default props => {
    const { loading, status } = props;

    const onChange = e => {
        props.onChange({ status: e.target.value });
    }
    
    return (
        <Select  style={styles.container} value={status} disabled={loading} onChange={onChange}>
            {_.map(TASK_STATUSES, (v, k) => (
                <MenuItem style={{...styles.select, ...styles[`select${v}`]}} key={k} value={v}>
                    <div style={styles.item}>
                        <Avatar>
                            {/* TODO: user pictures here */}
                            <PersonAddIcon/>
                        </Avatar> 
                        <Typography variant="button">{k}</Typography>
                    </div>
                </MenuItem>)
            )}
        </Select>    
    );                               
}