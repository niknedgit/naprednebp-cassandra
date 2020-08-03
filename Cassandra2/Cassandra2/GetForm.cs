using Cassandra2.QueryEntities;
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
    public partial class GetForm : Form
    {
        public GetForm()
        {
            InitializeComponent();
            
        }
        public string MarketID { get; set; }
        
        public GetForm(Type type, string ID, string marketID)
        {
            InitializeComponent();
            MarketID = marketID;
            ShowTable(type, ID, marketID);
        }
        public GetForm(List<Product> products, string marketId)
        {
            InitializeComponent();
            dataGridView1.DataSource = new BindingList<Product>(products);
        }
        public void ShowTable(Type type, string ID, string marketID)
        {
            switch (type) {
                case Type.Employee:
                    {
                        List<Employee> list = new List<Employee>();
                        BindingList<Employee> bindingList;
                        if (ID != null)
                        {
                            list.Add(DataProviders.EmployeeProvider.GetEmployee(marketID, ID));
                        }
                        else
                            list = DataProviders.EmployeeProvider.GetEmployeesOfOneMarket(marketID);          
                        bindingList = new BindingList<Employee>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                case Type.ProductExpDate:
                    {
                        List<Product> list = new List<Product>();
                        BindingList<Product> bindingList;
                        if (ID != null)
                            list = DataProviders.ProductExpirationDateProvider.GetProductsOfOneMarket(ID, marketID);
                        else
                            list = DataProviders.ProductExpirationDateProvider.ExpiredDate(marketID);
                        bindingList = new BindingList<Product>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                case Type.Market:
                    {
                        List<Market> list = new List<Market>();
                        BindingList<Market> bindingList; 
                        if (ID != null)
                            list.Add(DataProviders.MarketProvider.GetMarket(ID));
                        else
                            list = DataProviders.MarketProvider.GetMarkets();
                        bindingList = new BindingList<Market>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                case Type.ProductSold:
                    {
                        List<ProductSoldOnDate> list = new List<ProductSoldOnDate>();
                        BindingList<ProductSoldOnDate> bindingList;
                        list = DataProviders.ProductSoldProvider.GetProducts(ID, marketID);
                        bindingList = new BindingList<ProductSoldOnDate>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                case Type.Product:
                    {
                        DataProviders.ReducedPriceProductProvider.SetOldCostToProducts(marketID);
                        List<Product> list = new List<Product>();
                        BindingList<Product> bindingList;                       
                        if (ID != null)
                            list.Add(DataProviders.ProductProvider.GetProduct(ID, marketID));
                        else
                            list = DataProviders.ProductProvider.GetProductsOfOneMarket(marketID);
                        bindingList = new BindingList<Product>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                case Type.ReducedPriceProduct:
                    {
                        List<ReducedPriceProduct> list = new List<ReducedPriceProduct>();
                        BindingList<ReducedPriceProduct> bindingList;
                        list = DataProviders.ReducedPriceProductProvider.GetProductsOnSoldNow(marketID);
                        bindingList = new BindingList<ReducedPriceProduct>(list);
                        dataGridView1.DataSource = bindingList;
                    }
                    break;
                default:
                    break;
            }
            
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 forma = new Form1(MarketID);
            forma.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
