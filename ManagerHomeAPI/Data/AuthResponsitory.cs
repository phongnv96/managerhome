using System.Linq;
using System.Threading.Tasks;
using ManagerHomeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerHomeAPI.Data {
    public class AuthResponsitory : IAuthResponsitory {
        private readonly DataContext _context;

        public AuthResponsitory (DataContext context) {
            this._context = context;
        }
        public async Task<User> Login (string username, string password) {
            var user = await _context.Users.FirstOrDefaultAsync (x => x.userName == username);
            if (!VerifyPasswordHash (password, user.passWordHash, user.passWordSalt))
                return null;
            return user;
        }

        public async Task<User> Register (User user, string password) {
            byte[] passWordHash, passWordSalt;
            CreatePasswordHash(password, out passWordHash, out passWordSalt);
            user.passWordHash = passWordHash;
            user.passWordSalt = passWordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passWordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                passWordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }
        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var ComputeHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < ComputeHash.Length; i++) {
                    if (ComputeHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
        public async Task<bool> UserExists (string username) {
            if (await _context.Users.AnyAsync (x => x.userName == username))
                return true;
            return false;

        }
    }
}