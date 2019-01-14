import React, { Component } from 'react';
import { graphql } from "react-apollo";
import _ from "lodash";
import mutation from "./mutation";
import ROUTES from "../appRouter/routes";
import filmQuery from "../films/query";

class AddFilm extends Component {
    constructor(props){
        super(props);

        this.state = {
            name: "",
            showedDate: new Date(),
        };
    }

    _onNameChange = (e) => {
        this.setState({name: e.target.value});
    }

    _onShowedDateChange = (e) => {
        this.setState({showedDate: e.target.value});
    }

    _onSubmit = (e) => {
        const { mutate, history } = this.props;
        e.preventDefault();
        mutate({
            variables: this.state,
            update: (cache, { data: { createFilm } }) => {
                const { films } = cache.readQuery({ query: filmQuery });
                cache.writeQuery({
                  query: filmQuery,
                  data: { films: [...films, createFilm] },
                });
              }}
            )
            .then(() => {
                history.push(ROUTES.HOME);
            });
    }

    render() {
        return (
            <form onSubmit={this._onSubmit}>
                <input
                    type='text'
                    onChange={this._onNameChange}
                />
                <input
                    type='date'
                    onChange={this._onShowedDateChange}
                />
                <button type='submit'>Create</button>
            </form>
        );
    }
}
  
export default graphql(mutation)(AddFilm);
