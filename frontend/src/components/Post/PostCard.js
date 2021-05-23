import React, { useState, useRef }  from 'react';
import { Link }                     from 'react-router-dom';

import { makeStyles }               from '@material-ui/core/styles';
import { red }                      from '@material-ui/core/colors';

import {
  Card,
  CardActions,
  CardContent,
  Typography,
  IconButton,
  Button,
  Avatar,
} from '@material-ui/core';

import {
  Favorite as FavoriteIcon,
  QuestionAnswer as QuestionAnswerIcon,
  ReplyAll       as ReplyAllIcon,
  Save           as SaveIcon,
  Image          as ImageIcon,

} from '@material-ui/icons';

import { MediaGrid }                from 'components/Media';

import { handleResponse }  from '_helpers';
import { authenticationService }    from '_services';
import { routes, fileToBase64 }     from '_utils';

import { 
  updatePost,
  deletePost,
  createLike,
  deleteLike,
  createRepost,
  deleteRepost
} from '_api';

import{
  UPostModel,
  LikeViewModel,
  RepostViewModel
} from '_model';

const useStyles = makeStyles((theme) => ({
  root: {
    width:            '100%',
    maxWidth:         '600px',
    marginTop:         theme.spacing( 3 ),
    backgroundColor:  '#2b387f',
    boxShadow:        'black 1px 1px 8px',
    borderRadius:     theme.spacing( 1, 1, 0, 0 ),
    [theme.breakpoints.down('sm')]:{
    }
  },
  media: {
    height: '140px',
  },
  cardBtn: {
    alignItems:     'center',
    justifyContent: 'space-around',
    color:          'white',
  },
  repostText:{
    color:    'white',
    padding:  theme.spacing(2, 4)
  },
  title: {
    display:        'inline-block',
    width:          'auto',
    color:          'white',
    marginLeft:     theme.spacing(2),
    textDecoration: 'none',
    '&:visited':{
      color: 'white',
    }
  },
  content: {
    color:        'white',
    marginLeft:   theme.spacing(7),
    marginBottom: theme.spacing(1),
    textIndent:   '0',
    whiteSpace:   'pre-wrap'
  },
  contentNoMedia:{
    color:        'white',
    marginLeft:   theme.spacing(7),
    marginBottom: theme.spacing(1),
    fontSize:     '1.3rem',
    textIndent:   '0',
    whiteSpace:   'pre-wrap'
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
    whiteSpace:     'pre-wrap'
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
  },
  date:{
    fontSize: '0.7rem',
    color:    'gray',
    display:  'block'
  },
  grayIcon:{
    color: 'gray'
  },
  avatar:{
    backgroundColor: '#333333',
    border: '1px solid #f28a9a'
  },
  repostUserLink:{
    color: 'white',
    textDecoration: 'none',
    '&:visited':{
      color: 'white'
    },
    '&:hover':{
      color: '##ea5970'
    }
  },
  deleteBtn:{
    marginRight: theme.spacing(2)
  }
}));

