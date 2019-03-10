import React, { Component } from 'react';
import moment from 'moment';
import _ from "lodash";
import StarRatings from 'react-star-ratings';
import { Mutation, Query, withApollo, graphql } from "react-apollo";
import query from "./query";

class Reviews extends Component {

    renderReviews = () => {
        const { data: { loading, error, film } } = this.props;
        const empty = !loading && !error && !_.some(film.reviews);
        if (empty) {
            return <span>No Reviews</span>
        }
        if (loading) {
            return <span>Loading...</span>
        }
        return _.map(film.reviews, r => (
            <div key={r.id} style={reviewStyle}>
                <div style={reviewHeaderStyle}>
                    <span>{moment().format(r.addedAt)}</span>
                    <StarRatings rating={r.rate} starRatedColor="#FFE438" starEmptyColor="#FFFFAA" starDimension="14px" starSpacing="2px"/>
                </div>
                <div style={reviewTextStyle}>
                    <span>{r.comment}</span>
                </div>
            </div>
        ))
    }

    render() {
        const { data: { loading, error, film } } = this.props;
        const empty = !loading && !error && !_.some(film.reviews);
        if (error) {
            return <span>Error...</span>
        }
        return (
            <div style={containerStyle}>
                <span>Reviews</span>
                <form style={{display: "flex", flexDirection: "row"}}>
                    <input
                        style={{flex: 1}}
                        type='text'
                    />
                    <input type='submit' value="Add"/>
                </form>
                {this.renderReviews()}
            </div>
        );
    }
}

export default graphql(query)(Reviews);

const containerStyle = {
    display: "flex",
    flexDirection: "column",
    textAlign: "left"
}

const reviewStyle = {
    marginTop: 30
}

const reviewHeaderStyle = {
    display: "flex",
    flexDirection: "row",
    fontSize: 14
}

const reviewTextStyle = {
    textAlign: "justify",
    marginTop: 10
}