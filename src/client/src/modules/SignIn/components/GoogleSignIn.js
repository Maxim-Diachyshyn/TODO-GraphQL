import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { Mutation } from "react-apollo";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Typography } from "@material-ui/core";
import { signIn } from "../mutations";
import { compose } from 'recompose';
import withError from '../../shared/withError';

const texts = {
    header: "Sign in",
    content: "Sign in with Google Account"
}

const googleKey = "todo-graph-ql:google";

const GoogleSignIn = props => {
    const { onSignIn, error } = props;
    
    if (error) {
        localStorage.removeItem(googleKey);
    }

    const googleToken = localStorage.getItem(googleKey);
    if (googleToken) {
        onSignIn(googleToken);
    }

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
                onFailure={e => localStorage.removeItem(googleKey)}
                cookiePolicy={'single_host_origin'}
            />                        
        </DialogActions>

        </Dialog>

    );
}

const withData = WrappedComponent => props => (
    <Mutation mutation={signIn}>{(signIn, { data, loading, error }) => (
        <WrappedComponent {...props} data={data} loading={loading} error={error} signIn={signIn}/>
    )}</Mutation>
);

export default compose(
    withData,
    withError,
    WrappedComponent => props => {
        const { loading, data, error, signIn } = props;
        if ((data || loading) && !error) return <WrappedComponent {...props} loading={loading}>{props.children}</WrappedComponent>
        return <GoogleSignIn {...props} loading={loading} error={error} onSignIn={token => {
            localStorage.setItem(googleKey, token);
            signIn({ variables: { token }})
        }} />
    }
);