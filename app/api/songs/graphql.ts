import { execute, GraphQLRequest } from 'apollo-link';
import gql from 'graphql-tag';
import { webSocketLink } from '../server-link';

const query: string = `
subscription SongsUpdated {
  songsUpdated {
    currentlyPlaying {
      id,
      artist,
      title,
      duration
    },
    votableSongs {
      id,
      artist,
      title,
      duration
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

export const songsUpdatedObservable = () => execute(webSocketLink, request);
