import { gql } from "apollo-boost";

export default gql`
query {
    films {
      id
      name
      showedDate,
      photo
    }
  }
`;