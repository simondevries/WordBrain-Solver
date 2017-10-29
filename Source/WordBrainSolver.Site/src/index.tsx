import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {Router, Route, browserHistory} from 'react-router';

import {HomePage} from './app/homePage';

import './index.less';
import '../images/background.jpg';

ReactDOM.render(
  <Router history={browserHistory}>
    <Route path='/' component={HomePage}/>
  </Router>,
  document.getElementById('root')
);
