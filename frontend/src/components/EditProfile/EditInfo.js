import React from 'react';
import {
    Button,
    CssBaseline,
    TextField,
    Grid,
    Box,
    Typography,
    Container,
}from  '@material-ui/core';
import ImageIcon from '@material-ui/icons/Image';

import IconButton from "@material-ui/core/IconButton";
import { makeStyles } from '@material-ui/core/styles';

import AccountCircle from "@material-ui/icons/AccountCircle";

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
    margin: theme.spacing(3, 0, 2),
  },
  input:{
    display: 'none',
  },
  profilePicture:{
    maxWidth: 100,
    maxHeight: 100,
    borderRadius: 5,
  },
  backgroundPicture:{
    maxWidth: 300,
    maxHeight: 250,
    borderRadius: 5,
    
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
}));

function EditInfo() {
  const classes = useStyles();
  
  return (

    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>

        <AccountCircle className={classes.userIcon}/>

        <Typography component='h1' variant='h5'>
          <strong>Mi Perfil</strong>
        </Typography>

        <Typography variant='h7'>
          Actualizar información.
        </Typography>

        <form className={classes.form} noValidate>
       
          <Grid container spacing={2}>
            <Grid item xs={12} className={classes.pictures}>
              <img className={classes.profilePicture} id="profilePicture" src="https://image.freepik.com/vector-gratis/perfil-avatar-hombre-icono-redondo_24640-14044.jpg"/>
            </Grid>

           
            <Grid item xs={12}   className={classes.pictures}>
              <input accept="image/*" className={classes.input} id="profile-button-file" type="file" />
                <label htmlFor="profile-button-file"> Foto de perfil
                  <IconButton color="secondary" aria-label="upload picture" component="span">
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
                id='Username'
                label='Usuario'
                autoFocus
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                variant='outlined'
                required
                fullWidth
                id='tag'
                label='Tag'
                name='tag'
                autoComplete='tagname'
              />
            </Grid>

            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                id='email'
                label='Email'
                name='email'
                autoComplete='email'
              />
            </Grid>
            
           
          </Grid>
          <Grid item xs={12}   className={classes.pictures}>
              <img className={classes.backgroundPicture} id="backgroundPicture" src="https://png.pngtree.com/thumb_back/fw800/background/20190220/ourmid/pngtree-blue-gradient-summer-creative-image_9270.jpg"/>
          </Grid>

          <Grid item xs={12}   className={classes.pictures}>
              <input accept="image/*" className={classes.input} id="background-button-file" type="file" />
                <label htmlFor="background-button-file"> Foto de portada
                  <IconButton color="secondary" aria-label="upload picture" component="span">
                    <ImageIcon className={classes.imageIcon}/>
                  </IconButton>
                </label>
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