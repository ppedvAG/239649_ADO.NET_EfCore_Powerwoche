using System.Globalization;

namespace HalloEfCore_CodeFirst.Model
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class Customer : Person
    {
        public string CustomerNr { get; set; } = string.Empty;
        public virtual Employee? SupportAgent { get; set; } 
    }

    public class Employee : Person
    {
        public string Job { get; set; } = string.Empty;
        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
