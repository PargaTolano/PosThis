export  class UpdateUserViewModel{

    userName     ;
    tag         ;
    email       ;
    profilePic  ;
    coverPic    ;

    constructor({userName, tag, email, profilePic, coverPic}){
        this.userName   = userName   ;
        this.tag        = tag        ;
        this.email      = email      ;
        this.profilePic = profilePic ;
        this.coverPic   = coverPic   ;
    }

}

export default UpdateUserViewModel;