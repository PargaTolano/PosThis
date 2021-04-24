import {  getURL  } from 'API/url.API';

import LikeViewModel from 'model/LikeViewModel';

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
const createLike = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/likes/Create` ), options );
    return res.json();
};

/**
 * @param {LikeViewModel} model
 */
 const deleteLike = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "DELETE",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/likes/Delete` ), options );
    return res.json();
};

export{
    getLikes,
    getLike,
    createLike,
    deleteLike
}