import Library from "./pages/Library"
import Book from "./pages/Book"
import Auth from "./pages/Auth";
import Admin from "./pages/Admin";
import {ADMIN_ROUTE, LIBRARY_ROUTE, LOGIN_ROUTE, REGISTRATION_ROUTE, BOOK_ROUTE} from "./utils/consts";



export const authRoutes = [
    {
        path: ADMIN_ROUTE,
        Component: Admin
    },
    
]

export const publicRoutes = [
    {
        path: LIBRARY_ROUTE,
        Component: Library
    },
    {
        path: BOOK_ROUTE + '/:id',
        Component: Book
    },
    {
        path: LOGIN_ROUTE,
        Component: Auth
    },
    {
        path: REGISTRATION_ROUTE,
        Component: Auth
    },
   
]