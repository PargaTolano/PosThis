import React, {useEffect}               from 'react'
import {BrowserRouter as Router, Route} from 'react-router-dom';
import Feed                             from 'components/Feed';
import Login                            from 'components/Login';
import DetailPost                       from 'components/DetailPost';
import SearchResult                     from 'components/SearchResult';
import Profile                          from 'components/Profile';
import {makeStyles}                     from '@material-ui/core/styles';
import backapp                          from 'assets/backapp.png'
import { PrivateRoute }                 from 'components/Routing';
import useCheckAuth                     from 'hooks/useCheckAuth';
import { routes, authTokenKey }         from '_utils';

function App() {

  const [auth, setAuth] = useCheckAuth();

  const temp = {auth, setAuth};

  return (
    <div className= 'App'>
      <Router>
        <PrivateRoute exact path={routes.feed}          component={Feed}            {...temp}  />
        <PrivateRoute exact path={routes.postDetail}    component={DetailPost}      {...temp}  />
        <PrivateRoute exact path={routes.searchResult}  component={SearchResult}    {...temp}  />
        <PrivateRoute exact path={routes.profile}       component={Profile}         {...temp}  />
        <Route        exact path={routes.login}         component={Login}           {...temp}  />
      </Router>
    </div>
  );
}

export default App;
