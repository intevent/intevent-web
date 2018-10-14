import { execute, GraphQLRequest } from 'apollo-link';
import gql from 'graphql-tag';
import { webSocketLink } from '../server-link';

const query: string = `
subscription VotingResultsUpdated {
  votingResultsUpdated {
    canVote,
    votes {
      songId,
      votes
    }
  }
}
`;

const request: GraphQLRequest = {
  //query: gql`query { hello }`,
  query,
  variables: {}, //optional
  //operationName: '{}', //optional
  //context: {}, //optional
  //extensions: {}, //optional
};

export const votingResultsUpdatedObservable = () => execute(webSocketLink, request);
