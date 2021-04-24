import React from 'react';
import Logo from 'assets/placeholder.png';

import { getUsers } from 'API/User.API';
import useRequestLoadOnMount from 'hooks/useRequestLoadOnMount';

const Login = () => {

    const mainStyle = {
        backgroundImage: `url( '${Logo}' )`,
        backgroundSize: 'contain',
        backgroundPosition: 'center',
        backgroundRepeat: 'no-repeat'
    };

    const {ready, data} = useRequestLoadOnMount( getUsers );

    console.log(data);
    
    return (
        <div className="login">
            <header></header>
            <aside><p>dsdasdadasdd sdasd sadasd dasdasd</p></aside>
            <main 
                style={ mainStyle }
            >
                <div>
                    {
                        ready && data.data.map( u => <h2 key={u.Id}>{u.userName}</h2>)
                    }
                </div>
            </main>  
        </div>
    );
}

export default Login;