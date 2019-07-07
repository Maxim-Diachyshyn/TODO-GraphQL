import { gql } from "apollo-boost";

const todosQuery = gql`
query($status: TodoStatus){
    todos(status:$status){
      id
      name
      status
      assignedUser {
        id
      }
    }
}`;

const currentUserQuery = gql`
query {
    currentUser {
      id
      username
      email
      picture
      logoutRequested
    }
}`;

export { todosQuery, currentUserQuery };
