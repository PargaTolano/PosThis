import {useState, useEffect} from 'react';

import { getPost }           from '_api';
import { handleResponse }    from '_helpers';

export const useGetDetailedPost = ( id ) =>{
    
    const [ state, setState ] =  useState([false, null]);

    useEffect(()=>{
        getPost( id )
        .then ( handleResponse )
        .then ( res =>{ setState( [ true, res.data ] ); console.warn(res); } )
        .catch( res =>{ setState( [ true, null     ] ); console.warn(res); });
    }, []);

    const setPost = ( post )=>{
        setState( [true, post ]);
    };

    return [ state, setPost ];
};

export default useGetDetailedPost;