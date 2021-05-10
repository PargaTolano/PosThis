import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import QuestionAnswerIcon from '@material-ui/icons/QuestionAnswer';
import ReplyAllIcon from '@material-ui/icons/ReplyAll';
import Avatar from '@material-ui/core/Avatar';

const useStyles = makeStyles((theme) => ({
  root: {
    width: 600,
    marginBottom: theme.spacing(3),
    backgroundColor: '#2b387f',
  },
  media: {
    height: 140,
  },
  cardBtn: {
    alignItems: 'center',
    justifyContent: 'space-around',
    color: 'white',
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
  likeIcon:{
    color: red[500],
  },
  commentIcon:{
    color: '#ea5970',
  },
  repostIcon:{
    color: '#f28a9a',
  },
  displayTitle:{
    display: 'inline-flex'
  },
  imgPost:{
    maxWidth: 450,
  },
  contMedia:{
    display: 'flex',
    flexDirection: 'column',
    flexWrap: 'wrap'
  },
  contImg:{
    display: 'inline-block',
    flexGrow: 1,
    alignItems: 'center',
    textAlign: 'center',
  },
  mediaMask:{
    display: 'inline-block',
    position: 'relative',
    width: '100%',
    height: '400px',
    overflow: 'hidden',
  },
  media:{
    display: 'inline-block',
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate( -50%, -50%)',
    width: '100%',
  }
}));

function CardPost( props ) {


  const { post } = props;

  const classes = useStyles();
  return (
    <Card className={classes.root}>

      <CardActionArea >
      <CardContent>
          <div className={classes.displayTitle}>
            <Avatar id='avatarUser' src=''/>
            <Typography id='userTag' variant='h6' component='h2' className={classes.title}>
              <strong>{post.userName} {"@"+post.userTag}</strong>
            </Typography>
          </div>
          <Typography id='contentP' variant='body2' component='p' className={classes.content}>
            {post.content}
          </Typography>
          <div className={classes.contMedia}>
            {
              post.medias.map( (mediaViewModel, i) =>(
                <div 
                  key={mediaViewModel.mediaID} 
                  className={classes.contImg}
                  style={
                     post.medias.Length === 4 && i === 0 ?
                      {height: '100%',width: '50%'} : {height: 'auto',width: 'auto'} 
                     }
                >
                  <div className={classes.mediaMask}>
                    {
                      mediaViewModel.isVideo ?
                      (
                        <video className={classes.media} width="320" height="240" controls>
                            <source src={mediaViewModel.path} type={mediaViewModel.mime}/>
                        </video>
                      )
                      :
                      (<img src={mediaViewModel.path} className={classes.media}/>)
                    }
                  </div>
                </div>
              ))
            }
          </div>
        </CardContent>
      </CardActionArea>

      <CardActions disableSpacing className={classes.cardBtn}>
        <div id='likeNum'>
          <IconButton>
            <FavoriteIcon className={classes.likeIcon}/>
          </IconButton>
          10
        </div>
        <div id='commentNum'>
          <IconButton>
            <QuestionAnswerIcon className={classes.commentIcon}/>
          </IconButton>
          5
        </div>
        <div id='repostNum'>
          <IconButton>
            <ReplyAllIcon className={classes.repostIcon}/>
          </IconButton>
          2
        </div>
      </CardActions>
    </Card>
  );
}

export default CardPost;
