using System.Threading.Tasks;
using ManagerHomeAPI.Model;

namespace ManagerHomeAPI.Data
{
    public interface IAuthResponsitory
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);

    }
}