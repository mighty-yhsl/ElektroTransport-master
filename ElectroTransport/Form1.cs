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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var c = new DBEntities())
            {
                var user = c.User.FirstOrDefault(u => u.Login == textBox1.Text && u.Password == textBox2.Text);
                if (user != null)
                {
                    if (user.Role == "admin")
                    {
                        Menu f = new Menu("a");
                        f.Show();
                        Hide();
                    }
                    else
                    {
                        Menu f = new Menu("u");
                        f.Show();
                        Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Користувач з такими даними не знайдений!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserForm frm_sign = new UserForm();
            frm_sign.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;
        }
    }
}
