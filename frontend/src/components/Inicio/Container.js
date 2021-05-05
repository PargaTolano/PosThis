import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';

function FixedContainer() {
  return (
    <React.Fragment>
      <CssBaseline />
      <Container maxWidth="md" fixed>
        <Typography component="div" style={{ backgroundColor: '#6573c3', height: '100vh' }} />
      </Container>
    </React.Fragment>
  );
}

export default FixedContainer;