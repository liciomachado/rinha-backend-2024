using Microsoft.EntityFrameworkCore;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData([
                new(1, 100000, 0),
                new(2, 80000, 0),
                new(3, 1000000, 0),
                new(4, 10000000, 0),
                new(5, 500000, 0)
            ]);

            base.OnModelCreating(modelBuilder);
        }

    }
}
