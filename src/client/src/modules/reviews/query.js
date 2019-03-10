import { gql } from "apollo-boost";

export default gql`
    query($id: ID!) {
        film(id: $id) {
            id
            reviews {
                id
                addedAt
                comment
                rate
            }
        }
    }
`;