using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL.Database
{
    public class MyDbContext : DbContext
    {
        private void InitialDBContext()
        {
            DbInitializer.Initialize(this);
        }

        // Default Constructor
        public MyDbContext()
        {
            InitialDBContext();
        }

        // Constructor with options
        public MyDbContext(DbContextOptions<MyDbContext> options)
                : base(options)
        {
            InitialDBContext();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Mod2Lab1DB;Trusted_Connection=True;");
            }
        }
    }
}