import React from 'react';
import { GoogleLogin } from 'react-google-login';
import { Mutation } from "react-apollo";
import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Typography } from "@material-ui/core";
import { signIn } from "../mutations";

const texts = {
    header: "Sign in",
    content: "Sign in with Google Account"
}

const GoogleSignIn = props => {
    const { onSignIn } = props;

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
            />                        
        </DialogActions>

        </Dialog>

    );
}

export default props => {
    return (
        <Mutation mutation={signIn}>{(signIn, { data }) => {
            if (data) return props.children;
            return <GoogleSignIn {...props} onSignIn={token => signIn({ variables: { token }})} />
        }}</Mutation>
    );
}