import React, { Component } from 'react';
import moment from 'moment';
import _ from "lodash";
import StarRatings from 'react-star-ratings';
import { Mutation, Query, withApollo, graphql } from "react-apollo";
import query from "./query";
import filmsQuery from "../films/query"
import mutation from "./mutation"

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

    onSubmit = (e, createReview) => {
        e.preventDefault();
        const filmId = this.props.id;
        const comment = this.input.value;
        createReview({ variables: { comment, filmId, rate: 1 } });
    }

    render() {
        const { data: { error }, id } = this.props;
        if (error) {
            return <span>Error...</span>
        }
        return (
            <div style={containerStyle}>
                <span>Reviews</span>
                <Mutation 
                    mutation={mutation}
                    update={(cache, { data: { createReview } }) => {
                        const { film } = cache.readQuery({ query: query, variables: { id }});
                        const newReviews = film.reviews.concat([createReview]);
                        cache.writeQuery({
                            query: query,
                            variables: { id },
                            data: { film: {
                                ...film,
                                reviews: newReviews
                            }},
                        });

                        const newRates = _.map(newReviews, "rate");
                        const newRate = _.reduce(newRates, (a, b) => a + b, 0) / (newRates.length || 1);
                        const { films } = cache.readQuery({ query: filmsQuery});
                        debugger;
                        cache.writeQuery({
                            query: filmsQuery,
                            data: {
                                films: _.map(films, x => x.id === id ? {...x, rate: newRate} : x)
                            }
                        });
                    }}>
                    {(createReview, { loading }) => {
                        if (loading) {
                            return <span>Adding review...</span>
                        }
                        return (
                            <form style={{display: "flex", flexDirection: "row"}} onSubmit={e => this.onSubmit(e, createReview)}>
                                <input
                                    ref={node => {
                                        this.input = node;
                                    }}
                                    style={{flex: 1}}
                                    type='text'
                                />
                                <input type='submit' value="Add"/>
                            </form>
                        )
                    }}
                </Mutation>
                
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