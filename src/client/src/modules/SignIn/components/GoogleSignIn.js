import React from 'react';
import _ from "lodash";
import { GoogleLogin } from 'react-google-login';
import { Mutation, Query, withApollo } from "react-apollo";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Typography } from "@material-ui/core";
import { signIn } from "../mutations";
import { compose } from 'recompose';
import withError from '../../shared/withError';
import { currentUserQuery } from '../../Board/queries';
import withLoader from '../../shared/withLoader';

const texts = {
    header: "Sign in",
    content: "Sign in with Google Account"
}

const googleKey = "todo-graph-ql:google";

const GoogleSignIn = props => {
    const { onSignIn, onError } = props;

    return (
        <Dialog open={true}>
        <DialogTitle disableTypography={false}>{texts.header}</DialogTitle>
        <DialogContent>
            <Typography variant="subtitle1">{texts.content}</Typography>
        </DialogContent>
        <DialogActions>
            <GoogleLogin
                clientId={process.env.REACT_APP_GOOGLE_ID}
                buttonText="Login with Google"
                onSuccess={r => onSignIn(r.tokenId)}
                onFailure={onError}
                cookiePolicy={'single_host_origin'}
            />                        
        </DialogActions>

        </Dialog>

    );
}

const withData = WrappedComponent => props => (
    <Query query={currentUserQuery} fetchPolicy="cache-only">{({ data }) => (
        <Mutation 
            mutation={signIn} 
            update={(cache, { data: { signIn } }) => {
                cache.writeQuery({
                    query: currentUserQuery,
                    data: { 
                        currentUser: {
                            ...signIn,
                            logoutRequested: false
                        }  
                    },
                });
            }}
            >{(signIn, { loading, error }) => (
            <WrappedComponent {...props} data={_.get(data, "currentUser", null)} loading={loading} error={error} signIn={signIn}/>
        )}</Mutation>
    )}</Query>
);

export default compose(
    withApollo,
    withData,
    withError,
    withLoader,
    WrappedComponent => props => {
        const { loading, data, error, signIn, client } = props;

        const onSignIn = token => {
            localStorage.setItem(googleKey, token);
            signIn({ variables: { token }})
        };

        const onError = () => localStorage.removeItem(googleKey);

        if (_.get(data, "logoutRequested", false)) {
            localStorage.removeItem(googleKey);
            client.writeQuery({
                query: currentUserQuery,
                data: {
                    currentUser: null
                }
            });
            client.resetStore();
            return <GoogleSignIn {...props} onSignIn={onSignIn} onError={onError} />
        }
       
        if ((data || loading) && !error) return <WrappedComponent {...props}>{props.children}</WrappedComponent>
        else if (!loading) {
            const googleToken = localStorage.getItem(googleKey);
            if (googleToken) {
                onSignIn(googleToken);
            }
            if (error) {
                localStorage.removeItem(googleKey);
            }
        }
        return <GoogleSignIn {...props} onSignIn={onSignIn} onError={onError} />
    }
);