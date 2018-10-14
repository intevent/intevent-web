import 'tslib';
import { Action, ActionCreator } from 'redux';
import { VotingResultsUpdated } from './models';

export enum ACTIONS {
  START_SUBSCRIPTION = 'api/voting/start-subscription',
  VOTING_RESULTS_UPDATED = 'api/voting/voting-results-updated',
};

export interface StartSubscriptionAction extends Action {
  type: ACTIONS.START_SUBSCRIPTION,
};

export interface VotingResultsUpdatedAction extends Action {
  type: ACTIONS.VOTING_RESULTS_UPDATED,
};

export const startSubscriptionAction: ActionCreator<StartSubscriptionAction> = () => ({
  type: ACTIONS.START_SUBSCRIPTION,
});

export const votingResultsUpdatedAction: ActionCreator<VotingResultsUpdatedAction> = (voting: Readonly<VotingResultsUpdated>) => ({
  type: ACTIONS.VOTING_RESULTS_UPDATED,
  voting,
});
