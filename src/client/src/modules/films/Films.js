import React, { Component } from 'react';
import { graphql } from "react-apollo";
import { Link } from 'react-router-dom'
import _ from "lodash";
import query from "./query";
import ROUTES from "../appRouter/routes";
import subscription from './subscription';

class Films extends Component {
    componentDidMount() {
        this.props.data.subscribeToMore({
            document: subscription,
            updateQuery: (prev, { subscriptionData }) => {
              if (!subscriptionData.data || !subscriptionData.data.filmAdded)
                return prev;
              const newFilmAdded = subscriptionData.data.filmAdded;
  
              return Object.assign({}, prev, {
                films: [...prev.films, newFilmAdded]
              });
            }
          });
    }

    render() {
        const { data } = this.props;
        if (data.loading) {
            return "Loading...";
        }
        return (
            <div>
                <ul key='allFilms'>
                    {_.map(data.films, ({ id, name }) => (
                        <li key={id}>{name}</li>
                    ))}
                </ul>
                <Link to={ROUTES.CREATE_FILM}>Add</Link>
            </div>
        );
    }
}
  
export default graphql(query)(Films);
