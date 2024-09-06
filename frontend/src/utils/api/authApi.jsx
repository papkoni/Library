
import React from "react";
import $api from '../../http/index';

 import { getAccessToken, getRefreshToken } from '../../store/tokens';

//за аксесом
export const checkAccess = async () => {
    try{
        const accessToken = getAccessToken();
        const refreshToken = getRefreshToken();
    
        if (accessToken) {
            try {
                const accessToken = getAccessToken();
                if (accessToken) {
                    const response = await $api.get('Users/CheckAccess', {
                        headers: {
                            Authorization: `Bearer ${accessToken}`,
                        },
                    });
        
                    if (response.status === 200) {
                        return response.data; // Возвращаем данные пользователя
                    }
                }
            } catch (error) {
                console.error('Access token invalid, trying to refresh token...', error);
                return null;
            }
        
        }
    
        // undefined === наличие токена
        if (refreshToken === undefined) {
            return await handleRefreshToken();
        } else {
            return null;
        }
    }
      
        catch(error){
            console.error(error);
        }
        return null;

}


//за рефрешом (+ аксес)
export const handleRefreshToken = async () => {
    try {
        // Выполняем запрос для обновления токена
        const response = await $api.post('Users/RefreshToken', { withCredentials: true });

        // Обработка успешного ответа (200)
        if (response.status === 200) {
            const { accessToken, ...userData } = response.data;
            localStorage.setItem('accessToken', accessToken);
            return userData; // Возвращаем обновленные данные пользователя
        }

        // Обработка статуса 401 (Unauthorized)
        if (response.status === 401) {
            // Возвращаем null для явной обработки
            return null;
        }
    } catch (error) {
        // Обрабатываем только реальные ошибки сети
        if (error.response && error.response.status === 401) {
            // Если возникла ошибка с кодом 401, возвращаем null
            return null;
        }
        console.error('Error refreshing token:', error);
    }

    return null;
};

export const logout = async () => {
	try {
		const response = await $api.get('Users/Logout', {
			credentials: 'include',
		});
		if (response.ok) {
			localStorage.removeItem('accessToken');
		}
	} catch (error) {
		console.error('Failed to logout:', error);
		throw error;
	}
}