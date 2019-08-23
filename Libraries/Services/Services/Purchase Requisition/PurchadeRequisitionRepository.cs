using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Core.Domain.Purchase_Requisition;

namespace Services.Purchase_Requisition
{
  
    public class PurchadeRequisitionRepository
    {
        private System.Data.IDbConnection db;


        public PurchadeRequisitionRepository()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            
        }
        public List<PROutstandingListMainModel> GetOutstandingPR()
        {
           
            var obj = db.Query<PROutstandingListMainModel>("Select a.ID,a.PRNo,a.PRDate from TH_PurchaseRequisition a Where Status=1").ToList();
            db.Close();
            return obj;

        }
        public List<PROutstandingListMainModel> GetPRNotSubmitted()
        {

            var obj = db.Query<PROutstandingListMainModel>("Select a.ID,a.PRNo,a.PRDate from TH_PurchaseRequisition a Where Status=0").ToList();
            db.Close();
            return obj;

        }

        public List<PROutstandingListMainModel> GetPRSubmitted()
        {

            var obj = db.Query<PROutstandingListMainModel>("Select a.ID,a.PRNo,a.PRDate from TH_PurchaseRequisition a Where Status=3").ToList();
            db.Close();
            return obj;

        }
        


        public List<PRDetail> GetItemDetail(int RecordID)
        {

            var obj = db.Query<PRDetail>(" Select a.ID as itemID,b.PRID,a.Name as ItemName ,b.Qty as RequiredQty,b.Unit,b.Specs,c.Name as Brand,0 as QuotedPrice  from tblitem  a " +
                                                  " inner join TD_PurchaseRequisition b on a.id = b.ItemId" +
                                                  "  left outer join tblBrand c on c.Id = a.BrandID " +
                                                  " where b.PRID =" + RecordID).ToList();
            db.Close();
            return obj;

        }

        public string NewPurchaseReq(List<Core.Domain.Purchase_Requisition.PurchaseRequisitionDetail> PurchaseItem, Core.Domain.Purchase_Requisition.PurchaseRequisition PurchaseHeader)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;
            
            
            string NewPrNO = "PR-" + DateTime.Now.ToLongTimeString();
            sql = " INSERT INTO TH_PurchaseRequisition(PRNo,PRDate,KitchenID,Remarks)VALUES ('" + NewPrNO + "',@PRDate,1,@Remarks) " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";

            try
            {
                ID = db.Query<int>(sql, new
                {
                   
                    PurchaseHeader.PRDate,
                    PurchaseHeader.Remarks

                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("") > 0)
                {
                    db.Close();
                    return "-1";
                }
            }
            foreach (PurchaseRequisitionDetail obj in PurchaseItem)
            {

                sql = " Insert into TD_PurchaseRequisition (PRID,ItemID,Qty,Unit,Specs) " +
                          " Values (" + ID + ",@ItemID,@Qty,@Unit,@Specs)";


                db.Execute(sql, new
                {
                    obj.ItemId,
                    obj.Qty,
                    obj.Unit,
                    obj.Specs

                });
            }
           
            return NewPrNO;
        }

        public string EditPurchaseReq(List<Core.Domain.Purchase_Requisition.PurchaseRequisitionDetail> PurchaseItem, Core.Domain.Purchase_Requisition.PurchaseRequisition PurchaseHeader)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

           
            foreach (PurchaseRequisitionDetail obj in PurchaseItem)
            {

                sql = " Insert into TD_PurchaseRequisition (PRID,ItemID,Qty) " +
                          " Values (@PRID,@ItemID,@Qty)";


                db.Execute(sql, new
                {
                    obj.PRID,
                    obj.ItemId,
                    obj.Qty
                    

                });
            }

            return "";
        }


        public List<EditPurchaseReqDetail> GetPurchaseReqDetail(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            var obj = db.Query<EditPurchaseReqDetail>(" Select Th_PurchaseRequisition.ID as PRID,b.id ,tblCategory.Name as Category,c.Name as Brand, " +
                                                      " a.ID as itemID, a.Name as ItemName, b.Unit,b.Specs, b.Qty" +
                                                     " From tblitem a inner join TD_PurchaseRequisition b on a.id = b.ItemId" +
                                                     " Left outer join tblBrand c on c.Id = a.BrandID" +
                                                     " inner join Th_PurchaseRequisition on Th_PurchaseRequisition.id = b.PRID" +
                                                     " inner join tblCategory on tblCategory.id = a.CategoryID" +
                                                     " Where Th_PurchaseRequisition.id= " + id + " order by tblCategory.Name").ToList();

            db.Close();
            return obj;

        }
        public EditPurchaseReqHeader GetPurchaseReqHeader(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            var obj = db.Query<EditPurchaseReqHeader>(" Select Th_PurchaseRequisition.PRNO, Th_PurchaseRequisition.PRDate,Remarks " +
                                                     " From Th_PurchaseRequisition " +
                                                     "  Where Th_PurchaseRequisition.ID = " + id + "").FirstOrDefault();

            db.Close();
            return obj;

        }


        
            public List<ReqitemCategory> GetPurchaseReqCategory(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            var obj = db.Query<ReqitemCategory>(" Select distinct tblcategory.Name from tblCategory, Td_PurchaseRequisition, tblitem " +
                                                " where  Td_PurchaseRequisition.ItemId = tblitem.id" +
                                                " and tblCategory.ID = tblitem.CategoryID" +
                                                " and Td_PurchaseRequisition.prid =" + id + " order by tblcategory.name").ToList();

            db.Close();
            return obj;

        }

        public bool SubmitApproval(int nPRID)
        {
            string sql = " Update TH_PurchaseRequisition Set Status=3,SubmittedBy='" + System.Web.HttpContext.Current.User.Identity.Name + "' where id =@nPRID";


            db.Execute(sql, new
            {
                nPRID
            });
            db.Close();
            return true;
        }
        public bool ApprovedPR(int nPRID)
        {
            string sql = " Update TH_PurchaseRequisition Set Status=1,ApprovedBy='" + System.Web.HttpContext.Current.User.Identity.Name + "' where id =@nPRID";


            db.Execute(sql, new
            {
                nPRID
            });
            db.Close();
            return true;
        }
    }
}
