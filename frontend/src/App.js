import React, { useState, useEffect }                         from 'react';
import { BrowserRouter as Router, Route, Switch }  from 'react-router-dom';

import { 
  Feed, 
  Login, 
  NotFound, 
  PostDetail, 
  ProfileDetail, 
  SearchResult 
} from 'components';

import { PrivateRoute, PublicRoute }              from 'components/Routing';

import { routes }                                 from '_utils';
import { history }                                from '_helpers';
import { authenticationService }                  from '_services';

function App() {

  const [ user, setUser ] = useState(null);

  useEffect(()=>{
    authenticationService
              .currentUser
              .subscribe(x=>setUser(x));
  },[]);
  
  const temp = { history, user };

  return (
    <div className= 'App'>
      <Router>
        <Switch>
          <PrivateRoute exact path={routes.feed}          component={Feed}            {...temp}   />
          <PrivateRoute exact path={routes.postDetail}    component={PostDetail}      {...temp}   />
          <PrivateRoute       path={routes.searchResult}  component={SearchResult}    {...temp}   />
          <PrivateRoute exact path={routes.profile}       component={ProfileDetail}   {...temp}   />
          <PublicRoute  exact path={routes.login}         component={Login}           {...temp}   />
          <Route        exact path={'*'}                  component={NotFound}        {...temp}   />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
