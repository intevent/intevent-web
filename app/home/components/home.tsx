import * as React from 'react';
import { Link } from 'react-router-dom';
import AppBar from '../../Components/Appbar';
import Card from '../../Components/Card';
import { Grid } from '@material-ui/core';
import gql from 'graphql-tag';
import { Query } from 'react-apollo';

const GET_SONGS = gql`
  query {
    songs {
      id
      title
      artist
    }
    votingResults {
      votes {
        votes
        songId
      }
    }
  }
`;

export const Home: React.SFC = () => (
  <React.Fragment>
    <AppBar />
    <Grid>
      <Query query={GET_SONGS}>
        {({ loading, error, data }) => {
          if (loading) return 'Loading...';
          if (error) return `Error! ${error.message}`;

          return (
            <div>
              {data.songs.map(song => (
                <Card title={song.title} artist={song.artist} votes={'0'} />
              ))}
            </div>
          );
        }}
      </Query>
    </Grid>
  </React.Fragment>
);