export const PostCard = ( props ) => {

  const { history } = props;

  const [ post, setPost ] = useState( props.post );

  const [ state, setState ] = useState({
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
        isVideo:  file.type.includes('video')
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

  const onClickLike = ()=>{

    const model = new LikeViewModel({
      postID: post.postID, 
      userID: authenticationService.currentUserValue.id 
     });

    if( post.isLiked ){
      deleteLike(model)
       .then(handleResponse)
       .then( res =>{
         const { data } = res;
         setPost( data );
         setState( x=>{
          let copy = {...x};
          copy.content         = data.content;
          copy.originalContent = data.content;
          copy.medias          = data.medias;
          copy.originalMedias  = data.medias;
          return copy;
        });
       })
       .catch(console.warn)
    }
    else{
      createLike(model)
        .then(handleResponse)
        .then(res=>{
          const { data } = res;
          setPost( data );
          setState( x=>{
            let copy = {...x};
            copy.content         = data.content;
            copy.originalContent = data.content;
            copy.medias          = data.medias;
            copy.originalMedias  = data.medias;
            return copy;
          });
        })
        .catch(console.warn);
    }
  };

  const onClickRepost = ()=>{

    const model = new RepostViewModel({
      postID: post.postID, 
      userID: authenticationService.currentUserValue.id 
     });

    if( post.isReposted){
      deleteRepost(model)
      .then(handleResponse)
      .then ( res =>{
        const { data } = res;
        setPost( data );
        setState( x=>{
          let copy = {...x};
          copy.content         = data.content;
          copy.originalContent = data.content;
          copy.medias          = data.medias;
          copy.originalMedias  = data.medias;
          return copy;
        });
      })
      .catch(console.warn);
    }
    else{
      createRepost(model)
      .then(handleResponse)
      .then ( res =>{
        const { data } = res;
        setPost( data );
        setState( x=>{
          let copy = {...x};
          copy.content         = data.content;
          copy.originalContent = data.content;
          copy.medias          = data.medias;
          copy.originalMedias  = data.medias;
          return copy;
        });
      })
      .catch(console.warn);
    }
  };

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

  const onClickDelete = ()=>{
    if(window.confirm('Seguro que quiere borrar el post')){
      deletePost(post.postID)
      .then(handleResponse)
      .then(res=>{
        history.replace(routes.feed);
      })
      .catch(console.warn);
    }
  };

  const dateString = new Date(Date.parse( post.date )).toLocaleString();

  return (
    <Card className={classes.root}>
      {
        post.isRepost &&
        <Typography variant='body2' className={classes.repostText}>
          <Link to={routes.getProfile(post.publisherID)} className={classes.repostUserLink}>{post.reposterUserName}</Link> reposted this!
        </Typography>
      }
      <CardContent>
          <div className={classes.displayTitle}>
            <Link to={routes.getProfile(post.publisherID)}>
              <Avatar src={post.publisherProfilePic} className={classes.avatar}/>
            </Link>
            <Link to={routes.getProfile(post.publisherID)}>
              <Typography variant='h6' component='h2' className={classes.title}>
                <strong>{post.publisherUserName} {'@'+post.publisherTag}</strong>
              </Typography>
            </Link>
          </div>
          <p className={classes.date}>{ dateString.slice( 0, dateString.length - 6 ) } { dateString.slice(dateString.length-3) }</p>
          
          {
            state.editMode ? 
            (<textarea className={classes.contentEdit} value={state.content} onChange={onChangeContent}></textarea>)
            :
            (<Typography variant='body2' component='p' className={state.medias.length !== 0 ? classes.content : classes.contentNoMedia} >{state.originalContent}</Typography>)
          }
          
          {
            state.editMode &&
            <>
            <input accept='image/*' className={classes.input} type='file' multiple ref={inputFileRef} onChange={onChangeImages}/>
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
            <>
              <Button 
                variant='contained' 
                color='secondary' 
                onClick={onClickDelete}
                className={classes.deleteBtn}
              >
                  Eliminar
              </Button>
              <Button 
                variant='contained' 
                color='secondary' 
                onClick={onToggleEditMode}
              >
                { state.editMode ? 'Cancelar' : 'Editar' }
              </Button>
            </>
          }
          </div>
        </CardContent>

      <CardActions disableSpacing className={classes.cardBtn}>

        {
          (!state.editMode)
          && 
          <div>
            <IconButton onClick={onClickLike}>
              <FavoriteIcon className={ post.isLiked ? classes.likeIcon : classes.grayIcon }/>
            </IconButton>
            {post.likeCount}
          </div>
        }
        
        {
          (!state.editMode)
          && 
          <div>
            <Link to={routes.getPost(post.postID || 1)}>
              <IconButton>
                <QuestionAnswerIcon className={classes.commentIcon}/>
              </IconButton>
            </Link>
            {post.replyCount}
          </div>
        }
        
        {
          (!state.editMode)
          && 
          <div>
            <IconButton onClick={onClickRepost}>
              <ReplyAllIcon className={ post.isReposted ? classes.repostIcon : classes.grayIcon }/>
            </IconButton>
            {post.repostCount}
          </div>
        }

        {
          (authenticationService.currentUserValue.id === post.publisherID && state.editMode)
          && 
          <div>
            <IconButton onClick={onClickSave}>
              <SaveIcon className={classes.saveIcon}/>
            </IconButton>
          </div>
        }

      </CardActions>
    </Card>
  );
}

export default PostCard;