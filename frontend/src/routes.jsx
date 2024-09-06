
import Library from "./pages/Library"
import BookPage from "./pages/BookPage"
import Auth from "./pages/Auth";
import Admin from "./pages/Admin";
import {ADMIN_ROUTE, LIBRARY_ROUTE, LOGIN_ROUTE, REGISTRATION_ROUTE, BOOK_ROUTE} from "./utils/consts";



export const adminRoutes = [
    {
        path: ADMIN_ROUTE,
        Component: Admin
    },
    
]

export const usersRoutes = [
    {
        path: LIBRARY_ROUTE,
        Component: Library
    },
    {
        path: BOOK_ROUTE + '/:id',
        Component: BookPage
    },
   
   
]

export const UnauthRoutes = [
    
    {
        path: LOGIN_ROUTE,
        Component: Auth
    },
    {
        path: REGISTRATION_ROUTE,
        Component: Auth
    },
   
]

