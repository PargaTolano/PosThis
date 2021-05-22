import {  getURL  }         from '_config';
import { authHeader }       from '_helpers'
import { FollowViewModel }  from '_model';

/**
 * @param   {Number} id
 */
const getFollowers= async ( id ) => {

    const headers = {
        ...authHeader()
    }

    const options = {
        headers
    };

    return fetch( getURL( `api/follow/GetFollowers/${id}` ), options );
}

/**
 * @param   {Number} id
 */
const getFollowing = async ( id ) => {

    const headers = {
        ...authHeader()
    }

    const options = {
        headers
    };

    return fetch( getURL( `api/follow/GetFollowing/${id}` ), options );
};

/**
 * @param   {Number} id
 */
const getFollowersCount = async ( id ) => {

    const headers = {
        ...authHeader()
    }

    const options = {
        headers
    };

    return fetch( getURL( `api/follow/GetFollowersCount/${id}` ), options );
};

/**
 * @param   {Number} id
 */
const GetFollowingCount = async ( id ) => {

    const headers = {
        ...authHeader()
    }

    const options = {
        headers
    };

    return fetch( getURL( `api/follow/GetFollowingCount/${id}` ), options);
};

/**
 * @param {FollowViewModel} model
 */
const createFollow = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    }

    const options = {
        method: 'POST',
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/follow/Create` ), options );
};

/**
 * @param {FollowViewModel} model
 */
const deleteFollow = async ( model ) =>{

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    }

    const options = {
        method: 'DELETE',
        body: JSON.stringify( model ),
        headers
    };
    
    return fetch( getURL( `api/follow/Delete` ), options );
};

export{
    getFollowers,
    getFollowing,
    getFollowersCount,
    GetFollowingCount,
    createFollow,
    deleteFollow
}