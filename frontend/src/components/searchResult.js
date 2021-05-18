import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import { makeStyles } from "@material-ui/core/styles";
import backapp3 from "assets/backapp3.png";
import CardPost from "./Post/CardPost";
import UserCard from "components/Search/UserCard";

import SearchRequestModel from 'model/SearchRequestModel';

import useMakeSearch from 'hooks/useMakeSearch';

const useStyles = makeStyles( ( theme ) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundAttachment: "fixed",
    backgroundRepeat: "no-repeat",
    minHeight: "100vh",
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

  const {auth, match, history } = props;

  const { query } = match.params;

  const classes = useStyles();
  const [ready, response] = useMakeSearch(query || '');
  
  return (
    <div className={classes.Background}>
      <SearchAppBar  auth={auth} history={history}/>
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Resultado de Búsqueda</strong>
        </div>
      </div>
      <div className={classes.ucHolder}>
        {
          ready && ( response.users?.map( user =><UserCard key={user.publisherID} user={user} auth={auth}/>) )
        }
        
      </div>
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Posts/Hashtags Relacionados</strong>
        </div>
        {
          ready && ( response.posts?.map( post =><CardPost key={post.postId} post={post} auth={auth}/>) )
        }
      </div>
    </div>
  );
};

export default SearchResult;