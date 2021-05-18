import {  getURL  }         from 'API/url.API';
import { arrayToCSV }       from '_utils';
import { authHeader }       from '_helpers';

import SignUpModel          from 'model/SignUpModel';
import LogInModel           from 'model/LogInModel';
import UserViewModel        from 'model/UserViewModel';
import SearchRequestModel   from 'model/SearchRequestModel';

const getUsers = () => {

    let headers= {
        ...authHeader()
    };

    let options ={
        headers
    };

    return fetch( getURL( 'api/users/Get' ), options );
}

const getUser = ( id ) => {

    let headers= {
        ...authHeader()
    };

    let options ={
        headers
    };
    
    return fetch( getURL( `api/users/Get/${id}` ), options );
};

/**
 * @param {SearchRequestModel} model 
 */
const getSearch = ( model ) =>{

    let headers= {
        ...authHeader()
    };

    let options ={
        headers
    };

    let url = new URL( getURL( 'api/users/GetSearch'));

    url.searchParams.set( 'SearchPosts', model.searchPosts );
    url.searchParams.set( 'SearchUsers', model.searchUsers );
    url.searchParams.set( 'Query', model.query );
    url.searchParams.set( 'Hashtags', arrayToCSV( model.hashtags ) );

    return fetch( url.href, options );
};

/**
 * @param {Number} id id del usuario en sesion
 */
const getFeed = ( id ) =>{
    let headers= {
        ...authHeader()
    };

    let options ={
        headers
    };

    return fetch( getURL( `api/users/GetFeed/${id}` ), options );
};

/**
 * @param {SignUpModel} model
 */
const createUser = async ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    };

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/security/CreateUser` ), options );
};

/**
 * @param {LogInModel} model
 */
const logIn = ( model ) => {

    const headers = {
        'Content-Type': 'application/json'
    };

    const options = {
        method: "POST",
        body: JSON.stringify( model ),
        headers: headers
    };

    return fetch( getURL( `api/security/Login` ), options );
};

/**
 * @param {Number} id 
 * @param {UserViewModel} model
 */
const updateUser = ( id, model ) =>{

    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    };

    const options = {
        method: "PUT",
        body: JSON.stringify( model ),
        headers
    };

    return fetch( getURL( `api/users/Update/${id}` ), options );
};

/**
 * @param   {Number} id
 */
const deleteUser = ( id ) =>{
    const headers = {
        'Content-Type': 'application/json',
        ...authHeader()
    };

    const options = {
        method: "DELETE",
        headers
    };
    
    return fetch( getURL( `api/users/Delete/${id}` ), options );
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