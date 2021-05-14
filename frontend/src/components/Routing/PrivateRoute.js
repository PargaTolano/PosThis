import React                    from 'react';
import { Route, Redirect }      from 'react-router-dom';
import { authTokenKey }         from '_utils';

const PrivateRoute = ({ component: Component, auth, ...rest }) => {
    return <Route {...rest} render={props => (
        auth.isAuthenticated
            ? <Component auth={auth} {...props} />
            : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
    )} />
};

export { PrivateRoute };