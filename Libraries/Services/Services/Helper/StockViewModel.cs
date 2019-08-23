using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class StockViewModel
    {
    }

    public class AsofStock
    {

        public string ItemName { get; set; }
        public string Cateogry { get; set; }
        public string Unit { get; set; }
        public decimal CurrentStock { get; set; }

        public decimal CostofGood { get; set; }


    }

    public class StockLedger
    {

        public int Document_ID { get; set; }
        public string CustomerContractNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public string document_type { get; set; }
        public string document_Description { get; set; }
        
        public string ItemCode { get; set; }
        public string idesc { get; set; }
        public decimal issued { get; set; }
        public decimal received { get; set; }
        public decimal Rate { get; set; }
        public decimal Openbal { get; set; }

        public decimal balance { get; set; }
        public decimal Actualbalance { get; set; }
        public decimal CostofGood { get; set; }

        public decimal Aup { get; set; }
        public decimal LineStockValue { get; set; }

    }

    public class DailyIssuance
    {
        public int id { get; set; }

        public DateTime IssueDate { get; set; }

        public string Categrory { get; set; }
        public string ItemName { get; set; }

        public string Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal TotalValue { get; set; }

        public string IssueTo { get; set; }

        public string Remarks { get; set; }

    }

    public class DailyPurchase
    {
        public int id { get; set; }

        public string Remarks { get; set; }
        public string InvoiceNo { get; set; }

        public string Supplier { get; set; }
        public DateTime Date { get; set; }

        public string ItemName { get; set; }

        public string Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal TotalValue { get; set; }

        public string ReceivedBy { get; set; }

    }


    public class PurchaseReturn
    {
        public int id { get; set; }

        public DateTime RDate { get; set; }

        public string Returnby { get; set; }

        public string Remarks { get; set; }

        public string ItemName { get; set; }

        public string Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal TotalValue { get; set; }

        public string Supplier { get; set; }

    }



    public class DayEndReport
        {
            public int id { get; set; }

            public string itemCode { get; set; }

            public string Description { get; set; }

            public string cDes { get; set; }

            public int CATID { get; set; }



        public string Unit { get; set; }

            public decimal Op_bal { get; set; }

            public decimal Op_balValue { get; set; }

            public decimal Total_Receive { get; set; }
                        
            public decimal Total_Issue { get; set; }

            public decimal TotalPrReturn { get; set; }
            public decimal TotalIssueReturn { get; set; }
            public decimal Total_Adjusted { get; set; }
            
                    

            public decimal ActualAmt { get; set; }

            public decimal Tot_bal1 { get; set; }


    }

    public class TotalValueCategoryWise
    {
        public string  Category { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class TransactionPanel
    {
        public string IssueTo { get; set; }
        public Decimal TotalValue { get; set; }

    }
       



    }
