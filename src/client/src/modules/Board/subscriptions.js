import { gql } from "apollo-boost";

const todoAdded = gql`
subscription{
    todoAdded{
      id
      name
      description
      status
      assignedUser {
        id
      }
    }
  }`;

const deleteTodo = gql`
subscription{
    todoDeleted{
      id
      status
    }
}`;

const todoUpdated = gql`
subscription{
    todoUpdated{
      id
      name
      description
      status
      assignedUser {
        id
      }
    }
}`;

export { todoAdded, deleteTodo, todoUpdated };
