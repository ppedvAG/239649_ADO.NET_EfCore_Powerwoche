dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2019;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer  --no-onconfiguring --context-dir . --output-dir ..\ppedv.AdventureApp.Model\DbModel --no-build --force --namespace ppedv.AdventureApp.Model.DbModel --context-namespace ppedv.AdventureApp.Model.Data.EfCore