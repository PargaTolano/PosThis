import React,{useState, useEffect, useRef} from 'react';

import { makeStyles } from '@material-ui/core/styles';
import ImageIcon      from '@material-ui/icons/Image';
import CancelIcon     from '@material-ui/icons/Cancel';
import { createPost } from 'API/Post.API';

import CPostModel     from 'model/CPostModel';

import IconButton     from "@material-ui/core/IconButton";

import {
  TextField,
  Button,
  Icon,
} from "@material-ui/core";

import { authenticationService } from '_services';
import { fileToBase64 } from '_utils';

const useStyles = makeStyles((theme) => ({
  form: {
    background:'white',
    width: '100%',
    height:'40%',
    padding:theme.spacing(3),
    borderRadius: 10,  
    alignItems: 'center',
  },
  submit: {
    width: '20%',
  },
  cardBtn: {
    alignItems: 'center',
    justifyContent: 'space-around',
    color: 'white',
    alignSelf:'center',
  },
  titleForm: {
    justifyContent: 'space-around',
    color: theme.palette.primary.dark ,
    alignSelf:'center',
    color:'#ea5970',
  },
  input: {
    display: 'none',
  },
  previewGrid: {
    display:        'flex',
    width:          '100%',
    justifyContent: 'center',
    flexWrap:       'wrap',
    borderRadius:   '10px',
    boxShadow:      'black 0px 0px 2px',
    overflow:       'hidden',
  },
  previewContainer:{
    display:    'inline-block',
    flexGrow:   '1',
    width:      '50%',
    height:     '180px',
  },
  previewImage:{
    postion:    'absolute',
    top:        '0',
    left:       '0',
    display:    'inline-block',
    width:      '100%',
    height:     '100%',
    objectFit:  'cover'
  },
  closePreviewIcon:{
    position: 'absolute',
    zIndex: 1000,
    root:{
      "&:hover $icon": {
        color: 'blue',
      }
    }
  }
}));

const GridImage =( props )=>{

  const { classes, image, index, images, setImages } = props;
  const { preview, file } = image;

  const deleteImage = ()=>{
    const i = images.indexOf( image );
    setImages( x => x.filter( (elem,index)=> index != i ) );
  };

  return (
    <div className={classes.previewContainer}>
      <IconButton className={classes.closePreviewIcon} color="secondary" aria-label="upload picture" component="span" onClick={deleteImage}>
        <CancelIcon/>
      </IconButton>
      <img src={preview} className={classes.previewImage}/>
    </div>
  );

};

function CreatePost(props) {

  const {auth} = props;
  
  const [images, setImages]   = useState( [] );
  const [content, setContent] = useState( '' );
  const [count, setCount]     = useState( 0  );

  const inputFileRef = useRef(null);
    
  useEffect(()=>{
    //console.log(count);
    //setTimeout(()=>setCount(c=>c+1), 1000);
  },[count]);
  
  const classes = useStyles();
  
  const onSubmit = async ( e )=>{
    e.preventDefault();
    let res = await createPost( new CPostModel({userID: authenticationService.currentUserValue.id, content,files: images.map(x=>x.file)})) ;
    setImages([]);
    setContent('');
  };

  const onChange = async ( e )=>{
    let { files } = e.target;

    if( images.length + files.length > 4 )
      return;

    const filePairs = [];
    for ( let i = 0; i < files.length; i++ ){
      let file = files[i];
      let preview = await fileToBase64( file );
      filePairs.push( {
        file,
        preview
      });
    }

    setImages( x=> [...x,...filePairs] );
  };

  const mediaBtnOnClick = () =>inputFileRef.current?.click();

  return (

    <form className={classes.form} noValidate onSubmit={onSubmit}>
        <div component='h4' variant='h2' className={classes.titleForm}>
          <strong>Nuevo PosThis!</strong>
        </div>
        <TextField
          variant="outlined"
          margin="normal"
          multiline
          rows={3}
          rowsMax={3}
          fullWidth
          id="postContent"
          label="Escribir..."
          name="postContent"
          autoComplete="postContent"
          autoFocus
          value = {content}
          className={classes.postContent}
          onChange ={e=>setContent(e.target.value)}
        />

      <div className={classes.previewGrid}>
        {
          images.map( (image, i)=>( <GridImage
                                      key={i}
                                      index={i}
                                      image={image}
                                      images={images}
                                      setImages={setImages}
                                      classes={classes}
                                    />))
        }
      </div>

      <div className = {classes.cardBtn}>
      
        <Button
        type="submit"
        fullWidth
        variant="contained"
        color="primary"
        className={classes.submit}
        >
          Publicar
        </Button>
          
        <input accept="image/*" className={classes.input} type="file" multiple ref={inputFileRef} onChange={onChange}/>
        <label htmlFor="icon-button-file">
          <IconButton color="primary" aria-label="upload picture" component="span" onClick={mediaBtnOnClick}>
            <ImageIcon className={classes.imageIcon}/>
          </IconButton>
        </label>
        
        </div>
    </form>
       

  );
}
export default CreatePost;