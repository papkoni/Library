import $api from '../../http/index';

export const login = async (userData) => {
	try {
		const response = await $api.post('Users/Login', userData, {
			withCredentials: true // Включаем передачу учетных данных (cookies)
		});

		// Проверка на успешный статус запроса
		if (response.status !== 200) {
			console.error('Login failed:', response.data);
			return false;
		}
		console.log('userData log: ', response.data);
		// Данные уже распарсены и находятся в response.data
		const responseData = response.data;

		localStorage.setItem('accessToken', responseData.accessToken);
		
		return responseData;
	} catch (error) {
		console.error('Failed to login:', error);
		return 'Incorrect password';
	}
};


export const registration = async userData  => {
		try {
		console.log('userData reg: ', userData);

		const response = await $api
			.post('Users/Registrat', { json: userData })
			.json();

		localStorage.setItem('accessToken', response.accessToken);

		return true;
	} catch (error) {
		console.error('Failed to login:', error);
		return 'User with this email already exists';
	}
};