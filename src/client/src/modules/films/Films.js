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
            document: subscription.filmAddedSubscription,
            updateQuery: (prev, { subscriptionData }) => {
              if (!subscriptionData.data || !subscriptionData.data.filmAdded)
                return prev;
              const newFilmAdded = subscriptionData.data.filmAdded;
  
              return {
                ...prev,
                films: [...prev.films, newFilmAdded]
              };
            }
          });

        this.props.data.subscribeToMore({
            document: subscription.filmUpdatedSubscription,
            updateQuery: (prev, { subscriptionData }) => {
              if (!subscriptionData.data || !subscriptionData.data.filmAdded)
                return prev;
              const filmUpdated = subscriptionData.data.filmUpdated;
              const index = _.index(prev.films, f => f.id === filmUpdated.id);
              if (!index) {
                return prev;
              }
              return {
                ...prev,
                films: {...prev.films, [index]: filmUpdated}
              }
            }
          });
    }

    render() {
        const { data } = this.props;
        if (data.loading) {
            return <span>Loading...</span>
        }
        return (
            <div style={containerStyle}>
                <div style={filmsContainerStyle}>     
                    {_.map(data.films, film => (
                        <div key={film.id} style={filmContainerStyle}>
                            <span style={filmNameStyle}>{film.name}</span>
                            <Link to={{pathname: _.replace(ROUTES.EDIT_FILM, ":id", film.id)}}>
                                <img src={film.photo} height={300} width={300}/>
                            </Link>
                            <span>{new Date(film.showedDate).toLocaleDateString()}</span>
                        </div>
                    ))}
                </div>
                <div>
                    <Link to={ROUTES.CREATE_FILM}>Add</Link>
                </div>
            </div>
        );
    }
}

const containerStyle = {
    padding: 20
}

const filmsContainerStyle = {
    display: "grid",
    justifyContent: "center",
    alignItems: "flex-end",
    gridTemplateColumns: "repeat(auto-fill, minmax(300px, 1fr))",
    gridColumnGap: 10,
    gridRowGap: 20,
    padding: 20
}

const filmContainerStyle = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center"
}

const filmNameStyle = {
    fontWeight: "bold",
    fontSize: 24
}
  
export default graphql(query)(Films);
