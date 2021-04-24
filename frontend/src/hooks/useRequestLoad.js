import {useState} from 'react';

const useRequestLoad = ( request ) =>{

    const [ state, setState ] = 
        useState({
            ready: false, 
            data: null
        });

    request()
        .then( res => setState( res ) );

    return state;
};

export default useRequestLoad;