import React, { useState, useEffect } from "react";
import "./comment.css";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import axios from "axios";
import { render } from "@testing-library/react";
import Post from "../post/post";
import Spinner from "../spinner/spinner";
import * as url from '../../baseUrl.js'
import useValidateCookie, { getJWTToken, getUserID } from '../../Hooks/Hooks'
import Cookies from "universal-cookie";
import { useNavigate } from "react-router-dom"; 
import jwtDecode from "jwt-decode";
import { LongMenu } from '../post/Menu'


const Comment = (props) => {
  const jwt = "jwt_authorization"
  let date = new Date(props.datetime);
  const cookies = new Cookies();
  let navigate = useNavigate();

  const [liked, setLiked] = useState(props.liked);
  const [likes, setLikes] = useState(0);
  const decoded = jwtDecode(cookies.get("jwt_authorization"))

  function menu() {
    if (props.user === decoded.ID) {
      return <div className="justify-end"><LongMenu id={props.post_id}></LongMenu></div>
    }
  }

  // let date = new Date(props.date.$date);
  return (
    <div className="Comment">
      <img src={"https://drive.google.com/uc?export=view&id=1LUxHzacFb2IlZSTmgxrrOK04Sz7uTKQggQ"} className="posterImage" />
      <div>
        <div>
          {/* <Link to={`/profile/tweets/${props.user._id.$oid}`}>
            {props.user.username}
          </Link> */}
          <p>
            Fendamear @ 
            {date.getDate()} {date.toLocaleString("en", { month: "long" })} at{" "}
            {date.getUTCHours()}:{date.getUTCMinutes()}
            {menu(props.post_id)}
          </p>
          <p>{props.caption}</p>
        </div>
        <div>
          {/* <a
            onClick={likeComment}
            style={
              liked
                ? { color: "#EB5757", cursor: "pointer" }
                : { color: "black", cursor: "pointer" }
            }
          >
            <i className="material-icons-outlined">favorite_border</i>
            {liked ? "Liked" : "Like"}
          </a> */}
          {/* <a>{likes} Likes</a> */}
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = (state) => {
  return {
    imageURL: state.imageURL,
    username: state.username,
    error: state.error,
    token: state.token,
  };
};

export default connect(mapStateToProps, null)(Comment);
