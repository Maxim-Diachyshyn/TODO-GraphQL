import React from 'react';
import { CircularProgress } from '@material-ui/core';
import { branch, renderComponent } from 'recompose';

const styles = {
    loader: {
        position: "absolute",
        height: "100%",
        width: "100%",
        display: "flex",
        alignItems: "center",
        justifyContent: "center"
    }
}

export default WrappedComponent => branch(
    ({ loading }) => !!loading,
    renderComponent(() => (
        <div style={styles.loader}>
            <CircularProgress />
        </div>
    ))
)(WrappedComponent);