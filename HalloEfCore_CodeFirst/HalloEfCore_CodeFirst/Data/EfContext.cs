using HalloEfCore_CodeFirst.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HalloEfCore_CodeFirst.Data
{
    public class EfContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conString = "Server=(localdb)\\mssqllocaldb;Database=EfCore_CodeFirst;Trusted_Connection=true;";
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conString).LogTo(Console.WriteLine).EnableSensitiveDataLogging(true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Person>().UseTpcMappingStrategy();
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(99);
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
    }
}
