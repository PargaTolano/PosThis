class SearchResultPostModel{

    content;
    publisherID;
    publisherUsername;
    publisherTag;
    publishingTime;
    mediaIDs;

    constructor({content, publisherID, publisherUsername, publisherTag, publishingTime, mediaIDs}){
        this.content            = content;
        this.publisherID        = publisherID;
        this.publisherUsername  = publisherUsername;
        this.publisherTag       = publisherTag;
        this.publishingTime     = publishingTime;
        this.mediaIDs           = mediaIDs;
    }

}

export default SearchResultPostModel;