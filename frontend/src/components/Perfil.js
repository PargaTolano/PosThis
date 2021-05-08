import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import FixedContainer from "components/Inicio/Container";
import ContainerPerfil from "components/Perfil/ContainerPerfil";
import { ThemeProvider } from "@material-ui/core/styles";
import theme from "../temaConfig";
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

const Perfil = (props) => {
  //useCheckAuth();
  const classes = useStyles();

  return (
      <div className= {classes.Background}>
        <SearchAppBar />
       <ContainerPerfil></ContainerPerfil>
      </div>

      
  );
};

export default Perfil;
