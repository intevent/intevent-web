import * as React from 'react';
import { render } from 'react-dom';
import { applyMiddleware, combineReducers, createStore } from 'redux';

import { App, AppProps, configureStore } from './core';

const appProps: AppProps = {
  store: configureStore(),
};

render(
  React.createElement(App, appProps, null),
  document.getElementById('app-root')
);
