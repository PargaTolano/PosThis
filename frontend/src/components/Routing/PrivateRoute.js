import React                    from 'react';
import { Route, Redirect }      from 'react-router-dom';
import {  routes }              from '_utils';

const PrivateRoute = ({ component: Component, user, ...rest }) => {
    return <Route {...rest} render={props => (
        user
            ? <Component {...props} />
            : <Redirect to={{ pathname: routes.login, state: { from: props.location } }} />
    )} />
};

export { PrivateRoute };