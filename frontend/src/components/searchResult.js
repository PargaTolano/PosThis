import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import { makeStyles } from "@material-ui/core/styles";
import backapp3 from "assets/backapp3.png";
import CardPost from "./Post/CardPost";
import UseCard from "components/Search/userCard";

import { getPost } from 'API/Post.API';
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
  },
}));

const SearchResult = ( props ) => {
  //useCheckAuth();
  const classes = useStyles();

  const [ ready, response ] = useRequestLoadOnMount( async () => await getPost(6) );
  console.log(response);

  return (
    <div className={classes.Background}>
      <SearchAppBar />
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Resultado de Búsqueda</strong>
        </div>
      </div>
      <div className={classes.ucHolder}>
        <UseCard />
        <UseCard />
        <UseCard />
        <UseCard />
      </div>
      <div className={classes.cardHolder}>
        <div component="h4" variant="h2" className={classes.titleBegin}>
          <strong>Posts/Hashtags Relacionados</strong>
        </div>
        {
          ready && <CardPost post={response.data}/>
        }
      </div>
    </div>
  );
};

export default SearchResult;
