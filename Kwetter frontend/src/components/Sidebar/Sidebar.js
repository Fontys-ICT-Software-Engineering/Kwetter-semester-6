import React from "react";
import "./Sidebar.css";

function SideBar(props) {
  return <div className="sideBar">{props.children}</div>;
};

export default SideBar;
