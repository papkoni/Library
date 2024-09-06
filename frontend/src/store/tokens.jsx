
import Cookies from 'js-cookie';


export const getAccessToken  =  async() =>
	localStorage.getItem('accessToken');
export const getRefreshToken =  async() =>
	Cookies.get('yummy-cackes');





