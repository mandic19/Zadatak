import axios from 'axios';


const API_URL = 'http://localhost:5000/api/Account/';

class AuthService {
    login(user) {
        return axios
            .post(API_URL + 'Token', "username=" + user.email + "&password=" + user.password + "&grant_type=password")
            .then(response => {
                if (response.data.access_token) {
                    localStorage.setItem('user', JSON.stringify(response.data));
                }

                return response.data;
            });
    }

    logout() {
        localStorage.removeItem('user');
    }

    register(user) {
        return axios.post(API_URL + 'Register', {
            email: user.email,
            password: user.password,
            ConfirmPassword: user.confirm_password
        });
    }
}

export default new AuthService();
