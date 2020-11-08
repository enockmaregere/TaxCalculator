using Microsoft.EntityFrameworkCore;

using TaxCalculator.Entities;

namespace TaxCalculator.DataContext
{
    public class TaxCalculatorContext : DbContext
    {
        public TaxCalculatorContext(DbContextOptions<TaxCalculatorContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxBracket>()
                .Property(b => b.Rate).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxBracket>()
                .Property(b => b.From).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxBracket>()
                .Property(b => b.To).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxCalculationResult>()
               .Property(b => b.AnnualIncome).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxCalculationResult>()
               .Property(b => b.CalculatedTax).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxFlatRate>()
                .Property(b => b.Rate).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxFlatValue>()
                .Property(b => b.FlatValue).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxFlatValue>()
                .Property(b => b.IncomeThreshold).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TaxFlatValue>()
                .Property(b => b.Rate).HasColumnType("decimal(18,2)");
        }

        public DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }
        public DbSet<TaxBracket> TaxBrackets { get; set; }
        public DbSet<TaxFlatRate> TaxFlatRates { get; set; }
        public DbSet<TaxFlatValue> TaxFlatValues { get; set; }
        public DbSet<TaxCalculationResult> TaxCalculationResults { get; set; }
    }
}
