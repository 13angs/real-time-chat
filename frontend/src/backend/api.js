import Axios from '../backend/Axios';

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
    async get(callback){
        try {
            const endpoint = this.getFullEndpoint('users');
            const res = await Axios.get(endpoint);

            if(res.status === 200)
            {
                callback(res.data);
            }
        }catch (err){
            console.log(err);
            callback([]);
        }
    }
}

export {UserAPI};