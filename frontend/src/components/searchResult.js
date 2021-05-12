import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import { makeStyles } from "@material-ui/core/styles";
import backapp3 from "assets/backapp3.png";
import CardPost from "./Post/CardPost";
import UserCard from "components/Search/UserCard";

import SearchRequestModel from 'model/SearchRequestModel';

import { getPosts } from 'API/Post.API';
import { getSearch } from 'API/User.API';
import useRequestLoadOnMount from "hooks/useRequestLoadOnMount";

const useStyles = makeStyles( ( theme ) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundAttachment: "fixed",
    backgroundRepeat: "no-repeat",
    height: "100%",
  },
  cardHolder: {
    backgroundColor: "transparent",
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(2),
    marginLeft: "auto",
    marginRight: "auto",
    display: "flex",
    alignItems: "center",
    flexDirection: "column",
  },
  titleBegin: {
    color: "white",
    fontFamily: "Arial",
    fontStyle: "normal",
    fontSize: 30,
    width: "100%",
    paddingBottom: theme.spacing(3),
    textAlign: "center",
    flexDirection: "column",
  },
  ucHolder: {
    marginLeft: "auto",
    marginRight: "auto",
    flexWrap: "wrap",
    flexDirection: "row",
    display: "flex",
    alignItems: "center",
    textAlign: "center",
    justifyContent: "center",
    width: 600,
    [theme.breakpoints.down('md')]:{
      width: 'auto'
    }
  },
}));

const SearchResult = ( props ) => {
  //useCheckAuth();
  const classes = useStyles();

  const searchRequest = new SearchRequestModel({
    query: '',
    searchPosts: true, 
    searchUsers: true,
    hashtags: []
  });
  const [ readySearch, responsePosts ] = useRequestLoadOnMount( async ()=> await getSearch(searchRequest) );
  
  return (
    <div className={classes.Background}>
      <SearchAppBar />
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Resultado de Búsqueda</strong>
        </div>
      </div>
      <div className={classes.ucHolder}>
        {
          readySearch && ( responsePosts.data.users.map( user =><UserCard key={user.publisherID} user={user}/>) )
        }
        
      </div>
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Posts/Hashtags Relacionados</strong>
        </div>
        {
          readySearch && ( responsePosts.data.posts.map( post =><CardPost key={post.postId} post={post}/>) )
        }
      </div>
    </div>
  );
};

export default SearchResult;
