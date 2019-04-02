import { gql } from "apollo-boost";

const filmDeletedByIdSubscription = gql`
subscription($id: ID!) {
    filmDeletedById(id: $id) {
        id
    }
  }
`;

export default { filmDeletedByIdSubscription }