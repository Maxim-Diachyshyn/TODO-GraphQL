import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar, Typography, Avatar, Menu, MenuItem, Tooltip } from '@material-ui/core';
import { Delete as DeleteIcon, Person as PersonIcon } from '@material-ui/icons';
import { Create } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";
import { Mutation, Query, withApollo } from 'react-apollo';
import { signIn } from '../../SignIn/mutations';
import { compose } from 'recompose';
import { currentUserQuery } from '../queries';

const styles = {
    container: {
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "8px 8px",
        background: "darkblue"
    },
    title: {
        color: "#FFFF",
        fontWeight: "bold"
    },
    avatarButton: {
        padding: 0
    },
    iconButton: {
        background: "#979797",
        color: "white",
        fontSize: 18
    },
    avatar: {
        height: "100%",
        width: "100%"
    }
};

const texts = {
    title: "TODO GraphQL",
    create: "Create task"
}

const UserMenu = props => {
    const { username, picture, onLogout } = props;

    const [anchorEl, setAnchorEl] = React.useState(null);

    const handleClick = event => {
        setAnchorEl(event.currentTarget);
    }

    const handleClose = () => {
        setAnchorEl(null);
    }

    const handleLogout = () => {
        handleClose();
        onLogout();
    }

    return (
        <React.Fragment>
            <Tooltip title={username}>
                <IconButton style={picture ? styles.avatarButton : styles.iconButton} onClick={handleClick}>
                    {picture ? (
                        <Avatar src={picture} style={styles.avatar} />
                    ) : (
                        <PersonIcon />
                    )}
                </IconButton>
            </Tooltip>
            <Menu
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem onClick={handleLogout}>Logout</MenuItem>
            </Menu>
        </React.Fragment>               
    )
};

const withData = WrappedComponent => props => (
    <Query query={currentUserQuery} fetchPolicy="cache-only">{({ data }) => (
        <WrappedComponent {...props} {..._.get(data, "currentUser", {})} />
    )}</Query>
);

export default compose(
    withApollo,
    withRouter,
    withData,

)(props => {
    const { client, history } = props;
    const createTodo = () => history.push(ROUTES.CREATE_FILM);
    const logout = () => {
        const { currentUser } = client.readQuery({ query: currentUserQuery });
        client.writeQuery({
            query: currentUserQuery,
            data: {
                currentUser: {
                    ...currentUser,
                    logoutRequested: true
                }
            }
        });
    }
    return (
        <UserMenu {...props} createTodo={createTodo} onLogout={logout}/>
    );
})