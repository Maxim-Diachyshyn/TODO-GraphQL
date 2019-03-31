import React, { Component } from 'react';
import { graphql } from "react-apollo";
import { Link } from 'react-router-dom'
import { CircularProgress } from '@material-ui/core';
import _ from "lodash";
import moment from "moment";
import StarRatings from 'react-star-ratings';
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
                films: [newFilmAdded, ...prev.films]
              };
            }
          });

        this.props.data.subscribeToMore({
            document: subscription.filmUpdatedSubscription,
            updateQuery: (prev, { subscriptionData }) => {
              if (!subscriptionData.data || !subscriptionData.data.filmUpdated)
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

          this.props.data.subscribeToMore({
            document: subscription.filmDeletedSubscription,
            updateQuery: (prev, { subscriptionData }) => {
              if (!subscriptionData.data || !subscriptionData.data.filmDeleted)
                return prev;
              const filmDeleted = subscriptionData.data.filmDeleted;
              return {
                ...prev,
                films: _.reject(prev.films, {id: filmDeleted.id})
              }
            }
          });
    }

    render() {
        const { data } = this.props;
        if (data.loading) {
            return (
                <div style={loaderStyle}>
                    <CircularProgress />
                    <span>Loading films...</span>
                </div>
            )
        }
        if (data.error) {
            return <span>{data.error.message}</span>;
        }
        return (
            <div style={containerStyle}>
                <div style={filmsContainerStyle}>     
                    {_.map(data.films, film => (
                        <div key={film.id} style={filmContainerStyle}>
                            <span style={filmNameStyle}>{film.name} ({moment(film.showedDate).format("YYYY")})</span>
                            <StarRatings rating={film.rate || 0} starRatedColor="#FFE438" starEmptyColor="#FFFFAA"/>
                            <Link to={{pathname: _.replace(ROUTES.EDIT_FILM, ":id", film.id), state: {film}}}>
                                <img src={film.photo} height={300} width={300}/>
                            </Link>
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

const loaderStyle = {
    position: "absolute",
    top: 0,
    bottom: 0,
    right: 0,
    left: 0,

    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center"
}
  
export default graphql(query)(Films);
