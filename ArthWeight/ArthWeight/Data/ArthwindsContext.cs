using ArthWeight.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArthWeight.Data
{
    public sealed class ArthwindsContext : DbContext
    {
        public ArthwindsContext(DbContextOptions<ArthwindsContext> options) : base(options)
        {

        }
        public DbSet<WeightEntry> WeightEntries { get; set; }
    }
}
