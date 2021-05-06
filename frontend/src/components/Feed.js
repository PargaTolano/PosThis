import React from "react";
import SearchAppBar from "components/Inicio/Navbar";
import FixedContainer from "components/Inicio/Container";
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

const Feed = (props) => {
  //useCheckAuth();
  const classes = useStyles();

  return (
      <div className= {classes.Background}>
        <SearchAppBar />
        <FixedContainer/>
      </div>

      
  );
};

export default Feed;
