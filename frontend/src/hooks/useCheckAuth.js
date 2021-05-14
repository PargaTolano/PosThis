import React, { useState, useEffect }   from 'react';
import jwt_decode                       from 'jwt-decode';
import { validateToken }                from 'API/User.API';
import { authHeader }                   from '_helpers';
import { authTokenKey, routes }         from '_utils';
import { checkAuth }                    from '_helpers';


const useCheckAuth = ()=>{

    const [auth, setAuth] = useState( {
        isAuthenticated:    true,
        profilePicPath:     null,
        userId:             null
    });
    useEffect(()=>{
        checkAuth( setAuth );
    },[]);

    return [auth, setAuth];
};

export default useCheckAuth;