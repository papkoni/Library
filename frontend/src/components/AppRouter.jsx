
import React, {useContext} from "react";
import {Context} from "../main.jsx";
import { LOGIN_ROUTE} from "../utils/consts.jsx";

import {
    Routes,
    Route
} from "react-router-dom";
import { adminRoutes, usersRoutes, UnauthRoutes } from "../routes";
import {observer} from "mobx-react-lite";
import { Navigate } from 'react-router-dom';

const AppRouter = observer(() => {
    console.log(123);

    const { user } = useContext(Context);
   
    const ProtectedRoute = ({ children }) => {
        if (!user.isAuth) {
            console.log("i here" ,user);
            console.log(user.isAuth);
            // Если пользователь не авторизован, перенаправляем на страницу логина
            return <Navigate to={LOGIN_ROUTE} replace />;
            
        }
        return children;
    };

    return (
        <Routes>
            {user.isAuth ? (
                <>
               {    console.log(111)
               }
                    {usersRoutes.map(({ path, Component }) => (
                        <Route 
                            key={path} 
                            path={path} 
                            element={
                                <ProtectedRoute>
                                    <Component />
                                </ProtectedRoute>
                            } 
                        />
                    ))}
                </>
            ) : (
                // Если пользователь не авторизован, показываем неавторизованные маршруты
                <>
                {UnauthRoutes.map(({ path, Component }) => (
                    <Route
                    key={path}
                    path={path}
                    element={<Component />}
                    />
                ))}
                <Route path="/" element={<Navigate to={LOGIN_ROUTE} replace />} /> {/* Перенаправление на login */}
                </>
            )}
        </Routes>
    );
});

export default AppRouter;