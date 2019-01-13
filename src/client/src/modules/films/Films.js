import React, { Component } from 'react';
import { graphql } from "react-apollo";
import { Link } from 'react-router-dom'
import _ from "lodash";
import query from "./query";
import ROUTES from "../appRouter/routes";

class Films extends Component {
    render() {
        const { data } = this.props;
        if (data.loading) {
            return "Loading...";
        }
        return (
            <div>
                <ul key='allFilms'>
                    {_.map(data.films, (({ id, name }) => (
                        <li key={id}>{name}</li>
                    )))}
                </ul>
                <Link to={ROUTES.CREATE_FILM}>Add</Link>
            </div>
        );
    }
}
  
export default graphql(query)(Films);
