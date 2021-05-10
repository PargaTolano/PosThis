import React from 'react';

import {makeStyles} from '@material-ui/core/styles';
import ImageIcon from '@material-ui/icons/Image';

import IconButton from "@material-ui/core/IconButton";

import {
  TextField,
  Button,
} from "@material-ui/core";
import { CenterFocusStrong } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
 
  form: {
    marginLeft :theme.spacing(4),
    marginRight:theme.spacing(35),
    background:'white',
    width: '87%', 
    marginTop: theme.spacing(0.5),
    height:'40%',
    padding:theme.spacing(3),
    borderRadius: 10,  
    alignItems: 'center',
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
  titleForm: {
    justifyContent: 'space-around',
    color: theme.palette.primary.dark ,
    alignSelf:'center',
    color:'#ea5970',
  },
  
}));

function CreatePost() {
  const classes = useStyles();
  return (

             <form className={classes.form} noValidate>
                <div component='h4' variant='h2' className={classes.titleForm}>
                <strong>Nuevo PosThis!</strong>
                </div>
            <TextField
              variant="outlined"
              margin="normal"
              multiline
              rows={3}
              rowsMax={3}
              fullWidth
              id="postContent"
              label="Escribir..."
              name="postContent"
              autoComplete="postContent"
              autoFocus
              className={classes.postContent}
            />

            <div className = {classes.cardBtn}>
             
             <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
             >
              Publicar
            </Button>
            <IconButton>
                <ImageIcon className={classes.imageIcon}/>
             </IconButton>
            </div>
            </form>
       

  );
}
export default CreatePost;