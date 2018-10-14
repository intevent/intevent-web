import { Action, Reducer } from 'redux';
import { ACTIONS } from './actions';
import { VoteResult } from '../../models/vote-result';

export interface VotingState {
  canVote: Readonly<boolean>,
  votes: ReadonlyArray<VoteResult>,
};

export const defaultState: VotingState = {
  canVote: false,
  votes: [],
};

export const reducer: Reducer<VotingState> = (state: VotingState = defaultState, action) => {
  let stateChanges: Partial<VotingState> = {};

  switch (action.type as ACTIONS) {
    case ACTIONS.START_SUBSCRIPTION: {
      stateChanges = defaultState;
      break;
    }

    case ACTIONS.VOTING_RESULTS_UPDATED: {
      stateChanges = {
        canVote: action.voting.canVote,
        votes: action.voting.votes,
      };
      break;
    }

    default: return state;
  }

  return { ...state, ...stateChanges };
};
