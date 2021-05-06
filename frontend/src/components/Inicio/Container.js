import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import {makeStyles} from '@material-ui/core/styles'
import CardPost from 'components/Post/CardPost';

const useStyles = makeStyles((theme) => ({
  cardHolder:{
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(1),
    marginLeft : 'auto',
    marginRight : 'auto',
    display: 'flex',
    alignItems: 'center',
    flexDirection: 'column',
  }
}));

function FixedContainer() {
  const classes = useStyles();
  return (

        <div className = {classes.cardHolder}>
        <CardPost />
        <CardPost />
        <CardPost />
        <CardPost />
        </div>
     
  );
}
export default FixedContainer;