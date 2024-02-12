using Microsoft.Data.SqlClient;

Console.WriteLine("Hello Database");

var conString = "Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=true;TrustServerCertificate=true;";
//var conString = "Server=.;Database=NORTHWiND;Trusted_Connection=true;TrustServerCertificate=true;";

try
{
    var con = new SqlConnection(conString);
    con.Open();
    Console.WriteLine("DB Verbindung OK");
}
catch (SqlException ex)
{
    Console.WriteLine($"Datenbankfehler: {ex.ErrorCode} - {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Fehler: {ex.Message}");
}