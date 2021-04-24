import {  getURL  } from 'API/url.API';
import { arrayToCSV } from 'utils/utils';

import SignUpModel from 'model/SignUpModel';
import LogInModel from 'model/LogInModel';
import UserViewModel from 'model/UserViewModel';
import SearchRequestModel from 'model/SearchRequestModel'

const getUsers = async () => {
    let res = await fetch( getURL( 'api/users/Get' ) );
    return res.json();
}

const getUser = async ( id ) => {
    let res = await fetch( getURL( `api/users/Get/${id}` ) );
    return res.json();
};

/**
 * @param {SearchRequestModel} model 
 */
const getSearch = async( model ) =>{

    let url = new URL( getURL( 'api/users/GetSearch'));

    url.searchParams.set( 'SearchPosts', model.searchPosts );
    url.searchParams.set( 'SearchUsers', model.searchUsers );
    url.searchParams.set( 'Query', model.query );
    url.searchParams.set( 'SearchPosts', arrayToCSV( model.hashtags ) );

    let res = await fetch( url.href );
    return res.json();
};

/**
 * @param {Number} id id del usuario en sesion
 */
const getFeed = async ( id ) =>{
    let res = await fetch( getURL( `api/users/GetFeed/${id}` ) ); 
    return res.json();
};

/**
 * @param {SignUpModel} model
 */
const createUser = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/security/CreateUser` ), options );
    return res.json();
};

/**
 * @param {LogInModel} model
 */
const logIn = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/security/Login` ), options );
    return res.json();
};

/**
 * @param {Number} id 
 * @param {UserViewModel} model
 */
const updateUser = async ( id, model ) =>{

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "PUT",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/users/Update/${id}` ), options );
    return res.json();
};

/**
 * @param   {Number} id
 */
const deleteUser = async ( id ) =>{
    const options = {
        method: "DELETE",
    };
    
    let res = await fetch( getURL( `api/users/Delete/${id}` ), options );
    return res.json();
};

export{
    getUsers,
    getUser,
    getSearch,
    getFeed,
    createUser,
    logIn,
    updateUser,
    deleteUser
}