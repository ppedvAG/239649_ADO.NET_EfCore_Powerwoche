namespace ppedv.AdventureApp.Model.Contracts
{
    //https://deviq.com/design-patterns/repository-pattern
    //https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
    public interface IRepository
    {
        T? GetById<T>(int id) where T : class;

        IQueryable<T> Query<T>() where T : class; 

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;

        void SaveAll();
    }
}