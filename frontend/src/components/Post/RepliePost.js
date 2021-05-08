import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import Avatar from '@material-ui/core/Avatar';

const useStyles = makeStyles((theme) => ({
  root: {
    width: 600,
    marginTop: theme.spacing(0),
    marginBottom: theme.spacing(1),
    backgroundColor: '#1b2452',
  },
  media: {
    height: 140,
  },
  title: {
    color: 'white',
    marginLeft: theme.spacing(2),
  },
  content: {
    color: 'white',
    marginLeft: theme.spacing(7),
    marginBottom: theme.spacing(1),
  },
  displayTitle:{
    display: 'inline-flex'
  },
  imgPost:{
    maxWidth: 450,
  },
  contImg:{
    alignItems: 'center',
    textAlign: 'center',
    marginBottom: theme.spacing(1),
  },
}));

function Reply() {
  const classes = useStyles();
  return (
    <Card className={classes.root}>
      <CardActionArea >
        <CardContent>

          <div className={classes.displayTitle}>
            <Avatar id='avatarUser' src=''/>

            <Typography id='userTag' variant='h6' component='h2' className={classes.title}>
              <strong>Usuario @Tag</strong>
            </Typography>

          </div>

          <Typography id='contentP' variant='body2' component='p' className={classes.content}>
            Comentario del post
          </Typography>

          <div className={classes.contImg}>
            <img className={classes.imgPost} id='imagenP' src='https://cnnespanol.cnn.com/wp-content/uploads/2019/05/190524002834-20190524-boss-texting-super-tease.jpg?quality=100&strip=info&w=460&h=260&crop=1'/>
          </div>
          
        </CardContent>
      </CardActionArea>
    </Card>
  );
}

export default Reply;
