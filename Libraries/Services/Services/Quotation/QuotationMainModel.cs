using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Quotation
{
    public class QuotationMainModel
    {

    }
        public class QuotationItemDetail
        {
            public int itemID { get; set; }

            public int ID { get; set; }
            public string ItemName { get; set; }
            public string Brand { get; set; }
            public string Unit { get; set; }

            public string Specs { get; set; }
            public double RequiredQty { get; set; }

            public double QuoatedRate { get; set; }

        }

       public class GeneratedQuatations
        {
        public int RecordID { get; set; }
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public DateTime QuoteDate { get; set; }
        public double Total { get; set; }
    }


    public class SupplierQuotedViewModel
    {

        public List<QuaotedSupplier> lSupplier { get; set; }
        public List<QuaotedSupplierRates> lSupplierQuotedRate { get; set; }
        public List<QuaotedTotal> lSupplierTotal { get; set; }
    }

    public class QuaotedSupplier
    {
        public string Name { get; set; }
    }
    public class QuaotedSupplierRates

  

    {
        public int itemid { get; set; }

        public string Category { get; set; }
        public string Brand { get; set; }

        public string Name { get; set; }
        public string Unit { get; set; }
        public string Specs { get; set; }

        public decimal quoatedRate { get; set; }
        public decimal Qty { get; set; }

        public decimal Total { get; set; }

        public int SupplierID { get; set; }



    }
    public class QuaotedTotal
    {
        public int SupplierID { get; set; }
        public decimal Total { get; set; }
    }


}


//Select a.SupplierID, tblSupplier.Name, a.QuoteDate , sum(TD_Quotation.QuoatedRate) as Total
  //   from Th_Quotation a

   //  inner join TD_Quotation on a.id= TD_Quotation.QuotationID

   //  inner join tblSupplier on tblSupplier.id= a.SupplierID

    // GROUP BY a.SupplierID, tblSupplier.Name, a.QuoteDate