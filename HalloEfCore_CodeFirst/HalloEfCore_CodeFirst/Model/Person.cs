using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HalloEfCore_CodeFirst.Model
{
    public abstract class Person
    {
        public int Id { get; set; }

        [MaxLength(77)]
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class Customer : Person
    {
        [MaxLength(55)]
        public string CustomerNr { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public virtual Employee? SupportAgent { get; set; }
    }

    public class Employee : Person
    {
        public string Job { get; set; } = string.Empty;
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Employee> Employees{ get; set; } = new HashSet<Employee>();

    }
}
