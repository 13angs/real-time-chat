import Axios from '../backend/Axios';
import Cookies from 'js-cookie';

class BackendAPI{
    apiUrl = process.env.REACT_APP_API_URL;
    apiVersion = 'v1'
    
    getFullEndpoint(method){
        return `/api/${this.apiVersion}/${method}`
    }
    get(){}
}

// user api
class UserAPI extends BackendAPI {
    async getUsers(callback){
        try {
            const endpoint = this.getFullEndpoint('users');
            const res = await Axios.get(endpoint);

            if(res.status === 200)
            {
                callback(res.data);
            }
        }catch (err){
            console.log(err);
            callback(null);
        }
    }

    async getUser(userId, callback){
        try {
            const endpoint = this.getFullEndpoint('users');
            const res = await Axios.get(`${endpoint}/${userId}`);

            if(res.status === 200)
            {
                callback(res.data);
            }
        }catch (err){
            console.log(err);
            callback(null);
        }
    }
}

class LoginAPI extends BackendAPI {
    async post(userId="user_id_here"){
        try {
            const endpoint = this.getFullEndpoint('login');
            const res = await Axios.post(`${endpoint}/${userId}`);

            if(res.status === 200)
            {
                if(res.data)
                {
                    Cookies.set('login-user-id', res.data['user_id'], { expires: 0.01 })
                }

                if(Cookies.set('login-user-id'))
                {
                    window.location.href = `/${userId}`
                }
            }
        }catch (err){
            console.log(err);
        }
    }
}

export {UserAPI, LoginAPI};