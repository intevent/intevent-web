import * as React from 'react';
import { Link } from 'react-router-dom';
import AppBar from '../../Components/Appbar';
import Card from '../../Components/Card';
import { Grid } from '@material-ui/core';

export const Home: React.SFC = () => (
  <React.Fragment>
    <AppBar />
    <Grid>
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
      <Card />
    </Grid>
  </React.Fragment>
);
