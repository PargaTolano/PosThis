import { getURL, getURLWithParams } from './url.API';

const getPosts = async () => (await fetch( getURL( 'posts-get' ) )).json();

const searchPost = async ( query )=>{
    let res = await fetch( getURLWithParams( 'posts-search', query ) );
    return res.json();
};

const getPost = async ( id )=>{
    let res = await fetch( getURLWithParams( 'posts-get-one', id ) );
    return res.json();
};

const createPost = async ( post )=>{
    const options = {
        method: "POST",
        body: JSON.stringify( post )
    };

    let res = await fetch( getURL( 'posts-create' ), options );
    return res.json();
};

const editPost = async ( id, user ) =>{
    const options = {
        method: "PUT",
        body: JSON.stringify( user )
    };

    let res = await fetch( getURLWithParams( 'posts-edit', id ), options );
    return res.json();
};

const deletePost = async ( id ) =>{
    const options = {
        method: "DELETE",
    };
    
    let res = await fetch( getURLWithParams( 'posts-delete', id ), options );
    return res.json();
};

export {
    getPosts,
    searchPost,
    getPost,
    createPost,
    editPost,
    deletePost
}