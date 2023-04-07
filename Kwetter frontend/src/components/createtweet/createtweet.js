import React, { useState } from "react";
import "./createtweet.css";
import * as actions from "../../store/action";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actionTypes";
import axios from "axios";
import { useNavigate, useLocation } from "react-router-dom";
import tweets from "../tweets/tweets";
import * as url from "../../baseUrl.js"
import Cookies from "universal-cookie";


export default function CreateTweet(id) {

  const [Tweet, setTweet] = useState([]);
  let navigate = useNavigate();
  
  const postTweet = (e) => {
    e.preventDefault();
    var data = JSON.stringify({
      "message": Tweet,
      "user": id.id
    })

    var config = {
      method: 'post',
      url: url.kweetUrl,
      headers: {
        'Content-Type': 'application/json'
      },
      data: data
    };
    axios(config).then((response) => {
      console.log(response.message)
      window.location.reload()
    }).catch(function (error) {
      //console.log('Error', error.message)
      // setMessage(error.response.data.message);
      // showError(true);
      //console.log(error.response.status);
      //console.log(error.response.headers);
    });

  
  };

  const setImage = () => {
    // const imageurl = URL.createObjectURL(this.state.tweet.file);
    // this.setState(() => {
    //   return { tweetimageURL: imageurl };
    // });
  };

  const onImageChange = (event) => {
    // this.setState(() => {
    //   return {
    //     tweet: {
    //       ...this.state.tweet,
    //       file: event.target.files[0],
    //     },
    //   };
    // }, this.setImage);
  };

  const setpermission = () => {
    // this.setState({ showpermission: !this.state.showpermission });
  };

  const handleNewTweet = (event) => {
    setTweet(event.target.value);
    console.log(Tweet);
  };

  return (
    <React.Fragment>
      <div className="createTweet">
        <p>Tweet something</p>
        <div className="newtweetInput">
          {/* image for profile picture */}
          <img className="posterImage" src={"https://drive.google.com/uc?export=view&id=1LUxHzacFb2IlZSTmgxrrOK04Sz7uTKQggQ"} />
          <div className="tweetbox">
            <textarea
              placeholder="What’s happening?"
              className="tweetBox"
              onChange={handleNewTweet}
              //onKeyDown={handleKeyDown}
              maxLength="250"
            />
            {/* <img src={tweetimageURL} width="100%" /> */}
          </div>
        </div>
        <div className="newtweetIcons">
          <label htmlFor="file-input">
            <i className="material-icons-outlined tweetImageIcon">image</i>
          </label>
          <input
            type="file"
            accept="image"
            id="file-input"
            name="image-upload"
            onChange={onImageChange}
          />
          <button id="tweetButton" onClick={postTweet}>
            Tweet
          </button>
        </div>
      </div>
    </React.Fragment>
  );
}
// const mapStateToProps = (state) => {
//   return {
//     imageURL: state.imageURL,
//     error: state.error,
//     postedTweet: state.postedTweet,
//     token: state.token,
//   };
// };

// const mapDispatchToProps = (dispatch) => {
//   return {
//     onSubmitTweet: (tweet) => dispatch(actions.postTweet(tweet)),
//     onResetPostedTweet: () =>
//       dispatch({ type: actionTypes.RESET_POSTED_TWEET }),
//   };
// };

// const withHooksHOC = (Component) => {
//   return (props) => {
//     const navigate = useNavigate();
//     const location = useLocation();
//     return <Component navigate={navigate} location={location} {...props} />;
//   };
// };

// export default connect(
//   mapStateToProps,
//   mapDispatchToProps
// )(withHooksHOC(CreateTweet));
