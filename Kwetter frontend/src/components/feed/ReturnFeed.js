import React from "react";
import Feed from "./feed";
import { useState } from "react";


// export default function returnFeed(param) {
//     {console.log("methode bereikt")}
//     return <Feed id={"dit is mijn id"}></Feed>
// }

export default function useEditing(id, )
{
    const [editable, setEditable] = useState([]);
    setEditable(id);
    return [editable];
}

