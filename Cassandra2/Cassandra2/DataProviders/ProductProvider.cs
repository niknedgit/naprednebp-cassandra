using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    class ProductProvider
    {
        public static Product GetProduct(string ID, string m_id)
        {
            ISession session = SessionManager.GetSession();
            Product product = new Product();

            if (session == null)
                return null;

            Row productData = session.Execute("select * from \"Product\" where \"id\"= '" + ID + "' " +
                "and \"market_id\"= '" + m_id + "'").FirstOrDefault();

            if (productData != null)
            {
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.Name = productData["name"] != null ? productData["name"].ToString() : string.Empty;
                product.Kind = productData["kind"] != null ? productData["kind"].ToString() : string.Empty;
                product.Cost = productData["cost"] != null ? productData["cost"].ToString() : string.Empty;
                product.ExpirationDate = productData["expiration_date"] != null ? productData["expiration_date"].ToString() : string.Empty;
                return product;
            }

            return null;
        }
        public static List<Product> GetProductsOfOneMarket(string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product\"where \"market_id\"= '" + m_id + "'");

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
        public static List<Product> GetProducts()
        {
            ISession session = SessionManager.GetSession();
            List<Product> products = new List<Product>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product\"");

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

        public static bool AddProduct(string ID, string market_id, string name, string kind, string cost, string expirationDate)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return false;
            if (GetProduct(ID, market_id) == null)
            {
                RowSet productData = session.Execute("insert into \"Product\" (\"id\", market_id, name, kind, cost, expiration_date)  " +
                "values ('" + ID + "', '" + market_id + "', '" + name + "', '" + kind + "', '" + cost + "', '" + expirationDate + "')");
                return true;
            }
            return false;
        }

        public static void DeleteProduct(string id, string marketId)
        {
            ISession session = SessionManager.GetSession();
            Product Product = new Product();

            if (session == null)
                return ;
              
            RowSet ProductData = session.Execute("delete from \"Product\" where \"id\" = '" + id + "' " +
                "and \"market_id\" = '" + marketId + "'");
            
            
        }
        public static void UpdateCostOfProduct(string id, string marketId, string cost)
        {
            ISession session = SessionManager.GetSession();
            Product Product = new Product();

            if (session == null)
                return;
         //  ("UPDATE user_profiles SET email=? WHERE key=?");
            RowSet ProductData = session.Execute("UPDATE  \"Product\" SET cost= '" + cost + "' where \"id\" = '" + id + "' " +
                "and \"market_id\" = '" + marketId + "'");

        }
        
    }
}
