import React, { useState } from 'react';
import { makeStyles }     from '@material-ui/core/styles';
import Card               from '@material-ui/core/Card';
import CardActionArea     from '@material-ui/core/CardActionArea';
import CardActions        from '@material-ui/core/CardActions';
import CardContent        from '@material-ui/core/CardContent';
import Grid               from '@material-ui/core/Grid';
import Typography         from '@material-ui/core/Typography';
import IconButton         from '@material-ui/core/IconButton';
import { red }            from '@material-ui/core/colors';
import FavoriteIcon       from '@material-ui/icons/Favorite';
import QuestionAnswerIcon from '@material-ui/icons/QuestionAnswer';
import ReplyAllIcon       from '@material-ui/icons/ReplyAll';
import Avatar             from '@material-ui/core/Avatar';
import { Link }           from 'react-router-dom';
import SaveIcon           from '@material-ui/icons/Save';
import Button             from '@material-ui/core/Button';

const defaultImage = 'https://www.adobe.com/express/create/media_1900d303a701488626835756419ca3a50b83a2ae5.png?width=2000&format=webply&optimize=medium';

const useStyles = makeStyles((theme) => ({
  root: {
    width: 600,
    marginBottom: theme.spacing(3),
    backgroundColor: '#2b387f',
    [theme.breakpoints.down('sm')]:{
      width: 'auto'
    }
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
  contentEdit: {
    color: 'white',
    marginBottom: theme.spacing(1),
    background: 'transparent',
    outline: 'none',
    width: '100%',
    height: '70px',
    resize: 'none',
    textDecoration: 'underline',
    boxSizing: 'border-box',
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
  saveIcon:{
    color: '#33eaff',
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
    [theme.breakpoints.down('sm')]:{
      height: '300px'
    }
  },
  media:{
    display: 'inline-block',
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate( -50%, -50%)',
    width: '100%',
  },
  usertag:{
    color: 'white',
  },
  displaybtn:{
    textAlign: 'right',
    marginTop: theme.spacing(2),

  }

}));

const useMediaGridStyles = makeStyles((theme)=>({
  gridHeight: {
    height: '400px',
    overflow: 'hidden',
    borderRadius: theme.spacing(1)
  },
  subGridContainer:{
    height: '100%'
  },
  subGrid:{
    height: '100%'
  },
  subGridRow:{
    height: '100%'
  },
  mediaContainer:{
    display:  'inline-block',
    width:    '100%',
    height:   '100%',
    overflow: 'hidden',
    backgroundColor: '#333333'
  },
  mediaFit: {
    display:  'inline-block',
    width: '100%',
    height: '100%',
    objectFit: 'cover'
  }
}));

function MediaGrid( props ){

  const classes = useMediaGridStyles();
  const { media } = props;

  if ( media?.length === 0 )
    return (<></>);

  return (
    <>
    <Grid container className={classes.gridHeight}>
      <Grid item xs={ media === 1 ? 12 : 6 } className={classes.subGridContainer}>
        <Grid container className={classes.subGrid}>
            <Grid item xs={ 12 } className={classes.subGridRow}>
              <div className={classes.mediaContainer}>
                {
                media[0].isVideo ?
                (
                  <video className={classes.mediaFit} controls>
                      <source src={media[0].path} type={media[0].mime}/>
                  </video>
                )
                :
                (<img src={media[0].path} className={classes.mediaFit}/>)
              }
            </div>
            </Grid>
            {
              media.length === 4 
              &&
              (
                <Grid item xs={ 12 } className={classes.subGridRow}>
                <div className={classes.mediaContainer}>
                  {
                    media[2].isVideo ?
                    (
                      <video className={classes.mediaFit} controls>
                          <source src={media[2].path} type={media[2].mime}/>
                      </video>
                    )
                    :
                    (<img src={media[2].path} className={classes.mediaFit}/>)
                  }
                </div>
              </Grid>   
              )
            }
        </Grid>
      </Grid>
      <Grid item xs={ media === 1 ? 12 : 6 } className={classes.subGridContainer}>
        <Grid container className={classes.subGrid}>
        {
          media.length > 1
          &&
          (
            <Grid item xs={ 12 } className={classes.subGridRow}>
            <div className={classes.mediaContainer}>
              {
              media[1].isVideo ?
              (
                <video className={classes.mediaFit} controls>
                    <source src={media[1].path} type={media[1].mime}/>
                </video>
              )
              :
              (<img src={media[1].path} className={classes.mediaFit}/>)
            }
            </div>
          </Grid>   
          )
          }
        
          {
          ( media.length > 2) 
          &&
          (
            <Grid item xs={ 12 } className={classes.subGridRow}>
            <div className={classes.mediaContainer}>
              {
              media[3].isVideo ?
              (
                <video className={classes.mediaFit} controls>
                    <source src={media[3].path} type={media[3].mime}/>
                </video>
              )
              :
              (<img src={media[3].path} className={classes.mediaFit}/>)
            }
            </div>
          </Grid>   
          )
        }
        </Grid>
      </Grid>
    </Grid>
    </>
  );
};

function CardPost( props ) {

  const { post, uid, editMode} = props;
  const id = post.publisherId;
  let [editValue, setEditValue] = useState(post.content);

  const classes = useStyles();
  return (
    <Card className={classes.root}>
      <CardActionArea >
      <CardContent>
          <div className={classes.displayTitle}>
            <Avatar id='avatarUser' src={post.publisherProfilePic || defaultImage}/>
            <Typography id='userTag' variant='h6' component='h2'>
              <Link to={`/profile/${5}`} className={classes.title}>
                <strong>{post.publisherUserName} {"@"+post.publisherTag}</strong>
              </Link>
            </Typography>

          </div>
          <Typography id='contentP' variant='body2' component='p' className={classes.content}>

            {
              editMode ? 
              (<textarea className={classes.contentEdit} value={editValue} onChange={e=>setEditValue(e.target.value)}></textarea>)
              :
              (post.content)
            }
          </Typography>
          <div className={classes.contMedia}>
            <MediaGrid media={post.medias}/>
          </div>
          <div  className={classes.displaybtn}>
          
          <Button variant='contained' color='secondary'>
            Edit Post
          </Button>
          </div>
          
        </CardContent>
      </CardActionArea>

      <CardActions disableSpacing className={classes.cardBtn}>
        <div id='likeNum'>
          <IconButton>
            <FavoriteIcon className={classes.likeIcon}/>
          </IconButton>
          {post.likeCount}
        </div>
        <div id='commentNum'>
          
          <Link to={`/DetailPost`}>
          <IconButton>
            <QuestionAnswerIcon className={classes.commentIcon}/>
          </IconButton>
          </Link>
          {post.replyCount}
        </div>

        <div id='repostNum'>
          <IconButton>
            <ReplyAllIcon className={classes.repostIcon}/>
          </IconButton>
          {post.repostCount}
        </div>

        <div id='saveedit'>
          <IconButton>
            <SaveIcon className={classes.saveIcon}/>
          </IconButton>
        </div>

      </CardActions>
    </Card>
  );
}

export default CardPost;
