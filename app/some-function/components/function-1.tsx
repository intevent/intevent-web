import * as React from 'react';
import { Link } from 'react-router-dom';

export const Function1: React.SFC<{}> = (props: {}) => (
  <React.Fragment>
    <div>
      function 1
    </div>
    <div>
      <Link to="/">Home</Link>
    </div>
    <div>
      <Link to="/function-2">To Function 2</Link>
    </div>
  </React.Fragment>
);
