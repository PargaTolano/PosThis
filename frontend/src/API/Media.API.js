import {  getURL  } from 'API/url.API';

import MediaViewModel from 'model/MediaViewModel';

/**
 * @param   {Number} id
 */
const getMedias = async ( ) => {
    let res = await fetch( getURL( `api/media/Get` ) );
    return res.json();
};

/**
 * @param {Number} id
 */
const getMediaUrl = async ( id ) => getURL( `api/media/Get/${id}` );

/**
 * @param {Number} id
 */
const getMediaBlob = async ( id ) => {
    let res = await fetch( getMediaUrl(id) );
    return res.blob();
};

/**
 * @param {MediaViewModel} model
 */
const createMedia = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/media/Create` ), options );
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

    let res = await fetch( getURL( `api/media/Delete/${id}` ), options );
    return res.json();
};

export{
    getMedias,
    getMediaUrl,
    getMediaBlob,
    createMedia,
    deleteRepost
}