import { gql } from "apollo-boost";

const todosQuery = gql`
query($status: TodoStatus){
    todos(status:$status){
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
