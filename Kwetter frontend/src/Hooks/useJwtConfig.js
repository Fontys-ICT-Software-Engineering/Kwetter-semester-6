import { useState } from "react";
import axios from "axios";
import Cookies from "universal-cookie";
import jwt from 'jwt-decode'
import * as url from '../baseUrl'

const useJwtConfig = (method, url, path, ) => {
    const [isValidated, setIsValidated] = useState(false);

    const cookies = new Cookies();

    var config = {
        method: 'get',
        url: url.authUrl + "validate",
        headers: {
          'Content-Type': 'application/json', 
          'Authorization': 'Bearer ' + cookies.get("jwt_authorization")
        }
      };
  
    }
export default useValidateCookie;
