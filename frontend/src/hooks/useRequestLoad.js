import {useState} from 'react';

const useRequestLoad = ( request ) =>{

    const [ state, setState ] = 
        useState({
            ready: false, 
            response: null
        });

    request()
        .then( res => setState( {ready: true, response: res} ) );

    return state;
};

export default useRequestLoad;