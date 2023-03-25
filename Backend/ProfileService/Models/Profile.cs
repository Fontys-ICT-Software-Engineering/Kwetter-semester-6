using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
{
    public class Profile
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }    

        public string UserName { get; set; }

        public string PhoneNumber { get; set; } 

        public string Adress { get; set; }  

        public string Bio { get; set; }

    }
}
