class ReplyViewModel{
    replyID;
    content;
    postID;
    userID;
    mediaIDs;

    constructor({replyID, content, postID, userID, mediaIDs}){
        this.replyID = replyID;
        this.content = content;
        this.postID = postID;
        this.userID = userID;
        this.mediaIDs = mediaIDs;
    }
}

export default ReplyViewModel;