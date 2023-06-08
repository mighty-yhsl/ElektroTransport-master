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
    public partial class AddC : Form
    {
        public AddC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                using(DBEntities db=new DBEntities())
                {
                    if(!db.Category.Any(c=>c.Name==textBox1.Text))
                    {
                        db.Category.Add(new Category
                        {
                            Name = textBox1.Text
                        });
                        db.SaveChanges();
                        MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Така категорія вже існує", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заповніть текстове поле", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddC_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
