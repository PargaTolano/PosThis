import {useState, useEffect} from 'react';

import { getPost }           from '_api';
import { handleResponse }    from '_helpers';

export const useGetDetailedPost = ( id ) =>{
    
    const [ state, setState ] =  useState([false, null]);

    useEffect(()=>{
        getPost( id )
        .then ( handleResponse )
        .then ( res => setState( [ true, res.data ] ) )
        .catch( res => setState( [ true, null     ] ) );
    }, [id]);

    const setPost = ( post )=>{
        setState( [true, post ]);
    };

    const setReplies = ( replies )=>{
        setState( [true, {...state, replies }]);
    };

    return [ state, setPost, setReplies];
};

export default useGetDetailedPost;