import { gql } from "apollo-boost";

const signIn = gql`
mutation($token: String!) {
    signIn(token: $token) {
        id
        username
        email
        picture
    }
}`;

export { signIn };
