

using Microsoft.EntityFrameworkCore;

namespace ECommerceNet.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<DataItem> DataItems { get; set; }
    }
}