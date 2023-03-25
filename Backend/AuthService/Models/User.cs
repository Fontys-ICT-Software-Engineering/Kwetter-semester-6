using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }  

        public User()
        {

        }

        public User(string userName, string password, UserRole role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
