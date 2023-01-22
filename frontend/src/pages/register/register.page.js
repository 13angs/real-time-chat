import React from "react";
import "./register.page.css";

const RegisterPage = () => {
  return (
    <div className="register-page">
      <div className="register-form">
        <form>
          <label>
            Name:
          </label>
          <input type="text" name="name" />
          {/* <br /> */}
          <div>
            <button type="submit">Register</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default RegisterPage;
