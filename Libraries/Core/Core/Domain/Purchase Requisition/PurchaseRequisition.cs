using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Purchase_Requisition
{
    public class PurchaseRequisition
    {
        public int ID { get; set; }
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public List<PurchaseRequisitionDetail> PurchaseDetail { get; set; }

    }
    public class PurchaseRequisitionDetail
    {
        public int id { get; set; }

        public int PRID { get; set; }
        public int ItemId { get; set; }

        public string Unit { get; set; }

        public string Specs { get; set; }

        public double Qty { get; set; }
        public double AverageRate { get; set; }
    }
}
