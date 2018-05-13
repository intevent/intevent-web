import * as React from 'react';
import { Link } from 'react-router-dom';

export const Home: React.SFC = () => (
  <React.Fragment>
    <div>
      home
    </div>
    <div>
      <Link to="/function-1">To Function 1</Link>
    </div>
    <div>
      <Link to="/function-2">To Function 2</Link>
    </div>
  </React.Fragment>
);
