import { gql } from "apollo-boost";

const reviewAddedSubscription = gql`
subscription {
    reviewAdded {
      id
      rate
      addedAt
      comment
    }
}`;

export default { reviewAddedSubscription }