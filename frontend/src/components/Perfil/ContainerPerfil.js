import React from 'react';

import Container from '@material-ui/core/Container';
import {makeStyles} from '@material-ui/core/styles';
import CardPost from 'components/Post/CardPost';
import CardPerfil from 'components/Perfil/CardPerfil';
import FixedContainer from "components/Inicio/Container";
import CreatePost from "components/Post/CreatePost";
import ImageIcon from '@material-ui/icons/Image';

import IconButton from "@material-ui/core/IconButton";

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
  topHolder:{
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(4),
    paddingLeft:theme.spacing(35),
    paddingBottom: theme.spacing(1),
    marginLeft : 'auto',
    marginRight : 'auto',
    display: 'flex',
    alignItems: 'row',
    flexDirection: 'row',
  },
  rightHolder:{

  },
  imgBackG: {
    width: 580 ,
    height: 180,
    paddingLeft:theme.spacing(4),
    borderRadius: 10,  
    
  },
  title:{
    color: '#f28a9a',
    marginLeft : theme.spacing(50),
  }
  
}));

function ContainerPerfil() {
  const classes = useStyles();
  return (
        <div>
        <div className = {classes.topHolder}>
            <CardPerfil/> 
            <div className={classes.rightHolder}>
            <img className={classes.imgBackG} id="imagenP" src="https://png.pngtree.com/thumb_back/fw800/background/20190220/ourmid/pngtree-blue-gradient-summer-creative-image_9270.jpg"/>
           
            <CreatePost/>
            </div>
          
        </div>
        <Typography id="userTag"  component="h2" className={classes.title}>
              <strong>Mis PosThis</strong>
            </Typography>
        <FixedContainer></FixedContainer>
        </div>
  );
}
export default ContainerPerfil;