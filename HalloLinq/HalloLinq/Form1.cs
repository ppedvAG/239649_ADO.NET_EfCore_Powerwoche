using System.Runtime.CompilerServices;

namespace HalloLinq
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IEnumerable<Person> GetDemoPersonen()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new Person()
                {
                    Name = $"Fred #{i:000}",
                    GebDatum = DateTime.Now.AddYears(-30).AddDays(i * 25),
                    City = "Steinhausen"
                };
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDemoPersonen().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = new List<Person>();
            foreach (var p in GetDemoPersonen())
            {
                if (p.GebDatum.Month == DateTime.Now.Month)
                    result.Add(p);
            }
            dataGridView1.DataSource = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var query = from p in GetDemoPersonen()
                        where p.GebDatum.Month == DateTime.Now.Month
                        orderby p.GebDatum.Year descending, p.GebDatum.Day descending
                        select p;

            dataGridView1.DataSource = query.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //DateTime.Now.GetKW();

            dataGridView1.DataSource = GetDemoPersonen().Where(p => p.GebDatum.Month == DateTime.Now.Month)
                                                        .OrderByDescending(x => x.GebDatum.Year)
                                                        .ThenByDescending(x => x.GebDatum.Day)
                                                        .ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var avg = GetDemoPersonen().Average(x => x.GebDatum.Year);
            MessageBox.Show($"Durchschnittsjahr: {avg}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var count = GetDemoPersonen().Count(x => x.GebDatum.Month == DateTime.Now.Month);
            MessageBox.Show($"Die Monat Gebtag: {count}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var first = GetDemoPersonen().OrderBy(x => x.GebDatum).FirstOrDefault(x => x.GebDatum.Month == 12);
            //var first = GetDemoPersonen().First(x => x.GebDatum.Month == 13);

            if (first != null)
                MessageBox.Show(first.Name);
            else
                MessageBox.Show("Nix gefunden");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var grps = GetDemoPersonen().GroupBy(x => x.GebDatum.Month);

            treeView1.Nodes.Clear();
            foreach (var item in grps.OrderBy(x => x.Key))
            {
                var monthNode = new TreeNode() { Text = $"{item.Key:00}" };
                treeView1.Nodes.Add(monthNode);
                foreach (var p in item)
                {
                    monthNode.Nodes.Add(new TreeNode() { Text = $"{p.Name} - {p.GebDatum:d}" });
                }
            }
        }
         
        private void button9_Click(object sender, EventArgs e)
        {
            //var namen = string.Join(" ,", GetDemoPersonen().Select(x => x.Name));
            var namen = string.Join(" | ", GetDemoPersonen().Select(x => $"{x.Name}: {x.GebDatum.Year}"));
            MessageBox.Show(namen);
        }
    }

    static class DateTimeEx
    {
        //extension Method
        public static int GetKW(this DateTime dt)
        {
            return 7;
        }
    }
}
