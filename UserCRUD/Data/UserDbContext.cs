using Microsoft.EntityFrameworkCore;
using UserCRUD.Models.Domain;

namespace UserCRUD.Data
{
	public class UserDbContext : DbContext
	{
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
