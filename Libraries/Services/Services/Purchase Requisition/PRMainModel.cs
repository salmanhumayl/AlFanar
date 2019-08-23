using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Purchase_Requisition
{
    public class PRMainModel
    {
    

    }


    public class PROutstandingListMainModel
    {
        public int ID { get; set; }
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
        

    }

    public class PRDetail
    {
        public int itemID { get; set; }

        public int PRID { get; set; }
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }

        public string Specs { get; set; }
        public double RequiredQty { get; set; }

       public double QuotedPrice { get; set; }

    }



    public class EditPurchaseReqModelView
    {
        public EditPurchaseReqModelView()
        {
            obj = new EditPurchaseReqHeader();
        }
        public EditPurchaseReqHeader obj { get; set; }
     
        public List<EditPurchaseReqDetail> PurchaseReqItem { get; set; }
        public List<ReqitemCategory> ReqItemCategory { get; set; }

    }
    public class EditPurchaseReqHeader
    {
        public int ID { get; set; }
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
        public string Remarks { get; set; }
    }
    public class EditPurchaseReqDetail
    {

        public int PRID { get; set; }
        public int ID { get; set; }
        public int itemID { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public string Specs { get; set; }
        public decimal Qty { get; set; }

     
    }

    public class ReqitemCategory
    {
        public string  Name { get; set; }
    }
}


