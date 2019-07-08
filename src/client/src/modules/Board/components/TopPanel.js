import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar, Typography, Tooltip } from '@material-ui/core';
import { Create } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";
import UserMenu from "./UserMenu"

const styles = {
    container: {
        display: "grid",
        gridTemplateColumns: "48px 1fr 48px",
        gridTemplateRows: "48px",
        padding: "8px 8px",
        background: "darkblue"
    },
    titleContainer: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center"
    },
    title: {
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
                <UserMenu />
                <div style={styles.titleContainer}>
                    <Typography variant="h5" style={styles.title}>{texts.title}</Typography>
                </div>
                <IconButton style={styles.button} aria-label={texts.create} onClick={props.createTodo}>
                <Tooltip title={texts.create}>
                    <Create />
                </Tooltip>
                </IconButton>
            </div>
        </AppBar>
    )
};

export default withRouter(props => {
    const { history } = props;
    return (
        <TopPanel {...props} createTodo={() => history.push(ROUTES.CREATE_FILM)} />
    );
});