import React from 'react';

import {
  Button,
  Card,
  CardActions,
  CardContent,
  Typography
} from '@material-ui/core';

import { makeStyles }   from '@material-ui/core/styles';

import { DialogEditInfo, EditInfo }   from 'components/EditProfile';

import { createFollow, deleteFollow } from '_api';
import { handleResponse }             from '_helpers';
import { authenticationService}       from '_services';
import { FollowViewModel }            from '_model';

const useStyles = makeStyles((theme) => ({
  cardContainer: {
    position:       'absolute',
    display:        'flex',
    top:            theme.spacing( 48.66 ),
    width:          '23%',
    flexDirection:  'column',
    alignContent:   'center',
    alignItems:     'center',
    justifyContent: 'center',
    [theme.breakpoints.down('md')]:{
      width:      '27%',
    },
    [theme.breakpoints.down('sm')]:{
      width:      'auto',
      top:        theme.spacing( 30 ),
      left:       '50%',
      transform:  'translateX(-50%)'
    }
  },
  card: {
    padding:          theme.spacing(2),
    marginBottom:     theme.spacing(5),
    backgroundColor:  '#ea5970',
    borderRadius:     '10px',
    zIndex: '10'
  },
  media: {
    height: 140,
  },
  cardBtn: {
    padding:        '0',
    margin:         '0',
    alignItems:     'center',
    justifyContent: 'space-between',
    color:          'white',
  },
  title: {
    color:    'white',
    fontSize: '1.1rem'
  },
  secondaryTitle: {
    color:    'white',
    fontSize: '0.8rem'
    
  },
  content: {
    color:          'white',
    marginLeft:     theme.spacing(7),
    marginBottom:   theme.spacing(1),
  },
  followContainer:{
    marginBottom: theme.spacing(1)
  },
  layTitle:{
    display: 'inline-flex'
  },
  profilePicture:{
    width:        150,
    height:       150,
    objectFit:    'cover',
    borderRadius: '50%',
    backgroundColor: '#333333'
  },
  contImg:{
    alignItems: 'center',
    textAlign:  'center',
  },
  followNum:{
    alignItems: 'center',
    textAlign:  'center',
  }
}));

const profilePicPlaceholder = 'https://image.freepik.com/vector-gratis/perfil-avatar-hombre-icono-redondo_24640-14044.jpg';

export const ProfileCard = ( props ) => {
  const { user, setUser } = props;
  const classes = useStyles();

  const OnClickFollowButton = ()=>{
    const model = new FollowViewModel({
      followedID: user.id,
      followerID: authenticationService.currentUserValue.id });
    
    if( user.isFollowed ){
      deleteFollow( model )
      .then( handleResponse )
      .then( res =>{
        const { data } = res;
        setUser( data );
      })
      .catch( console.warn );
    }else{
      createFollow( model )
      .then( handleResponse )
      .then( res =>{
        const { data } = res;
        setUser( data );
      })
      .catch( console.warn );
    }
  };

  return (
    <div className={classes.cardContainer}>
      <Card className={classes.card}>
          
        <CardContent>
          <div className={classes.displayTitle}>
          </div>
          <div className={classes.contImg}>
            <img className={classes.profilePicture} src={ user.profilePicPath || profilePicPlaceholder }/>
            <Typography variant='h6' component='h2' className={classes.title}>
              <strong>{user.userName} {'@'+user.tag}</strong>
            </Typography>
          </div>
        </CardContent>
          
        <div className={classes.followContainer}>
          <Typography component='h2' className={classes.secondaryTitle}>
            Followers - {user.followerCount}
          </Typography>
          <Typography component='h2' className={classes.secondaryTitle}>
            Following - {user.followingCount}
          </Typography>
        </div>

        <CardActions disableSpacing className={classes.cardBtn}>

        {
          (authenticationService.currentUserValue.id !== user.id) 
          &&
          <div>
            <Button
              fullWidth
              variant='contained'
              color='secondary'
              className={classes.submit}
              onClick={OnClickFollowButton}
            >
              { user.isFollowed ? 'Dejar de Seguir' : 'Seguir'}
            </Button>
          </div>
        }

        {
          (authenticationService.currentUserValue.id === user.id) 
          &&
          <DialogEditInfo color={'primary'}>
            <EditInfo user={user} setUser={setUser}/>
          </DialogEditInfo>
        }
          
        </CardActions>
      </Card>
    </div>
  );
}

export default ProfileCard;
