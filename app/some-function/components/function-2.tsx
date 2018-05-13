import * as React from 'react';
import { Link } from 'react-router-dom';

export const Function2: React.SFC<{}> = (props: {}) => (
  <React.Fragment>
    <div>
      function 2
    </div>
    <div>
      <Link to="/">Home</Link>
    </div>
    <div>
      <Link to="/function-1">To Function 1</Link>
    </div>
  </React.Fragment>
);
