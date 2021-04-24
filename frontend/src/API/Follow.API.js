import {  getURL  } from 'API/url.API';

import FollowViewModel from 'model/FollowViewModel';

/**
 * @param   {Number} id
 */
const getFollowers= async ( id ) => {
    let res = await fetch( getURL( `api/follow/GetFollowers/${id}` ) );
    return res.json();
}

/**
 * @param   {Number} id
 */
const getFollowing = async ( id ) => {
    let res = await fetch( getURL( `api/follow/GetFollowing/${id}` ) );
    return res.json();
};

/**
 * @param   {Number} id
 */
const getFollowersCount = async ( id ) => {
    let res = await fetch( getURL( `api/follow/GetFollowersCount/${id}` ) );
    return res.json();
}

/**
 * @param   {Number} id
 */
const GetFollowingCount = async ( id ) => {
    let res = await fetch( getURL( `api/follow/GetFollowingCount/${id}` ) );
    return res.json();
};

/**
 * @param {FollowViewModel} model
 */
const createFollow = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/post/Create` ), options );
    return res.json();
};

/**
 * @param   {Number} id
 */
const deleteFollow = async ( id ) =>{
    const options = {
        method: "DELETE",
    };
    
    let res = await fetch( getURL( `api/post/Delete/${id}` ), options );
    return res.json();
};

export{
    getFollowers,
    getFollowing,
    getFollowersCount,
    GetFollowingCount,
    createFollow,
    deleteFollow
}