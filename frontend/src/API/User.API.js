import {  getURL, getURLWithParams  } from './url.API';

const getURL = ( subroute ) =>`${url}/${subroute}`;
const getURLWithParams = ( subroute, param ) =>`${url}/${subroute}/${param}`;

const getUsers = async () => (await fetch( getURL( 'users-get' ) )).json();

const searchUser = async ( query )=>{
    let res = await fetch( getURLWithParams( 'users-search', query ) );
    return res.json();
};

const getUser = async ( id )=>{
    let res = await fetch( getURLWithParams( 'users-get-one', id ) );
    return res.json();
};

const createUser = async ( user )=>{
    const options = {
        method: "POST",
        body: JSON.stringify( user )
    };

    let res = await fetch( getURL( 'users-create' ), options );
    return res.json();
};

const editUser = async ( id, user ) =>{
    const options = {
        method: "PUT",
        body: JSON.stringify( user )
    };

    let res = await fetch( getURLWithParams( 'users-edit', id ), options );
    return res.json();
};

const deleteUser = async ( id ) =>{
    const options = {
        method: "DELETE",
    };
    
    let res = await fetch( getURLWithParams( 'users-delete', id ), options );
    return res.json();
};

export{
    getUsers,
    searchUser,
    getUser,
    createUser,
    editUser,
    deleteUser
}