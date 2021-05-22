import {  getURL  }     from '_config';
import { authHeader } from '_helpers';

import LikeViewModel from '_model/LikeViewModel';

/**
 * @param   {Number} id
 */
const getLikes= async ( ) => {
    let res = await fetch( getURL( `api/likes/Get` ) );
    return res.json();
};

/**
 * @param   {Number} id
 */
 const getLike = async ( id ) => {
    let res = await fetch( getURL( `api/likes/Get/${id}` ) );
    return res.json();
};

/**
 * @param {LikeViewModel} model
 */
const createLike = ( model ) => {

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    }

    const options = {
        method: 'POST',
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/likes/Create` ), options );;
};

/**
 * @param {LikeViewModel} model
 */
 const deleteLike = ( model ) => {

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    }

    const options = {
        method: 'DELETE',
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/likes/Delete` ), options );
};

export{
    getLikes,
    getLike,
    createLike,
    deleteLike
}