import {  getURL  }     from '_config';

import HashtagViewModel from 'model/HashtagViewModel';

/**
 * @param   {Number} id
 */
const getHashtags= async ( ) => {
    let res = await fetch( getURL( `api/hashtags/Get` ) );
    return res.json();
};

/**
 * @param   {Number} id
 */
 const getPostsWithHashtag = async ( text ) => {
    let res = await fetch( getURL( `api/hashtags/GetPosts/${text}` ) );
    return res.json();
};

/**
 * @param {HashtagViewModel} model
 */
const createHashtag = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: 'POST',
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/hashtags/Create` ), options );
    return res.json();
};

export{
    getHashtags,
    getPostsWithHashtag,
    createHashtag
}