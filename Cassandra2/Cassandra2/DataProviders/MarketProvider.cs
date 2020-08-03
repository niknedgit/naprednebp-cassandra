using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    class MarketProvider
    {
        public static Market GetMarket(string ID)
        {
            ISession session = SessionManager.GetSession();
            Market market = new Market();

            if (session == null)
                return null;

            Row marketData = session.Execute("select * from \"Market\" where \"id\"= '" + ID + "'").FirstOrDefault();

            if (marketData != null)
            {
                market.ID = marketData["id"] != null ? marketData["id"].ToString() : string.Empty;
                market.Name = marketData["name"] != null ? marketData["name"].ToString() : string.Empty;
                market.Address = marketData["address"] != null ? marketData["address"].ToString() : string.Empty;
                market.City = marketData["city"] != null ? marketData["city"].ToString() : string.Empty;
                return market;
            }

            return null;
            
        }

        public static List<Market> GetMarkets()
        {
            ISession session = SessionManager.GetSession();
            List<Market> markets = new List<Market>();

            if (session == null)
                return null;

            var marketsData = session.Execute("select * from \"Market\"");

            foreach (var marketData in marketsData)
            {
                Market market = new Market();
                market.ID = marketData["id"] != null ? marketData["id"].ToString() : string.Empty;
                market.Name = marketData["name"] != null ? marketData["name"].ToString() : string.Empty;
                market.Address = marketData["address"] != null ? marketData["address"].ToString() : string.Empty;
                market.City = marketData["city"] != null ? marketData["city"].ToString() : string.Empty;
                markets.Add(market);
            }

            return markets;
        }

        public static bool AddMarket(string ID, string address, string city, string name)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return false;

            if (GetMarket(ID) == null)
            {
                RowSet marketData = session.Execute("insert into \"Market\" (\"id\", address, city, name)  " +
                "values ('" + ID + "', '" + address + "', '" + city + "', '" + name + "')");
                return true;
            }
            return false;
        }

        public static bool DeleteMarket(string id)
        {
            ISession session = SessionManager.GetSession();
            Market market = new Market();

            if (session == null)
                return false;
            if (GetMarket(id) != null)
            {
                RowSet marketData = session.Execute("delete from \"Market\" where \"id\" = '" + id + "'");
                return true;
            }
            return false;
        }
    }
}
