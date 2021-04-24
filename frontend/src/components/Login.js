import React from 'react';
import Logo from 'assets/placeholder.png';

import { getUsers } from 'API/User.API';
import useRequestLoadOnMount from 'hooks/useRequestLoadOnMount';

const Login = ( props ) => {

    const { ready, response } = useRequestLoadOnMount( getUsers );
    
    return (
        <div className="login">
            <header></header>
            <aside><p>A la derecha apareceran usuarios ;)</p></aside>
            <main>
                <div>
                    {
                        ready && response.data.map( u => <h2 key={u.Id}>{u.userName}</h2>)
                    }
                </div>
            </main>  
        </div>
    );
}

export default Login;