import 'tslib';
import * as React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter, Switch } from 'react-router-dom';
import { Store } from 'redux';
import { Routes } from './routes';
import { AppState } from '../reducers/app';
import { ApolloProvider } from 'react-apollo';
import { ApolloClient } from 'apollo-client';
import { createHttpLink, HttpLink } from 'apollo-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { split } from 'apollo-link';
import { WebSocketLink } from 'apollo-link-ws';
import { getMainDefinition } from 'apollo-utilities';

// Create an http link:
const httpLink = new HttpLink({
  uri: 'http://${window.location.host}/graphql',
});

// Create a WebSocket link:
const wsLink = new WebSocketLink({
  uri: `ws://${window.location.host}/graphql`,
  options: {
    reconnect: true,
  },
});

// using the ability to split links, you can send data to each link
// depending on what kind of operation is being sent
const link = split(
  // split based on operation type
  ({ query }) => {
    const { kind, operation } = getMainDefinition(query);
    return kind === 'OperationDefinition' && operation === 'subscription';
  },
  wsLink,
  httpLink
);

const client = new ApolloClient({
  cache: new InMemoryCache(),
  link,
});
export interface AppProps {
  store: Store<AppState>;
}

export class App extends React.Component<AppProps> {
  render(): JSX.Element {
    return (
      <ApolloProvider client={client}>
        <Provider store={this.props.store}>
          <BrowserRouter>
            <Switch>
              <Routes />
            </Switch>
          </BrowserRouter>
        </Provider>
      </ApolloProvider>
    );
  }
}
