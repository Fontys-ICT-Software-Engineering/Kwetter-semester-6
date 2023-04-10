import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { deleteTweet } from '../tweets/deleteTweet';
import Feed from '../feed/feed';
import { useState } from 'react';
import UserHeader from '../userHeader/userHeader';
import Post from './post';
import returnFeed, { isEditable } from '../feed/ReturnFeed';
import useEditing from '../feed/ReturnFeed';


const options = [
    'Delete Kweet'
];

const ITEM_HEIGHT = 48;

export function LongMenu(props) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuItemClick = (event, index) => {
    console.log(index)
    console.log("event:" + event)
    if(event === "Delete")
    {
        deleteTweet(index);
    }
    else 
    {
        localStorage.setItem("editableId", index)
        window.location.reload();
    }
    setAnchorEl(null);
  };

  const handleClose = (event) => {
    setAnchorEl(null);
  };

  return (
    <div>
      <IconButton
        aria-label="more"
        id="long-button"
        aria-controls={open ? 'long-menu' : undefined}
        aria-expanded={open ? 'true' : undefined}
        aria-haspopup="true"
        onClick={handleClick}
      >
        <MoreVertIcon />
      </IconButton>
      <Menu
        id="long-menu"
        MenuListProps={{
          'aria-labelledby': 'long-button',
        }}
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        PaperProps={{
          style: {
            maxHeight: ITEM_HEIGHT * 4.5,
            width: '18ch',
          },
        }}
      >
        <MenuItem style={{gap: 4}} onClick={(event) => handleMenuItemClick("Edit", props.id)} disableRipple>
          <EditIcon />
            Edit
        </MenuItem>

        <MenuItem style={{color: 'red', gap: 4}} onClick={(event) => handleMenuItemClick("Delete", props.id)} disableRipple>
          <DeleteIcon />
            <a>Delete Kweet</a>
        </MenuItem>
      </Menu>
    </div>
  );
}