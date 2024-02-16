using ppedv.AdventureApp.Model.DbModel;

namespace ppedv.AdventureApp.Model.DomainModel
{
    public class EmployeeBirthdayCard
    {
        public Employee? Employee { get; set; } 

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
    }
}
