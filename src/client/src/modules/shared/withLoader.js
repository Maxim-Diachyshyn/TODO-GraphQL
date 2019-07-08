import React from 'react';
import { CircularProgress } from '@material-ui/core';
import { branch, renderComponent } from 'recompose';

const styles = {
    container: {
        position: "relative",
        height: "100%",
        width: "100%"
    },
    loader: {
        position: "absolute",
        top: 0,
        bottom: 0,
        left: 0,
        right: 0,
        margin: "auto"
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