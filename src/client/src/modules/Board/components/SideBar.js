import React, { useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';
import _ from "lodash";
import clsx from 'clsx';
import { IconButton, AppBar, Typography, Avatar, Menu, MenuItem, Tooltip, Drawer, TextField, useMediaQuery } from '@material-ui/core';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import CssBaseline from '@material-ui/core/CssBaseline';
import Divider from '@material-ui/core/Divider';
import MenuIcon from '@material-ui/icons/Menu';
import { PersonAdd as PersonAddIcon, Clear as ClearIcon } from '@material-ui/icons';
import { Scrollbars } from 'react-custom-scrollbars';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import { makeStyles, useTheme } from '@material-ui/core/styles';
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
    unassigned: "Unassigned",
    clear: "Clear All"
}

const useStyles = makeStyles(theme => ({
  menuButton: {
    marginRight: 36,
  },
  hide: {
    visibility: 'hidden',
  },
  drawerLeft: {
    width: drawerWidth,     
    flexShrink: 0,
    whiteSpace: 'nowrap',
    paddingTop: 72
  },
  drawerTop: {
    width: "auto",
    height: "calc(100vh - 64px)",
    marginTop: 64
  },
  drawerLeftOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
    paddingTop: 0
  },
  drawerTopOpen: {
    transition: theme.transitions.create('height', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    })
  },
  drawerLeftClose: {
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
  drawerTopClose: {
    transition: theme.transitions.create('height', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    })
  },
  toolbar: {
    display: 'grid',
    alignItems: 'center',
    padding: '0 8px',
    ...theme.mixins.toolbar,
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
  },
  iconButton: {
    fontSize: 18
  },
  clearButton: {
    background: "#f44336"
  }
}));

const SideBar = (props) => {
    const { currentUser, onChange, handleClose, handleOpen, users } = props;

    const classes = useStyles();
    const theme = useTheme();

    const menuOpened = _.get(currentUser, "menuOpened", false);
    const searchTextProp = _.get(currentUser, "searchText", "");
    const searchUserProp = _.get(currentUser, "searchUser", null);

    const drawerAnchorTop = useMediaQuery(theme.breakpoints.down('xs'));

    const [searchText, setSearchText] = useState(searchTextProp);
    const [timer, setTimer] = useState(null);

    const onChangeHandler = e => {
      const { id, value } = e.target;
      if (timer) {
        clearTimeout(timer);
      }
      setSearchText(value);
      setTimer(setTimeout(() => onChange({[id]: value}), 500))
    };

    const onChangeUserHandler = value => {
      if (timer) {
        clearTimeout(timer);
      }
      onChange({ searchText, searchUser: value });
    };

    useEffect(() => {
      if (timer) {
        clearTimeout(timer);
      }
      setSearchText(searchTextProp);
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [searchTextProp]);

    return (
        <Drawer
        anchor={drawerAnchorTop ? "top" : "left"}
        variant={drawerAnchorTop ? "persistent" : "permanent"}
        className={clsx({
            [classes.drawerLeft]: !drawerAnchorTop,
            [classes.drawerTop]: drawerAnchorTop,
            [classes.drawerLeftOpen]: menuOpened && !drawerAnchorTop,
            [classes.drawerLeftClose]: !menuOpened && !drawerAnchorTop,
            [classes.drawerTopOpen]: menuOpened && drawerAnchorTop,
            [classes.drawerTopClose]: !menuOpened && drawerAnchorTop,
        })}
        classes={{
            paper: clsx({
                [classes.drawerLeft]: !drawerAnchorTop,
                [classes.drawerTop]: drawerAnchorTop,
                [classes.drawerLeftOpen]: menuOpened && !drawerAnchorTop,
                [classes.drawerLeftClose]: !menuOpened && !drawerAnchorTop,
                [classes.drawerTopOpen]: menuOpened && drawerAnchorTop,
                [classes.drawerTopClose]: !menuOpened && drawerAnchorTop,
        })}}
        open={menuOpened}
      >
        <Scrollbars>
          {menuOpened ? (
            <div className={clsx(classes.toolbar, {
                [classes.toolbarTop]: drawerAnchorTop
            })}>
                <TextField
                    id="searchText"
                    label="Search"
                    value={searchText}
                    margin="normal"
                    variant="standard"
                    onChange={onChangeHandler} 
                />
            </div>
            ) : null}
            <Divider />
            <List>
            <ListItem button={true} onClick={() => onChangeUserHandler("unassigned")} selected={searchUserProp === keys.unassigned} key={keys.unassigned} value={keys.unassigned} className={classes.iconButton}>
                <ListItemIcon>
                  <Avatar>
                    <PersonAddIcon/>
                  </Avatar>
                </ListItemIcon>
                <ListItemText>
                    <Typography variant="button">{texts.unassigned}</Typography>
                </ListItemText>
            </ListItem>
                {_.map(users, u => (
                    <ListItem button={true} onClick={() => onChangeUserHandler(u.id)} selected={searchUserProp === u.id} key={u.id} value={u.id}>
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
        </Scrollbars>
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
    // withLoader,
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
        },
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
        onChange: props => updates => {
          const { client, currentUser } = props;

          client.writeQuery({
              query: currentUserQuery,
              data: {
                  currentUser: {
                    ...currentUser,
                    ...updates
                  }
              }
          });
        }
    })
)(SideBar);