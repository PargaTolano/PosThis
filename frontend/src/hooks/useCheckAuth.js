import React, { useState, useEffect } from 'react';

import { authTokenKey } from 'utils/localStorage';

const useCheckAuth = ()=>{
    useEffect(()=>{
        let authToken = localStorage.getItem( authTokenKey );

        if (authToken === null)
            window.location.href = "/login";
    },[]);
};

export default useCheckAuth;