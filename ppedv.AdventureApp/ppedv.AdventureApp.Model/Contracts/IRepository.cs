
using ppedv.AdventureApp.Model.DbModel;
using System.Linq.Expressions;

namespace ppedv.AdventureApp.Model.Contracts
{
    //https://deviq.com/design-patterns/repository-pattern
    //https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
    //https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
    public interface IRepository
    {
        IQueryable<Employee> GetEmployeesWithBusinessEntity();


        T? GetById<T>(int id) where T : class;

        IQueryable<T> Query<T>() where T : class; 

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        void SaveAll();
    }
 
}