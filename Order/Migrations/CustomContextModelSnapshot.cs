﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockAccess;

namespace StockAccess.Migrations
{
    [DbContext(typeof(CustomContext))]
    partial class CustomContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StockAccess.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("主键");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("商品名");

                    b.Property<int>("TotalStock")
                        .HasColumnType("int")
                        .HasComment("总库存");

                    b.Property<int>("UsedStock")
                        .HasColumnType("int")
                        .HasComment("已用库存");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7e1f54d9-ab72-e583-4375-0565349c3982"),
                            Name = "商品",
                            TotalStock = 10000,
                            UsedStock = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}