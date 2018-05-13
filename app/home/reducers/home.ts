import { Action, Reducer } from 'redux';
import { ACTIONS } from '../actions/home';

export interface HomeState {
};

export const defaultHomeState: HomeState = {
};

export const homeReducer: Reducer<HomeState> = (state: HomeState = defaultHomeState, action: Action) => {
  let stateChanges: Partial<HomeState> = {};

  switch (action.type as ACTIONS) {
    case ACTIONS.SOME_ACTION:
      stateChanges = {
      };
      break;
    default: return state;
  };

  return { ...state, ...stateChanges };
};