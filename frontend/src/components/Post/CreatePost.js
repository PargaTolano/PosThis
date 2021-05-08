import React from 'react';

import {makeStyles} from '@material-ui/core/styles';
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

const useStyles = makeStyles((theme) => ({
 
  form: {
    marginLeft :theme.spacing(4),
    marginRight:theme.spacing(35),
    background:'white',
    width: '90%', 
    marginTop: theme.spacing(0.5),
    height:'30%',
    padding:theme.spacing(2),
    borderRadius: 10,  
  },
  submit: {
    
    width: '20%',
  },
 
  cardBtn: {
    alignItems: 'center',
    justifyContent: 'space-around',
    color: 'white',
    alignSelf:'center',
  },
}));

function CreatePost() {
  const classes = useStyles();
  return (
             <form className={classes.form} noValidate>
            <TextField
              variant="outlined"
              margin="normal"
              
              fullWidth
              id="postContent"
              label="Escribir..."
              name="postContent"
              autoComplete="postContent"
              autoFocus
              className={classes.postContent}
            />

            <div className = {classes.cardBtn}>
             <IconButton>
                <ImageIcon className={classes.imageIcon}/>
             </IconButton>
             <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
             >
              Publicar
            </Button>
            </div>
            </form>
       

  );
}
export default CreatePost;