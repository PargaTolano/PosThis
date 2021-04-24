import React, { useState, useEffect } from 'react';
import useRequestLoadOnMount from 'hooks/useRequestLoadOnMount';

import { logIn } from 'API/User.API';

/**
 * @param {LogInModel} model
 */
export const useLogin = ( model )=>{
    return useRequestLoadOnMount( () => logIn(model) );
};