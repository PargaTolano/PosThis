import {  getURL  }     from 'API/url.API';

import CPostModel      from 'model/CPostModel';
import { authHeader }   from '_helpers';

const getPosts = async () => {
    const url = getURL( 'api/post/Get' );
    return fetch( url );
}

const getPost = async ( id ) => {
    return fetch( getURL( `api/post/Get/${id}` ) );
};

/**
 * @param {CPostModel} model
 */
const createPost = async ( model ) => {

    const headers = {
        ...authHeader()
    };

    let fd = new FormData();
    
    fd.append( 'userID',  model.userID  );
    fd.append( 'Content', model.content );
    for( let file of model.files ){
        fd.append('Files', file );
    };

    const options = {
        method: "POST",
        body: fd,
        headers
    };

    return fetch( getURL( `api/post/Create` ), options );
};

/**
 * @param {Number} id 
 * @param {CUPostModel} model
 */
const updatePost = async ( id, model ) =>{

    const headers = {
        'Content-Type': 'application/json'
    }

    const options = {
        method: "PUT",
        body: JSON.stringify( model ),
        headers: headers
    };

    let res = await fetch( getURL( `api/post/Update/${id}` ), options );
    return res.json();
};

/**
 * @param   {Number} id
 */
const deletePost = async ( id ) =>{
    const options = {
        method: "DELETE",
    };
    
    let res = await fetch( getURL( `api/post/Delete/${id}` ), options );
    return res.json();
};

export{
    getPosts,
    getPost,
    createPost,
    updatePost,
    deletePost
}