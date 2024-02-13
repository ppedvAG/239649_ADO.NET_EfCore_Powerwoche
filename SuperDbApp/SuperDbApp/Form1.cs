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
    }
}
