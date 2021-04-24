class FeedPostModel
{
    isRepost;
    content;
    publisherID;
    postID;
    reposterID;
    mediaIDs;
    date;

    constructor( {isRepost, 
        content, 
        publisherID, 
        postID, 
        reposterID, 
        mediaIDs, 
        date }){

            this.isRepost       = isRepost;
            this.content        = content;
            this.publisherID    = publisherID;
            this.postID         = postID;
            this.reposterID     = reposterID;
            this.mediaIDs       = mediaIDs;
            this.date           = date;
    }
}

export default FeedPostModel;