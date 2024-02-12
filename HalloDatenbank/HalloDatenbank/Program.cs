using Microsoft.Data.SqlClient;
using System.Dynamic;

Console.WriteLine("Hello Database");

var conString = "Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=true;TrustServerCertificate=true;";
//var conString = "Server=.;Database=NORTHWiND;Trusted_Connection=true;TrustServerCertificate=true;";

try
{
    var con = new SqlConnection(conString);
    con.Open();
    Console.WriteLine("DB Verbindung OK");

    var cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "SELECT COUNT(*) FROM Employees";

    var empCount = cmd.ExecuteScalar();
    Console.WriteLine($"Es sind {empCount} Employees in der DB");

    Console.WriteLine("Suche:");
    var such = Console.ReadLine();

    var cmdGetAll = new SqlCommand();
    cmdGetAll.Connection = con;
    cmdGetAll.CommandText = "SELECT * FROM Employees WHERE FirstName LIKE '%" + such + "%'";

    var reader = cmdGetAll.ExecuteReader();
    while (reader.Read())
    {
        int id = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
        string name = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
        DateTime bDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
        Console.WriteLine($"{id} {name} {bDate:d}");
    }

    con.Close();
}
catch (SqlException ex)
{
    Console.WriteLine($"Datenbankfehler: {ex.ErrorCode} - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler: {ex.Message}");
}