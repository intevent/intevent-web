import * as React from 'react';
import { Route } from 'react-router';
import { App } from './app';
import { Home } from '../../home';
import { Function1, Function2 } from '../../some-function';

export const Routes: React.SFC = () => (
  <React.Fragment>
    <Route exact path="/" component={Home} />
    <Route exact path="/function-1" component={Function1} />
    <Route exact path="/function-2" component={Function2} />
  </React.Fragment>
);
