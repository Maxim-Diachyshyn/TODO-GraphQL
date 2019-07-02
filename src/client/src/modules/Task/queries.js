import { gql } from "apollo-boost";

const todoByIdQuery = gql`
query($id: ID!){
    todo(id:$id) {
      id
      name
      description
      status
    }
}`

const usersQuery = gql`
query {
  users {
    id
    email
    picture
    username
  }
}`

export { todoByIdQuery, usersQuery };