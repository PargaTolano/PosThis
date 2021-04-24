class ResponseApiError
{
    code;
    httpStatusCode;
    message;

    constructor({code, httpStatusCode, message}){
        this.code = code;
        this.httpStatusCode = httpStatusCode;
        this.message = message;
    }
}

export default ResponseApiError;