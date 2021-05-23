import React,{ useState, useEffect }from 'react';

import {
  Grid,
  TextField,
  FormControlLabel,
  Button,
  Link,
  CssBaseline,
  Avatar,
  Paper,
  Checkbox,
  Typography,
} from '@material-ui/core';

import { makeStyles }             from '@material-ui/core/styles';
import PersonPinIcon              from '@material-ui/icons/PersonPin';

import { DialogSignup, SignUp }   from 'components/Registro';

import { authenticationService }  from '_services';
import { routes }                 from '_utils';

const useStyles = makeStyles((theme) => ({
  root: {
    height: '100vh',
  }, 
  image: {
    backgroundImage: 'url(/img/backgroundPT.png)',
    backgroundRepeat: 'no-repeat',
    backgroundSize: 'cover',
    backgroundPosition: 'center',
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.primary.dark,
  },
  form: {
    width: '100%', 
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export const Login = (props) => {

  const { match, history} = props;

  const classes = useStyles();

  const [ username, setUsername ] = useState('');
  const [ password, setPassword ] = useState('');

  const getOnChange = ( setState )=>e=>setState(e.target.value);

  const onSubmit = async e =>{
    e.preventDefault();

    authenticationService
      .login( username, password )
      .then( () => history.push( routes.feed ) )
      .catch( console.warn );
  };

  return (
    <Grid container component='main' className={classes.root}>
      <CssBaseline />
      <Grid item xs={false} sm={4} md={7} className={classes.image} />
      <Grid item xs={12}    sm={8} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <PersonPinIcon />
          </Avatar>

          <Typography component='h1' variant='h5'>
            <strong>Inicia Sesión</strong>
          </Typography>

          <form className={classes.form} noValidate onSubmit={onSubmit}>
            <TextField
              variant='outlined'
              margin='normal'
              required
              fullWidth
              id='email'
              label='Correo electrónico o Nombre de Usuario'
              name='email'
              autoComplete='email'
              autoFocus
              onChange = {getOnChange(setUsername)}
            />

            <TextField
              variant='outlined'
              margin='normal'
              required
              fullWidth
              name='password'
              label='Contraseña'
              type='password'
              id='password'
              autoComplete='current-password'
              onChange = {getOnChange(setPassword)}
            />

            <Button
              type='submit'
              fullWidth
              variant='contained'
              color='primary'
              className={classes.submit}
            >
              Ingresar
            </Button>
          </form>
          <Grid container>
              <Grid item>
                <DialogSignup>
                  <SignUp/>
                </DialogSignup>
              </Grid>
            </Grid>
        </div>
      </Grid>
    </Grid>
  );
};

export default Login;
