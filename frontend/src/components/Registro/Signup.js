import React,{ useState, useEffect } from 'react';

import { makeStyles } from '@material-ui/core/styles';

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

import { 
  AccessibilityNewRounded as AccessibilityNewRoundedIcon 
} from '@material-ui/icons';

import { handleResponse } from '_helpers';
import { validateSignup } from '_utils';
import { createUser }     from '_api';

import { SignUpModel }    from '_model';
import { authenticationService } from '_services';

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
  fieldWarning:{
    color: '#ea5970',
    fontSize: '0.8rem',
    marginTop: theme.spacing(1)
  }
}));

export const SignUp = (props)=>{

  const { handleClose } = props;
  const classes = useStyles();

  const [state, setState] = useState({
      userName: '',
      tag: '',
      email: '',
      password:''
  });

  const [ validation, setValidation] = useState({
    userName:   false,
    tag:        false,
    email:      false,
    password:   false,
    validated:  false,
  });

  const OnChangeInput = (e) =>{
    setState({
      ...state,
      [e.target.name] : e.target.value
    });
  }

  const onSubmit = (e) =>{
    
    e.preventDefault();

    const model = new SignUpModel(state);

    createUser(model)
    .then( handleResponse)
    .then( handleClose )
    .catch(console.warn)
    .then( handleClose );
  };

  useEffect(() => {
    setValidation( validateSignup( state ) );
  }, [state]);

  return (
    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>

        <Avatar className={classes.avatar}>
          <AccessibilityNewRoundedIcon/>
        </Avatar>

        <Typography component='h2' variant='h5'>
          <strong>Regístrate</strong>
        </Typography>

        <Typography variant='body2'>
          Únete a la nueva comunidad de PosThis
        </Typography>

        <form className={classes.form} noValidate onSubmit = {onSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete='fname'
                name='userName'
                variant='outlined'
                required
                fullWidth
                label='Username'
                autoFocus
                onChange = {OnChangeInput}
              />
              {
                !validation.userName
                && 
                <Typography variant='body2' className={classes.fieldWarning}>
                * Nombre de usuario no valido
                </Typography>
              }
            </Grid>
            

            <Grid item xs={12} sm={6}>
              <TextField
                variant='outlined'
                required
                fullWidth
                label='Tag'
                name='tag'
                autoComplete='tagname'
                onChange = {OnChangeInput}
              />
              {
                !validation.tag
                && 
                <Typography variant='body2' className={classes.fieldWarning}>
                * Tag no valido
                </Typography>
              }
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
                onChange = {OnChangeInput}
              />
              {
                !validation.email
                && 
                <Typography variant='body2' className={classes.fieldWarning}>
                * Email no valido
                </Typography>
              }
            </Grid>
            

            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                name='password'
                label='Contraseña'
                type='password'
                autoComplete='current-password'
                onChange = {OnChangeInput}
              />
              {
                !validation.password 
                &&
                <Typography variant='body2' className={classes.fieldWarning}>
                * Contraseña no valida
                </Typography>
              }
            </Grid>
           
          </Grid>

          <Button
            type='submit'
            fullWidth
            variant='contained'
            color='secondary'
            className={classes.submit}
            disabled = {!validation.validated}
          >
            Regístrate ahora
          </Button>
        </form>
      </div>
      
    </Container>
  );
}

export default  SignUp;