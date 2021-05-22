import { useState, useEffect }  from 'react';
import { getSearch }            from '_api';
import { handleResponse }       from '_helpers';

import { SearchRequestModel }   from '_model';

export const useMakeSearch = ( query ) =>{

    const [ state, setState ] = 
        useState([
            false,
            null
        ]);

        useEffect( ()=>{
            getSearch(new SearchRequestModel({searchPosts: true, searchUsers: true, query, hashtags: []}))
                .then ( handleResponse )
                .then ( res => setState( [true, res.data] ) )
                .catch( err => setState( [true, err] ) );
        },[ query ] );

    return state;
};

export default useMakeSearch;