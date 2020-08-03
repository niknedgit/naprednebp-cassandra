using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    class ProductSoldProvider
    {
        public static List<ProductSoldOnDate> GetProducts(string s_date,string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<ProductSoldOnDate> products = new List<ProductSoldOnDate>();

            if (session == null)
                return null;

            var productsData = session.Execute("select * from \"Product_sold_on_date\" where \"date_s\" = '" + s_date + "' and \"market_id\" = '" + m_id + "'");

            foreach (var productData in productsData)
            {
                ProductSoldOnDate product = new ProductSoldOnDate();
                product.ID = productData["id"] != null ? productData["id"].ToString() : string.Empty;
                product.MarketId = productData["market_id"] != null ? productData["market_id"].ToString() : string.Empty;
                product.Name = productData["name"] != null ? productData["name"].ToString() : string.Empty;
                product.DateS = productData["date_s"] != null ? productData["date_s"].ToString() : string.Empty;
                products.Add(product);
            }

            return products;
        }

        public static bool AddProduct(string ID, string market_id, string name, string date_s)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return false;

            if (ProductProvider.GetProduct(ID, market_id) != null)
            {
                RowSet productData = session.Execute("insert into \"Product_sold_on_date\" (\"id\", market_id, name, date_s)  " +
                  "values ('" + ID + "', '" + market_id + "', '" + name + "', '" + date_s + "')");
                return true;
            }
            return false;
        }

        public static void DeleteProduct(string s_date,string market_id, string id)
        {
            ISession session = SessionManager.GetSession();
            ProductSoldOnDate Product = new ProductSoldOnDate();

            if (session == null)
                return;

            RowSet ProductData = session.Execute("delete from \"Product_sold_on_date\" where \"date_s\" = '" + s_date + "' and \"market_id\" = '" + market_id + "' and \"id\" = '" + id + "'");
        }
    }
}
