import { gql } from "apollo-boost";

const todosQuery = gql`
query{
    todos{
      id
      name
      description
      status
    }
}`;

const todoQuery = gql`
query($id: ID!){
    todo(id:$id) {
      id
      name
      description
      status
    }
}`;

export { todosQuery, todoQuery };
