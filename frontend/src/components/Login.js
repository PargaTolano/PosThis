import React from 'react';
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

const Login = (props) => {
  const { ready, response } = useRequestLoadOnMount(getUsers);
  const classes = useStyles();

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

          <form className={classes.form} noValidate>
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
