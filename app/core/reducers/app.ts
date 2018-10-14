import { combineReducers, Reducer } from 'redux';
import { reducer as formReducer } from 'redux-form';
import {
  SongsState,
  songsDefaultState,
  songsReducer,
  VotingState,
  votingDefaultState,
  votingReducer,
} from '../../api/reducers';
import { defaultHomeState, HomeState, homeReducer } from '../../home';

export interface AppState {
  home: HomeState;
  songs: SongsState;
  voting: VotingState;
};

export const defaultAppState: AppState = {
  home: defaultHomeState,
  songs: songsDefaultState,
  voting: votingDefaultState,
};

export const appReducer: Reducer<AppState> = combineReducers<AppState>({
  //form: formReducer,
  home: homeReducer,
  songs: songsReducer,
  voting: votingReducer,
});
