import React, { useState, useRef}  from 'react';
import { Link }                    from 'react-router-dom';

import { makeStyles }     from '@material-ui/core/styles';

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

import { authenticationService }    from '_services';
import { handleResponse, history }  from '_helpers';
import { fileToBase64, routes }     from '_utils';
import { updateReply, deleteReply } from '_api';
import { UReplyModel }              from '_model';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    maxWidth: '600px',
    marginTop: theme.spacing(0),
    borderBottomStyle: 'solid',
    borderBottomColor: 'white',
    borderBottomWidth: theme.spacing(0.3),
    backgroundColor: '#1b2452',
    borderRadius: theme.spacing( 0 ),
    boxShadow:        'black 1px 1px 8px',
  },
  rootFirst:{
    width: '100%',
    maxWidth: '600px',
    marginTop: theme.spacing(0),
    borderBottomStyle: 'solid',
    borderBottomColor: 'white',
    borderBottomWidth: theme.spacing(0.3),
    backgroundColor: '#1b2452',
    borderRadius: theme.spacing( 1, 1, 0, 0 ),
    boxShadow:        'black 1px 1px 8px',
  },
  rootLast:{
    width: '100%',
    maxWidth: '600px',
    marginTop: theme.spacing(0),
    borderBottomStyle: 'solid',
    borderBottomColor: 'white',
    borderBottomWidth: theme.spacing(0),
    backgroundColor: '#1b2452',
    borderRadius: theme.spacing( 0, 0, 1, 1 ),
    boxShadow:        'black 1px 1px 8px',
  },
  rootOnly:{
    width: '100%',
    maxWidth: '600px',
    marginTop: theme.spacing(0),
    borderBottomStyle: 'solid',
    borderBottomColor: 'white',
    borderBottomWidth: theme.spacing(0),
    backgroundColor: '#1b2452',
    borderRadius: theme.spacing( 2 ),
    boxShadow:        'black 1px 1px 8px',
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
  input:{
    display: 'none'
  },
  displayTitle:{
    display: 'inline-flex'
  },
  displaybtn:{
    textAlign: 'right',
    marginTop: theme.spacing(2),
  },
  imgPost:{
    maxWidth: 450,
  },
  contImg:{
    alignItems: 'center',
    textAlign: 'center',
    marginBottom: theme.spacing(1),
  },
  contMedia:{
    display:        'flex',
    flexDirection:  'column',
    flexWrap:       'wrap'
  },
  date:{
    fontSize: '0.7rem',
    color:    'gray',
    display:  'block'
  },
  avatar:{
    backgroundColor: '#333333',
    border: '1px solid #f28a9a'
  },
  cardBtn: {
    alignItems:     'center',
    justifyContent: 'space-around',
    color:          'white',
  },
  saveIcon:{
    color: '#33eaff',
  },
  mediaIcon:{
    color: '#ea5970'
  },
  deleteBtn:{
    marginRight: theme.spacing(2)
  }
}));

export const ReplyCard = ( props ) => {

  const { reply, first, last }    = props;
  const classes = useStyles();
  const inputFileRef = useRef();

  const [ state, setState ] = useState({
    editMode:         false,
    content:          reply.content,
    originalContent:  reply.content,
    medias:           reply.medias,
    originalMedias:   reply.medias,
    deleted:          [],
    newMedias:        []
  });
  const [del, setDel] = useState(false);

  const temp = { state, setState};

  let cardRootClass;
  if( first && last ){
    cardRootClass = classes.rootOnly;
  }
  else if( first ){
    cardRootClass = classes.rootFirst;
  }
  else if( last ){
    cardRootClass = classes.rootLast;
  }
  else{
    cardRootClass = classes.root;
  }

  const dateString = new Date( Date.parse( reply.date ) ).toLocaleString();

  const onClickSave = ()=>{

    let model = new UReplyModel({
      content: state.content,
      deleted: state.deleted,
      files:   state.newMedias });

    updateReply( reply.replyID, model)
      .then( handleResponse )
      .then( res => {
        let { data } = res;

        setState(x=>{
          let copy              = {...x};
          copy.editMode         = false;
          copy.originalContent  = data.content;
          copy.medias           = data.medias;
          copy.originalMedias   = data.medias;
          return copy;
        });
      })
      .catch(console.warn);
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
    if(window.confirm('Seguro que quiere borrar la respuesta')){
      deleteReply(reply.replyID)
      .then(handleResponse)
      .then( res =>{
        setDel( true );
      })
      .catch(console.warn);
    }
  };

  if( del ){
    return <></>
  }

  return (
    <Card className={cardRootClass}>
        <CardContent>

          <div className={classes.displayTitle}>
            <Link to={routes.getProfile(reply.publisherID)}>
              <Avatar className={classes.avatar} src={reply.publisherProfilePic}/>
            </Link>

            <Link to={routes.getProfile(reply.publisherID)}>
              <Typography variant='h6' component='h2' className={classes.title}>
                <strong>{reply.publisherUserName} {'@'+reply.publisherTag}</strong>
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
            <MediaGrid media={ state.medias } {...temp}/>
          </div>
          
          <div  className={classes.displaybtn}>
          {
            (authenticationService.currentUserValue.id === reply.publisherID)
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
            (authenticationService.currentUserValue.id === reply.publisherID && state.editMode)
            && 
            <div>
              <IconButton onClick   = { onClickSave }>
                <SaveIcon className = { classes.saveIcon}/>
              </IconButton>
            </div>
          }
        </CardActions>
    </Card>
  );
};

export default ReplyCard;