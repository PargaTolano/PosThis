import React from 'react';
import {Grid, Typography, makeStyles, Avatar, Link, Button} from '@material-ui/core';
import veryfied from 'assets/veryfied.svg';

const defaultImage = 'https://www.adobe.com/express/create/media_1900d303a701488626835756419ca3a50b83a2ae5.png?width=2000&format=webply&optimize=medium';

const useStyles = makeStyles(theme => ({
    root: {
        border: `1.2px solid ${theme.palette.grey[300]}`,
        padding: theme.spacing(2),
        borderRadius: '3px',
        maxWidth: 200,
        textAlign: 'center',
        backgroundColor: 'white',
        marginBottom: theme.spacing(2),
        alignItems: 'center',
        marginLeft: '10px',
        marginRight: '10px',
    },
    header:{
        color: theme.palette.grey[400],
        height: '15px',
    },
    avatar:{
        width: theme.spacing(9),
        height: theme.spacing(9),
    },
    channelusername: {
        fontWeight: theme.typography.fontWeightBold,
    },
    veryf:{
        height: 20,
    },
    photoContainer:{
        marginBottom: theme.spacing(2),
    },
    btn:{
        textTransform: 'none',
        paddingLeft: theme.spacing(6),
        paddingRight: theme.spacing(6),
        fontWeight: theme.typography.fontWeightMedium,
    },
}));
const ChannelPhoto = ({classes, user}) => 
{
    return(
        <div className ={classes.photoContainer}>
            <Avatar className = {classes.avatar} alta='pic' src ={ user.profilePicPath }/>
        </div>
        
    );
};

const ChannelUsername = ({classes, user}) => {
    return(

        <Grid container justify='center' spacing={5}>
            <Grid item xs={4}>
                <Typography variant = 'body2' className={classes.channelusername}>{user.userName}</Typography>
            </Grid>
            <Grid item xs={1}>
                <img alt='veryfied' src={veryfied} className={classes.veryf}/>
            </Grid>
        </Grid>
    );
};

const SeeButton = ({classes, user}) => {
    return(
        <Button disableElevation component={Link} to='/' color='primary' variant = 'contained' size = 'small' className={classes.btn}>
            Ver Perfil
        </Button>
    );
};

export const UserCard = ( {user} ) => {
    const classes = useStyles();
    return(
        <Grid container direction = 'column' className = {classes.root}>
            <ChannelPhoto  classes = {classes} user={user}/>
            <ChannelUsername  classes = {classes} user={user}/>
            <SeeButton classes = {classes} user={user}/>
        </Grid>
    );
}

export default UserCard;