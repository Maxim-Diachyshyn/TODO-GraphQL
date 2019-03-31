import React, { Component } from 'react';
import { Mutation, withApollo } from "react-apollo";
import _ from "lodash";
import { Link } from 'react-router-dom'
import {editMutation, createMutation, deleteMutation} from "./mutation";
import filmQuery from "./query";
import filmsQuery from "../films/query";
import ROUTES from "../appRouter/routes";
import moment from 'moment';
import { Button, Form, Image } from "react-bootstrap";
import Reviews from "../reviews";

const DATE_FORMAT = "YYYY-MM-DD";

class EditForm extends Component {
    constructor(props){
        super(props);

        this.state = {
            film: {
                name: "",
                id: "",
                showedDate: moment(),
                photo: ""
            },
            isUploadingFile: false
        };
    }
    
    componentWillMount() {
        if (!this.props.isAddingForm) {
            let film = _.get(this.props.location, "state.film", null);
            if (!film) {
                this.props.client.query({query: filmQuery, variables: {id: this.props.match.params.id}})
                    .then(response => {
                        film = {
                            ...response.data.film,
                            showedDate: moment(response.data.film.showedDate)
                        };
                        this.setState({film});
                    });                
            }
            else {
                film = {
                    ...film,
                    showedDate: moment(film.showedDate)
                }
                this.setState({film});
            }
        }
    }

    _onNameChange = (e) => {
        const name = e.target.value;
        this.setState(prevState => ({film:{...prevState.film, name}}));
    }

    _onShowedDateChange = (e) => {
        const showedDate = moment(e.target.value);
        this.setState(prevState => ({film:{...prevState.film, showedDate}}));
    }

    _onFileChange = (e) => {
        var reader = new FileReader();
        const file = e.target.files[0];
        if (!file) {
            return;
        }
        reader.readAsDataURL(file);
        this.setState({isUploadingFile: true, photo: ""});
        reader.onload = () => {
            this.setState(prevState => ({
                isUploadingFile: false, 
                film: {
                    ...prevState.film, 
                    photo: reader.result
                }}
            ));
        };
    }

    withMutation = handler => {
        const { history, isAddingForm } = this.props;
        const mutation = isAddingForm 
            ? createMutation
            : editMutation;
        const variables = isAddingForm 
            ? _.pick(this.state.film, ["name", "showedDate", "photo"])
            : _.pick(this.state.film, ["id", "name", "showedDate", "photo"]);

        const update = isAddingForm ?
            (store, { data: { createFilm } }) => {
                if (store.data.data.ROOT_QUERY) {
                    const data = store.readQuery({ query: filmsQuery });
                    data.films.push(createFilm);
                    store.writeQuery({ 
                        query: filmsQuery,
                        data
                    });
                }
                
                history.push(ROUTES.HOME);
            }
            : () => history.push(ROUTES.HOME);
        return (
            <Mutation
                mutation={deleteMutation}
                variables={{id: this.state.film.id}}
                update={(store, { data: { deleteFilm } }) => {
                    if (store.data.data.ROOT_QUERY) {
                        const data = store.readQuery({ query: filmsQuery });
                        data.films = _.reject(data.films, {id: deleteFilm.id});
                        store.writeQuery({ 
                            query: filmsQuery,
                            data
                        });
                        history.push(ROUTES.HOME);
                    }
                }}
            >
                {(deleteFilm, {loading: deleteLoading}) =>
                    <Mutation
                    mutation={mutation}
                    variables={{
                        ...variables,
                        showedDate: variables.showedDate.format()
                    }}
                    update={update}
                    >
                        {isAddingForm
                            ? (createFilm, {loading, error}) => handler(createFilm, {loading, error}, null, {})
                            : (editFilm, {loading, error}) => handler(editFilm, {loading, error}, deleteFilm, {deleteLoading})
                        }
                    </Mutation>
                }              
            </Mutation>

        );
    }

    render() {
        const { isAddingForm } = this.props;
        const { film, isUploadingFile } = this.state;
        return this.withMutation((mutation, {loading, error}, deleteFilm, {deleteLoading}) => {
            const errors = _.get(error, "graphQLErrors", []);
            if (loading) {
                if (isAddingForm) {
                    return <span>Adding New Film...</span>
                }
                else {
                    return <span>Updating Film...</span>
                }
            }
            if (deleteLoading) {
                return <span>Deleting Film...</span>
            }
            return (
                <div style={containerStyle}>                
                    <Link to={ROUTES.HOME} style={backButtonStyle}>Back</Link>
                    <Form style={fromStyle} onSubmit={e => {e.preventDefault(); mutation();}}>
                        <Form.Group>
                            <Form.Label>Film Name:</Form.Label>
                                <Form.Control
                                    type='text'
                                    value={film.name}
                                    onChange={this._onNameChange}
                                />
                                {_.some(errors, e => e.extensions.code === "EmptyName") 
                                    ? (
                                        <Form.Text style={errorStyle}>Film name should not be empty</Form.Text>
                                    )
                                    : null}
                        </Form.Group>                        

                        <Form.Group>
                            <Form.Label>Premiere Date:</Form.Label>
                            <Form.Control
                                    type='date'
                                    value={film.showedDate.format(DATE_FORMAT)}
                                    onChange={this._onShowedDateChange}
                                />
                                {_.some(errors, e => e.extensions.code === "EmptyDate") 
                                ? (
                                    <Form.Text style={errorStyle}>Showed date should not be empty</Form.Text>
                                )
                                : null}
                        </Form.Group>

                        <Form.Group>
                            <Form.Label>Photo:</Form.Label>
                            <div style={{...photoContainerStyle, ...(!film.photo ? square : null)}}>
                                <Image src={ film.photo } rounded fluid/>
                                <div className="input-group" style={uploadPhotoButton}>
                                    <div className="custom-file">
                                        <input
                                        type="file"
                                        className="custom-file-input"
                                        id="inputGroupFile01"
                                        aria-describedby="inputGroupFileAddon01"
                                        onChange={this._onFileChange}
                                        />
                                        <label className="custom-file-label" htmlFor="inputGroupFile01">
                                            Choose file
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </Form.Group>
                        <Form.Group>
                            <Button variant="primary" type="submit" style={submitButtonStyle} disabled={isUploadingFile} block>Ok</Button>
                        </Form.Group>                                         
                            {!isAddingForm ? (
                                <Form.Group>
                                    <Button variant="danger" onClick={deleteFilm}>Delete</Button>
                                </Form.Group>       
                            ): null}
                        
                    </Form>
                    {!isAddingForm ? (
                        <div style={reviewsStyle}>
                            <Reviews id={film.id}/>
                        </div>
                    ): null}
                </div>
            )
        });
    }
}

export default withApollo(EditForm);

const containerStyle = {
    display: "flex",
    flexDirection: "column",
    padding: 20,
}

const fromStyle = {
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    margin: "0px auto",
    maxWidth: 512
}

const reviewsStyle = {
    display: "flex",
    flexDirection: "column",
    margin: "0px auto"
}

const errorStyle = {
    color: "red",
    fontSize: 12
}

const backButtonStyle = {
}

const submitButtonStyle = {
    maxWidth: 300
}

const photoContainerStyle = {
    position: "relative",
    width: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
}

const square = {
    paddingBottom: "100%",
    height: 0,
    border: "1px solid black",
    borderRadius: 5
}

const uploadPhotoButton = {
    position: "absolute",
    margin: "auto auto 5px auto",
    bottom: 0,
    left: 0,
    right: 0,
    maxWidth: 300
}