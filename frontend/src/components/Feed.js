import React from 'react';

import useCheckAuth from 'hooks/useCheckAuth';

const Feed = ( props ) => {

    useCheckAuth();

    return (
        <div>
            <h1>Este es el feed</h1>
        </div>
    )
}

export default Feed;