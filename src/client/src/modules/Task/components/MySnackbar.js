import React from 'react';
import { Snackbar, SnackbarContent } from '@material-ui/core';

const MySnackbar = props => {
    return (
        <Snackbar
            anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
            }}
            open={props.open}
            autoHideDuration={props.autoHideDuration || 2000}
            onClose={props.onClose}
        >
            <SnackbarContent message={
                <p>{props.text}</p>
            } />
        </Snackbar>
    );
};

export default MySnackbar;