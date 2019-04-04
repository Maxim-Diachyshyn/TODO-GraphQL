import { gql } from "apollo-boost";

const filmDeletedByIdSubscription = gql`
subscription($id: ID!) {
    filmDeletedById(id: $id) {
        id
    }
  }
`;

const filmUpdatedByIdSubscription = gql`
subscription($id: ID!) {
    filmUpdatedById(id: $id) {
        id
        name
        showedDate
        photo
    }
  }
`;

export default { filmDeletedByIdSubscription, filmUpdatedByIdSubscription }