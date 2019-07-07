import { gql } from "apollo-boost";

const createTodo = gql`
mutation($name: String!, $description: String!, $status: TodoStatus, $assignedUser: OnlyIdObjectType){
  createTodo(todo: {
    name: $name,
    description: $description
    status: $status
    assignedUser: $assignedUser
  }) {
    id
  }
}`;

export { createTodo };