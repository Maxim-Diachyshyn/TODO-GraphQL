import React, { Component } from 'react';
import { Switch, Route, BrowserRouter } from 'react-router-dom';
import ROUTES from "./routes";
import VIEWS from "../components";

class AppRouter extends Component {
    render() {
      return (
        <BrowserRouter>
          <Switch>
            <Route exact path={ROUTES.HOME} component={VIEWS.Films}/>
            <Route exact path={ROUTES.CREATE_FILM} component={VIEWS.AddFilm}/>
          </Switch>
        </BrowserRouter>
      );
    }
  }
  
  export default AppRouter;
