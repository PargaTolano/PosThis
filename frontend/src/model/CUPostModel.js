class CUPostModel {
    userID;
    content;
    mediaIDs;

    constructor({userID,content,mediaIDs}){
        this.userID = userID;
        this.content = content;
        this.mediaIDs = mediaIDs;
    }
}

export default CUPostModel;