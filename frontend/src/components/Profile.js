import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import ContainerPerfil from "components/Profile/ContainerProfile";

import {makeStyles} from '@material-ui/core/styles'
import backapp2 from 'assets/backapp2.png'

//import useCheckAuth from 'hooks/useCheckAuth';

const useStyles = makeStyles((theme) => ({
  Background:{
    backgroundImage: `url('${backapp2}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
    height: '100%',
  }
}));

const Profile = (props) => {
  //useCheckAuth();
  const classes = useStyles();

  return (
      <div className= {classes.Background}>
        <SearchAppBar />
       <ContainerPerfil></ContainerPerfil>
      </div>

      
  );
};

export default Profile;
