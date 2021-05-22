import {useState, useEffect} from 'react';

import { getUser }           from '_api';
import { handleResponse }    from '_helpers';

export const useGetUserProfile = ( id ) =>{

    const [ state, setState ] =  useState([false, null]);

    useEffect(()=>{
        console.log( id );
        getUser( id )
        .then ( handleResponse )
        .then ( res =>{ setState( [ true, res.data ]); console.log(res.data); } )
        .catch( res => setState( [ true, null     ] ) );
    }, [id]);

    const setUser = ( user )=>{
        setState( [true, user ]);
    };

    return [ state, setUser ];
};

export default useGetUserProfile;