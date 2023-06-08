using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroTransport
{
    public partial class Form2 : Form
    {
        string role;
        List<dynamic> transports;
        int r=-2;
        public Form2(string role)
        {
            InitializeComponent();
            if (role == "a")
                groupBox2.Visible = true;
        }

        public static List<dynamic> GetTransports()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.TransportAmount.Where(t => t.Count != 0).Select(t =>
                    new
                    {
                        Id = t.Transport.Id,
                        Name = t.Transport.Name,
                        Category = t.Transport.Category.Name,
                        Speed = t.Transport.Speed,
                        WheelDiameter = t.Transport.WheelDiameter,
                        Weight = t.Transport.Weight,
                        EnginePower = t.Transport.EnginePower,
                        ManufacturerName = t.Transport.Manufacturer.Name,
                        Price = t.Transport.Price,
                        Count = t.Count
                    }).ToList<dynamic>();
            }
        }

        void LoadTransports(List<dynamic> transports)
        {
            dataGridView1.Rows.Clear();
            if (transports.Any())
            {
                foreach (var t in transports)
                {
                    dataGridView1.Rows.Add(t.Id, t.Name, t.Category,
                        t.Speed, t.WheelDiameter,
                        t.Weight, t.EnginePower,
                        t.ManufacturerName,
                        t.Price, t.Count);
                }
            }
        }

        public static List<string> GetCategories()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Category.Select(c => c.Name).ToList();
            }
        }

        void LoadCategories()
        {
            comboBox1.Items.Clear();
            if(GetCategories().Any())
            comboBox1.Items.AddRange(GetCategories().ToArray());
        }

        public static List<string> GetManufacturers()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Manufacturer.Select(c => c.Name).ToList();
            }
        }

        void LoadManufacturers()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(GetManufacturers().ToArray());
        }

        List<dynamic> GetFilteredTransports()
        {
            r = -2;
            var transports = GetTransports();
            if (textBox1.Text != "")
            {
                transports = transports.Where(t => t.Name.Contains(textBox1.Text)).ToList<dynamic>();
            }
            if (comboBox1.SelectedIndex != -1)
            {
                transports = transports.Where(t => t.Category.Contains(comboBox1.SelectedItem.ToString())).ToList<dynamic>();
            }
            if (numericUpDown1.Value != 0)
            {
                transports = transports.Where(t => t.Speed == numericUpDown1.Value).ToList<dynamic>();
            }
            if (numericUpDown2.Value != 0)
            {
                transports = transports.Where(t => t.WheelDiameter == numericUpDown2.Value).ToList<dynamic>();
            }
            if (numericUpDown3.Value != 0)
            {
                transports = transports.Where(t => t.EnginePower == numericUpDown3.Value).ToList<dynamic>();
            }
            if (numericUpDown4.Value != 0)
            {
                transports = transports.Where(t => t.Weight == numericUpDown4.Value).ToList<dynamic>();
            }
            if (comboBox2.SelectedIndex != -1)
            {
                transports = transports.Where(t => t.ManufacturerName.Contains(comboBox2.SelectedItem.ToString())).ToList<dynamic>();
            }
            transports = transports.Where(t => t.Price >= trackBar1.Value && t.Price <= trackBar2.Value).ToList<dynamic>();
            return transports;
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            LoadCategories();
            LoadManufacturers();
            LoadTransports(GetTransports());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            trackBar1.Value = 0;
            trackBar2.Value = trackBar2.Maximum;
            LoadTransports(GetTransports());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadTransports(GetFilteredTransports());
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox4.Text = trackBar1.Value.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox6.Text = trackBar2.Value.ToString();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            r = -2;
            List<dynamic> sortedTransports;
            if (GetFilteredTransports().Count == dataGridView1.Rows.Count - 1)
            {
                sortedTransports = GetFilteredTransports();
            }
            else
            {
                sortedTransports = GetTransports();
            }
            switch(e.ColumnIndex)
            {
                case 1:
                    sortedTransports = sortedTransports.OrderBy(t => t.Name).ToList<dynamic>();
                    break;
                case 2:
                    sortedTransports = sortedTransports.OrderBy(t => t.Category).ToList<dynamic>();
                    break;
                case 3:
                    sortedTransports = sortedTransports.OrderBy(t => t.Speed).ToList<dynamic>();
                    break;
                case 4:
                    sortedTransports = sortedTransports.OrderBy(t => t.WheelDiameter).ToList<dynamic>();
                    break;
                case 5:
                    sortedTransports = sortedTransports.OrderBy(t => t.Weight).ToList<dynamic>();
                    break;
                case 6:
                    sortedTransports = sortedTransports.OrderBy(t => t.EnginePower).ToList<dynamic>();
                    break;
                case 7:
                    sortedTransports = sortedTransports.OrderBy(t => t.ManufacturerName).ToList<dynamic>();
                    break;
                case 8:
                    sortedTransports = sortedTransports.OrderBy(t => t.Price).ToList<dynamic>();
                    break;
                case 9:
                    sortedTransports = sortedTransports.OrderBy(t => t.Count).ToList<dynamic>();
                    break;
            }
            LoadTransports(sortedTransports);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add f = new Add();
            f.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                if (r >= 0&&r<dataGridView1.Rows.Count)
                {
                    var val = Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value);
                    var transport = db.Transport.First(t => t.Id == val);
                    Edit f = new Edit(transport.Id);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Оберіть запис", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                if (r >= 0 && r < dataGridView1.Rows.Count)
                {
                    var dialogResult = MessageBox.Show("Ви точно хочете зняти товар з продажу?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var val = Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value);
                        var transport = db.TransportAmount.First(t => t.Transport.Id == val);
                        transport.Count = 0;
                        db.Entry(transport).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTransports(GetTransports());
                        textBox1.Text = "";
                        textBox4.Text = "";
                        textBox6.Text = "";
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        numericUpDown1.Value = 0;
                        numericUpDown2.Value = 0;
                        numericUpDown3.Value = 0;
                        trackBar1.Value = 0;
                        trackBar2.Value = trackBar2.Maximum;
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть запис", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            LoadTransports(GetTransports());
            trackBar1.Minimum = 0;
            trackBar1.Maximum = Convert.ToInt32(GetTransports().Max(p => p.Price));
            trackBar2.Minimum = 0;
            trackBar2.Maximum = trackBar1.Maximum;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Buy f = new Buy();
            f.ShowDialog();
        }
    }
}
