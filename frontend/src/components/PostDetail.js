import React                        from 'react';
import { Redirect }                 from 'react-router-dom';

import { makeStyles }               from '@material-ui/core/styles';

import { NavBar }                   from 'components/Inicio';

import {
  PostCard,
  ReplyCard, 
  CreateReplyForm
} from 'components/Post';

import { routes }                   from '_utils';
import { useGetDetailedPost }       from '_hooks';

import backapp3                     from 'assets/backapp3.png';

const useStyles = makeStyles( ( theme ) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
    minHeight: '100vh',
  },
  cardHolder: {
    backgroundColor: 'transparent',
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(2),
    marginLeft: 'auto',
    marginRight: 'auto',
    display: 'flex',
    alignItems: 'center',
    flexDirection: 'column',
  },
  titleBegin:{
    postition: 'sticky',
    display: 'inline-block',
    top: '0',
    color: 'white',
    fontFamily: 'Arial',
    fontStyle: 'normal',
    fontSize: 30,
    width: '100%',
    paddingBottom: theme.spacing(3),
    textAlign: 'center',
    flexDirection:'column',
  },
}));

export const PostDetail = ( props ) => {
  
  const { match, history, ...rest } = props;
  const { id }    = match.params;
  const classes   = useStyles();

  const [[ready, post], setPost, setReplies] = useGetDetailedPost( id );

  if( id == 'undefined' || id === undefined || id === null || id === '' ){
    return (<Redirect to={routes.feed}/>);
  }

  return (
    <div className={classes.Background}>
      <NavBar history={history}/>

    {
      (ready && post )
      &&
      <>
        <div component='h4' variant='h2' className={classes.titleBegin}>
          <strong>Detalle del post</strong>
        </div>
        <div className={classes.cardHolder}>
          <PostCard post={post} history={history}/>
          <CreateReplyForm postId={post?.postID} setReplies={setReplies}/>
          {
            post.replies?.map((reply,i)=>{
              
              let first = i === 0;
              let last  = i === post.replies.length - 1;
              
              return <ReplyCard key={reply.replyID} reply={reply} first={first} last={last} />;
            })
          }
        </div>
      </>
    }
      
      
    </div>
  );
};

export default PostDetail;
