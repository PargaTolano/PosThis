import {  getURL  }              from '_config';
import { authHeader }            from '_helpers';
import { authenticationService } from '_services';

const getPosts = async () => {
    const url = getURL( 'api/post/Get' );
    return fetch( url );
}

const getPost = async ( id ) => {

    const headers = {
        ...authHeader(),
        'UserID': authenticationService.currentUserValue.id
    }

    const options = { 
        headers
    };

    return fetch( getURL( `api/post/Get/${id}` ), options );
};

/**
 * @param {CPostModel} model
 */
const createPost = ( model ) => {

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
        method: 'POST',
        body: fd,
        headers
    };

    return fetch( getURL( `api/post/Create` ), options );
};

/**
 * @param {Number} id 
 * @param {Object} model
 */
const updatePost = ( id, model ) =>{

    const headers = {
        ...authHeader()
    };

    let fd = new FormData();

    fd.append('Content', model.content);

    for( let id of model.deleted){
        fd.append('Deleted', id);
    }

    for( let file of model.files){
        fd.append('Files', file);   
    }

    const options = {
        method: 'PUT',
        body: fd,
        headers: headers
    };

    return fetch( getURL( `api/post/Update/${id}` ), options );
};

/**
 * @param   {Number} id
 */
const deletePost = async ( id ) =>{
    const options = {
        method: 'DELETE',
        headers: authHeader()
    };
    
    return fetch( getURL( `api/post/Delete/${id}` ), options );
};

export{
    getPosts,
    getPost,
    createPost,
    updatePost,
    deletePost
}