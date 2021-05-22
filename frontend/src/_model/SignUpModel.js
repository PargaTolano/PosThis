export class SignUpModel{

    userName;
    tag;
    email;
    password;

    constructor({userName, tag, email, password}){
        this.userName = userName;
        this.tag = tag;
        this.email = email;
        this.password = password;
    }
}

export default SignUpModel;