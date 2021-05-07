import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import CardPost from 'components/Post/CardPost';
import Typography from '@material-ui/core/Typography'
import Divider from '@material-ui/core/Divider';
import Link from '@material-ui/core/Link'

const useStyles = makeStyles((theme) => ({
  cardHolder: {
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(1),
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'flex',
    alignItems: 'center',
    flexDirection:'column',
  },
  titleBegin:{
    color: 'white',
    fontFamily: 'Arial',
    fontStyle: 'normal',
    fontSize: 30,
    width: '100%',
    paddingBottom: theme.spacing(3),
    textAlign: 'center',
    flexDirection:'column',
  },
  dividerTitle:{
    background: 'white',
    paddingTop: theme.spacing(0),
  }
}));

function FixedContainer() {
  const classes = useStyles();
  return (
    <div className={classes.cardHolder}>

      <div component='h4' variant='h2' className={classes.titleBegin}>
        <strong>Inicio</strong>
      </div>

      <CardPost />
      <CardPost />
      <CardPost />
      <CardPost />
    </div>
  );
}
export default FixedContainer;
