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
import {routes}           from '_utils';
import SaveIcon           from '@material-ui/icons/Save';
import Button             from '@material-ui/core/Button';

import { authenticationService }                  from '_services';

const defaultImage = 'https://www.adobe.com/express/create/media_1900d303a701488626835756419ca3a50b83a2ae5.png?width=2000&format=webply&optimize=medium';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: '600px',
    marginBottom: theme.spacing(3),
    backgroundColor: '#2b387f',
    boxShadow: 'gray 1px 1px 5px',
    [theme.breakpoints.down('sm')]:{
    }
  },
  media: {
    height: '140px',
  },
  cardBtn: {
    alignItems: 'center',
    justifyContent: 'space-around',
    color: 'white',
  },
  title: {
    color: 'white',
    marginLeft: theme.spacing(2),
    textDecoration: 'none',
    '&:visited':{
      color: 'white',
    }
  },
  content: {
    color:        'white',
    marginLeft:   theme.spacing(7),
    marginBottom: theme.spacing(1),
  },
  contentEdit: {
    color:          'white',
    marginBottom:   theme.spacing(1),
    background:     'transparent',
    outline:        'none',
    width:          '100%',
    height:         '70px',
    resize:         'none',
    textDecoration: 'underline',
    boxSizing:      'border-box',
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
    maxWidth: '450',
  },
  contMedia:{
    display:        'flex',
    flexDirection:  'column',
    flexWrap:       'wrap'
  },
  contImg:{
    display:      'inline-block',
    flexGrow:     1,
    alignItems:   'center',
    textAlign:    'center',
  },
  mediaMask:{
    display:      'inline-block',
    position:     'relative',
    width:        '100%',
    height:       '400px',
    overflow:     'hidden',
    [theme.breakpoints.down('sm')]:{
      height: '300px'
    }
  },
  media:{
    display:    'inline-block',
    position:   'absolute',
    top:        '50%',
    left:       '50%',
    transform:  'translate( -50%, -50%)',
    width:      '100%',
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
}));

const useSubgridStyles = makeStyles((theme) => ({
  subgridFull:{
    display:  'flex',
    width:    '100%',
    height:   '100%',
    flexDirection: 'column'
  },
  subgridHalf:{
    display:  'flex',
    width:    '50%',
    height:   '100%',
    flexDirection: 'column'
  },
  subGridNone:{
    display: 'none'
  }
}));

const useMediaContainerStyles= makeStyles((theme) => ({
  mediaContainer:{
    display:  'inline-block',
    flex: '1 1 0',
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

function MediaContainer(props) {

  const { media } = props;
  const classes = useMediaContainerStyles();

  return (
    <div className={classes.mediaContainer}>
      {
        media.isVideo ?
        (
          <video className={classes.mediaFit} controls>
              <source src={media.path} type={media.mime}/>
          </video>
        )
        :
        (<img src={media.path} className={classes.mediaFit}/>)
      }
    </div>
  );
}

function SubGrid( props ){

  const {children, size} = props;
  const classes = useSubgridStyles();

  if(children.length === 0)
    return (<></>);

  let subgridClass;
  switch( size ){
    case 'f':
      subgridClass = classes.subgridFull;
      break;
    case 'h':
      subgridClass = classes.subgridHalf;
      break;  
    case 'n':
      subgridClass = classes.subGridNone;
      break;
  }

  return (
    <div className={subgridClass}>
      {children}
    </div>
  );
}

function MediaGrid( props ){

  const classes = useMediaGridStyles();
  const { media } = props;

  if ( media?.length === 0 )
    return (<></>);

  const n = media.length;

  return (
    <Grid container className={classes.gridHeight}>
      <SubGrid size={ n === 1 ? 'f' : 'h' }>
        { media[0] && <MediaContainer media={media[0]}/>}
        { ( n === 4 && media[2] ) && <MediaContainer media={media[2]}/>}
      </SubGrid>
      <SubGrid size={ n > 1 ? 'h' : 'n' }>
        { ( n > 1 && media[1] ) && <MediaContainer media={media[1]}/>}
        { ( n > 2 && ( n === 3 ? media[2] : media[3] )) && <MediaContainer media={ n === 3 ? media[2] : media[3]}/>}
      </SubGrid>
    </Grid>
  );
};

function CardPost( props ) {

  const { post } = props;

  let [editMode , setEditMode] = useState( false );
  let [editValue, setEditValue] = useState(post.content);

  const classes = useStyles();

  return (
    <Card className={classes.root}>
      <CardActionArea >
      <CardContent>
          <div className={classes.displayTitle}>
            <Avatar src={post.publisherProfilePic || defaultImage}/>
            <Link to={routes.getProfile(post.publisherID)}>
              <Typography variant='h6' component='h2' className={classes.title}>
                <strong>{post.publisherUserName} {"@"+post.publisherTag}</strong>
              </Typography>
            </Link>
          </div>
          <Typography variant='body2' component='p' className={classes.content}>
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
          {
            (authenticationService.currentUserValue.id === post.publisherID)
             && 
            <Button variant='contained' color='secondary' onClick={()=>setEditMode(x=>!x)}>
              { editMode ? 'Cancelar' : 'Editar' }
            </Button>
          }
          </div>
        </CardContent>
      </CardActionArea>

      <CardActions disableSpacing className={classes.cardBtn}>
        <div>
          <IconButton>
            <FavoriteIcon className={classes.likeIcon}/>
          </IconButton>
          {post.likeCount}
        </div>
        <div>
          <Link to={routes.getPost(post.postID)}>
            <IconButton>
              <QuestionAnswerIcon className={classes.commentIcon}/>
            </IconButton>
          </Link>
          {post.replyCount}
        </div>

        <div>
          <IconButton>
            <ReplyAllIcon className={classes.repostIcon}/>
          </IconButton>
          {post.repostCount}
        </div>

        {
          (authenticationService.currentUserValue.Id === post.publisherID && editMode)
          && 
          <div>
            <IconButton>
              <SaveIcon className={classes.saveIcon}/>
            </IconButton>
          </div>
        }

      </CardActions>
    </Card>
  );
}

export default CardPost;