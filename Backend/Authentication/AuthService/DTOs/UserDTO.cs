﻿namespace AuthService.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }    



        public UserDTO(string username) 
        { 
            UserName = username;
        }
    }
}
