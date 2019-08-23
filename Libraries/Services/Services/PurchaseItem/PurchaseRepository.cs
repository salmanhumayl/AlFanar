using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Core.Domain.Purchase;
using Core.Domain.PurchaseReturn;

namespace Services.PurchaseItem
{
    public class PurchaseRepository
    {
        private System.Data.IDbConnection db;
        public int UpdatePurchase(List<Core.Domain.Purchase.PurchaseDetail> PurchaseItem, Core.Domain.Purchase.Purchase PurchaseHeader)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;
            
            sql = " INSERT INTO TH_Purchase(InvoiceNo,Date,SupplierID,PONo,KitchenID,ReceivedBy,Remarks)VALUES (@InvoiceNo,@Date,@SupplierID,@PONo,1,@ReceivedBy,@Remarks) " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    PurchaseHeader.InvoiceNo,
                    PurchaseHeader.Date,
                    PurchaseHeader.SupplierID,
                    PurchaseHeader.PONo,
                    PurchaseHeader.ReceivedBy,
                    PurchaseHeader.Remarks

                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("IX_TH_Purchase-Invoice") > 0)
                {
                    db.Close();
                    return 0;
                }
            }
            foreach (PurchaseDetail obj in PurchaseItem)
            {

                sql = " Insert into TD_Purchase (PurchaseID,ItemID,Quantity,Rate) " +
                          " Values (" + ID + ",@ItemID,@Quantity,@Rate)";


                db.Execute(sql, new
                {
                    obj.ItemID,
                    obj.Quantity,
                    obj.Rate
                });
            }
           
            return ID;
        }


        public int UpdatePurchaseReturn(List<Core.Domain.PurchaseReturn.PurchaseReturnDetail> ReturnItem, Core.Domain.PurchaseReturn.PurchaseReturn ReturnHeader)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;

            sql = " INSERT INTO TH_PurchaseReturn(PurchaseId,SupplierID,RDate,KitchenID,ReturnBy,Remarks)VALUES (0,@SupplierID,@RDate,1,@ReturnBy,@Remarks) " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    ReturnHeader.RDate,
                    ReturnHeader.ReturnBy,
                    ReturnHeader.Remarks,
                    ReturnHeader.SupplierID

                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("") > 0)
                {
                    db.Close();
                    return 0;
                }
            }
            foreach (PurchaseReturnDetail obj in ReturnItem)
            {

                sql = " Insert into TD_PurchaseReturn (ReturnID,ItemID,Quantity,Rate) " +
                          " Values (" + ID + ",@ItemID,@Quantity,@Rate)";


                db.Execute(sql, new
                {
                    obj.ItemID,
                    obj.Quantity,
                    obj.Rate
                });
            }

            db.Close();
            return ID;
        }


        public int UpdatePurchaseReturnByInvoice(Services.PurchaseItem.PurchaseReturnViewModel model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;

            sql = " INSERT INTO TH_PurchaseReturn(PurchaseId,SupplierID,RDate,KitchenID,ReturnBy)VALUES (@PurchaseID,@SupplierID,@returnDate,1,@ReturnBy) " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    model.PurchaseReturnItem[0].PurchaseID,
                    model.returnDate,
                    model.obj.SupplierID,
                    model.ReturnBy

                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("") > 0)
                {
                    db.Close();
                    return 0;
                }
            }
            foreach (PurchaseReturnItem objDetail in model.PurchaseReturnItem)
            {

                if (objDetail.ReturnQty > 0)
                {
                    sql = " Insert into TD_PurchaseReturn (ReturnID,ItemID,Quantity,Rate) " +
                              " Values (" + ID + ",@ItemID,@ReturnQty,@Rate)";


                    db.Execute(sql, new
                    {
                        objDetail.itemID,
                        objDetail.ReturnQty,
                        objDetail.Rate
                    });
                }
            }
            db.Close();
            return ID;
        }

        


        public List<PurchaseReturnItem> GetPurchaseDetail(string InvoiceNo)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            var obj = db.Query<PurchaseReturnItem>(" Select Th_Purchase.ID as PurchaseID,tblCategory.Name as Category,c.Name as Brand," +
                                                   " a.ID as itemID, a.Name as ItemName, a.Unit, b.Rate, b.Quantity as PurchaseQty, 0 as ReturnQty " +
                                                   " From tblitem a inner join TD_Purchase b on a.id = b.ItemId" +
                                                   " Left outer join tblBrand c on c.Id = a.BrandID" +
                                                   " inner join Th_Purchase on Th_Purchase.id = b.PurchaseID" +
                                                   " inner join tblCategory on tblCategory.id = a.CategoryID" +
                                                   " Where Th_Purchase.InvoiceNo = '" + InvoiceNo + "'").ToList();

            db.Close();
            return obj;

        }
        public PurchaseReturnHeader GetPurchaseHeader(string InvoiceNo)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            var obj = db.Query<PurchaseReturnHeader>(" Select Th_Purchase.SupplierID, tblSupplier.Name as Supplier, Th_Purchase.Date as PurchaseDate " +
                                                     " From Th_Purchase inner join tblSupplier  on Th_Purchase.SupplierID = tblSupplier.ID " +
                                                     " Where Th_Purchase.InvoiceNo = '" + InvoiceNo + "'").FirstOrDefault();

            db.Close();
            return obj;

        }

        
              public int ValidateInvoice(string InvoiceNo)
            {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = "select id from TH_Purchase where InvoiceNo='" + InvoiceNo + "'";
            var obj = db.Query<int>(sql).FirstOrDefault();
            //var obj2 = db.Query(sql).ToArray();
            //var obj = db.Query<int>(sql).FirstOrDefault();
            db.Close();
            return obj;
        }

    }
}
