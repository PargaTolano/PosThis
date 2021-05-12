import {  getURL  } from 'API/url.API';

import CUPostModel from 'model/CUPostModel';

const getPosts = async () => {
    const url = getURL( 'api/post/Get' );
    let res = await fetch( url );
    return res.json();
}

const getPost = async ( id ) => {
    let res = await fetch( getURL( `api/post/Get/${id}` ) );
    return res.json();
};

/**
 * @param {CUPostModel} model
 */
const createPost = async ( model ) => {

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