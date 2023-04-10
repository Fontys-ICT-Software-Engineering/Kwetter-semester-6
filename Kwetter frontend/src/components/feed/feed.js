import { render } from "@testing-library/react";
import Post from "../post/post";
import React from "react";
import axios from "axios";
import { connect } from "react-redux";
import Spinner from "../spinner/spinner";
import { useState } from "react";
import { useEffect } from "react";
import * as url from '../../baseUrl.js'
import useValidateCookie, { getJWTToken, getUserID } from '../../Hooks/Hooks'
import Cookies from "universal-cookie";
import { useNavigate } from "react-router-dom"; 


export default function Feed() {

  const jwt = "jwt_authorization"
  const [tweets, setTweets] = useState([]);
  const [editable, setEditable] = useState([]);
  const cookies = new Cookies();
  let navigate = useNavigate();

  useEffect(() => {
    var config = {
      method: 'get',
      url: url.kweetUrl,
      headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + cookies.get(jwt)
      },
    };  
    axios(config).then((response) => {
        setTweets(response.data)
        console.log(response.data)
    }).catch(function( err) {
      //navigate("/login")
      console.log(err);
    });
  }, []);

  function isEditable(id) {
    console.log("id uit methode: " + id);
    console.log("id uit params: " + localStorage.getItem("editableId"))

    const param = localStorage.getItem("editableId")
    if(id === param)
    {
        return [true];
    }
    else {
        return [false]
    }
  }

  return(
      <section>
        {tweets.map((post, index) => (
          <Post
            editable={isEditable(post.id)}
            edited={post.isEdited}
            user={post.user}
            caption={post.message}
            image="test"
            comments="test"
            //retweets="test"
            datetime={post.date}
            post_id={post.id}
            liked={post.liked}
            likes={post.likes}
            //retweeted="test"
            key={index}
          />
        ))}
        {/* {this.state.loading && <Spinner />} */}
        {/* uiteindelijk toevoegen spinner */}
       </section>
    )
};
// const mapStateToProps = (state) => {
//   return {
//     imageURL: state.imageURL,
//     username: state.username,
//     token: state.token,
//   };
// };

//export default connect(mapStateToProps, null)(Feed);
