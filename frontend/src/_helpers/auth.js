import { createUser }                       from 'API/User.API';
import { authenticationService }            from '_services';
import { handleResponse }                   from '_helpers';
import { authTokenKey }                     from '_utils';

/**
 * @param {SignUpModel} model 
 * @returns
 */
const signupHelper = ( model ) => {
    createUser(model)
        .then( handleResponse )
        .then( data =>{

        });
};

const authHeader = ()=>{
    const currentUser = authenticationService.currentUserValue;
    if (currentUser && currentUser.token) {
        return { Authorization: `Bearer ${currentUser.token}` };
    } else {
        return {};
    }
}

export { signupHelper, authHeader };