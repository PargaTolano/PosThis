import React from 'react';

import { makeStyles } from '@material-ui/core/styles';

import PostCard       from 'components/Post/PostCard';

const useStyles = makeStyles((theme) => ({
  cardHolder: {
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(3),
    paddingBottom: theme.spacing(1),
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'flex',
    alignItems: 'center',
    flexDirection:'column',
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
  dividerTitle:{
    background: 'white',
    paddingTop: theme.spacing(0),
  }
}));

export const PostContainer = (props) => {

  const {  posts, history } = props;
  const classes = useStyles();
  return (
    <div className={classes.cardHolder}>
      <div component='h4' variant='h2' className={classes.titleBegin}>
        <strong>Recientes</strong>
      </div>
      {
        posts 
        &&
        posts.map(x=><PostCard key={x.postID} post={x} history={history}/>)
      }
        
    </div>
  );
}
export default PostContainer;
