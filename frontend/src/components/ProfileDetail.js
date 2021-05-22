import React                    from 'react';
import { Redirect }             from 'react-router-dom';

import { makeStyles }           from '@material-ui/core/styles';

import { NavBar }               from 'components/Inicio';
import { ProfileContainer }     from 'components/Profile';

import { routes }               from '_utils';
import { useGetUserProfile }    from '_hooks';

import backapp2                 from 'assets/backapp2.png';

const useStyles = makeStyles((theme) => ({
  Background:{
    backgroundImage:      `url('${backapp2}')`,
    backgroundSize:       'cover',
    backgroundPosition:   'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat:     'no-repeat',
    minHeight:            '100vh'
  }
}));

export const ProfileDetail = (props) => {

  const { match, ...rest } = props;
  const { id }             = match.params;
  const classes            = useStyles();

  const [[ready, user], setUser] = useGetUserProfile( id || '' );

  if( id == 'undefined' || id === undefined || id === null || id === '' ){
    return (<Redirect to={routes.feed}/>);
  }

  return (
      <div className= {classes.Background}>
      <NavBar {...rest}/>
      {
        ( ready ) 
        && 
        (
          <ProfileContainer user={user} setUser={setUser} {...rest}/>
        )
      }
        
      </div>
  );
};

export default ProfileDetail;
