const protocol = 'https';

const host =  window.location.hostname;

const port = '44325';

const url = `${protocol}://${host}:${port}`;

const getURL = ( subroute ) =>`${url}/${subroute}`;

export {
    protocol,
    host,
    url,
    getURL
}