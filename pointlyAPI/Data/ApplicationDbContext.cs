using Microsoft.EntityFrameworkCore;
using Pointly.Models;

namespace Pointly.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Charger> Chargers { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<PointAssignment> PointAssignments { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // User configurations
    modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

    // Admin - User relationship
    modelBuilder.Entity<Admin>()
        .HasOne(a => a.User)
        .WithOne()
        .HasForeignKey<Admin>(a => a.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Charger - User relationship
    modelBuilder.Entity<Charger>()
        .HasOne(c => c.User)
        .WithOne()
        .HasForeignKey<Charger>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Merchant - User relationship
    modelBuilder.Entity<Merchant>()
        .HasOne(m => m.User)
        .WithOne()
        .HasForeignKey<Merchant>(m => m.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Customer - User relationship
    modelBuilder.Entity<Customer>()
        .HasOne(c => c.User)
        .WithOne()
        .HasForeignKey<Customer>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Customer - Wallet (one-to-one)
    modelBuilder.Entity<Wallet>()
        .HasOne(w => w.Customer)
        .WithOne(c => c.Wallet)
        .HasForeignKey<Wallet>(w => w.CustomerId)
        .OnDelete(DeleteBehavior.Cascade);

    // PointAssignment relationships - THIS IS THE FIX
    modelBuilder.Entity<PointAssignment>()
        .HasOne(pa => pa.FromAdmin)
        .WithMany()
        .HasForeignKey(pa => pa.FromAdminId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<PointAssignment>()
        .HasOne(pa => pa.FromCharger)
        .WithMany()
        .HasForeignKey(pa => pa.FromChargerId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<PointAssignment>()
        .HasOne(pa => pa.ToCharger)
        .WithMany(c => c.PointAssignments)
        .HasForeignKey(pa => pa.ToChargerId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<PointAssignment>()
        .HasOne(pa => pa.ToMerchant)
        .WithMany()
        .HasForeignKey(pa => pa.ToMerchantId)
        .OnDelete(DeleteBehavior.Restrict);

    // Decimal precision for points and money
    modelBuilder.Entity<Admin>()
        .Property(a => a.TotalPointsPool)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Charger>()
        .Property(c => c.AvailablePoints)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Merchant>()
        .Property(m => m.PointsBalance)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Wallet>()
        .Property(w => w.PointsBalance)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Transaction>()
        .Property(t => t.PointsAmount)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Transaction>()
        .Property(t => t.PurchaseAmount)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Offer>()
        .Property(o => o.PointsCost)
        .HasPrecision(18, 2);

    modelBuilder.Entity<PointAssignment>()
        .Property(pa => pa.PointsAmount)
        .HasPrecision(18, 2);
}
    }
}