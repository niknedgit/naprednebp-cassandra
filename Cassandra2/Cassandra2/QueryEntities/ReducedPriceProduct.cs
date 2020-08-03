using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.QueryEntities
{
    public class ReducedPriceProduct
    {
        public string MarketID { get; set; }
        public string ID { get; set; }
        public string NewCost { get; set; }
        public string OldCost { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }
}
