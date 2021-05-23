import React, { useState, useEffect } from 'react';

import { makeStyles }                 from '@material-ui/core/styles';
import { CircularProgress }           from '@material-ui/core';

import { NavBar, PostContainer }      from 'components/Inicio';
import { CreatePostForm }             from 'components/Post';

import { handleResponse }             from '_helpers';
import { authenticationService }      from '_services';
import { getFeed }                    from '_api';

import backapp3                       from 'assets/backapp3.png';

const useStyles = makeStyles((theme) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
    minHeight: '100vh'
  },
  titleBegin:{
    color: 'white',
    fontFamily: 'Arial',
    fontStyle: 'normal',
    fontSize: 30,
    width: '100%',
    marginTop: '24px',
    paddingBottom: theme.spacing(3),
    textAlign: 'center',
    flexDirection:'column',
  },
  loading:{
    position:       'fixed',
    display:        'flex',
    alignItems:     'center',
    justifyContent: 'center',
    top:            '0',
    left:           '0',
    width:          '100vw',
    height:         '100vh',
    zIndex:         '1000',
    backgroundColor: '#00000055',
  }
}));

export const Feed = (props) => {

  const { history } = props;
  
  const classes = useStyles();

  const [{ posts, loading }, setState] = useState({
    posts: [],
    loading: false
  });

  const loadFeed = ()=>{
    
    setState(x=>{
      let copy = {...x};
      copy.loading = true;
      return copy;
    });

    getFeed(authenticationService.currentUserValue.id)
      .then(handleResponse)
      .then(res=>{
        const { data } = res;
        setState(x=>{
          let copy = {...x};
          copy.loading = false;
          copy.posts = data;
          return copy;
        });
      })
      .catch(console.warn);
  };

  useEffect(() => {
    loadFeed();
  },[]);

  return (
  <>
    {
      loading &&
      <div className={classes.loading}>
        <CircularProgress color='primary'/>
      </div>
    }
    <div className={classes.Background}>
      <NavBar history={history}/>
      <div component='h4' variant='h2' className={classes.titleBegin}>
        <strong>Crear Post</strong>
      </div>
      <CreatePostForm afterUpdate={loadFeed}/>
      {
        (posts.length !== 0) && <PostContainer posts={posts} history={history}/>
      }
    </div>
  </>
  );
};

export default Feed;
