import React,{useState} from 'react';
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
}));

function SignUp() {
  const classes = useStyles();

  const [datos, setDatos] = useState({
      Username: '',
      tag: '',
      email: '',
      password:''
  })

  const handleInputChange = (event) =>{
    //console.log(event.target.value)
    setDatos({
      ...datos,
      [event.target.name] : event.target.value
    })
  }

  const SendData = (event) =>{
    event.preventDefault();
    console.log(datos.Username + ' ' + datos.tag + ' ' + datos.email + ' ' + datos.password)
  }

  return (

    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>

        <Avatar className={classes.avatar}>
          <AccessibilityNewRoundedIcon/>
        </Avatar>

        <Typography component='h1' variant='h5'>
          <strong>Regístrate</strong>
        </Typography>

        <Typography variant='h7'>
          Únete a la nueva comunidad de PosThis
        </Typography>

        <form className={classes.form} noValidate onSubmit = {SendData}>

          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete='fname'
                name='Username'
                variant='outlined'
                required
                fullWidth
                id='Username'
                label='Username'
                autoFocus
                onChange = {handleInputChange}
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
                onChange = {handleInputChange}
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
                onChange = {handleInputChange}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant='outlined'
                required
                fullWidth
                name='password'
                label='Contraseña'
                type='password'
                id='password'
                autoComplete='current-password'
                onChange = {handleInputChange}
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
            Regístrate ahora
          </Button>
        </form>
      </div>
      
    </Container>
  );
}

export default  SignUp;