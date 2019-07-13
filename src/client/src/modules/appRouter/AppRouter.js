import React, { Component } from 'react';
import { Switch, Route, BrowserRouter, Redirect } from 'react-router-dom';
import ROUTES from "./routes";
import { components as boardComponents } from "../Board";
import TopPanel from '../Board/components/TopPanel';
import BottomPanel from '../Board/components/BottomPanel';

class AppRouter extends Component {
    render() {
      return (
        <BrowserRouter>
          <React.Fragment>
            <TopPanel />
            <Switch>
              <Route exact path={ROUTES.HOME} render={props => <boardComponents.default {...props} />} />
              <Route exact path={ROUTES.CREATE_FILM} render={props => <boardComponents.default {...props} isCreating={true} />} />
              <Route exact path={ROUTES.EDIT_FILM.route} component={boardComponents.default}/>
              <Redirect to={ROUTES.HOME} />
            </Switch>
            <BottomPanel />
          </React.Fragment>
        </BrowserRouter>
      );
    }
  }
  
  export default AppRouter;
