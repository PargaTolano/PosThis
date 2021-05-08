import {useState, useEffect} from 'react';

const useRequestLoadOnMount = ( request ) =>{

    const [ state, setState ] = 
        useState({
            ready: false, 
            response: null
        });

    useEffect(()=>{
        request()
        .then( res => setState( { ready: true, response: res} ) );
    }, []);

    return [state.ready, state.response];
};

export default useRequestLoadOnMount;