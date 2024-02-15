using Microsoft.EntityFrameworkCore;

namespace ppedv.AdventureApp.Data.EfCore.Tests
{
    public class AdventureWorks2019ContextTests
    {
        [Fact]
        public void CanReadProduct()
        {
            var builder = new DbContextOptionsBuilder<AdventureWorks2019Context>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true");
            AdventureWorks2019Context con = new AdventureWorks2019Context(builder.Options);

            //AdventureWorks2019Context con = new AdventureWorks2019Context();
            var prod = con.Employees.FirstOrDefault();

            Assert.NotNull(prod);
        }
    }
}