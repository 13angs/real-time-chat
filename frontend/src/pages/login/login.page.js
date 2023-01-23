/* Update the JSX */
import React, { useState } from 'react';
import './login.page.css';
import { UserAPI } from '../../backend/api';

const LoginPage = () => {
    const [users, setUsers] = useState([]);

    React.useEffect(() => {
        const user = new UserAPI();
        user.get(setUsers);
    }, [])

    return (
        <div className='login-user-list-wrapper'>
            <h2>Switch accounts</h2>
            <div className="login-user-list">
                {users.map(user => (
                    <div className="user" key={user.id}>
                        <img src={'https://via.placeholder.com/150'} alt={user.name} />
                        <div className="info">
                            <h3>{user.name}</h3>
                        </div>
                        <button>Login</button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default LoginPage;
