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
    public partial class Edit : Form
    {
        Transport t;
        public Edit(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                this.t = db.Transport.First(t=>t.Id==id);
            }
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

        private void Edit_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadCategories();
            LoadManufacturers();
            textBox1.Text = t.Name;
            numericUpDown1.Value = t.Speed;
            numericUpDown2.Value = t.WheelDiameter;
            numericUpDown3.Value = t.EnginePower;
            numericUpDown4.Value = t.Weight;
            numericUpDown5.Value = t.Price;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1 &&
                comboBox2.SelectedIndex != -1 &&
                numericUpDown1.Value != 0 &&
                numericUpDown2.Value != 0 &&
                numericUpDown3.Value != 0 &&
                numericUpDown5.Value != 0)
            {
                using (DBEntities db = new DBEntities())
                {
                    var category = db.Category.First(c => c.Name == comboBox1.SelectedItem.ToString()).Id;
                    var manufacturer = db.Manufacturer.First(m => m.Name == comboBox2.SelectedItem.ToString()).Id;
                    t.Name = textBox1.Text;
                    t.Speed = numericUpDown1.Value;
                    t.WheelDiameter = numericUpDown2.Value;
                    t.EnginePower = numericUpDown3.Value;
                    t.Weight = numericUpDown4.Value;
                    t.Price = numericUpDown5.Value;
                    t.CategoryId = category;
                    t.ManufacturerId = manufacturer;
                    db.Entry(t).State = System.Data.Entity.EntityState.Modified;
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
    }
}
