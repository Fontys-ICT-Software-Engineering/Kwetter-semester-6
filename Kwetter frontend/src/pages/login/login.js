import React, { useState } from "react";
import "./login.css";
import Auth from "../../components/auth/auth";
import { Link, useNavigate } from "react-router-dom";
import * as url from '../../baseUrl';
import axios from "axios";
import Cookies from "universal-cookie";
import jwt from 'jwt-decode'
import Alert from '@mui/material/Alert'
import { Stack } from "react-bootstrap";
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Collapse from '@mui/material/Collapse';
import Button from '@mui/material/Button';
import CloseIcon from '@mui/icons-material/Close';

export default function Login() {

  let navigate = useNavigate();

  const [error, showError] = useState(false);
  const [errorMessage, setMessage] = useState([]);
  const [count, addCount] = useState(0);

  const cookies = new Cookies();

  const [formValue, setFormValue] = useState({
    email: "",
    password: ""
  });

  const inputChangedHandler = (event) => {
    setFormValue({
      ...formValue,
      [event.target.name]: event.target.value
    })
  };

  const submitHandler = (e) => {
    e.preventDefault();
    showError(false);
    var data = JSON.stringify({
      "email": formValue.email,
      "password": formValue.password
    });

    console.log(data)
    var config = {
      method: 'post',
      url: url.authUrl + "login",
      headers: {
        'Content-Type': 'application/json'
      },
      data: data
    };

    axios(config).then((response) => {

      const decoded = jwt(response.data.token)
      cookies.set("jwt_authorization", response.data.token, {
        expires: new Date(decoded.exp * 1000),
      });
      navigate("/");
      window.location.reload();
    }).catch(function (error) {
      showError(true);
      addCount(count + 1);
    });

  };
  return (
    <Auth>
      <div className="signupPage">
        <Link to="/">
          <h2>Kwetter login</h2>
        </Link>
        <form onSubmit={submitHandler}>
          <i className="material-icons-outlined">email</i>
          <input className="loginform-input"
            name="email"
            placeholder="Email"
            //onMouseOver={() => this.setState({ focus: { email: true } })}
            onChange={inputChangedHandler}
          />
          <i className="material-icons-outlined">lock</i>
          <input
            name="password"
            type="password"
            placeholder="Password"
            //onMouseOver={() => this.setState({ focus: { password: true } })}
            //onMouseLeave={() => this.setState({ focus: { password: false } })}
            onChange={inputChangedHandler}
          />
          <div>
            <Collapse in={error}>
            <Alert show={error} severity="error"           
            >Email or password is incorrect  ( {count} )</Alert>
            </Collapse>
            <br></br>
          </div>
          <button>Login</button>
        </form>

        <p>
          Don't have an account yet? <Link to="/signup">Register</Link>
        </p>
      </div>
    </Auth>
  );
}
