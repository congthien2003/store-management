﻿using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Models;

namespace StoreManagement.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductSell> ProductSells { get; set; }
        public DbSet<OrderAccessToken> OrderAccessTokens { get; set; }
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<BankInfo> BankInfos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboItem> ComboItems { get; set; }
        public DbSet<Staff> Staffs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentType>()
                .HasOne(pt => pt.Store)
                .WithMany(s => s.PaymentTypes)
                .HasForeignKey(pt => pt.IdStore)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.IdOrder, od.IdFood });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.IdOrder)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Food)
                .WithMany(f => f.OrderDetails)
                .HasForeignKey(od => od.IdFood)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasOne(s => s.Store)
                .WithMany(st => st.Staff) // Một Store có nhiều Staff
                .HasForeignKey(s => s.IdStore) // Foreign Key
                .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(s => s.User)
                      .WithOne(u => u.Staff)
                      .HasForeignKey<Staff>(s => s.IdUser)
                      .OnDelete(DeleteBehavior.NoAction); 
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
