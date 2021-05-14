import React, { useState, useEffect } from 'react';
import useRequestLoadOnMount from 'hooks/useRequestLoadOnMount';
import jwt_decode from "jwt-decode";

import { logIn } from 'API/User.API';

/**
 * @param {LogInModel} model
 */
const useLogin = ( model )=>{

    const [ ready , response ] = useRequestLoadOnMount( () => logIn(model) );

    if (response?.code === 200){
        const decoded = jwt_decode( response.data );
        console.log( response.data, decoded );
        return [ ready, decoded ];
    }

    return [ready , null];
};

export default useLogin;