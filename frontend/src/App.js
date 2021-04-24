import React from 'react'
import {BrowserRouter as Router, Route} from 'react-router-dom';

import Feed from 'components/Feed';
import Login from 'components/Login';

import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <Router>
        <Route exact path="/" component={Feed}/>
        <Route exact path="/login" component={Login}/>
      </Router>
    </div>
  );
}

export default App;
