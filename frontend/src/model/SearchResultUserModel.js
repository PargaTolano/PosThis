class SearchResultUserModel{
    userName;
    tag;
    followerCount;
    profilePicID;

    constructor({userName,tag,followerCount,profilePicID}){
        this.userName       = userName;
        this.tag            = tag;
        this.followerCount  = followerCount;
        this.profilePicID   = profilePicID;
    }
}

export default SearchResultUserModel;