import { gql } from "apollo-boost";

const filmAddedSubscription = gql`
subscription {
    filmAdded {
      id
      name
      showedDate
      photo
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
  }
}`;

export default {filmAddedSubscription, filmUpdatedSubscription}