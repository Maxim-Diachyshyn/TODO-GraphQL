export default {
    HOME: '/',
    EDIT_FILM: {
        route: '/:id',
        build: id => `/${id}`
    },
    CREATE_FILM: '/create'
}