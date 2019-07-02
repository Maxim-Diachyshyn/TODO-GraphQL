import { gql } from "apollo-boost";

const userAdded = gql`
subscription {
    userAdded {
      id
      email
      picture
      username
    }
}`;

export { userAdded };
