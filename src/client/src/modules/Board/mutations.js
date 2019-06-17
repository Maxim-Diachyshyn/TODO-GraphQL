import { gql } from "apollo-boost";

const deleteTodo = gql`
mutation($id: ID!){
    deleteTodo(id: $id) {
        id
    }
}`;

const updateTodo = gql`
mutation($todo: UpdateTodoInputType!) {
    updateTodo(todo: $todo) {
      id
    }
}`;

export { deleteTodo, updateTodo };
