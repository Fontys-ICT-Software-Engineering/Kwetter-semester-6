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
  let date = new Date(props.datetime);
  let navigate = useNavigate();
  const jwt = "jwt_authorization"
  const cookies = new Cookies()
  const decoded = jwtDecode(cookies.get("jwt_authorization"))

  console.log(props.post_id + " : " + props.edited)

  function menu() {
    if (props.user === decoded.ID) {
      return <div className="justify-end"><LongMenu id={props.post_id}></LongMenu></div>
    }
  }
  const [liked, setLiked] = useState(props.liked);
  const [likes, setLikes] = useState(props.likes);
  const [comment, setComment] = useState("");
  const [allcomments, setallcomments] = useState([]);
  const [commentSent, setCommentsent] = useState(null);
  const [edit, setEdit] = useState(props.editable);
  const [editTweet, setEditTweet] = useState(props.caption)

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

  function likePost() {

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

      if (response.data) {
        setLikes(likes + 1)
      }
      else {
        setLikes(likes - 1)
      }
    }).catch(function (error) {
      console.log('Error', error.message)
      console.log(error.response.status);
    });
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

  const handleUpdateTweet = (event) => {
    setEditTweet(event.target.value);
  }

  function handleUpdatePost(id) {
    var data = JSON.stringify({
      "id": id,
      "user": decoded.ID,
      "message": editTweet
    })

    var config = {
      method: 'Put',
      url: url.kweetUrl,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + cookies.get(jwt)
      },
      data: data
    };

    console.log(data)
    axios(config).then((response) => {
      localStorage.removeItem("editableId")
      window.location.reload();
    }).catch(function (err) {
      //navigate("/login")
      console.log(err);
    });
  }

  function getReactionKweets(id) {
    setallcomments([]);
    var config = {
      method: 'get',
      url: url.reactionUrl + "?KweetID=" + id,
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + cookies.get(jwt)
      },
    };
    axios(config).then((response) => {
      //return response.data;
    }).catch(function (err) {
      //navigate("/login")
      console.log(err);
    });

  }

  // useEffect((post_id) => {
  //   console.log("id in use effect " + post_id)
  //   var config = {
  //     method: 'get',
  //     url: url.reactionUrl + "?KweetID=" + props.post_id,
  //     headers: {
  //       'Content-Type': 'application/json',
  //       'Authorization': 'Bearer ' + cookies.get(jwt)
  //     },
  //   };
  //   axios(config).then((response) => {
  //     setallcomments(response.data)
  //     console.log(response.status)
  //   }).catch(function (err) {
  //     //navigate("/login")
  //     console.log(err);
  //   });
  // }, []);

  function cancelEdit() {
    localStorage.removeItem("editableId")
    window.location.reload();
  }

  function editing() {
    console.log("in de editing functie " + edit)
    if (edit === [true]) {
      return <>dit return hij in plaats van een tweet</>
    }
    else {
      return
    }
  }

  return (
    <article className="post">
      {commentSent === false ? (
        <p className="tweetFail">An error occured. Please try again.</p>
      ) : null}
      <header className="postingDetails">
        <img className="posterImage" src={"https://drive.google.com/uc?export=view&id=1LUxHzacFb2IlZSTmgxrrOK04Sz7uTKQggQ"} />
        <div>
          <Link
            to={`/profile/tweets/1`}
            className="posterName"
          >
            {props.user.username}
          </Link>
          <p className="postingDate">
            {date.getDate()} {date.toLocaleString("en", { month: "long" })} at{" "}
            {date.getHours()}:{(date.getMinutes() < 10 ? '0' : '') + date.getMinutes()}
            {props.edited == true ? <> (Edited)</> : null}
          </p>
        </div>
        {menu(props.post_id)}
      </header>

      {props.editable == "true" ? (
        <>
          <div className="tweetbox ">
            <textarea
              className="tweetBox color-ligning"
              onChange={handleUpdateTweet}
              //onKeyDown={handleKeyDown}
              maxLength="250"
            >{props.caption}</textarea>
            {/* <img src={tweetimageURL} width="100%" /> */}
          </div>
          <div className="tweet">
            <button className="button" onClick={() => handleUpdatePost(props.post_id)}>Update</button>
            &nbsp;
            <button className="button danger" onClick={() => cancelEdit()}>Cancel Update</button>
          </div>
        </>
      ) : <Link
        className="tweet"
        to={`/${props.user.username}/${props.post_id.$oid}`}
      >
        {props.caption}
      </Link>}

      <div className="engagementLinks">
        <a className="engagementLink margin-right">{likes} likes</a>
        <a className="engagementLink margin-right">{props.comments} comments</a>
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

      {/* {getReactionKweets.forEach(element => {
        


      })} */}

      {getReactionKweets(props.post_id)}

      {allcomments.map((post, index) => (
        <Comment
          caption={post.message}
          datetime={post.dateSend}
          key={index}
        />
      ))}

      {/* {allcomments.map((post, index) => (
          <Comment
            caption = {post.message}
            datetime={post.dateSend}
            key={index}
          />
        ))} */}



      {/* {allcomments.map((post, index) => (
          <Comment
            caption = {post.message}
            datetime={post.dateSend}
            key={index}
          />
        ))} */}
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
