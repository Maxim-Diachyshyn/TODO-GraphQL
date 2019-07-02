import React, { Component } from 'react';
import { ApolloProvider } from 'react-apollo';
import { ApolloClient } from 'apollo-client';
import { split } from 'apollo-link';
import { HttpLink } from 'apollo-link-http';
import { WebSocketLink } from 'apollo-link-ws';
import { InMemoryCache } from 'apollo-boost';
import { getMainDefinition } from 'apollo-utilities';
import CssBaseline from '@material-ui/core/CssBaseline';
import AppRouter from '../appRouter';
import './App.css';
import { SubscriptionClient } from 'subscriptions-transport-ws';
import { components as SignIn } from "../SignIn";

// Create an http link:
const httpLink = new HttpLink({
  uri: 'http://localhost:5000/graphql'
});

const wsClient = new SubscriptionClient('ws://localhost:5000/graphql', { reconnect: true });

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

class App extends Component {
  render() {
    return (
      <ApolloProvider client={client}>
        <SignIn.GoogleSignIn>
          <div className="App">
            <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500" />
            <CssBaseline />
            <AppRouter/>
          </div>
        </SignIn.GoogleSignIn>
      </ApolloProvider>
    );
  }
}

export default App;
