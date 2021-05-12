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
import { makeStyles } from '@material-ui/core/styles';
import PersonPinIcon from '@material-ui/icons/PersonPin';

import CustomizedDialogs from 'components/Registro/dialogSignup';
import SignUp from 'components/Registro/Signup';

import { getUsers } from 'API/User.API';
import { getPosts } from 'API/Post.API';
import useRequestLoadOnMount from 'hooks/useRequestLoadOnMount';

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

const makeOnSubmit = ( object ) =>
  e =>{
    e.preventDefault();
    let body = {};

    for( let key of Object.keys(object)){
      console.log(key, object[key]);
    }
  };

const Login = (props) => {
  //const [ready, response ]= useRequestLoadOnMount(getUsers);
  //const [readyP, responseP ]= useRequestLoadOnMount(getPosts);
  const classes = useStyles();

  const [ email, setEmail ] = useState('');
  const [ password, setPassword ] = useState('');

  const getOnChange = ( setState )=>e=>setState(e.target.value);

  return (
    <Grid container component='main' className={classes.root}>
      <CssBaseline />
      <Grid item xs={false} sm={4} md={7} className={classes.image} />
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <PersonPinIcon />
          </Avatar>

          <Typography component='h1' variant='h5'>
            <strong>Inicia Sesión</strong>
          </Typography>

          <form className={classes.form} noValidate onSubmit={makeOnSubmit({email, password})}>
            <TextField
              variant='outlined'
              margin='normal'
              required
              fullWidth
              id='email'
              label='Correo electrónico'
              name='email'
              autoComplete='email'
              autoFocus
              onChange = {getOnChange(setEmail)}
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

            <FormControlLabel
              control={<Checkbox value='remember' color='primary' />}
              label='Recordar contraseña'
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

            <Grid container>
              <Grid item xs>
                <Link href='#' variant='body2'>
                  ¿Olvidaste tu contraseña?
                </Link>
              </Grid>
              <Grid item>
                <CustomizedDialogs>
                  <SignUp/>
                </CustomizedDialogs>
              </Grid>
            </Grid>
          </form>
        </div>
      </Grid>
    </Grid>
  );
};

export default Login;
