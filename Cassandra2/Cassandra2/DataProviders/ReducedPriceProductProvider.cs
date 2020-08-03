using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    public class ReducedPriceProductProvider
    {
        public static ReducedPriceProduct GetProduct(string date_to, string date_from, string m_id, string id)
        {
            ISession session = SessionManager.GetSession();
            ReducedPriceProduct product = new ReducedPriceProduct();

            if (session == null)
                return null;

            Row productData = session.Execute("select * from \"Product_reduced_price\" where \"id\"= '" + id + "' " +
                "and \"market_id\"= '" + m_id + "'").FirstOrDefault();

            if (productData != null)
            {
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.OldCost = productData["old_cost"] != null ? productData["old_cost"].ToString() : string.Empty;
                product.NewCost = productData["new_cost"] != null ? productData["new_cost"].ToString() : string.Empty;
                product.DateTo = productData["date_to"] != null ? productData["date_to"].ToString() : string.Empty;
                product.DateFrom = productData["date_from"] != null ? productData["date_from"].ToString() : string.Empty;
                return product;
            }

            return null;
        }
    
        public static List<ReducedPriceProduct> GetProductsOfOneMarket(string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<ReducedPriceProduct> products = new List<ReducedPriceProduct>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product_reduced_price\" where \"market_id\" = '" + m_id + "'");

            foreach (var productData in productsData)
            {
                ReducedPriceProduct product = new ReducedPriceProduct();
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.OldCost = productData["old_cost"] != null ? productData["old_cost"].ToString() : string.Empty;
                product.NewCost = productData["new_cost"] != null ? productData["new_cost"].ToString() : string.Empty;
                product.DateTo = productData["date_to"] != null ? productData["date_to"].ToString() : string.Empty;
                product.DateFrom = productData["date_from"] != null ? productData["date_from"].ToString() : string.Empty;
                products.Add(product);
            }

            return products;
        }

        public static void SetOldCostToProducts(string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<ReducedPriceProduct> products = new List<ReducedPriceProduct>();

            if (session == null)
                return;

            products = GetProductsOfOneMarket(m_id);
            foreach (ReducedPriceProduct product in products)
            {
                if(DateTime.Compare(Convert.ToDateTime(product.DateTo), DateTime.Now) <= 0)
                    DataProviders.ProductProvider.UpdateCostOfProduct(product.ID,m_id, product.OldCost);
                
            }
        }

        public static List<ReducedPriceProduct> GetProductsOnSoldNow( string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<ReducedPriceProduct> products = new List<ReducedPriceProduct>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product_reduced_price\" where \"market_id\" = '" + m_id + "'");

            foreach (var productData in productsData)
            {
                ReducedPriceProduct product = new ReducedPriceProduct();
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketID = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.OldCost = productData["old_cost"] != null ? productData["old_cost"].ToString() : string.Empty;
                product.NewCost = productData["new_cost"] != null ? productData["new_cost"].ToString() : string.Empty;
                product.DateTo = productData["date_to"] != null ? productData["date_to"].ToString() : string.Empty;
                product.DateFrom = productData["date_from"] != null ? productData["date_from"].ToString() : string.Empty;
                if (DateTime.Compare(Convert.ToDateTime(product.DateTo), DateTime.Now) >= 0 && DateTime.Compare(Convert.ToDateTime(product.DateFrom), DateTime.Now) <= 0)
                    products.Add(product);
            }

            return products;
        }
        public static void AddProduct(string ID, string market_id, string oldCost, string newCost, string date_to, string date_from)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet productData = session.Execute("insert into \"Product_reduced_price\" (\"id\", market_id, new_cost, old_cost, date_to, date_from)  " +
                "values ('" + ID + "', '" + market_id + "', '" + newCost + "', '" + oldCost + "', '" + date_to + "', '" + date_from + "')");
        }

        public static void DeleteProduct(string date_from, string date_to, string m_id, string id)
        {
            ISession session = SessionManager.GetSession();
            Product Product = new Product();

            if (session == null)
                return;

            RowSet ProductData = session.Execute("delete from \"Product_expiration_date\" where \"date_to\" = '" + date_to + "' and \"date_from\" = '" + date_from + "' and \"market_id\" = '" + m_id+ "' and \"id\" = '" + id + "'");
        }
    }
}
