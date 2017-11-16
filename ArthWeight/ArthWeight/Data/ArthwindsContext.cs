using ArthWeight.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArthWeight.Data
{
    public sealed class ArthwindsContext : IdentityDbContext<User>
    {
        public ArthwindsContext(DbContextOptions<ArthwindsContext> options) : base(options)
        {

        }
        public DbSet<WeightEntry> WeightEntries { get; set; }
    }
}
