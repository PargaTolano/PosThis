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
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import ReplyAllIcon from "@material-ui/icons/ReplyAll";
import Avatar from "@material-ui/core/Avatar";

const useStyles = makeStyles((theme) => ({
  root: {
    width: 200,
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
    marginRight: theme.spacing(2),
  },
  content: {
    color: 'white',
    marginLeft: theme.spacing(7),
    marginBottom: theme.spacing(1),
  },
  personAddIcon:{
    color: '#ea5970',
  },
 layTitle:{
    display: 'inline-flex'
  },
  imgPerfil:{
    maxWidth: 150,
    maxHeight: 150,
  },
  contImg:{
    alignItems: 'center',
    textAlign: 'center',
  },
  followNum:{
    alignItems: 'center',
    textAlign: 'center',
    
  }
}));

function CardPerfil() {
  const classes = useStyles();
  return (
    <Card className={classes.root}>
     
        <CardContent>

        <div className={classes.displayTitle}>

          </div>
          <div className={classes.contImg}>
            <img className={classes.imgPerfil} id="imagenP" src="https://image.freepik.com/vector-gratis/perfil-avatar-hombre-icono-redondo_24640-14044.jpg"/>
            <Typography id="userTag" variant="h6" component="h2" className={classes.title}>
              <strong>Usuario @Tag</strong>
              
            </Typography>
          </div>
          

        </CardContent>

     
        <div id="followNum">
        
        <Typography id="followerNum" component="h2" className={classes.title}>
              Followers: 20
        </Typography>
        <Typography id="followingNum"  component="h2" className={classes.title}>
             Following: 45
        </Typography>
        </div>

     

        <CardActions disableSpacing className={classes.cardBtn}>
        <div id="followBtn">
          <IconButton>
            <PersonAddIcon className={classes.personAddIcon}/>
          </IconButton>
          
        </div>
      </CardActions>
    </Card>
  );
}

export default CardPerfil;
