using Microsoft.Data.Sqlite;
using SuperDbApp.Model;

namespace SuperDbApp.Data
{
    internal class SQLiteDbManager
    {
        private readonly string filename;
        string conString;

        public SQLiteDbManager(string filename)
        {
            this.filename = filename;
            conString = $"Data Source={filename};";
        }

        public bool DoesDbExist()
        {
            return File.Exists(filename);
        }

        private void CreateDb()
        {
            using var con = new SqliteConnection(conString);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Factory (
                                    Id INTEGER PRIMARY KEY,
                                    Adress TEXT NOT NULL,
                                    Country TEXT NOT NULL
                                );

                                CREATE TABLE Product (
                                    Id INTEGER PRIMARY KEY,
                                    Name TEXT NOT NULL,
                                    Material TEXT NOT NULL,
                                    Price REAL NOT NULL, -- SQLite unterstützt den DECIMAL-Typ nicht direkt, REAL ist eine Alternative
                                    FactoryId INTEGER,
                                    FOREIGN KEY (FactoryId) REFERENCES Factory(Id)
                                );";

            cmd.ExecuteNonQuery();

        }

        public void SaveProducts(IEnumerable<Product> products)
        {
            if (!DoesDbExist())
                CreateDb();

            using var con = new SqliteConnection(conString);
            con.Open();

            foreach (var product in products)
            {
                long facId = 0;
                if (product.Factory != null)
                {
                    using var facInsertCmd = con.CreateCommand();
                    facInsertCmd.CommandText = "INSERT INTO Factory (Adress,Country) VALUES (@adr,@country)";
                    facInsertCmd.Parameters.AddWithValue("@adr", product.Factory.Adress);
                    facInsertCmd.Parameters.AddWithValue("@country", product.Factory.Country);
                    var result = facInsertCmd.ExecuteNonQuery();

                    using var lastIdCmd = con.CreateCommand();
                    lastIdCmd.CommandText = "SELECT last_insert_rowid()";
                    facId = (long)lastIdCmd.ExecuteScalar();

                }

                using var prodInsert = con.CreateCommand();
                prodInsert.CommandText = "INSERT INTO Product (Name, Material, Price, FactoryId) VALUES (@name,@mat,@price,@facId)";
                prodInsert.Parameters.AddWithValue("@name", product.Name);
                prodInsert.Parameters.AddWithValue("@mat", product.Material);
                prodInsert.Parameters.AddWithValue("@price", product.Price);
                prodInsert.Parameters.AddWithValue("@facId", facId);

                prodInsert.ExecuteNonQuery();
            }
        }


        public List<Product> LoadProductsWithFactories()
        {
            var products = new List<Product>();
            var factories = new Dictionary<int, Factory>();

            using (var con = new SqliteConnection(conString))
            {
                con.Open();

                var query = @"
                SELECT p.Id, p.Name, p.Material, p.Price, f.Id AS FactoryId, f.Adress, f.Country
                FROM Product p
                INNER JOIN Factory f ON p.FactoryId = f.Id";

                using var cmd = new SqliteCommand(query, con);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var factoryId = Convert.ToInt32(reader["FactoryId"]);
                    if (!factories.ContainsKey(factoryId))
                    {
                        factories[factoryId] = new Factory
                        {
                            Id = factoryId,
                            Adress = reader["Adress"].ToString(),
                            Country = reader["Country"].ToString(),
                            Products = new List<Product>()
                        };
                    }
                    var product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Material = reader["Material"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Factory = factories[factoryId]
                    };

                    factories[factoryId].Products.Add(product);
                    products.Add(product);
                }
            }

            return products;
        }

    }
}
