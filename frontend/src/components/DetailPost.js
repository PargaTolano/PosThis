import React from 'react';
import SearchAppBar from 'components/Inicio/Navbar';
import { makeStyles } from '@material-ui/core/styles';
import backapp3 from 'assets/backapp3.png';
import CardPost from 'components/Post/CardPost';
import Reply from './Post/RepliePost';

const useStyles = makeStyles((theme) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
    height: '100%',
  },
  cardHolder: {
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(2),
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'flex',
    alignItems: 'center',
    flexDirection: 'column',
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
}));

const DetailPost = (props) => {
  //useCheckAuth();
  const classes = useStyles();

  return (
    <div className={classes.Background}>
      <SearchAppBar />
      <div className={classes.cardHolder}>

        <div component='h4' variant='h2' className={classes.titleBegin}>
          <strong>Detalle del post</strong>
        </div>

        <CardPost />
        <Reply />
        <Reply />
        <Reply />
      </div>
    </div>
  );
};

export default DetailPost;
