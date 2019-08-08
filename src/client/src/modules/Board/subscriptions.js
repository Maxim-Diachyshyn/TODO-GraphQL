import { gql } from "apollo-boost";

const todoAdded = gql`
subscription($searchText: String, $assignedUser: String){
    todoAdded(searchText: $searchText, assignedUser: $assignedUser){
      id
      name
      description
      status
      createdAt
      assignedUser {
        id
        username
        email
        picture
      }
    }
  }`;

const deleteTodo = gql`
subscription($searchText: String, $assignedUser: String){
    todoDeleted(searchText: $searchText, assignedUser: $assignedUser){
      id
      status
    }
}`;

const todoUpdated = gql`
subscription($searchText: String, $assignedUser: String){
    todoUpdated(searchText: $searchText, assignedUser: $assignedUser){
      id
      name
      description
      status
      createdAt
      assignedUser {
        id
        username
        email
        picture
      }
    }
}`;

export { todoAdded, deleteTodo, todoUpdated };
