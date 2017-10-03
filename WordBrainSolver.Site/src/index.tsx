import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {Router, Route, browserHistory} from 'react-router';

import {Hello} from './app/hello';

import './index.less';

ReactDOM.render(
  <Router history={browserHistory}>
    <Route path='/' component={Hello}/>
  </Router>,
  document.getElementById('root')
);
