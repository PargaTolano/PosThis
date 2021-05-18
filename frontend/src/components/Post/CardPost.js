import React, { useState, useRef } from 'react';
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
import ImageIcon          from '@material-ui/icons/Image';
import CancelIcon         from '@material-ui/icons/Cancel';
import Button             from '@material-ui/core/Button';

import { handleResponse }         from '_helpers';
import { authenticationService }  from '_services';
import { fileToBase64 }           from '_utils';
import { updatePost }             from 'API/Post.API';

import UPostModel from 'model/UPostModel';

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
  mediaIcon:{
    color: '#ea5970'
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
  },
  input:{
    display: 'none'
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
    position:         'relative',
    display:          'inline-block',
    flex:             '1 1 0',
    width:            '100%',
    height:           '100%',
    overflow:         'hidden',
    backgroundColor:  '#333333'
  },
  mediaFit: {
    display:    'inline-block',
    width:      '100%',
    height:     '100%',
    objectFit:  'cover'
  },
  deleteIcon:{
    position: 'absolute',
    top:      0,
    right:    0,
    color:    'red',
    '&:hover': {
      color: 'white'
    },
    
  }
}));

function MediaContainer(props) {

  const { media, state, setState } = props;
  const classes = useMediaContainerStyles();

  const onClickDelete = ()=>{
    const index = state.medias.indexOf(media);
    if ( index === -1 )
      return;

    setState( x =>{
      let copy = {...x};
      copy.deleted = [...copy.deleted, copy.medias[index].mediaID];
      copy.medias = copy.medias.filter((elem, i)=> i !== index);

      console.log( copy.deleted, copy.medias );
      return copy;
    });
  };

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
      {
        state.editMode &&
          <CancelIcon className={classes.deleteIcon} onClick={onClickDelete}/>
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
  const { media, ...temp } = props;

  if ( media?.length === 0 )
    return (<></>);

  const n = media.length;

  return (
    <Grid container className={classes.gridHeight}>
      <SubGrid size={ n === 1 ? 'f' : 'h' }>
        { media[0]                && <MediaContainer media={media[0]} {...temp}/>}
        { ( n === 4 && media[2] ) && <MediaContainer media={media[2]} {...temp}/>}
      </SubGrid>
      <SubGrid size={ n > 1 ? 'h' : 'n' }>
        { ( n > 1 && media[1] )   && <MediaContainer media={media[1]} {...temp}/>}
        { ( n > 2 && ( n === 3 ? media[2] : media[3] )) && <MediaContainer media={ n === 3 ? media[2] : media[3]} {...temp}/>}
      </SubGrid>
    </Grid>
  );
};

/*
  fileEntry = {
    media: base64 String,
    file: File,
    id: Number?,
  }
*/

function CardPost( props ) {

  const { post } = props;

  let [ state, setState ] = useState({
    editMode:         false,
    content:          post.content,
    originalContent:  post.content,
    medias:           post.medias,
    originalMedias:   post.medias,
    deleted:          [],
    newMedias:        []
  });
  const classes = useStyles();
  const inputFileRef = useRef();

  const temp = { state, setState };

  const onClickSave = ()=>{
    updatePost( post.postID,  new UPostModel({content: state.content, deleted: state.deleted, files: state.newMedias}))
      .then( handleResponse )
      .then( res =>{

        let { data } = res;

        setState(x=>{
          let copy              = {...x};
          copy.editMode         = false;
          copy.originalContent  = copy.content;
          copy.medias           = data.medias;
          copy.originalMedias   = data.medias;
          return copy;
        });
      })
      .catch( res =>{
        console.log('err',res)
      });
  };
  const onChangeContent = e=>{
      setState( x =>{
        let copy = {...x};
        copy.content = e.target.value;
        return copy;
      });
  };
  const onChangeImages = async e=>{
    let {files} = e.target;

    if( state.medias.length + files.length > 4 )
      return;

    const mediaInfo    = [];
    const newMediaFiles = [];
    for ( let i = 0; i < files.length; i++ ){
      let file = files[i];
      let preview = await fileToBase64( file );

      const mediaViewModel = {
        mediaID:  null,
        mime:     file.type,
        path:     preview,
        isVideo:  file.type.includes("video")
      };

      mediaInfo     .push( mediaViewModel );
      newMediaFiles .push( file );
    }

    setState( x=>{
      let copy = {...x};
      copy.medias = [...copy.medias, ...mediaInfo];
      copy.newMedias = [...copy.newMedias, ...newMediaFiles];
      return copy;
    });
  };
  const onClickFileOpen = ()=>inputFileRef.current?.click();
  const onToggleEditMode = ()=>{
    setState( x =>{
      let copy = {...x};
      copy.editMode = !copy.editMode;

      if( copy.editMode === false ){
        copy.medias     = [...copy.originalMedias];
        copy.content    = copy.originalContent;
        copy.deleted    = [];
        copy.newMedias  = [];
      }
      return copy;
    })
  };

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
              state.editMode ? 
              (<textarea className={classes.contentEdit} value={state.content} onChange={onChangeContent}></textarea>)
              :
              (state.originalContent)
            }
          </Typography>
          {
            state.editMode &&
            <>
            <input accept="image/*" className={classes.input} type="file" multiple ref={inputFileRef} onChange={onChangeImages}/>
              <IconButton onClick={onClickFileOpen}>
                <ImageIcon className={classes.mediaIcon}/>
              </IconButton>
            </>
          }

          <div className={classes.contMedia}>
            <MediaGrid media={ state.editMode ? state.medias : state.originalMedias } {...temp}/>
          </div>

          <div  className={classes.displaybtn}>
          {
            (authenticationService.currentUserValue.id === post.publisherID)
             && 
            <Button variant='contained' color='secondary' onClick={onToggleEditMode}>
              { state.editMode ? 'Cancelar' : 'Editar' }
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
          (authenticationService.currentUserValue.id === post.publisherID && state.editMode)
          && 
          <div>
            <IconButton>
              <SaveIcon className={classes.saveIcon} onClick={onClickSave}/>
            </IconButton>
          </div>
        }

      </CardActions>
    </Card>
  );
}

export default CardPost;