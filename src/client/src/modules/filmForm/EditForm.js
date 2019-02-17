import React, { Component } from 'react';
import { graphql } from "react-apollo";
import _ from "lodash";
import { Link } from 'react-router-dom'
import {editMutation} from "./mutation";
import ROUTES from "../appRouter/routes";
import filmQuery from "../films/query";
import moment from 'moment';

class EditForm extends Component {
    constructor(props){
        super(props);

        this.state = {
            name: "",
            id: "",
            showedDate: moment().format("YYYY-MM-DD"),
            photo: "",
            isUploadingFile: false,
            errors: [],
            isAdding: false
        };
    }

    componentDidMount() {
        var film = this.props.location.state;
        this.setState({id: film.id, name: film.name, showedDate: moment(film.showedDate).format("YYYY-MM-DD"), photo: film.photo})
    }

    _onNameChange = (e) => {
        this.setState({name: e.target.value});
    }

    _onShowedDateChange = (e) => {
        this.setState({showedDate: e.target.value});
    }

    _onfileChange = (e) => {
        var reader = new FileReader();
        const file = e.target.files[0];
        if (!file) {
            return;
        }
        reader.readAsDataURL(file);
        this.setState({isUploadingFile: true, photo: ""});
        reader.onload = () => {
            this.setState({isUploadingFile: false, photo: reader.result});
        };
    }

    _onSubmit = (e) => {
        const { mutate, history } = this.props;
        e.preventDefault();
        this.setState({errors: [], isAdding: true});
        mutate({
            variables: _.pick(this.state, ["id", "name", "showedDate", "photo"]),
            update: (cache, { data: { film } }) => {
                const { films } = cache.readQuery({ query: filmQuery });
                cache.writeQuery({
                    query: filmQuery,
                    data: { films: [...films, [film.id]: film] },
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
                    <input 
                        type="file"
                        onChange={this._onfileChange}
                    />
                    <img src={this.state.photo} style={photoStyle} height={300} width={300}/>
                    <button type='submit' disabled={this.state.isUploadingFile} style={submitStyle}>Ok</button>
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
}

const submitStyle = {
    gridColumn: "1 / span 2",
}
  
export default graphql(editMutation)(EditForm);
