﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreManagement.Infrastructure.Data;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241030031109_DbInit")]
    partial class DbInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StoreManagement.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdStore")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdStore");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Charge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int>("IdPaymentType")
                        .HasColumnType("int");

                    b.Property<int?>("IdVoucher")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TotalOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdOrder");

                    b.HasIndex("IdPaymentType");

                    b.HasIndex("IdVoucher");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTable")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("NameUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdTable");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.OrderDetail", b =>
                {
                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int>("IdFood")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("IdOrder", "IdFood");

                    b.HasIndex("IdFood");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdStore")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdStore");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.ProductSell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("ProductSells");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdStore")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdStore");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("IdStore")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdStore");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Category", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Store", "Store")
                        .WithMany("Categories")
                        .HasForeignKey("IdStore")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Food", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Invoice", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement.Domain.Models.PaymentType", "PaymentType")
                        .WithMany("Invoices")
                        .HasForeignKey("IdPaymentType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreManagement.Domain.Models.Voucher", "Voucher")
                        .WithMany("Invoices")
                        .HasForeignKey("IdVoucher");

                    b.Navigation("Order");

                    b.Navigation("PaymentType");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Order", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Table", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("IdTable")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.OrderDetail", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Food", "Food")
                        .WithMany("OrderDetails")
                        .HasForeignKey("IdFood")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("StoreManagement.Domain.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.PaymentType", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Store", "Store")
                        .WithMany("PaymentTypes")
                        .HasForeignKey("IdStore")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.ProductSell", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Food", "Food")
                        .WithMany("ProductSells")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Store", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.User", "User")
                        .WithOne("Store")
                        .HasForeignKey("StoreManagement.Domain.Models.Store", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Table", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Store", "Store")
                        .WithMany("Tables")
                        .HasForeignKey("IdStore")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Voucher", b =>
                {
                    b.HasOne("StoreManagement.Domain.Models.Store", "Store")
                        .WithMany("Vouchers")
                        .HasForeignKey("IdStore")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Category", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Food", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductSells");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.PaymentType", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Store", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("PaymentTypes");

                    b.Navigation("Tables");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Table", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.User", b =>
                {
                    b.Navigation("Store")
                        .IsRequired();
                });

            modelBuilder.Entity("StoreManagement.Domain.Models.Voucher", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
