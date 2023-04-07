import { useState } from "react";
import axios from "axios";
import Cookies from "universal-cookie";
import jwt from 'jwt-decode'
import * as url from '../baseUrl'

export const getUserID = () => {
    const cookies = new Cookies();
    cookies = cookies.get("jwt_authorization");
    if(!cookies) return "cookie bestaat niet"
    const decoded = jwt(cookies)
    return decoded.ID;
}

//export default getUserID;
