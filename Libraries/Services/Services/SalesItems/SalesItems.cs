using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SalesItems
{
    public class SalesItems
    {
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public int IssueTo { get; set; }

        public DateTime IssueDate { get; set; }

        public string Remarks { get; set; }

        public string ChiefName { get; set; }


    }

   
}
