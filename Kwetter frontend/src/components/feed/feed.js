import { render } from "@testing-library/react";
import Post from "../post/post";
import React from "react";
import axios from "axios";
import { connect } from "react-redux";
import Spinner from "../spinner/spinner";
import { useState } from "react";
import { useEffect } from "react";
import * as url from '../../baseUrl.js'

export default function Feed() {

  const [tweets, setTweets] = useState([]);

  useEffect(() => {
    var config = {
      method: 'get',
      url: url.kweetUrl,
      headers: {
          'Content-Type': 'application/json',
      },
    };  
    axios(config).then((response) => {
        setTweets(response.data)
        console.log(response.data)
    }).catch(function( err) {
      console.log(err);
    });
  }, []);

  return(
      <section>
        {tweets.map((post, index) => (
          <Post
            user={post.user}
            caption={post.message}
            image="test"
            comments="test"
            retweets="test"
            datetime={post.date}
            post_id={post.id}
            liked={false}
            likes="test"
            retweeted="test"
            saved="test"
            saves="test"
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
