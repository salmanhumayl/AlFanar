using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.PurchaseReturn
{

    public class PurchaseReturn
    {
        public int id { get; set; }
        public string PurchaseId { get; set; }

        public int SupplierID { get; set; }
        public DateTime RDate { get; set; }
        public string ReturnBy { get; set; }
        public decimal KitchenID { get; set; }
        public string Remarks { get; set; }
        public List<PurchaseReturnDetail> PurchaseReturnDetail { get; set; }
    }


    public class PurchaseReturnDetail
    {
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
