using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectroTransport
{
    public partial class UserForm : Form
    {
        Regex regex = new Regex(@"^[a-zA-Z0-9_]{5,}$");
        public UserForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (regex.IsMatch(textBox1.Text) && regex.IsMatch(textBox2.Text))
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    using (DBEntities db= new DBEntities())
                    {
                        if (!db.User.Any(u => u.Login == textBox1.Text))
                        {
                            db.User.Add(new User
                            {
                                Login = textBox1.Text,
                                Password = textBox2.Text,
                                Role = comboBox1.SelectedItem.ToString()
                            });
                            db.SaveChanges();
                            MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Користувач з таким логіном вже зареєстрований у системі", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Оберіть роль користувача!", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Вимоги до пароля та логіну:\n*Повинен бути більше 5 символів;\n*Допустимі символи:Латиниця, цифри та знак '_' ;", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = true;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox7.Visible = true;
            pictureBox8.Visible = false;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
