import SearchResultPostModel from 'model/SearchResultPostModel';
import SearchResultUserModel from 'model/SearchResultUserModel';

/**
 * @member {Array<SearchResultPostModel>} posts
 * @member {Array<SearchResultUserModel>} users
 */
class SearchResultModel {

    /**
     * @type {Array<SearchResultPostModel>}
     */
    posts;

    /**
     * @type {Array<SearchResultUserModel>}
     */
    users;

    constructor({posts, users}){
        this.posts = posts;
        this.users = users;
    }
}

export default SearchResultModel;