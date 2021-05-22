import React,{ useEffect } from 'react';

import { makeStyles }    from '@material-ui/core/styles';

import { routes }        from '_utils';

import backapp3          from 'assets/backapp3.png';

const useStyles = makeStyles((theme)=>({
    Background: {
        backgroundImage: `url('${backapp3}')`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        backgroundAttachment: 'fixed',
        backgroundRepeat: 'no-repeat',
        minHeight: '100vh'
      },
    message:{
        display: 'inline-block',
        position: 'fixed',
        top: '50%',
        left: '50%',
        transform: 'translate( -50%, -50%)',
        width: '100%',
        maxWidth: '500px',
        height: '100%',
        maxHeight: '300px',
        color: 'white',
        backgroundColor: '#2b387f',
        padding: theme.spacing(2),
        borderRadius: theme.spacing(1),
        boxShadow:        'black 1px 1px 8px',
    },
    error:{
        display: 'inline-block',
        color: 'white',
        fontSize: '3rem',
        marginBottom: 0,
    },
    errorCode:{
        display: 'inline-block',
        color: '#ea5970',
        fontSize: '4rem',
        marginLeft: theme.spacing(4),
        marginBottom: 0,
    },
    errorDescription:{
        color: 'gray',
        fontSize: '1rem',
        marginBottom: 0,
        wordSpacing: 2,
    },
    errorAction:{
        display: 'inline-block',
        color: 'white',
        fontSize: '0.8rem',
        marginBottom: 0,
        wordSpacing: 3,
    }
}));

export const NotFound = ( props ) => {

    const {history} = props;

    const classes = useStyles();

    useEffect(()=>{
        setTimeout(()=>history.replace(routes.feed), 3000);
    },[]);

    return (
        <div className={classes.Background}>
            <div className={classes.message}>
                <p className={classes.error}>   ERROR </p>
                <p className={classes.errorCode}> 404 </p>  
                <p className={classes.errorDescription}>Pagina no encontrada</p>
                <p className={classes.errorAction}>Se redirigira a la pagina principal en un momento...</p>
            </div>
        </div>
    );
}

export default NotFound;