import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import { red } from "@material-ui/core/colors";
import FavoriteIcon from "@material-ui/icons/Favorite";
import QuestionAnswerIcon from "@material-ui/icons/QuestionAnswer";
import ReplyAllIcon from "@material-ui/icons/ReplyAll";
import Avatar from "@material-ui/core/Avatar";

const useStyles = makeStyles((theme) => ({
  root: {
    width: 600,
    marginBottom: theme.spacing(5),
    backgroundColor: '#2b387f',
  },
  media: {
    height: 140,
  },
  cardBtn: {
    alignItems: 'center',
    justifyContent: 'space-around',
    color: 'white',
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
  likeIcon:{
    color: red[500],
  },
  commentIcon:{
    color: '#ea5970',
  },
  repostIcon:{
    color: '#f28a9a',
  },
  displayTitle:{
    display: 'inline-flex'
  },
  imgPost:{
    maxWidth: 450,
  },
  contImg:{
    alignItems: 'center',
    textAlign: 'center',
  },
}));

function CardPost() {
  const classes = useStyles();
  return (
    <Card className={classes.root}>
      <CardActionArea >
        <CardContent>

          <div className={classes.displayTitle}>
            <Avatar id="avatarUser" src=""/>

            <Typography id="userTag" variant="h6" component="h2" className={classes.title}>
              <strong>Usuario @Tag</strong>
            </Typography>

          </div>

          <Typography id="contentP" variant="body2" component="p" className={classes.content}>
            Contenido del post
          </Typography>

          <div className={classes.contImg}>
            <img className={classes.imgPost} id="imagenP" src="https://postcron.com/es/blog/wp-content/uploads/2015/05/post_facebook.jpg"/>
          </div>
          

        </CardContent>
      </CardActionArea>

      <CardActions disableSpacing className={classes.cardBtn}>
        <div id="likeNum">
          <IconButton>
            <FavoriteIcon className={classes.likeIcon}/>
          </IconButton>
          10
        </div>

        <div id="commentNum">
          <IconButton>
            <QuestionAnswerIcon className={classes.commentIcon}/>
          </IconButton>
          5
        </div>

        <div id="repostNum">
          <IconButton>
            <ReplyAllIcon className={classes.repostIcon}/>
          </IconButton>
          2
        </div>
      </CardActions>
    </Card>
  );
}

export default CardPost;
