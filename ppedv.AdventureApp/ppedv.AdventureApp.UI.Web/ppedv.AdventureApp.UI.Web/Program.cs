using ppedv.AdventureApp.Data.EfCore;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.UI.Web.Client.Pages;
using ppedv.AdventureApp.UI.Web.Components;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//var conString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true";
var conString = builder.Configuration["conString"];

builder.Services.AddScoped<IRepository>(x => new EfRepository(conString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ppedv.AdventureApp.UI.Web.Client._Imports).Assembly);

app.Run();
