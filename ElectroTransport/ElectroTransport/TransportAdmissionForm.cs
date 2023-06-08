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
    public partial class TransportAdmissionForm : Form
    {
        public TransportAdmissionForm()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(GetTransports().ToArray());
        }

        List<string> GetTransports()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Transport.Select(s => s.Name).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                using (DBEntities db = new DBEntities())
                {
                    var transport = db.TransportAmount.First(t => t.Transport.Name == comboBox1.SelectedItem.ToString());
                    transport.Count += Convert.ToInt32(numericUpDown1.Value);
                    db.Entry(transport).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else
            {
                {
                    MessageBox.Show("Оберіть товар для добавлення", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TransportAdmissionForm_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
