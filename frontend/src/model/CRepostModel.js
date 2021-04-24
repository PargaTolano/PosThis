class CRepostModel{
    userID;
    postID;
    repostDate;

    contructor({userID, postID, repostDate}){
        this.userID = userID;
        this.postID = postID;
        this.repostDate = repostDate;
    }
}

export default CRepostModel;