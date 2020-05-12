import axios from 'axios';
import { getAuthorizationHeaderValue } from '../auth-service'

export const axiosConfig = (user) => {
    axios.interceptors.request.use((config) => {
        if (user) {
            console.log("SET : ", user.access_token)
            config = {
                ...config,
                headers: {
                    ...config.headers,
                    'Authorization': `Bearer ${user ? user.access_token : "none"}`
                },
            }
        }

        return config;
    }, 
    (error) => {
        return Promise.reject(error);
    });

    axios.interceptors.response.use((response) => {

        return response;
    }, 
    (error) => {
        return Promise.reject(error);
    });
}