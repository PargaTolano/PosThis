import React from 'react'
import {BrowserRouter as Router, Route} from 'react-router-dom';
import Feed from 'components/Feed';
import Login from 'components/Login';
import {makeStyles} from '@material-ui/core/styles'
import backapp from 'assets/backapp.png'

import useCheckAuth from 'hooks/useCheckAuth';

function App() {

  return (
    <div className= 'App' >
      <Router>
        <Route exact path="/Feed" component={Feed}/>
        <Route exact path="/" component={Login}/>
      </Router>
    </div>
  );
}

export default App;
