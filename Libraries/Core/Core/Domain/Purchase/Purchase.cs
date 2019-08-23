using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Purchase
{
    public class Purchase
    {
        public int id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }

        public decimal SupplierID { get; set; }

        public decimal PONo { get; set; }
        public decimal KitchenID { get; set; }

        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public List<PurchaseDetail> PurchaseDetail { get; set; }
    }

    public class PurchaseDetail
    {
        public int ID { get; set; }
        public int PurchaseID { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

    }
}
