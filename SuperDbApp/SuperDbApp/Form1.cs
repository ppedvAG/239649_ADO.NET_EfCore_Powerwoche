using SuperDbApp.Data;
using SuperDbApp.Model;

namespace SuperDbApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DemoDatenGenerator.GetDemoProducts();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is Factory f)
            {
                e.Value = $"{f.Adress} {f.Country}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is IEnumerable<Product> prod)
            {
                var sqliteManager = new SQLiteDbManager("test.db");
                sqliteManager.SaveProducts(prod);
                MessageBox.Show(":-)");
            }
            else
            {
                MessageBox.Show("Keine Daten zum Speichern da :-(");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sqliteManager = new SQLiteDbManager("test.db");
            dataGridView1.DataSource = sqliteManager.LoadProductsWithFactories();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource is IEnumerable<Product> prod)
            {
                var xManager = new ExcelManager();
                xManager.SaveProductsToExcel(prod, "prod.xlsx");
                MessageBox.Show(":-)");
            }
            else
            {
                MessageBox.Show("Keine Daten zum Speichern da :-(");
            }
        }
    }
}
