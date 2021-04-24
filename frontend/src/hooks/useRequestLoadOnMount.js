import {useState, useEffect} from 'react';

const useRequestLoadOnMount = ( request ) =>{

    const [ state, setState ] = 
        useState({
            ready: false, 
            data: null
        });

    useEffect(()=>{
        request()
        .then( res => setState( { ready: true, data: res} ) );
    }, []);

    return state;
};

export default useRequestLoadOnMount;