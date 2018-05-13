import 'tslib';
import * as React from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter, Switch } from 'react-router-dom';
import { Store } from 'redux';
import { Routes } from './routes';
import { AppState } from '../reducers/app';

export interface AppProps {
  store: Store<AppState>;
};

export class App extends React.Component<AppProps> {
  render(): JSX.Element { return (
    <Provider store={this.props.store}>
      <BrowserRouter>
        <Switch>
          <Routes />
        </Switch>
      </BrowserRouter>
    </Provider>
  )};
};
