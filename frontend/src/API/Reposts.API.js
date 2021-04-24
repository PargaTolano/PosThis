import {  getURL  } from 'API/url.API';

import CRepostModel from 'model/CRepostModel';

/**
 * @param   {Number} id
 */
const getReposts= async ( ) => {
    let res = await fetch( getURL( `api/reposts/Get` ) );
    return res.json();
};

/**
 * @param {CRepostModel} model
 */
const createRepost = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/reposts/Create` ), options );
    return res.json();
};

/**
 * @param {Number} id
 */
 const deleteRepost = async ( id ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "DELETE",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/reposts/Delete/${id}` ), options );
    return res.json();
};

export{
    getLikes,
    getLike,
    createLike,
    deleteLike
}