export const routes = {
    feed:           '/',
    postDetail:     '/post/:id',
    searchResult:   '/search',
    profile:        '/profile/:id',
    login:          '/login',
    getPost:        id=>`/post/${id}`,
    getProfile:     id=>`/profile/${id}`
};