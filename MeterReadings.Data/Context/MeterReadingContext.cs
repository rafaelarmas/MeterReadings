using MeterReadings.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeterReadings.Data.Context
{
    public class MeterReadingContext : DbContext
    {
        public MeterReadingContext()
        {
        }

        public MeterReadingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<MeterReading>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\;Database=MeterReadings;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
