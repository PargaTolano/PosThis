import { BehaviorSubject }  from 'rxjs';
import { authTokenKey }     from '_utils';
import { handleResponse }   from '_helpers';

import { logIn }            from '_api/users';

const currentUserSubject = new BehaviorSubject( JSON.parse( localStorage.getItem( authTokenKey ) ) );

export const authenticationService = {
    login,
    logout,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login( username, password ) {

    let data = { username, password};

    return logIn(data)
        .then(handleResponse)
        .then(res => {
            let user = res.data;
            localStorage.setItem( authTokenKey, JSON.stringify( user ) );
            currentUserSubject.next( user );
            
            return user;
        });
}

function logout() {
    localStorage.removeItem( authTokenKey );
    currentUserSubject.next(null);
}