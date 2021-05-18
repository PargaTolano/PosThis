import React from 'react';

import {makeStyles}   from '@material-ui/core/styles';
import CardProfile    from 'components/Profile/CardProfile';
import FixedContainer from 'components/Inicio/Container';
import CreatePost     from "components/Post/CreatePost";

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
} from "@material-ui/core";
import { createPost } from 'API/Post.API';

const useStyles = makeStyles((theme) => ({
  root:{
    width:        '100%',
    maxWidth:     '1200px',
    marginLeft:   'auto',
    marginRight:  'auto'
  },
  coverPic:{
    display: 'inline-block',
    width: '100%',
    height: theme.spacing( 50 ),
    objectFit: 'cover',
    borderRadius: '0px 0px 10px 10px',
    marginBottom: theme.spacing( 3 ),
    [theme.breakpoints.down('sm')]:{
      marginBottom: theme.spacing( 33 ),
    }
  },
  column:{
    position:     'relative',
    width:        '100%',
    paddingLeft:  '30%',
    [theme.breakpoints.down('sm')]:{
      paddingLeft: '0'
    }
  }
}));

const coverPlaceholder = "https://png.pngtree.com/thumb_back/fw800/background/20190220/ourmid/pngtree-blue-gradient-summer-creative-image_9270.jpg";

function ContainerPerfil( props ) {
  const { user, ...rest } = props;
  const classes = useStyles();
  return (

      <div className={classes.root}>

        <img className={classes.coverPic} src={coverPlaceholder || user.coverPicPath} />

        <CardProfile user={user} {...rest}/>
        <div className={classes.column}>
          <CreatePost     {...rest}/>
          <FixedContainer {...rest}/>
        </div>
        
      </div>
  );
}
export default ContainerPerfil;