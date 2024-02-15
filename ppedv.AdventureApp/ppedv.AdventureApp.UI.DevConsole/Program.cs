using Autofac;
using ppedv.AdventureApp.Data.EfCore;
using ppedv.AdventureApp.Logic;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DomainModel;

Console.WriteLine("** AdventureApp v1.0 **");

var conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true";

//manual Injection
//IRepository repo = new EfRepository(conString);
//var empService = new EmployeeService(repo);

//AutoFac
var cb = new ContainerBuilder();
cb.Register<IRepository>(x => new EfRepository(conString));
cb.RegisterType<EmployeeService>();
var container = cb.Build();

EmployeeService employeeService = container.Resolve<EmployeeService>();
IRepository repo = container.Resolve<IRepository>();

var birthDayPeople = employeeService.GetBirthdayEmployees();
foreach (var p in birthDayPeople)
{
    Console.WriteLine($"{p.LoginId} {p.BirthDate:d}");
}

var query = repo.Query<Product>().Where(x => x.SellStartDate.Year > 2010).OrderBy(x => x.SellStartDate).ToList();

foreach (var p in query)
{
    Console.WriteLine($"{p.Name} ({p.ProductNumber}) {p.SellStartDate:d}");
}

