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
    public partial class AddM : Form
    {
        public AddM()
        {
            InitializeComponent();
        }

        private void AddM_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                using (DBEntities db = new DBEntities())
                {
                    if (!db.Manufacturer.Any(c => c.Name == textBox1.Text))
                    {
                        db.Manufacturer.Add(new Manufacturer
                        {
                            Name = textBox1.Text
                        });
                        db.SaveChanges();
                        MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Такий виробник вже існує", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заповніть текстове поле", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
