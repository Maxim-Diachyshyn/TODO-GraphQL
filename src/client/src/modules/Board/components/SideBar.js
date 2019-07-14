import React from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import clsx from 'clsx';
import { IconButton, AppBar, Typography, Avatar, Menu, MenuItem, Tooltip, Drawer, TextField } from '@material-ui/core';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import CssBaseline from '@material-ui/core/CssBaseline';
import Divider from '@material-ui/core/Divider';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import { Delete as DeleteIcon, PersonAdd as PersonAddIcon } from '@material-ui/icons';
import { Create } from '@material-ui/icons';
import ROUTES from "../../appRouter/routes";
import { Mutation, Query, withApollo } from 'react-apollo';
import { signIn } from '../../SignIn/mutations';
import { compose, withHandlers } from 'recompose';
import { currentUserQuery } from '../queries';
import { usersQuery } from '../../Task/queries';
import withLoader from '../../shared/withLoader';
import { userAdded } from '../../Task/subscriptions';

const drawerWidth = 240;

const keys = {
    unassigned: "unassigned"
};

const texts = {
    unassigned: "Unassigned"
}

const useStyles = makeStyles(theme => ({
  menuButton: {
    marginRight: 36,
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  drawerClose: {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: theme.spacing(7) + 1,
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing(9) + 1,
    },
  },
  toolbar: {
    display: 'grid',
    alignItems: 'center',
    gridTemplateColumns: '1fr auto',
    gridColumnGap: 4,
    padding: '0 8px',
    ...theme.mixins.toolbar,
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
}));

const SideBar = (props) => {
    const { currentUser, handleClose, users } = props;

    const classes = useStyles();
    const theme = useTheme();

    const menuOpened = _.get(currentUser, "menuOpened", false);

    return (
        <Drawer
        variant="persistent"
        className={clsx(classes.drawer, {
          [classes.drawerOpen]: menuOpened,
          [classes.drawerClose]: !menuOpened,
        })}
        classes={{
          paper: clsx({
            [classes.drawerOpen]: menuOpened,
            [classes.drawerClose]: !menuOpened,
          }),
        }}
        open={menuOpened}
      >
        <div className={classes.toolbar}>
            <TextField
                // style={styles.input}
                // disabled={loading}
                label="Filter"
                // value={name}
                margin="normal"
                variant="standard"
                // onChange={onChange} 
            />
            <IconButton onClick={handleClose}>
                {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
            </IconButton>
        </div>
        <Divider />
        <List>
        <ListItem key={keys.unassigned} value={keys.unassigned}>
            <ListItemIcon>
                <PersonAddIcon/>
            </ListItemIcon>
            <ListItemText>
                <Typography variant="button">{texts.unassigned}</Typography>
            </ListItemText>
        </ListItem>
            {_.map(users, u => (
                <ListItem key={u.id} value={u.id}>
                    <ListItemIcon>
                        <Avatar src={u.picture} />
                    </ListItemIcon>
                    <ListItemText>
                        <Typography variant="button">{u.username}</Typography>
                    </ListItemText>
                </ListItem>)
            )}        
        </List>
        <Divider />
      </Drawer>               
    )
};

let subscription = null;
const withData = WrappedComponent => props => (
    <Query query={usersQuery}>
    {({ loading, error, data: usersData, subscribeToMore }) => {
        if (!subscription) {
            subscription = () => subscribeToMore({
                document: userAdded,
                updateQuery: (prev, { subscriptionData }) => {
                    if (!subscriptionData.data) return prev;
                    const { userAdded } = subscriptionData.data;
                    const users = _.reject(prev.users, x => x.id === userAdded.id);
                    return {
                        ...prev,
                        users: [...users, userAdded]
                    }
                },
            });
            subscription();
        }
        return (
            <Query query={currentUserQuery} fetchPolicy="cache-only">{({ data }) => (
                <WrappedComponent {...props} currentUser={_.get(data, "currentUser", {})} loading={loading} users={_.get(usersData, "users", [])}/>
            )}</Query>
        )
    }}</Query>
);

export default compose(
    withApollo,
    withRouter,
    withData,
    withLoader,
    withHandlers({
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
)(SideBar);