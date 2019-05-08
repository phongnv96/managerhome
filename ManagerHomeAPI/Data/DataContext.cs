using ManagerHomeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagerHomeAPI.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
    }
}