using Microsoft.Data.SqlClient;

namespace NorthwindEmployeeManager
{
    internal class DbManager
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=true;TrustServerCertificate=true;";

        internal void DeleteEmployee(Employee emp)
        {
            var con = new SqlConnection(conString);
            con.Open();

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Employees WHERE EmployeeId=@id";
            cmd.Parameters.AddWithValue("@id", emp.Id);

            cmd.ExecuteNonQuery();
        }

        internal void AddNewEmployee(string firstName, string lastName, DateTime birthDate)
        {
            var con = new SqlConnection(conString);
            con.Open();

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Employees (FirstName,LastName,BirthDate) VALUES (@fname,@lname,@bDate)";
            cmd.Parameters.AddWithValue("@fname", firstName);
            cmd.Parameters.AddWithValue("@lname", lastName);
            cmd.Parameters.AddWithValue("@bDate", birthDate);

            cmd.ExecuteNonQuery();
        }

        internal IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            var con = new SqlConnection(conString);
            con.Open();

            var cmdGetAll = new SqlCommand();
            cmdGetAll.Connection = con;
            cmdGetAll.CommandText = "SELECT * FROM Employees";

            var reader = cmdGetAll.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"))
                });
            }

            con.Close();

            return employees;
        }

        internal void AlleJüngerMachen()
        {
            var con = new SqlConnection(conString);
            con.Open();
            var trans = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            try
            {
                foreach (var emp in GetAllEmployees())
                {
                    //if (emp.FirstName.StartsWith("M"))
                        //throw new OutOfMemoryException();
                   
                    emp.BirthDate = emp.BirthDate.AddYears(1);
                    var cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = trans;
                    cmd.CommandText = "UPDATE Employees SET BirthDate = @bDate WHERE EmployeeId=@id";
                    cmd.Parameters.AddWithValue("@id", emp.Id);
                    cmd.Parameters.AddWithValue("@bDate", emp.BirthDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }

            trans.Commit();

            con.Close();
        }
    }
}
