import React, { Component } from 'react';
import { graphql } from "react-apollo";
import { gql } from "apollo-boost";
import _ from "lodash";

const query = gql`
query {
    films {
      id
      name
      showedDate
    }
  }
`;

class Films extends Component {
    render() {
        const { data } = this.props;
        return (
            <ul key='allUsers'>
                {_.map(data.films, (({ id, name }) => (
                    <li key={id}>{name}</li>
                )))}
            </ul>
        );
    }
}
  
export default graphql(query)(Films);
