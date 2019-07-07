import React, { useState } from 'react';
import _ from "lodash";
import { Snackbar, SnackbarContent } from '@material-ui/core';

const anchor = {
    vertical: 'top',
    horizontal: 'right',
}

const showStates = {
    initial: 0,
    hidden: 1,
    shown: 2
};

export default WrappedComponent => props => {
    const duration = props.autoHideDuration || 5000;
    const [showState, setShowState] = useState(showStates.initial);
    if (props.loading && showState === showStates.hidden) {
        setShowState(showStates.initial);
    }
    else if (props.error && showState === showStates.initial) {
        setShowState(showStates.shown);
        setTimeout(() => setShowState(showStates.hidden), duration);
    }
    return (
        <React.Fragment>
            <Snackbar
                anchorOrigin={anchor}
                open={showState === showStates.shown}
                onClose={() => props.onClose(props)}
            >
                <SnackbarContent message={
                    <p>{_.get(props.error, "message", "")}</p>
                } />
            </Snackbar>
            <WrappedComponent {...props}>
                {props.children}
            </WrappedComponent>
        </React.Fragment>
    );
};