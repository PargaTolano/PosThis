class MediaViewModel{
    mediaID;
    MIME;
    content;

    contructor({mediaID, MIME, content}){
        this.mediaID    = mediaID;
        this.MIME       = MIME;
        this.content    = content;
    }
}

export default MediaViewModel;