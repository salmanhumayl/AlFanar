using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Quotation
{
    public class Quotation
    {
        public int ID { get; set; }
        public string PRID { get; set; }
        public DateTime QuoteDate { get; set; }
        public string SupplierID { get; set; }
        public DateTime ProcessOn { get; set; }
        public List<QuotationDetail> QuotationDetail { get; set; }

    }
    public class QuotationDetail
    {
        public int ID { get; set; }

        public int QuotationID { get; set; }
        public int ItemId { get; set; }
        public double Qty { get; set; }
        public double QuoatedRate { get; set; }
    }
}
