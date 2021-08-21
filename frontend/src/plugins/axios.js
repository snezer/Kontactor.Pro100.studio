
import router from "../router";
import axios from "axios";
//import messages from '../resources/messages';

const api = axios.create();

api.interceptors.request.use(request => requestInterceptor(request));

api.interceptors.response.use(
    response => successHandler(response),
    error => errorHandler(error)
)

const requestInterceptor = (request) => {
    request.withCredentials = true;
    //request.headers['x-xsrf-token'] = cookie.get('.AspNetCore.Xsrf')
    return request;
}

const successHandler = (response) => {
    return new Promise((resolve, reject) => {
        if (response.status === 200) {
            resolve(response)
        }
    });
}
const errorHandler = (error) => {
    return new Promise((resolve, reject) => {
        // TODO: Обрабатывайте тут свои ошибки

        //console.log(error.response)
        if (error.response.status === 400) {
            return reject(error.response.data)
        }
        if (error.response.status === 401 || error.response.status === 403) {
            if ([401, 403].indexOf(error.response.status) !== -1) {
                router.push({path: '/'})
            }
            reject();
        }
        //console.log(error.response.data)
        return reject(error.response.data);
    })
}

export default api;
