using ppedv.AdventureApp.Data.EfCore;
using ppedv.AdventureApp.Logic;
using ppedv.AdventureApp.Model.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true";

builder.Services.AddScoped<IRepository>(x => new EfRepository(conString));
builder.Services.AddScoped<EmployeeBirthdayCardService>();
builder.Services.AddScoped<EmployeeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
