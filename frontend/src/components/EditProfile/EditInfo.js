import React,{ useState, useRef } from 'react';

import {
    Button,
    CssBaseline,
    TextField,
    Grid,
    Typography,
    Container,
    IconButton,
}from  '@material-ui/core';

import {
  Image as ImageIcon,
  AccountCircle
}from '@material-ui/icons'

import { makeStyles }     from '@material-ui/core/styles';

import { updateUser }             from '_api';
import { fileToBase64 }           from '_utils';
import { handleResponse }         from '_helpers';
import { authenticationService }  from '_services';    

import { UpdateUserViewModel }    from 'model';

const useStyles = makeStyles((theme) => ({
  paper: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.primary.main,
  },
  form: {
    width: '100%', 
    marginTop: theme.spacing(3),
  },
  submit: {
    margin: theme.spacing(3, 0, 2)
  },
  input:{
    display: 'none',
  },
  profilePicture:{
    width:            '100px',
    height:           '100px',
    objectFit:        'cover',
    borderRadius:     '50%',
    backgroundColor:  '#333333'
  },
  backgroundPicture:{
    width:            '100%',
    height:           '150px',
    objectFit:        'cover',
    borderRadius:     theme.spacing(0, 0, 2, 2),
    
    marginTop:theme.spacing(3),
  },
  pictures: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  userIcon:{
    color: '#ea5970',
    margin: theme.spacing(1),
  },
  inputImage:{
    display: 'none'
  }
}));

const defaultProfilePic = 'https://image.freepik.com/vector-gratis/perfil-avatar-hombre-icono-redondo_24640-14044.jpg';
const defaultCoverPic   = 'https://png.pngtree.com/thumb_back/fw800/background/20190220/ourmid/pngtree-blue-gradient-summer-creative-image_9270.jpg';

export function EditInfo( props ) {

  const { user, setUser } = props;

  const classes           = useStyles();

  const inputProfileRef   = useRef();
  const inputCoverRef     = useRef();

  const [state, setState] = useState({
    userName:           user.userName,
    userTag:            user.tag,
    userEmail:          user.email,
    changedProfilePic:  false,
    profilePic:         null,
    profilePicPreview:  user.profilePicPath,
    changedCoverPic:    false,
    coverPic:           null,
    coverPicPreview:    user.coverPicPath,
  });

  const onChangeUserName = e=>{
    setState(x=>{
      let copy = {...x};
      copy.userName = e.target.value;
      return copy;
    })
  };

  const onChangeTag = e=>{
    setState(x=>{
      let copy = {...x};
      copy.userTag = e.target.value;
      return copy;
    })
  };

  const onChangeEmail = e=>{
    setState(x=>{
      let copy = {...x};
      copy.userEmail = e.target.value;
      return copy;
    })
  };

  const onChangeProfilePic = async (e) => {
    let file = e.target.files[0];

    if( !file )
      return;

    let url  = await fileToBase64(file);
    setState( x=>{
      let copy = {...x};
      copy.profilePicPreview = url;
      copy.changedProfilePic = true;
      copy.profilePic        = file;
      return copy;
    })
  };

  const onChangeCoverPic   = async (e) => {
    let file = e.target.files[0];

    if( !file )
      return;
      
    let url  = await fileToBase64(file);
    setState( x=>{
      let copy = {...x};
      copy.coverPicPreview = url;
      copy.changedCoverPic = true;
      copy.coverPic        = file;
      return copy;
    })
  };

  const onClickProfilePic = () => inputProfileRef.current?.click();
 
  const onClickCoverPic   = () => inputCoverRef  .current?.click();

  const onSubmit = e=>{
    e.preventDefault();

    updateUser(authenticationService.currentUserValue.id,
      new UpdateUserViewModel({
        userName:   state.userName,
        tag:        state.userTag,
        email:      state.userEmail,
        profilePic: state.profilePic,
        coverPic:   state.coverPic
      }))
      .then( handleResponse )
      .then( res =>{
        const { data } = res;
        setUser( data );
      })
      .catch( console.warn );
  };
  
  return (
    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>

        <AccountCircle className={classes.userIcon}/>

        <Typography component='h1' variant='h5'>
          <strong>Mi Perfil</strong>
        </Typography>

        <Typography variant='body2'>
          Actualizar información.
        </Typography>

        <form className={classes.form} onSubmit={onSubmit} noValidate>

          <Grid container spacing={2}>

            <Grid item xs={12} className={classes.pictures}>
              <img className={classes.profilePicture} src={ state.profilePicPreview || defaultProfilePic}/>
            </Grid>
           
            <Grid item xs={12}   className={classes.pictures}>
              <input 
                accept='image/*' 
                className={classes.input} 
                type='file' ref={inputProfileRef} 
                onChange={onChangeProfilePic}
              />
              <label htmlFor='profile-button-file'> Foto de perfil
                <IconButton 
                 color='secondary' 
                 aria-label='upload picture' 
                 component='span' 
                 onClick={onClickProfilePic}
                >
                  <ImageIcon className={classes.imageIcon}/>
                </IconButton>
              </label>
            </Grid>
          
            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete='fname'
                name='Username'
                variant='outlined'
                required
                fullWidth
                value={state.userName}
                onChange={onChangeUserName}
                label='Usuario'
                autoFocus
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                variant='outlined'
                required
                fullWidth
                label='Tag'
                value={state.userTag}
                onChange={onChangeTag}
                autoComplete='tagname'
              />
            </Grid>

            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                label='Email'
                name='email'
                value={state.userEmail}
                onChange = {onChangeEmail}
                autoComplete='email'
              />
            </Grid>

            <Grid item xs={12} className={classes.pictures}>
              <img className={classes.backgroundPicture} src={ state.coverPicPreview || defaultCoverPic }/>
            </Grid>
    
            <Grid item xs={12} className={classes.pictures}>
              <input 
               accept='image/*' 
               className={classes.input} 
               type='file' 
               ref={inputCoverRef} 
               onChange={onChangeCoverPic}
              />
              <label htmlFor='background-button-file'> Foto de portada
                <IconButton 
                 color='secondary' 
                 aria-label='upload picture' 
                 component='span' 
                 onClick={onClickCoverPic}
                >
                  <ImageIcon className={classes.imageIcon}/>
                </IconButton>
              </label>
            </Grid>

          </Grid>

          <Button
            type='submit'
            fullWidth
            variant='contained'
            color='primary'
            className={classes.submit}
          >
            Guardar información
          </Button>
        </form>
      </div>
      
    </Container>
  );
}

export default  EditInfo;