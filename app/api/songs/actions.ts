import 'tslib';
import { Action, ActionCreator } from 'redux';
import { SongsUpdated } from './models';

export enum ACTIONS {
  START_SUBSCRIPTION = 'api/songs/start-subscription',
  SONGS_UPDATED = 'api/songs/songs-updated',
};

export interface StartSubscriptionAction extends Action {
  type: ACTIONS.START_SUBSCRIPTION,
};

export interface SongsUpdatedAction extends Action {
  type: ACTIONS.SONGS_UPDATED,
};

export const startSubscriptionAction: ActionCreator<StartSubscriptionAction> = () => ({
  type: ACTIONS.START_SUBSCRIPTION,
});

export const songsUpdatedAction: ActionCreator<SongsUpdatedAction> = (songs: Readonly<SongsUpdated>) => ({
  type: ACTIONS.SONGS_UPDATED,
  songs,
});
