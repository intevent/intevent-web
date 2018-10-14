import { applyMiddleware, compose, createStore, Store } from 'redux';
import createSagaMiddleware, { SagaMiddleware } from 'redux-saga';
import { appReducer, AppState, defaultAppState } from '../reducers/app';
import { helloSaga } from '../sagas/app';
import { performBeginSongSubscription, performBeginVotingSubscription } from '../../api/sagas';

export const configureStore = (defaultState: AppState = defaultAppState): Store<AppState> => {
  const sagaMiddleware = createSagaMiddleware();

  // TODO: Add createStore generic parameters
  const store: Store<AppState> = createStore(
    appReducer,
    defaultState,
    (window['__REDUX_DEVTOOLS_EXTENSION_COMPOSE__'] || compose)(
      applyMiddleware(sagaMiddleware)
    ),
  );

  sagaMiddleware.run(helloSaga);
  sagaMiddleware.run(performBeginSongSubscription);
  sagaMiddleware.run(performBeginVotingSubscription);
  return store;
};
