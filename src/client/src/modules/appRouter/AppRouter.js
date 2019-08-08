import React, { Component } from 'react';
import _ from "lodash";
import { Switch, Route, BrowserRouter, Redirect } from 'react-router-dom';
import ROUTES from "./routes";
import { components as boardComponents } from "../Board";
import TopPanel from '../Board/components/TopPanel';
import BottomPanel from '../Board/components/BottomPanel';
import { compose, withHandlers } from 'recompose';
import { currentUserQuery } from '../Board/queries';
import { withApollo, Query } from 'react-apollo';
import { makeStyles, useMediaQuery, useTheme } from '@material-ui/core';
import clsx from 'clsx';
import SideBar from '../Board/components/SideBar';

const useStyles = makeStyles(theme => ({
  boardWrapper: {
      paddingTop: "64px",
      transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  boardWrapperShiftLeft: {
      marginLeft: 240,
      transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  boardWithMargin: {
      marginLeft: 72,
  }
}));

const AppRouter = props => {

  const classes = useStyles();
  const theme = useTheme();

  const menuOpened = _.get(props.currentUser, "menuOpened", false);

  const drawerAnchorTop = useMediaQuery(theme.breakpoints.down('xs'));

  return (
    <BrowserRouter>
      <React.Fragment>
        <TopPanel />
        <div className={clsx(classes.boardWrapper, {
                [classes.boardWithMargin]: !drawerAnchorTop && !menuOpened,
                [classes.boardWrapperShiftLeft]: menuOpened && !drawerAnchorTop
            })} onClick={props.handleClose}>
          <Switch>
            <Route exact path={ROUTES.HOME} render={props => <boardComponents.default {...props} />} />
            <Route exact path={ROUTES.CREATE_FILM} render={props => <boardComponents.default {...props} isCreating={true} />} />
            <Route exact path={ROUTES.EDIT_FILM.route} component={boardComponents.default}/>
            <Redirect to={ROUTES.HOME} />
          </Switch>
        </div>
        <BottomPanel />
        <SideBar />
      </React.Fragment>
    </BrowserRouter>
  );
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
)(AppRouter);
