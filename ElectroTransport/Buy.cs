using FastReport;
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
    public partial class Buy : Form
    {
        public class ReportFormView
        {
            public string Id { set; get; }
            public string Name { set; get; }
            public int Count { set; get; }
            public decimal Price { set; get; }
            public decimal TotalPrice { set; get; }
            public ReportFormView(int id,string name,int count,decimal price)
            {
                Id = id.ToString();
                Name = name;
                Count = count;
                Price = price;
                TotalPrice = price*Convert.ToDecimal(count);
            }
        }
        int r = -2;
        public Buy()
        {
            transports = Form2.GetTransports();
            InitializeComponent();
        }

        List<dynamic> transports;

        void LoadTransports(List<dynamic> transports)
        {
            foreach(var t in transports)
            {
                dataGridView1.Rows.Add(t.Id, t.Name, t.Price, t.Count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool b = true;
            if (r >= 0 && r < dataGridView1.Rows.Count - 1)
            {
                for(int i=0;i<dataGridView2.Rows.Count-1;i++)
                {
                    if(Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value)==
                        Convert.ToInt32(dataGridView1.Rows[r].Cells[0].Value))
                    {
                        if (Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value)
                        + Convert.ToInt32(numericUpDown1.Value) > Convert.ToInt32(dataGridView1.Rows[r].Cells[3].Value))
                        {
                            MessageBox.Show("Такого товару не має у такій кількості!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value) +
                                Convert.ToInt32(numericUpDown1.Value);
                            dataGridView2.Rows[i].Cells[4].Value =Convert.ToDecimal(dataGridView2.Rows[i].Cells[4].Value) + transports[r].Price * Convert.ToDecimal(numericUpDown1.Value);
                        }
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    if (Convert.ToInt32(numericUpDown1.Value) >
                        Convert.ToInt32(dataGridView1.Rows[r].Cells[3].Value))
                    {
                        MessageBox.Show("Такого товару не має у такій кількості!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dataGridView2.Rows.Add(transports[r].Id, transports[r].Name, transports[r].Price, Convert.ToInt32(numericUpDown1.Value),
                            transports[r].Price * Convert.ToDecimal(numericUpDown1.Value));
                    }
                }
                r = -2;
                dataGridView1.ClearSelection();
            }
            else
            {
                MessageBox.Show("Для додавання товарів оберіть не пусту комірку!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Buy_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadTransports(transports);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView2.Rows.Count>1)
            {
                List<ReportFormView> data = new List<ReportFormView>();
                using (DBEntities db = new DBEntities())
                {
                    db.Order.Add(new Order {
                        OrderDate=DateTime.Now
                    });
                    db.SaveChanges();
                    int t;
                    int id;
                    int count;
                    var order = db.Order.ToList().Last().Id;
                    for(int i=0;i<dataGridView2.Rows.Count-1;i++)
                    {
                        count = Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                        id = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
                        t = db.Transport.First(tr => tr.Id == id).Id;
                        db.ListOrder.Add(new ListOrder
                        {
                            OrderId=order,
                            TransportId=t,
                            Count=count
                        });
                        db.SaveChanges();
                        TransportAmount transportAmount = db.TransportAmount.First(tr => tr.TransportId == t);
                        transportAmount.Count -= count;
                        db.Entry(transportAmount).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        data.Add(new ReportFormView(id, dataGridView2.Rows[i].Cells[1].Value.ToString(), count, Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value)));
                    }
                    Report f = Report.FromFile("Report.frx");
                    f.RegisterData(data, "view");
                    f.SetParameterValue("order", order);
                    f.Show();
                    Close();
                }
            }
        }
    }
}
