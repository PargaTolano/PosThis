const protocol = 'http';

const host =  'pargatolano-001-site1.dtempurl.com';

const port = '';

const url = `${protocol}://${host}${port}`;

const getURL = ( subroute ) =>`${url}/${subroute}`;

export {
    protocol,
    host,
    url,
    getURL
}