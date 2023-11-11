﻿// <auto-generated />
using System;
using CarteiraClientes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarteiraClientes.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231111140620_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarteiraClientes.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientId"));

                    b.Property<byte>("Age")
                        .HasColumnType("smallint")
                        .HasColumnName("age");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar")
                        .HasColumnName("gender");

                    b.Property<bool>("IsOverdue")
                        .HasColumnType("boolean")
                        .HasColumnName("is_overdue");

                    b.HasKey("ClientId");

                    b.HasIndex("ClientId")
                        .IsUnique()
                        .HasDatabaseName("client_id_idx");

                    b.ToTable("clients", (string)null);
                });

            modelBuilder.Entity("CarteiraClientes.Models.ClientCompany", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.HasKey("ClientId", "CompanyId");

                    b.HasIndex("ClientId")
                        .HasDatabaseName("client_company_client_id_idx");

                    b.HasIndex("CompanyId")
                        .HasDatabaseName("client_company_company_id_idx");

                    b.ToTable("clients_companies", (string)null);
                });

            modelBuilder.Entity("CarteiraClientes.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("FoundedDate")
                        .HasColumnType("date")
                        .HasColumnName("founded_date");

                    b.Property<decimal>("Revenue")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal")
                        .HasColumnName("revenue");

                    b.HasKey("CompanyId");

                    b.HasIndex("CompanyId")
                        .IsUnique()
                        .HasDatabaseName("company_id_idx");

                    b.ToTable("companies", (string)null);
                });

            modelBuilder.Entity("CarteiraClientes.Models.ClientCompany", b =>
                {
                    b.HasOne("CarteiraClientes.Models.Client", "Client")
                        .WithMany("ClientsCompanies")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarteiraClientes.Models.Company", "Company")
                        .WithMany("ClientsCompanies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CarteiraClientes.Models.Client", b =>
                {
                    b.Navigation("ClientsCompanies");
                });

            modelBuilder.Entity("CarteiraClientes.Models.Company", b =>
                {
                    b.Navigation("ClientsCompanies");
                });
#pragma warning restore 612, 618
        }
    }
}
