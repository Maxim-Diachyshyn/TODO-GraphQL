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

        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onShowedDateChange = this.onShowedDateChange.bind(this);
    }

    onNameChange = (e) => {
        this.setState({name: e.target.value});
    }

    onShowedDateChange = (e) => {
        this.setState({showedDate: e.target.value});
    }

    onSubmit = (e) => {
        const { mutate, history } = this.props;
        e.preventDefault();
        mutate({
            variables: this.state, 
            refetchQueries: [ { query: filmQuery }],
            })
            .then(() => {
                history.push(ROUTES.HOME);
            });
    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <input
                    type='text'
                    onChange={this.onNameChange}
                />
                <input
                    type='date'
                    onChange={this.onShowedDateChange}
                />
                <button type='submit'>Create</button>
            </form>
        );
    }
}
  
export default graphql(mutation)(AddFilm);
