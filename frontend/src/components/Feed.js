import React from 'react';
import SearchAppBar from 'components/Inicio/Navbar';
import FixedContainer from 'components/Inicio/Container';
import { makeStyles } from '@material-ui/core/styles';
import backapp3 from 'assets/backapp3.png';

//import useCheckAuth from 'hooks/useCheckAuth';

const useStyles = makeStyles((theme) => ({
  Background: {
    backgroundImage: `url('${backapp3}')`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    backgroundAttachment: 'fixed',
    backgroundRepeat: 'no-repeat',
    height: '100%',
  },
}));

const Feed = (props) => {
  //useCheckAuth();
  const classes = useStyles();

  return (
    <div className={classes.Background}>
      <SearchAppBar />
      <FixedContainer>
      </FixedContainer>
    </div>
  );
};

export default Feed;
