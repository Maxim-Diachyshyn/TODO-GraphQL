import { gql } from "apollo-boost";

export default gql`
mutation($name: String!, $showedDate: Date!) {
    createFilm(
        film: {
            name:$name
            showedDate: $showedDate
            }
    ) {
      id 
    }
  }
`;