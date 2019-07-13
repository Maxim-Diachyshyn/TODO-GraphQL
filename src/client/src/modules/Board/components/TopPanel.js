import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar, Typography, Tooltip, useMediaQuery } from '@material-ui/core';
import { useTheme } from '@material-ui/styles';
import { Create } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";
import UserMenu from "./UserMenu"

const styles = {
    container: {
        display: "grid",
        gridTemplateColumns: "48px 1fr 48px",
        gridColumnGap: "4px",
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
    const theme = useTheme();

    const smallHeader = useMediaQuery(theme.breakpoints.down('xs'));
    return (        
        <AppBar>
            <div style={styles.container}>
                <div />
                <div style={styles.titleContainer}>
                    <Typography variant={smallHeader ? "h6" : "h5"} style={styles.title}>{texts.title}</Typography>
                </div>
                <UserMenu />
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