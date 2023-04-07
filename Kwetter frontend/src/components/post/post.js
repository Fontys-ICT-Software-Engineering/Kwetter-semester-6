import React, { useState, useEffect } from "react";
import "./post.css";
import Comment from "../comment/comment";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { LongMenu } from './Menu'
import { getUserID } from "../../Hooks/Hooks";
import Cookies from "universal-cookie";
import jwtDecode from "jwt-decode";
import * as url from '../../baseUrl'


function Post(props) {
  let date = new Date(props.datetime.$date);
  let navigate = useNavigate();


  const [userID, setUserID] = useState("");

  const cookies = new Cookies()
  const decoded = jwtDecode(cookies.get("jwt_authorization"))

  console.log("uit de props " + props.user);
  console.log("uit de cookie " + decoded.ID);

  function menu() {
    if (props.user === decoded.ID) {
      return <div className="justify-end"><LongMenu id={props.post_id}></LongMenu></div>
    }
  }




  const [liked, setLiked] = useState(props.liked);
  const [likes, setLikes] = useState(props.likes);
  const [retweeted, setRetweeted] = useState(props.retweeted);
  const [retweets, setretweets] = useState(props.retweets);
  const [saved, setSaved] = useState(props.saved);
  const [saves, setSaves] = useState(props.saves);
  const [comment, setComment] = useState("");
  const [commentSent, setCommentsent] = useState(null);
  const [edit, setEdit] = useState(false);

  const handleComment = (event) => {
    setComment(event.target.value);
  };

  const sendComment = () => {
    // setComment("");
    // let data = new FormData();
    // data.append("caption", comment);
    // let url = `https://tweeter-8qqa.onrender.com/${props.post_id.$oid}/comments`;
    // axios({
    //   method: "post",
    //   url: url,
    //   data: data,
    //   headers: {
    //     "Content-Type": "multipart/form-data",
    //     Authorization: props.token,
    //   },
    // })
    //   .then((res) => {
    //     setCommentsent(!commentSent);
    //     props.onSendComment(commentSent);
    //     navigate(`/${props.username}/${props.post_id.$oid}`);
    //   })
    //   .catch((err) => console.log(false));
  };

  function likePost () {

    console.log("de like post methode bereikt!")
    var data = JSON.stringify({
      "kweetId": props.post_id,
      "userId": props.user
    })

    console.log(data);
    var config = {
      method: 'post',
      url: url.likeUrl,
      headers: {
        'Content-Type': 'application/json', 
        'Authorization': 'Bearer ' + cookies.get("jwt_authorization")
      },
      data: data
    };

    axios(config).then((response) => {
      console.log(response.data)
      setLiked(response.data)

      if(response.data)
      {
          setLikes(likes + 1)
      } 
      else {
          setLikes(likes - 1)
      } 
    }).catch(function (error) {
      console.log('Error', error.message)
      console.log(error.response.status);
    });

    // setLiked(!liked);
    // let url = `https://tweeter-test-yin.herokuapp.com/${props.post_id.$oid}/like`;
    // axios({
    //   method: "get",
    //   url: url,
    //   headers: {
    //     "Content-Type": "multipart/form-data",
    //     Authorization: props.token,
    //   },
    // })
    //   .then((res) => {
    //     setLiked(res.data.liked);
    //     setLikes(res.data.likes);
    //   })
    //   .catch((err) => setLiked(!liked));
  };

  const retweetPost = () => {
    // setRetweeted(!retweeted);
    // let url = `https://tweeter-test-yin.herokuapp.com/${props.post_id.$oid}/retweet`;
    // axios({
    //   method: "get",
    //   url: url,
    //   headers: {
    //     "Content-Type": "multipart/form-data",
    //     Authorization: props.token,
    //   },
    // })
    //   .then((res) => {
    //     setRetweeted(res.data.retweeted);
    //     setretweets(res.data.retweeted);
    //   })
    //   .catch((err) => console.log(err));
  };

  const bookmarkPost = () => {
    // setSaved(!saved);
    // let url = `https://tweeter-test-yin.herokuapp.com/${props.post_id.$oid}/bookmark`;
    // axios({
    //   method: "get",
    //   url: url,
    //   headers: {
    //     "Content-Type": "multipart/form-data",
    //     Authorization: props.token,
    //   },
    // })
    //   .then((res) => setSaved(res.data.bookmarked))
    //   .catch((err) => console.log(err));
  };

  return (
    <article className="post">
      {commentSent === false ? (
        <p className="tweetFail">An error occured. Please try again.</p>
      ) : null}
      <header className="postingDetails">
        <img className="posterImage" src={props.user.profile_image} />
        <div>
          <Link
            to={`/profile/tweets/1`}
            className="posterName"
          >
            {props.user.username}
          </Link>
          <p className="postingDate">
            {date.getDate()} {date.toLocaleString("en", { month: "long" })} at{" "}
            {date.getUTCHours()}:{date.getUTCMinutes()}
          </p>
        </div>
        {menu(props.post_id)}
      </header>

      <Link
        className="tweet"
        to={`/${props.user.username}/${props.post_id.$oid}`}
      >
        {props.caption}
      </Link>
      <div className="engagementLinks">
        <a className="engagementLink">{likes} likes</a>
        <a className="engagementLink">{props.comments} comments</a>
      </div>
      <div className="engageLinks">
        <a className="engageLink">
          <span className="material-icons-outlined engageLink">
            mode_comment
          </span>
          <span>Comment</span>
        </a>
        {/* <a
          className="engageLink"
          style={retweeted ? { color: "#27AE60" } : { color: "initial" }}
          onClick={retweetPost}
        >
          <span className="material-icons-outlined engageLink">autorenew</span>
          <span>{retweeted ? "Retweeted" : "Retweet"}</span>
        </a> */}
        <a
          className="engageLink"
          style={liked ? { color: "#EB5757" } : { color: "black" }}
          onClick={() => likePost()}        
        >
          <span className="material-icons-outlined engageLink">
            favorite_border
          </span>
          <span>{liked ? "Liked" : "Like"}</span>
        </a>
      </div>
      <div className="commentCard">
        <img src={props.imageURL} className="posterImage" />
        <div
          style={{
            position: "relative",
            width: "100%",
            display: "flex",
            alignItems: "center",
          }}
        >
          <input
            className="inputComment"
            placeholder="Tweet your reply"
            onChange={handleComment}
            value={comment}
          />
          {/* <i className="material-icons-outlined commentImageIcon">image</i> */}
          <i className="material-icons-outlined sendIcon" onClick={sendComment}>
            send
          </i>
        </div>
      </div>
      {/* <Comment/> */}
    </article>
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

const mapDispatchToProps = (dispatch) => {
  return {
    onSendComment: (commentSent) =>
      dispatch({ type: "SET_COMMENT", value: commentSent }),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Post);
