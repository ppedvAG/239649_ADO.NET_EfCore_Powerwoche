﻿// <auto-generated />
using System;
using HalloEfCore_CodeFirst.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HalloEfCore_CodeFirst.Migrations
{
    [DbContext(typeof(EfContext))]
    [Migration("20240214152709_TischMitLampeMitStuhl")]
    partial class TischMitLampeMitStuhl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("PersonSequence");

            modelBuilder.Entity("DepartmentEmployee", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsId", "EmployeesId");

                    b.HasIndex("EmployeesId");

                    b.ToTable("DepartmentEmployee");
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [PersonSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(99)
                        .HasColumnType("nvarchar(99)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Customer", b =>
                {
                    b.HasBaseType("HalloEfCore_CodeFirst.Model.Person");

                    b.Property<string>("CustomerNr")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("nvarchar(55)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupportAgentId")
                        .HasColumnType("int");

                    b.HasIndex("SupportAgentId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Employee", b =>
                {
                    b.HasBaseType("HalloEfCore_CodeFirst.Model.Person");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MitLampe")
                        .HasColumnType("bit");

                    b.Property<string>("Schreibtischnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Stuhl")
                        .HasColumnType("bit");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DepartmentEmployee", b =>
                {
                    b.HasOne("HalloEfCore_CodeFirst.Model.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HalloEfCore_CodeFirst.Model.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Customer", b =>
                {
                    b.HasOne("HalloEfCore_CodeFirst.Model.Employee", "SupportAgent")
                        .WithMany("Customers")
                        .HasForeignKey("SupportAgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("SupportAgent");
                });

            modelBuilder.Entity("HalloEfCore_CodeFirst.Model.Employee", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
