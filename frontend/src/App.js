import React, { useState, useEffect }                         from 'react';
import { Router, Route, Switch }  from 'react-router-dom';

import Feed                                       from 'components/Feed';
import Login                                      from 'components/Login';
import DetailPost                                 from 'components/DetailPost';
import SearchResult                               from 'components/SearchResult';
import Profile                                    from 'components/Profile';
import NotFound                                   from 'components/NotFound';  
import { PrivateRoute }                           from 'components/Routing';

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
      <Router history={history}>
        <Switch>
          <PrivateRoute exact path={routes.feed}          component={Feed}            {...temp}   />
          <PrivateRoute exact path={routes.postDetail}    component={DetailPost}      {...temp}   />
          <PrivateRoute       path={routes.searchResult}  component={SearchResult}    {...temp}   />
          <PrivateRoute exact path={routes.profile}       component={Profile}         {...temp}   />
          <Route        exact path={routes.login}         component={Login}           {...temp}   />
          <Route        exact path={'*'}                  component={NotFound}        {...temp}   />
        </Switch>
      </Router>
    </div>
  );
}

export default App;
