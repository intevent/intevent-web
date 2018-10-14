import { VoteResult } from '../../models/vote-result';

export interface VotingResultsUpdated {
  canVote: Readonly<boolean>,
  votes: ReadonlyArray<VoteResult>,
};
