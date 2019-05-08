using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerHomeAPI.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set;get;}
        public string userName {set;get;}
        public string firstName {set;get;}
        public string lastName {set;get;}
        public byte[] passWordHash {set;get;}
        public byte[] passWordSalt {set;get;}
    }
}