import { gql } from "apollo-boost";

const todoAdded = gql`
subscription{
    todoAdded{
      id
      name
      description
      status
    }
  }`;

const deleteTodo = gql`
subscription{
    todoDeleted{
      id
    }
}`;

const todoUpdated = gql`
subscription{
    todoUpdated{
      id
      name
      description
      status
    }
}`;

export { todoAdded, deleteTodo, todoUpdated };
