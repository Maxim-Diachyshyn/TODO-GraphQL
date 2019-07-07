import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { Mutation } from "react-apollo";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Typography } from "@material-ui/core";
import { signIn } from "../mutations";

const texts = {
    header: "Sign in",
    content: "Sign in with Google Account"
}

const googleKey = "todo-graph-ql:google";

const GoogleSignIn = props => {
    const { onSignIn } = props;
    
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
                onFailure={e => {throw e;}}
                cookiePolicy={'single_host_origin'}
            />                        
        </DialogActions>

        </Dialog>

    );
}

export default WrappedComponent => props => {
    return (
        <Mutation mutation={signIn}>{(signIn, { data, loading }) => {
            if (data || loading) return <WrappedComponent {...props} loading={loading}>{props.children}</WrappedComponent>
            return <GoogleSignIn {...props} loading={loading} onSignIn={token => {
                localStorage.setItem(googleKey, token);
                signIn({ variables: { token }})
            }} />
        }}</Mutation>
    );
}