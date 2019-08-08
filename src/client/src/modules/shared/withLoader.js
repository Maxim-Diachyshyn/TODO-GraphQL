import React from 'react';
import { CircularProgress } from '@material-ui/core';
import { branch, renderComponent } from 'recompose';

const styles = {
    container: {
        height: "100%",
        width: "100%",
        display: "flex",
        alignItems: "center",
        justifyContent: "center"
    },
    loader: {
    }
}

export default WrappedComponent => branch(
    ({ loading }) => !!loading,
    renderComponent(props => (
        <div style={styles.container}>
            <CircularProgress style={styles.loader}/>
        </div>
    )),
    renderComponent(props => (
        <WrappedComponent {...props}>{props.children}</WrappedComponent>
    ))
)(WrappedComponent);