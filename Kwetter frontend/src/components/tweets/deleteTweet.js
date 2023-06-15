import Post from "../post/post";
import React from "react";
import axios from "axios";
import { connect } from "react-redux";
import Spinner from "../spinner/spinner";
import { useParams } from "react-router-dom";
import { useState } from "react";
import Cookies from "universal-cookie";
import jwt from 'jwt-decode'
import * as url from '../../baseUrl'


export function deleteTweet(id) {

    const cookies = new Cookies();
    var config = {
        method: 'delete',
        url: url.kweetwriteurl + "delete/?id=" + id,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + cookies.get("jwt_authorization")
        },
      };  
  
      axios(config).then((response) => {
          console.log("tweet deleted succesfully")
          window.location.reload();
      }).catch(function( err) {
        console.log(err);
      });
}



