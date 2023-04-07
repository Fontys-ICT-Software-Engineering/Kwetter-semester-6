import React from "react";
import "./userHeader.css";

function UserHeader (props) {
  return <section className="headerImage">{props.children}</section>;
};

export default UserHeader;
