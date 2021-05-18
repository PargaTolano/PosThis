import { useState, useEffect }  from 'react';
import { getSearch }            from 'API/User.API';
import { handleResponse }       from '_helpers';

import SearchRequestModel from 'model/SearchRequestModel';

export const useMakeSearch = ( query ) =>{

    const [ state, setState ] = 
        useState([
            false,
            null
        ]);

        useEffect( ()=>{
            getSearch(new SearchRequestModel({searchPosts: true, searchUsers: true, query, hashtags: []}))
                .then ( handleResponse )
                .then ( res =>{setState( [true, res.data] ); console.log( res.data );} )
                .catch( err => setState( [true, err] ) );
        },[ query ] );

    return state;
};

export default useMakeSearch;