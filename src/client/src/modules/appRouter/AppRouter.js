import React, { Component } from 'react';
import { Switch, Route, BrowserRouter } from 'react-router-dom';
import ROUTES from "./routes";
import VIEWS from "../components";

class AppRouter extends Component {
    render() {
      return (
        <BrowserRouter>
            <Route exact path={ROUTES.HOME} component={VIEWS.Films}/>
        </BrowserRouter>
      );
    }
  }
  
  export default AppRouter;
