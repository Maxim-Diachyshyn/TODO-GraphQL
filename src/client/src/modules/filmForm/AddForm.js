import React, { Component } from 'react';
import { graphql } from "react-apollo";
import _ from "lodash";
import { Link } from 'react-router-dom'
import {createMutation} from "./mutation";
import ROUTES from "../appRouter/routes";
import filmQuery from "../films/query";
import moment from 'moment';

class AddForm extends Component {
    constructor(props){
        super(props);

        this.state = {
            name: "",
            showedDate: moment().format("YYYY-MM-DD"),
            photo: "",
            photoPath: "",
            isUploadingFile: false,
            errors: [],
            isAdding: false
        };
    }

    _onNameChange = (e) => {
        this.setState({name: e.target.value});
    }

    _onShowedDateChange = (e) => {
        this.setState({showedDate: e.target.value});
    }

    _onfileChange = (e) => {
        this.setState({isUploadingFile: true, photo: "", photoName: ""});
        var reader = new FileReader();
        const file = e.target.files[0];
        const photoPath = window.URL.createObjectURL(file);
        if (!file) {
            this.setState({isUploadingFile: false});
            return;
        }
        reader.readAsDataURL(file);
        reader.onload = () => {
            this.setState({isUploadingFile: false, photo: reader.result, photoPath});
        };
    }

    _onSubmit = (e) => {
        const { mutate, history } = this.props;
        e.preventDefault();
        this.setState({errors: [], isAdding: true});
        mutate({
            variables: _.pick(this.state, ["name", "showedDate", "photo"]),
            update: (cache, { data: { film } }) => {
                const { films } = cache.readQuery({ query: filmQuery });
                cache.writeQuery({
                    query: filmQuery,
                    data: { films: [...films, film] },
                });
              }}
            )
            .then(() => {
                this.setState({isAdding: false}, () => {
                    history.push(ROUTES.HOME);
                });
            })
            .catch(error => {
                this.setState({errors: error.graphQLErrors, isAdding: false});
            });
    }

    render() {
        const { errors, isAdding } = this.state;
        if (isAdding) {
            return <span>Adding New Films...</span>
        }
        return (
            <div style={containerStyle}>
                <Link to={ROUTES.HOME} style={backButtonStyle}>Back</Link>
                <form onSubmit={this._onSubmit} style={formContainerStyle}>
                    <span>Film Name:</span>
                    <div style={inputContainerStyle}>
                        <input
                            type='text'
                            value={this.state.name}
                            onChange={this._onNameChange}
                        />
                        {_.some(errors, e => e.extensions.code === "EmptyName") 
                        ? (
                            <span style={errorStyle}>Film name should not be empty</span>
                        )
                        : null}
                    </div>

                    <span>Premiere Date:</span>
                    <div style={inputContainerStyle}>
                        <input
                            type='date'
                            value={this.state.showedDate}
                            onChange={this._onShowedDateChange}
                        />
                        {_.some(errors, e => e.extensions.code === "EmptyDate") 
                        ? (
                            <span style={errorStyle}>Showed date should not be empty</span>
                        )
                        : null}
                    </div>

                    <span>Photo:</span>
                    <div style={inputContainerStyle}>                    
                    {_.some(errors, e => e.extensions.code === "EmptyPhoto") 
                    ? (
                        <span style={errorStyle}>Photo should not be empty</span>
                    )
                    : null}
                    <input 
                        type="file"
                        ref={this.state.photoPath}
                        onChange={this._onfileChange}
                    />
                    </div>
                    <img src={this.state.photo || null} style={photoStyle} height={300} width={300}/>
                    <button type='submit' disabled={this.state.isUploadingFile} style={submitStyle}>Create</button>
                </form>
            </div>
        );
    }
}

const containerStyle = {
    display: "flex",
    flexDirection: "column",
    padding: 20
}

const errorStyle = {
    color: "red",
    fontSize: 12
}

const inputContainerStyle = {
    display: "flex",
    flexDirection: "column"
}

const backButtonStyle = {
    alignSelf: "flex-start",
}

const formContainerStyle = {
    padding: 20,
    display: "grid",
    maxWidth: 450,
    justifyContent: "space-between",
    textAlign: "left",
    gridTemplateColumns: "1fr 1fr",
    gridColumnGap: 10,
    gridRowGap: 10,
    alignSelf: "center",
}

const photoStyle = {
    gridColumn: "1 / span 2",
    justifySelf: "center"
}

const submitStyle = {
    gridColumn: "1 / span 2",
}
 
export default graphql(createMutation)(AddForm);
