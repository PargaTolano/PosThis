class UserViewModel {
    id;
    userName;
    tag; 
    email;
    profilePhotoMediaID;
    birthDate;

    contructor( {id, userName, tag, email, profilePhotoMediaID,birthDate}){
        this.id = id;
        this.userName = userName;
        this.tag = tag;
        this.email = email;
        this.profilePhotoMediaID = profilePhotoMediaID;
        this.birthDate = birthDate;
    }
}

export default UserViewModel;