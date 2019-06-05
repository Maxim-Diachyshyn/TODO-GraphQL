import { gql } from "apollo-boost";

const deleteTodo = gql`
mutation($id: ID!){
    deleteTodo(id: $id) {
        id
    }
}`;

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

const updateTodo = gql`
mutation($todo: UpdateTodoInputType!) {
    updateTodo(todo: $todo) {
      id
    }
}`;

export { deleteTodo, createTodo, updateTodo };
