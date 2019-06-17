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

export { todoByIdQuery };