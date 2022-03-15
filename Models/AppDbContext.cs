

using Microsoft.EntityFrameworkCore;

namespace ECommerceNet.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<DataItem> DataItems { get; set; }

        public DbSet<User> UserItems { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistoryItems { get; set; }
        public DbSet<Products> ProductsItems { get; set; }
        public DbSet<Comments> CommentsItems { get; set; }

        public DbSet<Cart> CartItems { get; set; }
    }
}