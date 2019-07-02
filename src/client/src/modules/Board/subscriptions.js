import { gql } from "apollo-boost";

const todoAdded = gql`
subscription{
    todoAdded{
      id
      name
      description
      status
      assignedUserId
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
      assignedUserId
    }
}`;

export { todoAdded, deleteTodo, todoUpdated };
