const protocol = 'http';

const host = 'localhost:4000';

const url = `${protocol}//:${host}`;

const getURL = ( subroute ) =>`${url}/${subroute}`;

const getURLWithParams = ( subroute, param ) =>`${url}/${subroute}/${param}`;

export {
    protocol,
    host,
    url,
    getURL,
    getURLWithParams
}