import React,{ useState} from 'react';
import {
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  InputBase,
  MenuItem,
  Menu,
  Button,
} from '@material-ui/core';

import { Link, Redirect }         from 'react-router-dom';
import { fade, makeStyles }       from '@material-ui/core/styles';
import SearchIcon                 from '@material-ui/icons/Search';
import AccountCircle              from '@material-ui/icons/AccountCircle';
import Logo                       from 'assets/Logo.png';
import LogoNominado               from 'assets/logPT.svg';

import { routes }                 from '_utils';
import { authenticationService }  from '_services';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  PosthisLogo: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
    display: 'none',
    [theme.breakpoints.up('sm')]: {
      display: 'block',
    },
  },
  titleimg: {},
  search: {
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: fade(theme.palette.common.white, 0.15),
    '&:hover': {
      backgroundColor: fade(theme.palette.common.white, 0.25),
    },
    marginLeft: 0,
    marginRight: theme.spacing(2),
    width: '100%',
    [theme.breakpoints.up('sm')]: {
      marginLeft: theme.spacing(1),
      width: 'auto',
    },
  },
  searchIcon: {
    padding: theme.spacing(0, 2),
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  inputRoot: {
    color: 'inherit',
  },
  inputInput: {
    padding: theme.spacing(1, 1, 1, 0),
    paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('sm')]: {
      width: '12ch',
      '&:focus': {
        width: '20ch',
      },
    },
  },
  Tool:{
    position: 'sticky',
    top: 0,
    backgroundColor: '#1b2452',
  },
  userIcon:{
    color: '#ea5970',
  },
}));

export const NavBar = (props) => {

  const { history }= props;
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = useState(null);
  const [query, setQuery]       = useState('');

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const onClickProfile = ()=>{
    history.push( routes.getProfile( authenticationService.currentUserValue.id ) );
  };

  const handleLogOut = ()=>{
    authenticationService.logout();
    history.push( routes.login );
  }

  const onChange = ( e )=>setQuery( e.target.value);

  const onSearch = ( e )=>{
    e.preventDefault();
    history.push( routes.getSearch(query));
  };

  return (
      <AppBar className={classes.Tool}>
        <Toolbar>
          <div>
              <Link to={routes.feed}>
              <img className={classes.PosthisLogo} src= {Logo} width= '50' height='50'/>
              </Link>
          </div>

          <div className={classes.title}>
            <img src={LogoNominado} width='120' height='60' alt='Logo' />
          </div>

          <div className={classes.search}>
            <div className={classes.searchIcon}>
              <SearchIcon />
            </div>

            <form onSubmit ={onSearch}>
              <InputBase
                placeholder='Buscar…'
                value={query}
                classes ={{
                  root: classes.inputRoot,
                  input: classes.inputInput,
                }}
                inputProps={{ 'aria-label': 'search' }}
                onChange  ={onChange}

              />
            </form>
            
          </div>

          <div>
            <IconButton
              color='inherit'
              aria-controls='simple-menu'
              aria-haspopup='true'
              onClick={handleClick}
            >
            <AccountCircle className={classes.userIcon}/>
            </IconButton>

            <Menu
              id='simple-menu'
              anchorEl={anchorEl}
              keepMounted
              open={Boolean(anchorEl)}
              onClose={handleClose}
            >
              <MenuItem onClick={onClickProfile }>Perfil</MenuItem>
              <MenuItem onClick={handleLogOut}>Cerrar sesión</MenuItem>
            </Menu>
          </div>
        </Toolbar>
      </AppBar>
  );
}

export default NavBar;