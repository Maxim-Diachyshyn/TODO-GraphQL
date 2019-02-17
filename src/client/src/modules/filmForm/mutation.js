import { gql } from "apollo-boost";

export const createMutation = gql`
mutation ($name: String!, $showedDate: Date!, $photo: String!) {
  createFilm(
    film: {
      name: $name
      showedDate: $showedDate,
      photo: $photo
    }
    ) {
      id 
      name
      showedDate,
      photo
    }
  }
`;

export const editMutation = gql`
mutation ($id: ID!, $name: String!, $showedDate: Date!, $photo: String!) {
  updateFilm(
    film: {
      id: $id,
      name: $name
      showedDate: $showedDate,
      photo: $photo
    }
    ) {
      id 
      name
      showedDate,
      photo
    }
  }
`;

export const deleteMutation = gql`
mutation ($id: ID!) {
  deleteFilm(id: $id) {
      id 
      name
      showedDate,
      photo
    }
  }
`;