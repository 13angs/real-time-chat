import React from "react";
import "./register.page.css";
import Axios from "../../backend/Axios";
import {Link} from 'react-router-dom';

const RegisterPage = () => {
  const userMethod = '/api/v1/users'
  const [userName, setUserName] = React.useState('');

  async function createUser(e){
    e.preventDefault();

    try {
      const body = {
        name: userName
      }

      // when register the input can not be empty
      if(userName.length < 1)
      {
        alert('Name can not be empty')
        return;
      }
      const res = await Axios.post(userMethod, body)
      if(res.status === 200)
      {
        alert(`User with name '${res.data['name']}' created!`)
        setUserName('')
      }
    }catch (err){
      alert(`User with name ${userName} exist!`)
    }
  }

  return (
    <div className="register-page">
      <div className="register-form">
        <form onSubmit={createUser}>
          {/* <label>
            Name:
          </label> */}
          <input 
            type="text" 
            name="name"
            value={userName}
            onChange={(event) => setUserName(event.target.value)}
          />
          {/* <br /> */}
          <div>
            <button type="submit">Register</button>
          </div>
        </form>
      </div>
      <div className="chat-btn">
        <Link to={"/login"}>Login</Link>
      </div>
    </div>
  );
};

export default RegisterPage;
