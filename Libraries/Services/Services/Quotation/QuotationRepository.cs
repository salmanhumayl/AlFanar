using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Services.Quotation
{
    public class QuotationRepository
    {
        private System.Data.IDbConnection db;
        public QuotationRepository()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

        }


        
    public bool IsQuationGenerated(int PRID)
        {

            var obj = db.Query<int>("Select TH_Quotation.id  from TH_Quotation WHERE PRID =1 and SupplierID=18 and Status=1").ToArray();
            db.Close();


            if (Convert.ToInt16(obj[0]) > 0)
             {
                return true;
             }

            return false;
        }

        public List<QuotationItemDetail> GetQuotationItemDetailPRSupplierWise(int PRID)
        {

            var obj = db.Query<QuotationItemDetail>(" Select a.ID as itemID,b.ID,a.Name as ItemName ,b.Qty as RequiredQty,Z.Unit,Z.Specs,c.Name as Brand,QuoatedRate " +
                                                    " from tblitem a inner join TD_Quotation b on a.id = b.ItemId" +
                                                    " left outer join tblBrand c on c.Id = a.BrandID " +
                                                    " inner join Th_Quotation on Th_Quotation.id = b.QuotationID " +
                                                    " inner join TD_PurchaseRequisition Z  on Z.PRID=Th_Quotation.PRID" +
                                                    " Where Th_Quotation.PRID = " + PRID + "  and Th_Quotation.SupplierID =" + Convert.ToInt16(System.Web.HttpContext.Current.Session["SupplierID"])).ToList();
            
            db.Close();
            return obj;

        }
        public List<QuotationItemDetail> GetQuotationDetail(int QuotationID)
        {

            var obj = db.Query<QuotationItemDetail>(" Select a.ID as itemID,b.ID,a.Name as ItemName ,b.Qty as RequiredQty,a.Unit,c.Name as Brand,QuoatedRate " +
                                                    " from tblitem a inner join TD_Quotation b on a.id = b.ItemId" +
                                                    " left outer join tblBrand c on c.Id = a.BrandID " +
                                                    " inner join Th_Quotation on Th_Quotation.id = b.QuotationID " +
                                                    " Where Th_Quotation.ID = " + QuotationID).ToList();

            db.Close();
            return obj;

        }
        

        public List<GeneratedQuatations> GetQuotations(int PRID)
        {

            var obj = db.Query<GeneratedQuatations>(" Select a.id as RecordID,a.SupplierID, tblSupplier.Name, a.QuoteDate, sum(TD_Quotation.Qty * TD_Quotation.QuoatedRate) as Total " +
                                                    " from Th_Quotation a" +
                                                    " inner join TD_Quotation on a.id = TD_Quotation.QuotationID " +
                                                    " inner join tblSupplier on tblSupplier.id = a.SupplierID" +
                                                    " Where a.PRID = " + PRID + " " +
                                                    " GROUP BY a.id,a.SupplierID, tblSupplier.Name, a.QuoteDate").ToList();

            db.Close();
            return obj;

        }

        public List<QuaotedSupplier> GetSupplier(int PRID)
        {

            var obj = db.Query<QuaotedSupplier>(" Select tblSupplier.name  from TH_Quotation,tblSupplier " +
                                               " Where TH_Quotation.SupplierID = tblSupplier.id " +
                                               " And TH_Quotation.PRID= " + PRID + " " +
                                               " Order by SupplierID").ToList();

            db.Close();
            return obj;

        }

        public List<QuaotedSupplierRates> GetSupplierQuoatedRate(int PRID)
        {

            var obj = db.Query<QuaotedSupplierRates>(" Select TD_Quotation.itemid, tblCategory.Name as Category, tblBrand.Name as Brand,tblItem.Name, Td_PurchaseRequisition.Unit,Td_PurchaseRequisition.Specs, " +
                                                     " quoatedRate,SupplierID,TD_Quotation.Qty,quoatedRate * TD_Quotation.Qty as Total  from TD_Quotation " +
                                                     " Inner join TH_Quotation on TH_Quotation.ID = TD_Quotation.QuotationID" +
                                                     " Inner join tblitem on tblitem.id = TD_Quotation.ItemId "+
                                                     " Left outer join tblCategory on tblCategory.id = tblItem.CategoryID" +
                                                     " Left outer join tblBrand on tblBrand.id = tblItem.BrandID " +
                                                     " Inner join Td_PurchaseRequisition on Td_PurchaseRequisition.PRID=TH_Quotation.PRID  " +
                                                     " Where TH_Quotation.PRID= " + PRID + " " +
                                                      " Order by TD_Quotation.ItemId, SupplierID").ToList();

            db.Close();
            return obj;

        }

        public List<QuaotedTotal> GetSupplierQuoatedTotal(int PRID)
        {

            var obj = db.Query<QuaotedTotal>(" Select SupplierID,Sum(quoatedRate * Qty)  as Total from Td_Quotation " +
                                                     " Inner join  TH_Quotation on TH_Quotation.ID = TD_Quotation.QuotationID" +
                                                     " Where TH_Quotation.PRID= " + PRID + " " +
                                                     " Group by SupplierID " +
                                                     " order by SupplierID").ToList();

            db.Close();
            return obj;

        }

        

        public void GenerateQuotation(List<Services.Purchase_Requisition.PRDetail> model)
        {
            int ID = 0;
            string sql;

            //Create Header 
            // Status =1 // Submitted and cannot modilfy ... 

            sql = " INSERT INTO TH_Quotation(PRID,SupplierID,status)VALUES (@PRID," + Convert.ToInt16(System.Web.HttpContext.Current.Session["SupplierID"]) + ",1 ) " +
                                     "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    model[0].PRID

                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("IX_LicenseNo") > 0)
                {

                }
            }

            foreach (var obj in model)
            {
                if (ID > 0)
                {
                    sql = " INSERT INTO TD_Quotation(QuotationID,ItemId,Qty,QuoatedRate)VALUES (" + ID + " ,@ItemId,@RequiredQty,@QuotedPrice) ";
                    db.Execute(sql, new
                    {
                        obj.itemID,
                        obj.RequiredQty,
                        obj.QuotedPrice

                    });

                }

            }
        }
    }
}
