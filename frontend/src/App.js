import React from 'react'
import {BrowserRouter as Router, Route} from 'react-router-dom';
import Feed from 'components/Feed';
import Login from 'components/Login';
import DetailPost from 'components/DetailPost';
import Searchresult from 'components/searchResult';
import {makeStyles} from '@material-ui/core/styles'
import backapp from 'assets/backapp.png'

import useCheckAuth from 'hooks/useCheckAuth';

function App() {

  return (
    <div className= 'App' >
      <Router>
        <Route exact path='/feed' component={Feed}/>
        <Route exact path='/detailpost' component={DetailPost}/>
        <Route exact path='/searchresult' component={Searchresult}/>
        <Route exact path='/' component={Login}/>
      </Router>
    </div>
  );
}

export default App;
