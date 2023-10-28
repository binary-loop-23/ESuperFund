﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using esuperfund.Data;

#nullable disable

namespace esuperfund.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231028040615_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("esuperfund.Models.BankTransaction", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Narration")
                        .HasColumnType("longtext");

                    b.HasKey("AccountNumber", "Date");

                    b.ToTable("BankTransactions");
                });

            modelBuilder.Entity("esuperfund.Models.RawBankTransaction", b =>
                {
                    b.Property<int?>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Narration")
                        .HasColumnType("longtext");

                    b.ToTable("RawBankTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
