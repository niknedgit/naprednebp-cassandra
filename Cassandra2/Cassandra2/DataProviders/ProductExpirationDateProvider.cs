using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    public class ProductExpirationDateProvider
    {
        public static List<Product> GetProducts(string e_date)
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product_expiration_date\" where \"expiration_date\" = '" + e_date + "' ");

            foreach (var productData in productsData)
            {
                Product product = new Product();
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.Name = productData["name"] != null ? productData["name"].ToString() : string.Empty;
                product.Kind = productData["kind"] != null ? productData["kind"].ToString() : string.Empty;
                product.Cost = productData["cost"] != null ? productData["cost"].ToString() : string.Empty;
                product.ExpirationDate = productData["expiration_date"] != null ? productData["expiration_date"].ToString() : string.Empty;
                products.Add(product);
            }

            return products;
        }
        public static List<Product> GetProductsOfOneMarket(string e_date, string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product_expiration_date\" where \"expiration_date\" = '" + e_date + "' and \"market_id\" = '" + m_id + "'");

            foreach (var productData in productsData)
            {
                Product product = new Product();
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.Name = productData["name"] != null ? productData["name"].ToString() : string.Empty;
                product.Kind = productData["kind"] != null ? productData["kind"].ToString() : string.Empty;
                product.Cost = productData["cost"] != null ? productData["cost"].ToString() : string.Empty;
                product.ExpirationDate = productData["expiration_date"] != null ? productData["expiration_date"].ToString() : string.Empty;
                products.Add(product);
            }

            return products;
        }

        public static void AddProduct(string ID, string market_id, string name, string kind, string cost, string expirationDate)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet productData = session.Execute("insert into \"Product_expiration_date\" (\"id\", market_id, name, kind, cost, expiration_date)  " +
                "values ('" + ID + "', '" + market_id + "', '" + name + "', '" + kind + "', '" + cost + "', '" + expirationDate + "')");
        }


        public static List<Product> ExpiredDate(string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData= DataProviders.ProductProvider.GetProductsOfOneMarket(m_id);

            foreach (Product p in productsData)
            {
                if (DateTime.Parse(p.ExpirationDate) < DateTime.Now)
                {
                    products.Add(p);
                    DataProviders.ProductProvider.DeleteProduct(p.ID,p.MarketID);
                }
            }
            return products;
        }

        public static List<Product> ExpirationDate(string m_id, DateTime date)
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData = DataProviders.ProductProvider.GetProductsOfOneMarket(m_id);

            foreach (Product p in productsData)
                if (DateTime.Parse(p.ExpirationDate) < date)
                    products.Add(p);

            return products;
        }
        public static void DeleteProduct(string e_date, string m_id, string id)
        {
            ISession session = SessionManager.GetSession();
            Product Product = new Product();

            if (session == null)
                return;

            RowSet ProductData = session.Execute("delete from \"Product_expiration_date\" where \"expiration_date\" = '" + e_date + "' and \"market_id\" = '" + m_id + "' and \"id\" = '" + id + "'");
        }
    }
}
