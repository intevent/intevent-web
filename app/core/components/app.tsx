import 'tslib';
import * as React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter, Switch } from 'react-router-dom';
import { Store } from 'redux';
import { Routes } from './routes';
import { AppState } from '../reducers/app';
import { ApolloProvider } from 'react-apollo';
import { ApolloClient } from 'apollo-client';
import { createHttpLink } from 'apollo-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';

export const link = createHttpLink({
  uri: 'http://localhost/graphql',
});

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
