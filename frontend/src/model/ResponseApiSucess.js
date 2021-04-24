class ResponseApiSuccess{
    
    code;
    message;
    data; 

    constructor({code, message, data}){
        this.code = code;
        this.message = message;
        this.data = data;
    }
}

export default ResponseApiSuccess;