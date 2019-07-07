import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import { IconButton, AppBar, Typography, Avatar, Menu, MenuItem } from '@material-ui/core';
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
    avatar: {
        margin: 0,
        height: 48,
        width: 48,
        fontSize: 18
    }
};

const texts = {
    title: "TODO GraphQL",
    create: "Create task"
}

const UserMenu = props => {
    const { name, picture, onLogout } = props;

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
            <Avatar src={picture} style={styles.avatar} onClick={handleClick}>
            {picture ? null : (
                <PersonIcon />
            )}
            </Avatar> 
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
        <WrappedComponent {...props} picture={_.get(data, "currentUser.picture", null)} />
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