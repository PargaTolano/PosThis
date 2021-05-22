import React,{useEffect} from 'react';

import { routes }        from '_utils';

export const NotFound = (props) => {

    const {auth, history} = props;

    useEffect(()=>{
        setTimeout(()=>history.replace(routes.feed), 3000);
    },[]);

    return (
        <div>
            <h1>ERROR 404 PAGINA NO ENCONTRADA</h1>
            <p>Se redirigira a la pagina principal en unos segundos</p>
        </div>
    );
}

export default NotFound
