
namespace NorthwindEmployeeManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbManager dbm = new DbManager();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = dbm.GetAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dbm.AddNewEmployee("Fred", "Feuerstein", new DateTime(2000, 1, 1));
                dataGridView1.DataSource = dbm.GetAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Employee emp)
            {
                var msg = $"Soll der Employee {emp.FirstName} {emp.LastName} wirklich gelöscht werden?";
                var result = MessageBox.Show(msg, "Löschen?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dbm.DeleteEmployee(emp);
                        dataGridView1.DataSource = dbm.GetAllEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dbm.AlleJüngerMachen();
                dataGridView1.DataSource = dbm.GetAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
