import { gql } from "apollo-boost";

const todosQuery = gql`
query($status: TodoStatus, $searchText: String, $assignedUser: String){
    todos(status: $status, searchText: $searchText, assignedUser: $assignedUser){
      id
      name
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

const currentUserQuery = gql`
query {
    currentUser {
      id
      username
      email
      picture
      logoutRequested
      menuOpened
      searchText
      searchUser
    }
}`;

export { todosQuery, currentUserQuery };
