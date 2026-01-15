using Microsoft.EntityFrameworkCore;
using InlandMarinaData.Entities;

namespace InlandMarinaData.Data
{
    public class InlandMarinaContext : DbContext
    {
        public InlandMarinaContext(DbContextOptions<InlandMarinaContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dock> Docks { get; set; }
        public DbSet<Slip> Slips { get; set; }
        public DbSet<Lease> Leases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Dock>().ToTable("dock");
            modelBuilder.Entity<Slip>().ToTable("slip");
            modelBuilder.Entity<Lease>().ToTable("lease");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Leases)
                .WithOne(l => l.Customer)
                .HasForeignKey(l => l.CustomerID);

            modelBuilder.Entity<Dock>()
                .HasMany(d => d.Slips)
                .WithOne(s => s.Dock)
                .HasForeignKey(s => s.DockID);

            modelBuilder.Entity<Slip>()
                .HasMany(s => s.Leases)
                .WithOne(l => l.Slip)
                .HasForeignKey(l => l.SlipID);
        }
    }
}