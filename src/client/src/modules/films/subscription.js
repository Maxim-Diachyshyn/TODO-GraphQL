import { gql } from "apollo-boost";

export default gql`
subscription {
    filmAdded{
      id
      name,
      showedDate,
      photo
    }
  }
`;