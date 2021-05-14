import { logIn, createUser, validateToken } from 'API/User.API';
import { authTokenKey }                     from '_utils';
import LoginModel                           from 'model/LogInModel';
import jwt_decode                           from 'jwt-decode';

const checkAuth = async ( setAuth )=>{
    let token = localStorage.getItem( authTokenKey );
    if( token === null ){
        setAuth({
            userId          : null,
            profilePicPath  : null,
            isAuthenticated : false
        });
        return false;
    }

    let { nameid } = jwt_decode(token);

    try {
        let response = await validateToken( nameid );
        
        if (response.code === 200){
            let { data } = response;
            let { id, profilePicPath } = data;
            setAuth({
                userId          : id,
                profilePicPath  : profilePicPath,
                isAuthenticated : true
            });
            return true;
        }
    } catch (error) {
        setAuth({
            userId          : null,
            profilePicPath  : null,
            isAuthenticated : false
        });
        return false;
    }
};

/**
 * @param {LoginModel} model 
 * @returns
 */
const loginHelper = async ( model )=>{
    let res = await logIn(model);
    const {code, message} = res;

    if( code !== 200 ){
        return {
            code,
            message
        };
    }

    localStorage.setItem( authTokenKey, res.data );
    window.location.href = '/';

    return null;
};

/**
 * @param {SignUpModel} model 
 * @returns
 */
 const signupHelper = async( model )=>{
    let res = await createUser(model);
    const {code, message} = res;

    if( code !== 200 ){
        return {
            code,
            message
        };
    }

    return null;
};

const authHeader = ()=>{
    let token = localStorage.getItem(authTokenKey);

    if (token) {
        return { 'Authorization': `Bearer ${token}` };
    } else {
        return {};
    }
}



export { checkAuth, loginHelper, signupHelper, authHeader };