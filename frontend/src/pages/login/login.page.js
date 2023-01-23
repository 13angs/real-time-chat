/* Update the JSX */
import React, { useState } from 'react';
import './login.page.css';
import { UserAPI, LoginAPI } from '../../backend/api';
import {Link} from 'react-router-dom';
import Cookies from 'js-cookie';

const LoginPage = () => {
    const [users, setUsers] = useState([]);
    const userId = Cookies.get('login-user-id');

    React.useEffect(() => {
        const user = new UserAPI();
        user.getUsers(setUsers);
    }, [])

    const handleLogin = (event, userId) => {
        event.preventDefault();
        const login = new LoginAPI();
        login.post(userId);
    }

    return (
        <div>
            {userId ? <h3>User already logged in!</h3> :
                <div className='login-user-list-wrapper'>
                    <h2>Switch accounts</h2>
                    <span>or <Link to='/register'>Create a new one</Link></span>
                    <div className="login-user-list">
                        {users.map(user => (
                            <div className="user" key={user.id}>
                                <img src={'https://via.placeholder.com/150'} alt={user.name} />
                                <div className="info">
                                    <h3>{user.name}</h3>
                                </div>
                                <button onClick={(event) => handleLogin(event, user['id'])}>Login</button>
                            </div>
                        ))}
                    </div>
                </div>
            }
        </div>
    );
};

export default LoginPage;
