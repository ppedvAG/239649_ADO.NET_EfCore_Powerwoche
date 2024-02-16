using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DbModel;


namespace ppedv.AdventureApp.Logic
{
    public class EmployeeService
    {
        private readonly IRepository repository;

        public EmployeeService(IRepository repository)
        {
            ArgumentNullException.ThrowIfNull(nameof(repository));

            this.repository = repository;
        }

        public IEnumerable<Employee> GetBirthdayEmployees()
        {
            return repository.Query<Employee>().Where(x => x.BirthDate.Month == DateTime.Now.Month);
        }
    }
}
