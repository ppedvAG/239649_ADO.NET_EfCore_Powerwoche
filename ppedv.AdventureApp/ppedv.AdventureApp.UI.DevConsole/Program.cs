using ppedv.AdventureApp.Data.EfCore;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DomainModel;

Console.WriteLine("** AdventureApp v1.0 **");

var conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true";

IRepository repo = new EfRepository(conString);

var query = repo.Query<Product>().Where(x => x.SellStartDate.Year > 2010).OrderBy(x => x.SellStartDate).ToList();

foreach (var p in query)
{
    Console.WriteLine($"{p.Name} ({p.ProductNumber}) {p.SellStartDate:d}");
}

