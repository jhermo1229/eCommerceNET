

using Microsoft.EntityFrameworkCore;

namespace ECommerceNet.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> User { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public DbSet<Cart> Cart { get; set; }
    }
}