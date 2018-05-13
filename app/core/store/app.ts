import { Action, applyMiddleware, compose, createStore, Store } from 'redux';
import createSagaMiddleware, { SagaMiddleware } from 'redux-saga';
import { appReducer, AppState, defaultAppState } from '../reducers/app';
// import { appSaga } from '../sagas/app';

export const configureStore = (defaultState: AppState = defaultAppState): Store<AppState> => {
  const sagaMiddleware = createSagaMiddleware();

  // TODO: Add createStore generic parameters
  const store: Store<AppState> = createStore(
    appReducer,
    defaultState,
    compose(
      applyMiddleware(sagaMiddleware)
    ),
  );

  // sagaMiddleware.run(appSaga);
  return store;
};
