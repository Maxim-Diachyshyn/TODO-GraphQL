import { gql } from "apollo-boost";

const createTodo = gql`
mutation($name: String!, $description: String!, $status: TodoStatus!) {
    createTodo(todo: {
      name: $name,
      description: $description,
      status: $status
    }) {
    id
    }
}`;

export { createTodo };