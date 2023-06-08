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
    public partial class Menu : Form
    {
        string role;
        public Menu(string role)
        {
            this.role = role;
            InitializeComponent();
            if (role != "a")
            {
                button3.Visible = false;
                button4.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserForm f = new UserForm();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(role);
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Buy f = new Buy();
            f.ShowDialog();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TransportAdmissionForm f = new TransportAdmissionForm();
            f.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
