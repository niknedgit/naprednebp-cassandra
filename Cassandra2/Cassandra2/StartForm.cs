using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cassandra2
{
    public enum Type
    {
        Market,
        Employee,
        Product,
        ProductSold,
        ProductExpDate,
        ReducedPriceProduct
    }
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            this.groupBox1.Visible = false;
            this.btnOk.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = true;
            this.groupBox1.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = true;
            this.groupBox1.Visible = false;     
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                if (textBox1.Text != "")
                {
                    if (DataProviders.MarketProvider.GetMarket(this.textBox1.Text) != null)
                    {
                        this.Hide();
                        Form1 forma = new Form1(this.textBox1.Text);
                        forma.ShowDialog();
                    }
                    else
                    {
                        textBox1.Text = "";
                        MessageBox.Show("Ne postoji trazeni market");
                    }
                }
                else MessageBox.Show("Nije popunjeno polje ID");
               
            }
            else if (radioButton2.Checked)
            {
                this.Hide();
                AddForm addForm = new AddForm(Type.Market, null);
                addForm.ShowDialog();
            }
            else
            {
                if (textBox1.Text != "")
                {                 
                if(DataProviders.MarketProvider.DeleteMarket(textBox1.Text))
                        MessageBox.Show("Izbrisan market");
                else
                        MessageBox.Show("Ne postoji trazeni market u bazi");
                    textBox1.Text = "";
                }
                else MessageBox.Show("Nije popunjeno polje ID");
            }
            
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = true;
            this.groupBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
