import React                    from 'react';
import SearchAppBar             from 'components/Inicio/Navbar';
import ContainerPerfil          from 'components/Profile/ContainerProfile';
import { Redirect }             from 'react-router-dom';
import { getUser }              from 'API/User.API';
import { routes }               from '_utils';
import useRequestLoadOnMount    from 'hooks/useRequestLoadOnMount';

import {makeStyles}             from '@material-ui/core/styles';
import backapp2                 from 'assets/backapp2.png';

import { handleResponse }  from '_helpers';

const useStyles = makeStyles((theme) => ({
  Background:{
    backgroundImage: `url('${backapp2}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
  }
}));

const Profile = (props) => {

  const { match, ...rest } = props;
  const { id }    = match.params;
  const classes = useStyles();

  const [ready, response] = useRequestLoadOnMount(()=>getUser( id || '').then(handleResponse));

  if( id == 'undefined' || id === undefined || id === null || id === '' ){
    return (<Redirect to={routes.feed}/>);
  }
   
  console.log( response );

  return (
      <div className= {classes.Background}>
      {
        ( ready && response?.data ) 
        && 
        (
          <>
            <SearchAppBar {...rest}/>
            <ContainerPerfil user={response.data} {...rest}/>
          </>
        )
      }
        
      </div>
  );
};

export default Profile;
