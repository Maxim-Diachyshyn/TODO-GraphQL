import { gql } from "apollo-boost";

export default gql`
mutation($filmId: ID!, $comment: String!, $rate: Int!) {
    createReview(review: {filmId: $filmId, comment: $comment, rate: $rate}) {
        id
        addedAt
        comment
        rate
    }
  }
`;