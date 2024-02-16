using Microsoft.EntityFrameworkCore;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.Data.EfCore;
using ppedv.AdventureApp.Model.DomainModel;

namespace ppedv.AdventureApp.Data.EfCore
{
    public class EfRepository : IRepository
    {
        AdventureWorks2019Context context;

        public EfRepository(string conString)
        {
            var builder = new DbContextOptionsBuilder<AdventureWorks2019Context>();
            builder.UseSqlServer(conString);
            context = new AdventureWorks2019Context(builder.Options);
        }



        public void Add<T>(T entity) where T : class
        {
            context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return context.Set<T>();
        }

        public T? GetById<T>(int id) where T : class
        {
            return context.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            context.Set<T>().Update(entity);
        }

        public IQueryable<Employee> GetEmployeesWithBusinessEntity()
        {
            return context.Set<Employee>().Include(x => x.BusinessEntity);
        }
    }
}
