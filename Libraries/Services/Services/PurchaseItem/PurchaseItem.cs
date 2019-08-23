using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PurchaseItem
{
    public class PurchaseItem
    {
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

    }

    public class PurchaseReturnViewModel
    {
        public DateTime returnDate { get; set; }
        public string ReturnBy { get; set; }

        public PurchaseReturnHeader obj { get; set; }

        public PurchaseReturnViewModel()
        {
            obj = new PurchaseReturnHeader();
        }

        public List<PurchaseReturnItem> PurchaseReturnItem { get; set; }
    }


    public class PurchaseReturnHeader
    {
        public string Supplier { get; set; }
        public string SupplierID { get; set; }

        public DateTime PurchaseDate { get; set; }


    }

    public class PurchaseReturnItem
    {
    public int PurchaseID { get; set; }

    public string Category { get; set; }
    public string Brand { get; set; }

    public int itemID { get; set; }
    public string ItemName { get; set; }
    public string Unit { get; set; }
    public double PurchaseQty { get; set; }
    public double Rate { get; set; }
    public double ReturnQty { get; set; }
}

}
