import React from 'react';
import clsx from "clsx";
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar, Typography, Tooltip, useMediaQuery } from '@material-ui/core';
import { useTheme, makeStyles } from '@material-ui/styles';
import { Search as SearchIcon, ChevronLeft as ChevronLeftIcon, ExpandLess as ExpandLessIcon } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";
import UserMenu from "./UserMenu"
import { compose, withHandlers } from 'recompose';
import { currentUserQuery } from '../queries';
import { withApollo, Query } from 'react-apollo';

const useStyles = makeStyles(theme => ({
    root: {
      display: 'flex',
    },
    appBar: {
      zIndex: theme.zIndex.drawer + 1,
      transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
      }),
    },
    appBarShift: {
      marginLeft: 240,
      width: `calc(100% - ${240}px)`,
      transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
    },
    menuButton: {
        // marginRight: 36,
        fontSize: 18,
        color: "white"
    },
    hide: {
        visibility: "collapse",
    },
}));

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
    const { handleOpen, handleClose, currentUser } = props;
    const classes = useStyles();
    const theme = useTheme();

    const menuOpened = _.get(currentUser, "menuOpened", false);

    const drawerAnchorTop = useMediaQuery(theme.breakpoints.down('xs'));

    const smallHeader = useMediaQuery(theme.breakpoints.down('xs'));
    return (        
        <AppBar className={clsx(classes.appBar, {
            [classes.appBarShift]: menuOpened && !drawerAnchorTop,
          })}>
            <div style={styles.container}>
                <IconButton
                    color="inherit"
                    aria-label="Search"
                    onClick={menuOpened ? handleClose : handleOpen}
                    edge="start"
                    className={classes.menuButton}
                >
                    {menuOpened ? 
                        drawerAnchorTop ? (
                            <ExpandLessIcon />
                        ) : (
                            <ChevronLeftIcon />
                    ) : (
                        <SearchIcon />
                    )}
                </IconButton>
                <div style={styles.titleContainer}>
                    <Typography variant={smallHeader ? "h6" : "h5"} style={styles.title}>{texts.title}</Typography>
                </div>
                <UserMenu />
            </div>
        </AppBar>
    )
};

const withData = WrappedComponent => props => (
    <Query query={currentUserQuery} fetchPolicy="cache-only">{({ data }) => (
        <WrappedComponent {...props} currentUser={_.get(data, "currentUser", {})} />
    )}</Query>
);

export default compose(
    withApollo,
    withData,
    withHandlers({
        handleOpen: props => () => {
            const { client, currentUser } = props;
            
            client.writeQuery({
                query: currentUserQuery,
                data: {
                    currentUser: {
                        ...currentUser,
                        menuOpened: true
                    }
                }
            });
        },
        handleClose: props => () => {
            const { client, currentUser } = props;
            
            client.writeQuery({
                query: currentUserQuery,
                data: {
                    currentUser: {
                        ...currentUser,
                        menuOpened: false
                    }
                }
            });
        }
    })
)(TopPanel);