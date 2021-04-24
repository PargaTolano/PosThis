import {  getURL  } from 'API/url.API';

import ReplyViewModel from 'model/ReplyViewModel';

/**
 * @param   {Number} id
 */
const getReplies= async ( ) => {
    let res = await fetch( getURL( `api/replies/Get` ) );
    return res.json();
};

/**
 * @param   {Number} id
 */
 const getReply = async ( id ) => {
    let res = await fetch( getURL( `api/replies/Get/${id}` ) );
    return res.json();
};

/**
 * @param {ReplyViewModel} model
 */
const createReply = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/replies/Create` ), options );
    return res.json();
};

/**
 * @param {ReplyViewModel} model
 */
 const updateLike = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/likes/Update` ), options );
    return res.json();
};

/**
 * @param {Number} id
 */
 const deleteLike = async ( id ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "DELETE",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/replies/Delete/${id}` ), options );
    return res.json();
};

export{
    getReplies,
    getReply,
    createReply,
    updateLike,
    deleteLike
}