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
    public partial class AddForm : Form
    {
        Type type;
        public string ID { get; set; }
        public AddForm()
        {
            InitializeComponent();
        }
        public AddForm(Type type, string id)
        {
            this.ID = id;
            InitializeComponent();
            this.type = type;
            switch (type)
            {
                case Type.Market:
                    {
                        label5.Visible = false;  textBox5.Visible = false;
                        lblDate.Visible = false; dateTimePicker1.Visible = false;
                        label1.Text = "ID:";
                        label4.Text = "Ime:";
                        label2.Text = "Adresa:";
                        label3.Text = "Grad:";
                    }
                    break;
                case Type.Employee:
                    {
                        label5.Visible = false; textBox5.Visible = false;
                        lblDate.Visible = false; dateTimePicker1.Visible = false;
                        label1.Text = "JMBG:";
                        label2.Text = "Ime:";
                        label3.Text = "Plata:";
                        label4.Text = "Radno mesto:";
                    }
                    break;
                case Type.Product:
                    {
                        label5.Visible = false; textBox5.Visible = false;
                        label1.Text = "Barkod:";
                        label2.Text = "Ime:";
                        label3.Text = "Vrsta proizvoda:";
                        label4.Text = "Cena:";
                        lblDate.Text = "Rok trajanja";
                    }
                    break;
                case Type.ProductSold:
                    {
                        label3.Visible = false; textBox3.Visible = false;
                        label4.Visible = false; textBox4.Visible = false;
                        label5.Visible = false; textBox5.Visible = false;
                        label1.Text = "Barkod:";
                        label2.Text = "Ime:";
                        lblDate.Text = "Datum prodaje";
                    }
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool pok = true;
            switch (type)
            {
                case Type.Market:
                    {
                        ID = textBox1.Text;
                        if (!DataProviders.MarketProvider.AddMarket(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text))
                        {
                            MessageBox.Show("Vec postoji market sa datom vrednoscu za kljuc"); 
                            pok = false; 
                        }
                    }
                    break;
                case Type.Employee:
                    {
                        int salary;
                        if (Int32.TryParse(textBox3.Text.ToString(), out salary))
                        {
                            if (!DataProviders.EmployeeProvider.AddEmployee(textBox1.Text, textBox2.Text, ID, salary.ToString(), textBox4.Text))
                            {
                                MessageBox.Show("Vec postoji radnik sa datom vrednoscu za kljuc");
                                pok = false;
                            }
                        }
                        else MessageBox.Show("Uneta vrednost za platu nije broj");

                    }
                    break;
                case Type.Product:
                    {
                        int cost;
                        if (Int32.TryParse(textBox4.Text.ToString(), out cost))
                        {
                            if (DataProviders.ProductProvider.AddProduct(textBox1.Text,ID, textBox2.Text, textBox3.Text, cost.ToString(), dateTimePicker1.Value.Date.ToString()))
                        {
                            DataProviders.ProductExpirationDateProvider.AddProduct(textBox1.Text, ID, textBox2.Text, textBox3.Text, textBox4.Text, dateTimePicker1.Value.Date.ToString());
                        }
                       else
                        {
                            MessageBox.Show("Vec postoji proizvod sa datom vrednoscu za kljuc"); 
                            pok = false;
                            }
                        }
                        else MessageBox.Show("Uneta vrednost za cenu nije broj");
                    }
                    break;
                case Type.ProductSold:
                    {
                        if (!DataProviders.ProductSoldProvider.AddProduct(textBox1.Text, ID, textBox2.Text, dateTimePicker1.Value.Date.ToString()))
                        { 
                            MessageBox.Show("Ne postoji takav proizvod u bazi, samim tim ne moze biti prodat");
                            pok = false;
                        }
                    }
                    break;
                default:                    
                    break;
            }
            if (pok)
            {
                this.Hide();
                Form1 forma = new Form1(ID);
                forma.ShowDialog();
            }
            else
            {
                textBox1.Text = "";
            }
   
        }
    }
}
