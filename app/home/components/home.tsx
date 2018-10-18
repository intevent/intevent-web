import * as React from 'react';
import { Link } from 'react-router-dom';
import AppBar from '../../Components/Appbar';
import Card from '../../Components/Card';
import { Grid } from '@material-ui/core';
import gql from 'graphql-tag';
import { Subscription, Query, Mutation } from 'react-apollo';

const GET_NEW_SONGS = gql`
  subscription {
    songsUpdated {
      votableSongs {
        id
        artist
        title
        albumArtUrl
      }
    }
  }
`;

const GET_SONGS = gql`
  query {
    songs {
      id
      artist
      title
      albumArtUrl
      duration
    }
  }
`;

const GET_VOTES = gql`
  subscription {
    votingResultsUpdated {
      canVote
      votes {
        songId
        votes
      }
    }
  }
`;

const GET_NEW_VOTES = gql`
  subscription {
    votingResultsUpdated {
      canVote
      votes {
        songId
        votes
      }
    }
  }
`;
const ADD_VOTE = gql`
  mutation AddVote($voterId: String!, $songId: String!) {
    addVote(vote: { voterId: $voterId, songId: $songId }) {
      songId
      voterId
    }
  }
`;

class Guid {
  static newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = (Math.random() * 16) | 0,
        v = c == 'x' ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  }
}
const id = Guid.newGuid();
const SongsComponent = (songs, votes = new Map()) =>
  songs.map(song => (
    <Mutation mutation={ADD_VOTE} key={song.id}>
      {(updateVote, { data }) => (
        <Card
          title={song.title}
          artist={song.artist}
          votes={votes.get(song.id) || 0}
          albumArt={song.albumArtUrl}
          voteClick={() => {
            console.log('Voted');
            updateVote({ variables: { voterId: id, songId: song.id } });
          }}
        >
          {console.log(votes, votes.get(song.id))}
        </Card>
      )}
    </Mutation>
  ));

const Songs = () => (
  <Subscription subscription={GET_NEW_SONGS}>
    {({ data, loading }) => (
      <div>
        {!loading && console.log(data)}
        {!loading && data.songsUpdated && Votes(data.songsUpdated.votableSongs)}
      </div>
    )}
  </Subscription>
);
const Votes = songs => (
  <Subscription subscription={GET_NEW_VOTES}>
    {({ data, loading }) => (
      <div>
        {!loading && console.log(data)}
        {!loading &&
          data.votingResultsUpdated &&
          SongsComponent(songs, new Map(data.votingResultsUpdated.votes))}
      </div>
    )}
  </Subscription>
);
export class Home extends React.Component<any, any> {
  constructor(props: any) {
    super(props);
    this.state = {};
  }

  public render(): React.ReactNode {
    return (
      <React.Fragment>
        <AppBar />
        <Grid>
          <Songs />
        </Grid>
      </React.Fragment>
    );
  }
}
