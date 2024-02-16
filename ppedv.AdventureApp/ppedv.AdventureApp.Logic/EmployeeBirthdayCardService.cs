using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DbModel;
using ppedv.AdventureApp.Model.DomainModel;

namespace ppedv.AdventureApp.Logic
{
    public class EmployeeBirthdayCardService
    {
        private readonly IRepository repo;

        public EmployeeBirthdayCardService(IRepository repo)
        {
            this.repo = repo;
        }

        public EmployeeBirthdayCard CreateCard(int employeeId)
        {
            var emp = repo.GetEmployeesWithBusinessEntity().FirstOrDefault(x => x.BusinessEntityId == employeeId);

            if (emp == null)
                throw new ArgumentException($"Employee {employeeId} not found");

            var card = new EmployeeBirthdayCard();
            card.Employee = emp;
            card.Title = "HAPPY BIRTHDAY";
            card.Name = emp.BusinessEntity?.FirstName + " " + emp.BusinessEntity?.LastName;
            card.Text = $@"Dear {card.Name},

On this special day, I wanted to take a moment to celebrate you and all the wonderful things that make you who you are. You bring light and positivity wherever you go, and your presence is truly a gift to those around you.

May this year be filled with countless blessings, memorable moments, and endless opportunities for growth and happiness. You deserve the world and more.

Happy Birthday! Here's to another amazing year ahead.

With love and warmest wishes.";

            card.Age = CalcAge(emp.BirthDate.ToDateTime(TimeOnly.MinValue));

            return card;
        }

        public int CalcAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
