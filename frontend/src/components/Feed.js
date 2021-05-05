import React from 'react';
import SearchAppBar from "components/Inicio/Navbar";
import { Container, Grid } from '@material-ui/core';
import FixedContainer from "components/Inicio/Container"
//import useCheckAuth from 'hooks/useCheckAuth';

const Feed = ( props ) => {
    //useCheckAuth();
    return (
    <Container classname="ContenedorFeed">
        <SearchAppBar position="fixed"/>
        <FixedContainer/>
    </Container>
    );
}

export default Feed;