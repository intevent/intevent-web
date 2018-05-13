import { combineReducers, Reducer } from 'redux';
import { reducer as formReducer } from 'redux-form';
import { defaultHomeState, HomeState, homeReducer } from '../../home';

export interface AppState {
  home: HomeState;
};

export const defaultAppState: AppState = {
  home: defaultHomeState,
};

export const appReducer: Reducer<AppState> = combineReducers<AppState>({
  //form: formReducer,
  home: homeReducer,
});
