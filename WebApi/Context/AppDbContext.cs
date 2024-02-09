using Microsoft.EntityFrameworkCore;
using WebApi.DataModels;

namespace WebApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.
            UseSqlServer(@"Data Source = DESKTOP-7HLK92Q\SEPEHRSQL;Initial Catalog=Fanap;User ID=sa;Password=sphrm26sphrm26;Encrypt=False;TrustServerCertificate=True");
    }
}

