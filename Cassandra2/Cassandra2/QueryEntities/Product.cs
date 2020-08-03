using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.QueryEntities
{
   public class Product
    {
        public string ID { get; set; }
        public string MarketID { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string ExpirationDate { get; set; }
        public string Cost { get; set; }
    }
}
