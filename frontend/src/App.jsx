import React, { useContext, useEffect } from "react";
import { BrowserRouter } from "react-router-dom";
import Navbar from "./components/NavBar";
import AppRouter from "./components/AppRouter";
import { observer } from "mobx-react-lite";
import { Context } from "./main"; 
import { checkAccess, handleRefreshToken } from './utils/api/authApi'; // Импорт методов проверки

const App = observer(() => {
  const { user } = useContext(Context); // Получаем пользователя из контекста

  useEffect(() => {
    const authenticateUser = async () => {
      try {
        console.log("Starting authentication...");
        user.setLoading(true);
        
        const userData = await checkAccess(); // Проверка токена
        if (userData != null) {
          console.log("User authenticated via access token:", userData);
          user.setIsAuth(true);
          user.setUser(userData);
        } else {
          console.log("Access token invalid, trying to refresh...");
          const refreshedUserData = await handleRefreshToken();
          if (refreshedUserData != null) {
            console.log("User authenticated via refresh token:", refreshedUserData);
            user.setIsAuth(true);
            user.setUser(refreshedUserData);
          } else {
            console.log("Failed to authenticate, setting isAuth to false");
            user.setIsAuth(false); // Если не удалось обновить токен, сбрасываем авторизацию
          }
        }
      } catch (error) {
        console.error("Authentication error:", error);
        user.setIsAuth(false);
      } finally {
        user.setLoading(false); // Снимаем флаг загрузки в конце процесса
        console.log("Finished authentication, loading:", user.loading);
      }
    };

    authenticateUser(); // Запуск проверки при монтировании компонента
  }, [user]);

  if (user.loading) {
    console.log("Loading...");
    return <div>Loading...</div>;
  }


  // Если идет загрузка (проверка аутентификации), показываем индикатор загрузки
 

  return (
    <BrowserRouter>
    <Navbar />
    <AppRouter />
  </BrowserRouter>
  );
});

export default App;


