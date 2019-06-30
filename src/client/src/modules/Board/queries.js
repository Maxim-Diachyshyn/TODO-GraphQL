import { gql } from "apollo-boost";

const todosQuery = gql`
query($status: TodoStatus){
    todos(status:$status){
      id
      name
      status
    }
}`;

export { todosQuery };
