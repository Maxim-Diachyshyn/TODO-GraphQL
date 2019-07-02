import { gql } from "apollo-boost";

const createTodo = gql`
mutation($name: String!, $description: String!, $status: TodoStatus!, $assignedUserId: ID) {
    createTodo(todo: {
      name: $name,
      description: $description,
      status: $status,
      assignedUserId: $assignedUserId
    }) {
    id
    }
}`;

export { createTodo };