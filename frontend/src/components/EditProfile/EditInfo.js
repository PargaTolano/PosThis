import React from 'react';
import {
    Avatar,
    Button,
    CssBaseline,
    TextField,
    FormControlLabel,
    Link,
    Grid,
    Box,
    Typography,
    Container,
}from  '@material-ui/core';
import ImageIcon from '@material-ui/icons/Image';

import IconButton from "@material-ui/core/IconButton";

import AccessibilityNewRoundedIcon from '@material-ui/icons/AccessibilityNewRounded';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
  paper: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
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
}));

function EditInfo() {
  const classes = useStyles();
  

  
  
  return (

    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>

        <Avatar className={classes.avatar}>
          <AccessibilityNewRoundedIcon/>
        </Avatar>

        <Typography component='h1' variant='h5'>
          <strong>Mi Perfil</strong>
        </Typography>

        <Typography variant='h7'>
          Actualizar información.
        </Typography>

        <form className={classes.form} noValidate>
       
          <Grid container spacing={2}>
            
            <Grid item xs={12}>
              <input accept="image/*" className={classes.input} id="icon-button-file" type="file" />
                <label htmlFor="icon-button-file"> Seleccionar foto de perfil
                  <IconButton color="primary" aria-label="upload picture" component="span">
                    <ImageIcon className={classes.imageIcon}/>
                    
                  </IconButton>
                </label>
            <img id="profilePicture" ></img>
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete='fname'
                name='Username'
                variant='outlined'
                
                fullWidth
                id='Username'
                label='Username'
                autoFocus
                disabled
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                variant='outlined'
                
                fullWidth
                id='tag'
                label='Tag'
                name='tag'
                autoComplete='tagname'
                disabled
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
            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                name='password'
                label='Nueva contraseña'
                type='password'
                id='password'
                autoComplete='current-password'
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                name='password'
                label='Confirmar contraseña'
                type='password'
                id='passwordC'
                autoComplete='current-password'
              />
            </Grid>
           
          </Grid>

          <Button
            type='submit'
            fullWidth
            variant='contained'
            color='secondary'
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