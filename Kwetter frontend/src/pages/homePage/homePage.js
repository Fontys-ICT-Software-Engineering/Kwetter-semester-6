import Header from "../../components/header/header";
import CreateTweet from "../../components/createtweet/createtweet";
import FollowCard from "../../components/followCard/followCard";
import Container from "../../components/container/container";
import SideBar from "../../components/Sidebar/Sidebar";
import HomeContainer from "../../components/homeContainer/homeContainer";
import Feed from "../../components/feed/feed";
import Bio from "../../components/bio/bio";
import { useEffect } from "react";
import Cookies from "universal-cookie";
import Login from "../login/login";
import jwtDecode from "jwt-decode";

const HomePage = (props) => {
  const jwt = "jwt_authorization"
  const cookies = new Cookies();

  const cookieExists = cookies.get(jwt)

  if(!cookieExists)
  {
    return <Login></Login>
  }

  const decoded = jwtDecode(cookieExists);

  return (
    <div>
      <Header />
      <Container>
        <HomeContainer>
          <CreateTweet id={decoded.ID}/>
          <Feed />
        </HomeContainer>
        <SideBar>
          <Bio />
          <FollowCard />
        </SideBar>
      </Container>
    </div>
  );
};

export default HomePage;
