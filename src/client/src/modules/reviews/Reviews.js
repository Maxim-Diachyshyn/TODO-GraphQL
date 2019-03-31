import React, { Component } from 'react';
import moment from 'moment';
import _ from "lodash";
import StarRatings from 'react-star-ratings';
import { Mutation, Query, withApollo, graphql } from "react-apollo";
import query from "./query";
import filmsQuery from "../films/query";
import mutation from "./mutation";
import subscription from "../films/subscription";

const initialState = {
    rate: 0,
    comment: ""
}

class Reviews extends Component {
    
    constructor(props){
        super(props);

        this.state = initialState;
    }

    componentDidMount() {
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
    }

    mapReviewTime = review => {
        const { addedAt } = review;
        const momentDate = moment(addedAt);
        if (momentDate.diff(momentDate, "days") <= 0) {
            return momentDate.fromNow();
        }
        return momentDate.format("lll");
    }

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
            <div key={r.id} style={{ ...reviewStyle, ...(r.isNew ? newReviewStyle : null) }}>
                <div style={reviewHeaderStyle}>
                    <span>{this.mapReviewTime(r)}</span>
                    <StarRatings rating={r.rate} starRatedColor="#FFE438" starEmptyColor="#FFFFAA" starDimension="14px" starSpacing="2px"/>
                </div>
                <div style={reviewTextStyle}>
                    <span>{r.comment}</span>
                </div>
            </div>
        ))
    }

    onReviewAdded = () => {
        this.setState({...initialState});
    }

    onChangeComment = e => {
        const comment = e.target.value;
        this.setState({ comment });
    }

    onChangeRating = rate => {
        this.setState({ rate });
    }

    onSubmit = (e, createReview) => {
        e.preventDefault();
        const filmId = this.props.id;
        const { comment, rate } = this.state;
        createReview({ variables: { filmId, comment, rate } });
    }

    render() {
        const { data: { error }, id } = this.props;
        const { comment, rate }= this.state;
        if (error) {
            return <span>Error...</span>
        }
        return (
            <div style={containerStyle}>
                <span>Reviews</span>
                <Mutation 
                    mutation={mutation}
                    onCompleted={this.onReviewAdded}
                    >
                    {(createReview, { loading, error }) => {
                        if (loading) {
                            return <span>Adding review...</span>
                        }
                        if (error) {
                            return <span>{error.message}</span>
                        }
                        return (
                            <form style={formStyle} onSubmit={e => this.onSubmit(e, createReview)}>
                                <StarRatings rating={rate} changeRating={this.onChangeRating} starRatedColor="#FFE438" starEmptyColor="#FFFFAA" starDimension="20px" starSpacing="4px"/>
                                <div style={commentContainerStyle}>
                                    <input
                                        value={comment}
                                        onChange={this.onChangeComment}
                                        style={{flex: 1}}
                                        type='text'
                                    />
                                    <input type='submit' value="Add"/>
                                </div>
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

const newReviewStyle = {
    background: "yellow"
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

const formStyle = {
    display: "flex", 
    flexDirection: "column"
}

const commentContainerStyle = {
    display: "flex", 
    flexDirection: "row"
}