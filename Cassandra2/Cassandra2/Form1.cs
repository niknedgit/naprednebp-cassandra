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
    public partial class Form1 : Form
    {
        public string ID { get; set; } //marketID
        private Button button { get; set; }
        public Form1()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            PopustGroupBox.Visible = false;
        }
        public Form1( string ID)
        {
            InitializeComponent();
            this.ID = ID;
            PopustGroupBox.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dodaj Zaposlenog
            this.Hide();
            AddForm addForm = new AddForm(Type.Employee, ID);
            addForm.ShowDialog();
        }

        private void btnEmployeeDelete_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            PopustGroupBox.Visible = false;
            button = (Button)sender;
        }

        private void btnEmployeeGetAll_Click(object sender, EventArgs e)
        {
            this.Hide();
            GetForm forma = new GetForm(Type.Employee, null, ID);
            forma.ShowDialog();
        }

        private void btnEmployeeGet_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            PopustGroupBox.Visible = false;
            button = (Button)sender;        
        }

        private void btnProductGetAll_Click(object sender, EventArgs e)
        {
            this.Hide();
            GetForm forma = new GetForm(Type.Product,null, ID);
            forma.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            PopustGroupBox.Visible = false;
            button = (Button)sender;
        }

        private void btnProductGet_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            groupBox2.Visible = false;
            groupBox1.Visible = true;
            PopustGroupBox.Visible = false;
            button = (Button)sender;
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddForm addForm = new AddForm(Type.Product, ID);
            addForm.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {      
            if (textBox1.Text != "")
            {
                switch (button.Name)
                {
                    case "btnEmployeeDelete":
                        {
                            if(DataProviders.EmployeeProvider.DeleteEmployee(textBox1.Text, ID))
                                MessageBox.Show("Izbrisan");
                            else
                                MessageBox.Show("Nemoguće brisanje!");
                     
                        } break;
                    case "btnEmployeeGet":
                        {
                            this.Hide();
                            GetForm forma = new GetForm(Type.Employee, textBox1.Text, ID);
                              forma.ShowDialog();
                        }
                        break;
                    case "btnProductDelete":
                        {
                            var product = DataProviders.ProductProvider.GetProduct(textBox1.Text, ID);
                            if (product!=null)
                            {
                                DataProviders.ProductProvider.DeleteProduct(textBox1.Text, ID);
                                DataProviders.ProductExpirationDateProvider.DeleteProduct(product.ExpirationDate, ID, textBox1.Text);
                                // DataProviders.ProductSoldProvider.DeleteProduct(product.ExpirationDate,ID, textBox1.Text);
                                MessageBox.Show("Izbrisan");
                            }
                            else
                                MessageBox.Show("Nemoguće brisanje!");                               
                        }
                        break;
                    case "btnProductGet":
                        {
                            this.Hide();
                            GetForm forma = new GetForm(Type.Product, textBox1.Text, ID);
                                forma.ShowDialog();          
                        }
                        break;
                    case "ReducePriceBtn":
                        {
                            var products= DataProviders.ProductExpirationDateProvider.ExpirationDate(ID, dateTimePicker1.Value);
                            foreach(var product in products)
                            {
                                DataProviders.ProductProvider.UpdateCostOfProduct(product.ID, product.MarketID, (Int32.Parse(product.Cost) * (1-(float.Parse(textBox1.Text.ToString()) / 100))).ToString());
                            }
                            lblGroupBox2.Text = "Unesite barkod:";
                            button2.Visible = true;
                        }
                        break;
                }
                }
            else MessageBox.Show("Nije popunjeno polje ID");
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            PopustGroupBox.Visible = false;
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            PopustGroupBox.Visible = false;
            button = (Button)sender;
            textBox1.Text= "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //group box2 ok button
            this.Hide();
            switch (button.Name)
            {
                case "button4":
                    {                      
                        GetForm forma = new GetForm(Type.ProductSold, dateTimePicker1.Value.Date.ToString(), ID);
                        forma.ShowDialog();
                    }
                    break;
                case "button1":
                    {
                        GetForm forma = new GetForm(Type.ProductExpDate, dateTimePicker1.Value.Date.ToString(), ID);
                        forma.ShowDialog();
                    }
                    break;
            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            //prodat proizvod
            this.Hide();
            AddForm addForm = new AddForm(Type.ProductSold, ID);
            addForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //proizvodi prodati dana 
            groupBox1.Visible = false;
            PopustGroupBox.Visible = false;
            button = (Button)sender;
            groupBox2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            StartForm addForm = new StartForm();
            addForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //obrisi proizvode kojima je istekao rok trajanja
            this.Hide();
            GetForm forma = new GetForm(Type.ProductExpDate, null, ID);
            forma.ShowDialog();
        }


        private void PopustBtnGB_Click(object sender, EventArgs e)
        {
            int novaCena;
            if (Int32.TryParse(popustNovaCenaTxt.Text.ToString(), out novaCena))
            {
                var product = DataProviders.ProductProvider.GetProduct(PopustIDtxt.Text.ToString(), ID);
                if (product != null)
                {
                    DataProviders.ReducedPriceProductProvider.AddProduct(PopustIDtxt.Text.ToString(), ID, product.Cost, popustNovaCenaTxt.Text.ToString(), PopustDoDateTimeP.Value.ToString(), PopustOdDateTimeP.Value.ToString());
                    if (DateTime.Compare(PopustDoDateTimeP.Value, DateTime.Now) >= 0 && DateTime.Compare(PopustOdDateTimeP.Value, DateTime.Now) <= 0)
                        DataProviders.ProductProvider.UpdateCostOfProduct(PopustIDtxt.Text.ToString(), ID, popustNovaCenaTxt.Text.ToString());
                }
                else
                {
                    MessageBox.Show("Ne postoji proizvod sa zadatim ID");
                    PopustIDtxt.Text = "";
                }
            }
            else
                MessageBox.Show("Unesite broj u polju za cenu");

            PopustGroupBox.Visible = false;
        }
        private void popustAddBtn_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            groupBox2.Visible = false;
            groupBox1.Visible = false;
            PopustGroupBox.Visible = true;
            popustNovaCenaTxt.Text = "";  PopustIDtxt.Text = "";
        }

        private void GetProductReducedPriceBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            GetForm forma = new GetForm(Type.ReducedPriceProduct, null, ID);
            forma.ShowDialog();
        }

        private void ReducePriceBtn_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            groupBox2.Visible = true;
            groupBox1.Visible = true;
            PopustGroupBox.Visible = false;
            lblGroupBox2.Text = "Procenat snizenja:";
            button2.Visible = false;
        }
    }
}
