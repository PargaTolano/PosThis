import React from 'react';

import { makeStyles }   from '@material-ui/core/styles';
import Grid             from '@material-ui/core/Grid';
import CancelIcon       from '@material-ui/icons/Cancel';

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

const MediaContainer = (props) => {

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

const SubGrid = ( props ) => {

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

export const MediaGrid = ( props ) => {

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

export default MediaGrid;