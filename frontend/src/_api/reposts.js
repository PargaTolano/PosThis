import {  getURL  }     from '_config';
import { authHeader }   from '_helpers';

import RepostViewModel  from '_model/RepostViewModel';


/**
 * @param   {Number} id
 */
const getReposts= async ( ) => {
    return fetch( getURL( `api/reposts/Get` ) );
};

/**
 * @param {RepostViewModel} model
 */
const createRepost = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    }

    const options = {
        method: 'POST',
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/reposts/Create` ), options );
};

/**
 * @param {CRepostModel} model
 */
 const deleteRepost = async ( model ) => {

    const headers = {
        ...authHeader()
    }

    const options = {
        method: 'DELETE',
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/reposts/Delete/` ), options );
};

export{
    getReposts,
    createRepost,
    deleteRepost
}