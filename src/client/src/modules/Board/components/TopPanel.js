import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar } from '@material-ui/core';
import { Create } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";

const styles = {
    container: {
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "8px 8px",
        background: "darkblue"
    },
    title: {
        fontSize: 24,
        color: "#FFFF",
        fontWeight: "bold"
    },
    button: {
        fontSize: 18,
        color: "white",
        background: "#f44336"
    }
};

const texts = {
    title: "TODO GraphQL",
    create: "Create task"
}

const TopPanel = props => {
    return (        
        <AppBar>
            <div style={styles.container}>
                <div />
                <span style={styles.title}>{texts.title}</span>
                <IconButton style={styles.button} edge="end" aria-label={texts.create} onClick={props.createTodo}>
                    <Create />
                </IconButton>
            </div>
        </AppBar>
    )
};

export default withRouter(props => {
    const { history } = props;
    return (
        <TopPanel createTodo={() => history.push(ROUTES.CREATE_FILM)} />
    );
});