using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJC.KMS.Models
{
    public class Stock
    {

        public static double GetAverageUnitRate(double CurrentStock,int itemID, int KitchenID)
        {
            string strSQL = "";
            Double totRec;
            Double totIssued;
            Double OpeningBalance;
            Stock objStock = new Stock();


            strSQL = " Select ISNULL(Sum(Quantity * Rate), 0) as Quantity  from Stock where KitchenID = 1 and Stock_Type = 1 And itemid = " + itemID;
            totRec = Common.ExecuteReader(strSQL);

            OpeningBalance = GetOpeningBalanceRate(itemID, KitchenID);
            totRec = OpeningBalance + totRec;


            strSQL = "Select ISNULL(Sum(Quantity * Rate),0) as Quantity from Stock where Stock_Type = 0 and KitchenID = " + KitchenID + " And itemid = " + itemID;
            totIssued = Common.ExecuteReader(strSQL);

            Double Total = totRec - totIssued;

            return Total / CurrentStock;


        }

        public static double GetCurrentStock(int itemID, int KitchenID)
        {
            string strSQL = "";
            Double totRec;
            Double totIssued;
            Double OpeningBalance;
            Stock objStock = new Stock();


            strSQL = "Select ISNULL(Sum(Quantity),0) as Quantity from Stock where Stock_Type = 1 and KitchenID = " + KitchenID + " And itemid = " + itemID;
            totRec = Common.ExecuteReader(strSQL);

            OpeningBalance = GetOpeningBalance(itemID, KitchenID);
            totRec = OpeningBalance + totRec;

            strSQL = "Select ISNULL(Sum(Quantity),0) as Quantity from Stock where Stock_Type = 0 and KitchenID = " + KitchenID + " And itemid = " + itemID;
            totIssued = Common.ExecuteReader(strSQL);

            if (totRec - totIssued > 0)
            {
                return totRec - totIssued;
            }
            return 0;

        }

        public static double GetOpeningBalance(int itemID, int KitchenID)
        {

            string strSQL = "";
            strSQL = "Select ISNULL(Quantity,0) as Quantity from tblOpeningBalance where KitchenID = " + KitchenID + " And itemid = " + itemID;
            return Common.ExecuteReader(strSQL);


        }


        public static double GetOpeningBalanceRate(int itemID, int KitchenID)
        {

            string strSQL = "";
            strSQL = "Select ISNULL(Quantity * Rate,0) as Quantity from tblOpeningBalance where KitchenID = " + KitchenID + " And itemid = " + itemID;
            return Common.ExecuteReader(strSQL);


        }

        public static void UpdateStockTank(string docType, int stkType, int docID, int TankID,string TblName, string IDFieldName,
                                      Boolean lIssue = false)
        {
            string strSQL = "";

            strSQL = "INSERT into Stock SELECT FillingDate,'" + docType + "'," + TankID + "," + stkType + ",QUANTITY,''," + IDFieldName + " FROM " + TblName + " WHERE " + IDFieldName + " = " + docID;
            int RowEffected = Common.ExecuteQuery(strSQL);
        }

       
        public static void UpdateStockTankComsumption(string docType, int stkType, int docID, int TankID, string ContractNo,string TblName, string IDFieldName,
                                      Boolean lIssue = false)
        {
            string strSQL = "";

            strSQL = "INSERT into Stock SELECT CreatedOn,'" + docType + "'," + TankID + "," + stkType + ",Consumption,'" + ContractNo + "'," + IDFieldName + " FROM " + TblName + " WHERE " + IDFieldName + " = " + docID;
            int RowEffected = Common.ExecuteQuery(strSQL);
        }


        public static void UpdateStock(string docType, int stkType, int docID, string TblName, string IDFieldName,
                                        int KitchenId, string strRate = "Rate",
                                        Double Qty = -1,
                                        DateTime? dDate = null,
                                        Boolean lIssue = false)
        {
            string strSQL = "";

            if (Qty == -1) //stock Adjustment 
            {
                if (lIssue)
                {
                    strSQL = "INSERT into Stock SELECT '" + dDate + "','" + docType + "'," + IDFieldName + ",ITEMID," + stkType + ",QUANTITY," + strRate + "," + KitchenId + " FROM " + TblName + " WHERE " + IDFieldName + " = " + docID;
                }
                else
                {
                    strSQL = "INSERT into Stock SELECT '" + dDate + "','" + docType + "'," + IDFieldName + ",ITEMID," + stkType + ",QUANTITY," + strRate + "," + KitchenId + " FROM " + TblName + " WHERE " + IDFieldName + " = " + docID;
                }
            }
            else
            {
                strSQL = "INSERT into Stock SELECT '" + dDate + "','" + docType + "'," + IDFieldName + " ,ITEMID," + stkType + "," + Qty + "," + strRate + "," + KitchenId + " FROM " + TblName + " WHERE " + IDFieldName + " = " + docID;
            }
            // return strSQL;
            int RowEffected = Common.ExecuteQuery(strSQL);
        }


        public static List<AsofStock> GetAsofStock(int CateogryID)
        {



            string sql = " Select a.Name as ItemName,a.Unit,b.Name as Cateogry, " +
                    " (Select isnull(Sum(Quantity), 0) as Quantity from stock where a.ID = Stock.ItemID and stock.Stock_Type = 1 and KitchenID = 1)" +
                    " + " +

                    " isnull((Select Quantity from tblOpeningBalance" +
                    " Where a.id = tblOpeningBalance.ItemID and KitchenID = 1),0)" +

                    " -" +
                   " (Select isnull(Sum(Quantity), 0) as Quantity from stock where a.ID = Stock.ItemID and stock.Stock_Type = 0 and KitchenID = 1) as CurrentStock, " +

                   " Isnull((Select ISNULL(Sum(Quantity * Rate), 0) as Cost from Stock where a.ID = Stock.ItemID and KitchenID = 1 and Stock_Type = 1)" +
                     " + " +
                        " isnull((Select ISNULL(Quantity * Rate, 0) as Cost from tblOpeningBalance where tblOpeningBalance.itemid = a.ID  and KitchenID = 1),0)" +
                         "-" +
                        " (Select ISNULL(Sum(Quantity * Rate), 0) as Cost from Stock where a.ID = Stock.ItemID and KitchenID = 1 and Stock_Type = 0),0) as CostofGood" +


                   " From tblitem a left outer join  tblCategory b on a.categoryid=b.ID";

            if (CateogryID > 0)
            {
                sql += " Where CategoryID=" + CateogryID;
            }
            sql += " Order by b.name";

            var obj =Common.GetAsofStock(sql);
            return obj;
        }

        public static List<StockLedger> GetStockLedger(int ItemID)
        {

            string sql = " Select tblTank.ID as document_id,tblTank.balDate as Date,'OB' as document_type," +
                         " 'Opening Balance' as document_Description,'' as CustomerContractNo,'' as CustomerName,'' as issued,'' as received,0 as balance,OpeningBalance as Openbal" +
                         " From tblTank where tblTank.id = " + ItemID + "" +
                         " Union all" +



                        " SELECT ISNULL(Stock.Document_ID, 0) AS document_id, Stock.Date," +
                        " document_type," +
                        "   CASE document_type WHEN 'IN' THEN 'Consumption'" +
                                         " WHEN 'DC' THEN 'Filled'" +
                                         " WHEN 'SA' THEN 'Adjustment'" +
                                         " WHEN 'PR' THEN 'Purchase Return' ELSE ' ' END AS document_Description," +
                                         " Stock.CustomerContractNo,tblcustomer.CustomerName," +
                       " CASE stock_type WHEN 0 THEN Quantity ELSE 0 END AS issued," +
                       " CASE stock_type WHEN 1 THEN Quantity ELSE 0 END AS received," +
                       " balance = case stock_type when 1 then Quantity +0 else 0 - quantity  end," +
                       " isnull((select OpeningBalance from tblTank where id = 1),0) as Openbal" +
                       " FROM stock left outer join  tblCustomer on  stock.CustomerContractNo = tblcustomer.ContractNo" +
                       " Where stock.TankID = " + ItemID + "";


            var obj = Common.GetStockLedger(sql);
            if (obj.Count > 0 )
            { 
            if (obj.Count > 0)
            {

                decimal bQ = obj[0].Openbal;
                for (int i = 0; i < obj.Count; i++)
                {
                    if (obj[i].balance > 0)
                    {

          
                        bQ += obj[i].balance;
                        obj[i].Actualbalance = bQ;
         
                    }
                    else
                    {
         
                        bQ += obj[i].balance;
                        obj[i].Actualbalance = bQ;
         
                    }
                    //  obj[i].CurrentStockValue += obj[i].LineStockValue;
                }
            }
            }
            return obj;
        }



        public static List<DailyIssuance> GetDailyIssuance(DateTime txtFromDate, DateTime txtToDate, int cmbChief = 0, int cmbCategory = 0)
        {
            string sql = " Select a.id ,a.IssueDate,a.Remarks,tblCategory.Name as Categrory,c.Name as ItemName,c.Unit,b.Quantity,b.Rate,d.Name as IssueTo,(b.Quantity * b.Rate) as TotalValue from th_sales  a,TD_Sales b ,tblItem c ,tblChief d,tblCategory " +
                         " Where a.id = b.SalesID" +
                         " And b.ItemID = c.ID" +
                         " And a.IssueTo = d.id " +
                         " And c.CategoryID=tblCategory.ID" +
                         " And a.KitchenID=1" +
                         " And a.IssueDate >=' " + txtFromDate + "' And a.IssueDate <'" + txtToDate.AddDays(1) + "'";

            if (cmbChief > 0)
            {
                sql = sql + " And a.IssueTo=" + cmbChief;
            }


            if (cmbCategory > 0)
            {
                sql = sql + " And c.CategoryID=" + cmbCategory;
            }

            sql =sql + " Order by IssueDate ";

            var obj = Common.GetDailyIssuance(sql);
            return obj;

        }

        public static List<PurchaseReturn> GetPurchaseReturn(DateTime txtFromDate, DateTime txtToDate)
        {
            string sql = " Select a.id ,a.RDate,a.Returnby,a.Remarks,c.Name as ItemName,c.Unit,b.Quantity,b.Rate,d.Name as Supplier,(b.Quantity * b.Rate) as TotalValue " +
                         " From TH_PurchaseReturn a,Td_PurchaseReturn b, tblItem c ,tblSupplier d " +
                         " Where a.id = b.ReturnID" +
                         " And b.ItemID = c.ID" +
                         " And a.SupplierID = d.id" +
                         " And a.RDate >=' " + txtFromDate + "' And a.RDate <'" + txtToDate.AddDays(1) + "'" +
                         " Order by RDate ";

            var obj = Common.GetPurchaseReturn(sql);
            return obj;

        }

        

            public static List<PurchaseReturn> GetIssueReturn(DateTime txtFromDate, DateTime txtToDate)
        {
            string sql = " Select a.id ,a.RDate,a.Returnby,a.Remarks,c.Name as ItemName,c.Unit,b.Quantity,b.Rate,(b.Quantity * b.Rate) as TotalValue " +
                         " from TH_IssueReturn a,Td_IssueReturn b, tblItem c " +
                         " Where a.id = b.ReturnID" +
                         " And b.ItemID = c.ID " +
                         " And a.RDate >=' " + txtFromDate + "' And a.RDate <'" + txtToDate.AddDays(1) + "'" +
                         " Order by RDate ";

            var obj = Common.GetPurchaseReturn(sql); //Excute SQL ONLY 
            return obj;

        }




        public static List<DailyPurchase> GetDailyPurchase(DateTime txtFromDate, DateTime txtToDate,string InvoiceNo="")
        {
            string sql = " Select a.Remarks,a.InvoiceNo,a.id ,Date,a.ReceivedBy,c.Name as ItemName,c.Unit,b.Quantity,b.Rate,(b.Quantity * b.Rate) as TotalValue,tblSupplier.Name as Supplier from th_Purchase  a,TD_Purchase b ,tblItem c,tblSupplier " +
                         " Where a.id = b.PurchaseID" +
                         " And b.ItemID = c.ID" +
                         " And a.SupplierId=tblSupplier.ID" +
                         " And a.Date >=' " + txtFromDate + "' And a.Date <'" + txtToDate.AddDays(1) + "'";

            if (InvoiceNo !="")
            {
                sql = sql + " And a.InvoiceNo='" + InvoiceNo + "'";
            }
            sql = sql + " Order by Date ";

            var obj = Common.GetDailyPurchase(sql);
            return obj;
        }


        public static List<DayEndReport> GetDayEndReport(int CateogryID)

        {
            var obj = Common.GetDayEndReport(CateogryID);
            return obj;
        }

#region Dasboard
        public static decimal GetStockValue(int KitchenID)

        {
            var obj = Common.GetStockValue(1);
            return obj;
        }
        public static List<TotalValueCategoryWise> GetStockValueCategoryWise(int KitchenID)

        {
            var obj = Common.GetStockValueCategoryWise(KitchenID);
            return obj;
        }

        public static List<TotalValueCategoryWise> GetConsumptionCategoryWise(int KitchenID)

        {
            var obj = Common.GetConsumptionCategoryWise(KitchenID);
            return obj;
        }

        


        public static decimal GetIssueValue(int KitchenID)

        {
            var obj = Common.GetIssueValue(1);
            return obj;
        }

        public static decimal GetPurchaseValue(int KitchenID)

        {
            var obj = Common.GetPurchaseValue(1);
            return obj;
        }
        public static decimal GetIssueReturnValue(int KitchenID)

        {
            var obj = Common.GetIssueReturnValue(1);
            return obj;
        }
        public static decimal GetPurchaseReturnValue(int KitchenID)

        {
            var obj = Common.GetPurchaseReturnValue(1);
            return obj;
        }
        public static List<TransactionPanel> TransactionPanel (int KitchenID)
        {
            var obj = Common.TransactionPanel(1);
            return obj;
        }




        #endregion

    }



}