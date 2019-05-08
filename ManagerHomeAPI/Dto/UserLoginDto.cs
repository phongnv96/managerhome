using System.ComponentModel.DataAnnotations;

namespace ManagerHomeAPI.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string username{set;get;}
        [Required]
        public string password{set;get;}
    }
}