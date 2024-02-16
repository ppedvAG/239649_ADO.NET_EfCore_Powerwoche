using FluentAssertions;
using Moq;
using ppedv.AdventureApp.Model.Contracts;
using ppedv.AdventureApp.Model.DbModel;

namespace ppedv.AdventureApp.Logic.Tests
{
    public class EmployeeBirthdayCardServiceTests
    {
        [Fact]
        public void CreateCard_with_test_employee()
        {
            var testEmp = new Employee()
            {
                BusinessEntityId = 3,
                BusinessEntity = new Person() { FirstName = "Fred", LastName = "Feierstein" },
                BirthDate = new DateOnly(2000, 1, 1)
            };

            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetEmployeesWithBusinessEntity()).Returns(() => new[] { testEmp }.AsQueryable());

            var ebcs = new EmployeeBirthdayCardService(mock.Object);
            var result = ebcs.CreateCard(3);

            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("9.9.1941", 82)]
        [InlineData("28.12.1969", 54)]
        [InlineData("28.10.1955", 68)]
        public void CalcAgeTests(string birthDate, int expAge)
        {
            var ebcs = new EmployeeBirthdayCardService(null);

            var dt = DateTime.Parse(birthDate);

            ebcs.CalcAge(dt).Should().Be(expAge);
        }

    }
}