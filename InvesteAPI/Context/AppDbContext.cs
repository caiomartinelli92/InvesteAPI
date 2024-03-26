using InvesteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InvesteAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Investimento> Investimentos { get; set;}
    }
}
