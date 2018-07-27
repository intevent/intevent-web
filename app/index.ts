import * as React from 'react';
import { render } from 'react-dom';

import { App, AppProps, configureStore } from './core';

const appProps: AppProps = {
  store: configureStore(),
};

if ('serviceWorker' in navigator) {
  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/js/sw.intevent.js');
  });
}

render(
  React.createElement(App, appProps, null),
  document.getElementById('app-root')
);
