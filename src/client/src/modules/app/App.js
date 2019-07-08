import React from 'react';
import { ApolloProvider } from 'react-apollo';
import { ApolloClient } from 'apollo-client';
import { split } from 'apollo-link';
import { HttpLink } from 'apollo-link-http';
import { WebSocketLink } from 'apollo-link-ws';
import { InMemoryCache } from 'apollo-boost';
import { getMainDefinition } from 'apollo-utilities';
import CssBaseline from '@material-ui/core/CssBaseline';
import { createMuiTheme } from "@material-ui/core/styles";
import { ThemeProvider, StylesProvider } from "@material-ui/styles";
import AppRouter from '../appRouter';
import { SubscriptionClient } from 'subscriptions-transport-ws';
import { components as SignIn } from "../SignIn";
import './App.css';
import { compose } from 'recompose';
import withLoader from '../shared/withLoader';

// Create an http link:
const httpLink = new HttpLink({
  uri: process.env.REACT_APP_API_HTTP
});

const wsClient = new SubscriptionClient(process.env.REACT_APP_API_WS, { reconnect: true });

// Create a WebSocket link:
const wsLink = new WebSocketLink(wsClient);

// using the ability to split links, you can send data to each link
// depending on what kind of operation is being sent
const link = split(
  // split based on operation type
  ({ query }) => {
    const { kind, operation } = getMainDefinition(query);
    return kind === 'OperationDefinition' && operation === 'subscription';
  },
  wsLink,
  httpLink,
);

const cache = new InMemoryCache({
  cacheRedirects: {
    Query: {
      todo: (_, args, { getCacheKey }) =>
        getCacheKey({ __typename: 'TodoType', id: args.id })
    },
  },
});

const client = new ApolloClient({
  link,
  cache
});

wsClient.onReconnecting(client.resetStore);

const withApollo = WrappedComponent => props => (
  <ApolloProvider client={client}>
    <WrappedComponent {...props}>{props.children}</WrappedComponent>
  </ApolloProvider>
);

const theme = createMuiTheme({
  shape: {
    borderRadius: 6
  },
});

const withStyles = WrappedComponent => props => (
  <ThemeProvider theme={theme}>
    <WrappedComponent {...props}>{props.children}</WrappedComponent>
  </ThemeProvider>
);

const App = () => (
  <div className="App">
    <CssBaseline />
    <AppRouter/>
  </div>
);

export default compose(
  withApollo,
  withStyles,
  withLoader
)(App);
