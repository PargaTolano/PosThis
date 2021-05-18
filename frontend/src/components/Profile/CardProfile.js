import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import EditInfo from 'components/EditProfile/EditInfo';
import CustomizedDialog from 'components/EditProfile/dialogEditInfo';
import {
  TextField,
  Button,
} from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
  cardContainer: {
    position:       'absolute',
    display:        'flex',
    top:            theme.spacing( 48.66 ),
    width:          '23%',
    flexDirection:  'column',
    alignContent:   'center',
    alignItems:     'center',
    justifyContent: 'center',
    [theme.breakpoints.down('md')]:{
      width:      '27%',
    },
    [theme.breakpoints.down('sm')]:{
      width:      'auto',
      top:        theme.spacing( 30 ),
      left:       '50%',
      transform:  'translateX(-50%)'
    }
  },
  card: {
    padding:          theme.spacing(2),
    marginBottom:     theme.spacing(5),
    backgroundColor:  '#ea5970',
    borderRadius:     '10px',
    zIndex: '10'
  },
  media: {
    height: 140,
  },
  cardBtn: {
    padding:        '0',
    margin:         '0',
    alignItems:     'center',
    justifyContent: 'space-between',
    color:          'white',
  },
  title: {
    color:    'white',
    fontSize: '1.1rem'
  },
  secondaryTitle: {
    color:    'white',
    fontSize: '0.8rem'
    
  },
  content: {
    color:          'white',
    marginLeft:     theme.spacing(7),
    marginBottom:   theme.spacing(1),
  },
  followContainer:{
    marginBottom: theme.spacing(1)
  },
  layTitle:{
    display: 'inline-flex'
  },
  profilePicture:{
    width:        150,
    height:       150,
    objectFit:    'cover',
    borderRadius: '50%'
  },
  contImg:{
    alignItems: 'center',
    textAlign:  'center',
  },
  followNum:{
    alignItems: 'center',
    textAlign:  'center',
  }
}));

const profilePic = "https://image.freepik.com/vector-gratis/perfil-avatar-hombre-icono-redondo_24640-14044.jpg";

function CardProfile( props ) {
  const { user } = props;
  const classes = useStyles();
  return (

    <div className={classes.cardContainer}>
      <Card className={classes.card}>
          
        <CardContent>
          <div className={classes.displayTitle}>
          </div>
          <div className={classes.contImg}>
            <img className={classes.profilePicture} src={profilePic}/>
            <Typography variant="h6" component="h2" className={classes.title}>
              <strong>{user.userName} {'@'+user.tag}</strong>
            </Typography>
          </div>
        </CardContent>
          
        <div className={classes.followContainer}>
          <Typography component="h2" className={classes.secondaryTitle}>
            Followers - {user.followerCount}
          </Typography>
          <Typography component="h2" className={classes.secondaryTitle}>
            Following - {user.followingCount}
          </Typography>
        </div>

        <CardActions disableSpacing className={classes.cardBtn}>
          <div>
            <Button
              fullWidth
              variant="contained"
              color="secondary"
              className={classes.submit}
            >
              Seguir
            </Button>
          </div>
          <CustomizedDialog color={'primary'}>
            <EditInfo/>
          </CustomizedDialog>
        </CardActions>
      </Card>
    </div>
      
  );
}

export default CardProfile;
