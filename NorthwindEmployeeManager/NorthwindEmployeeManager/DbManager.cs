using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace NorthwindEmployeeManager
{
    internal class DbManager
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=true;TrustServerCertificate=true;";

        internal void DeleteEmployee(Employee emp)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM Employees WHERE EmployeeId=@id";
                    cmd.Parameters.AddWithValue("@id", emp.Id);

                    cmd.ExecuteNonQuery();
                }

            }//con.Dispose();  //--> con.Close();

            #region complete finally dispose
            //var con = new SqlConnection(conString);
            //try
            //{
            //    con.Open();

            //    var cmd = new SqlCommand();
            //    cmd.Connection = con;
            //    cmd.CommandText = "DELETE FROM Employees WHERE EmployeeId=@id";
            //    cmd.Parameters.AddWithValue("@id", emp.Id);

            //    cmd.ExecuteNonQuery();

            //    //con.Close();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    throw;
            //}
            //finally
            //{
            //    con.Dispose();  //--> con.Close();
            //}
            #endregion
        }

        internal void AddNewEmployee(string firstName, string lastName, DateTime birthDate)
        {
            using var con = new SqlConnection(conString);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Employees (FirstName,LastName,BirthDate) VALUES (@fname,@lname,@bDate)";
            cmd.Parameters.AddWithValue("@fname", firstName);
            cmd.Parameters.AddWithValue("@lname", lastName);
            cmd.Parameters.AddWithValue("@bDate", birthDate);

            cmd.ExecuteNonQuery();
        }// con.Dispose(), cmd.Dispose()

        internal IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using var con = new SqlConnection(conString);
            con.Open();

            using var cmdGetAll = new SqlCommand();
            cmdGetAll.Connection = con;
            cmdGetAll.CommandText = "SELECT * FROM Employees";

            using var reader = cmdGetAll.ExecuteReader();
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

            return employees;
        }

        internal void AlleJüngerMachen()
        {
            using var con = new SqlConnection(conString);
            con.Open();
            using var trans = con.BeginTransaction();

            foreach (var emp in GetAllEmployees())
            {
                if (emp.FirstName.StartsWith("M"))
                    throw new OutOfMemoryException();

                emp.BirthDate = emp.BirthDate.AddYears(1);
                using var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = trans;
                cmd.CommandText = "UPDATE Employees SET BirthDate = @bDate WHERE EmployeeId=@id";
                cmd.Parameters.AddWithValue("@id", emp.Id);
                cmd.Parameters.AddWithValue("@bDate", emp.BirthDate);
                cmd.ExecuteNonQuery();
            }
            trans.Commit();
        }
    }
}
