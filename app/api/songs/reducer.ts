import { Action, Reducer } from 'redux';
import { ACTIONS } from './actions';
import { Song } from '../../models/song';

export interface SongsState {
  songs: ReadonlyArray<Song>,
  currentlyPlayingSongId?: Readonly<string>,
  votableSongIds?: ReadonlyArray<string>,
};

export const defaultState: SongsState = {
  songs: [],
  currentlyPlayingSongId: undefined,
  votableSongIds: undefined,
};

export const reducer: Reducer<SongsState> = (state: SongsState = defaultState, action) => {
  let stateChanges: Partial<SongsState> = {};

  switch (action.type as ACTIONS) {
    case ACTIONS.START_SUBSCRIPTION: {
      stateChanges = defaultState;
      break;
    }
    
    case ACTIONS.SONGS_UPDATED: {
      stateChanges = {
        songs: [
          action.songs.currentlyPlaying,
          ...action.songs.votableSongs,
        ],
        currentlyPlayingSongId: action.songs.currentlyPlaying
          ? action.songs.currentlyPlaying.id
          : undefined,
        votableSongIds: action.songs.votableSongs.map(s => s.id),
      };
      break;
    }

    default: return state;
  }

  return { ...state, ...stateChanges };
};
