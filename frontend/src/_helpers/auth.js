import { authenticationService }            from '_services';
import { handleResponse }                   from '_helpers';
import { authTokenKey }                     from '_utils';

const authHeader = ()=>{
    const currentUser = authenticationService.currentUserValue;
    if (currentUser && currentUser.token) {
        return { Authorization: `Bearer ${currentUser.token}` };
    } else {
        return {};
    }
}

export { authHeader };