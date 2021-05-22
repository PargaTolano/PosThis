import { emailRegex, userNameRegex, passwordRegex, tagRegex } from '_utils';

export const validateLogin = (userName, password)=>{

    const validation = {
        userName:   true,
        password:   true,
        validated:  true
    };

    if ( !passwordRegex .test( password || '' ) ){
        validation.password = false;
        validation.validated = false;
    }
     

    if ( !( emailRegex.test( userName || '' ) || userNameRegex.test( userName || '' ) ) ){
        validation.userName = false;
        validation.validated = false;
    }
    
    return validation;
};

export const validateSignup = ( {userName, tag, email, password} ) =>{

    const validation = {
        userName:   true,
        tag:        true,
        email:      true,
        password:   true,
        validated:  true,
    };

    if ( !userNameRegex .test( userName || '' ) ){
        validation.userName     = false;
        validation.validated    = false;
    }

    if ( !tagRegex .test( tag || '' ) ){
        validation.tag          = false;
        validation.validated    = false;
    }       

    if ( !emailRegex .test( email || '' ) ){
        validation.email        = false;
        validation.validated    = false;
    }

    if ( !passwordRegex .test( password || '' ) ){
        validation.password     = false;
        validation.validated    = false;
    }
    
    return validation;
}

export const validateCreateAndUpdatePost = ( content, mediaCount )=>{

    const validation = {
        content:    true,
        mediaCount: true,
        validated:  true
    };

    if( content.length === 0 ){
        validation.content = false;
    }

    if( mediaCount === 0 ){
        validation.mediaCount = false;
    }

    validation.validated = validation.content || validation.validated;

    return validation;
};