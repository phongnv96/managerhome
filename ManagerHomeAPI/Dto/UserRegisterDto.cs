using System.ComponentModel.DataAnnotations;

namespace ManagerHomeAPI.Dto
{
    public class UserRegisterDto
    {
        public UserRegisterDto(string userName, string password, string firstName, string lastName)
        {
            this.userName = userName;
            this.Password = password;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        [Required]
        public string userName{set;get;}
        [Required]
        public string firstName{set;get;}
        [Required]
        public string lastName{set;get;}

        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify a password betwen 4 and 8 character")]
        public string Password{set;get;}
        
    }
}