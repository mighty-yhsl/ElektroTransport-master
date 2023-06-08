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
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }


        List<string> GetCategories()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Category.Select(c => c.Name).ToList();
            }
        }

        void LoadCategories()
        {
            comboBox1.Items.Clear();
            if (GetCategories().Any())
                comboBox1.Items.AddRange(GetCategories().ToArray());
        }

        List<string> GetManufacturers()
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

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!=""&&comboBox1.SelectedIndex!=-1&&
                comboBox2.SelectedIndex!=-1&&
                numericUpDown1.Value!=0&&
                numericUpDown2.Value != 0 &&
                numericUpDown3.Value != 0 &&
                numericUpDown5.Value != 0)
            {
                using(DBEntities db=new DBEntities())
                {
                    var category = db.Category.First(c => c.Name == comboBox1.SelectedItem.ToString()).Id;
                    var manufacturer = db.Manufacturer.First(m => m.Name == comboBox2.SelectedItem.ToString()).Id;
                    db.Transport.Add(new Transport
                    {
                        Name = textBox1.Text,
                        CategoryId = category,
                        ManufacturerId = manufacturer,
                        Speed = numericUpDown1.Value,
                        WheelDiameter = numericUpDown2.Value,
                        EnginePower = numericUpDown3.Value,
                        Weight = numericUpDown4.Value,
                        Price = numericUpDown5.Value
                    });
                    db.SaveChanges();
                    var transport = db.Transport.ToList().Last().Id;
                    db.TransportAmount.Add(
                        new TransportAmount
                        {
                            TransportId = transport,
                            Count = Convert.ToInt32(numericUpDown6.Value)
                        });
                    db.SaveChanges();
                    MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Заповніть усі поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadCategories();
            LoadManufacturers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddC f = new AddC();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddM f = new AddM();
            f.ShowDialog();
        }
    }
}
