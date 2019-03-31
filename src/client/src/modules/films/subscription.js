import { gql } from "apollo-boost";

const filmAddedSubscription = gql`
subscription {
    filmAdded {
      id
      name
      showedDate
      photo
      rate
    }
  }
`;

const filmUpdatedSubscription = gql`
subscription {
  filmUpdated {
    id
    name
    showedDate
    photo
    rate
    reviews {
      id
      rate
      addedAt
      comment
    }
  }
}`;

const filmDeletedSubscription = gql`
subscription {
  filmDeleted {
    id
    name
    showedDate
    photo
    rate
  }
}`;

export default {filmAddedSubscription, filmUpdatedSubscription, filmDeletedSubscription}