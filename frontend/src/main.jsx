
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import React, {createContext}  from "react";
import UserStore  from './store/UserStore'
import BookStore from './store/BookStore.jsx';
import AuthorStore from './store/AuthorStore.jsx';


export const Context = createContext(null)
const rootElement = document.getElementById('root');
const root = createRoot(rootElement); 
root.render(
    <Context.Provider value={{
        user: new UserStore(),
        book: new BookStore(),
        author: new AuthorStore()
    }}>
        <App />
    </Context.Provider>,
);
