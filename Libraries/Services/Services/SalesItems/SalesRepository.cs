using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Core.Domain.IssueReturn;

namespace Services.SalesItems
{
 
    public class SalesRepository
    {
        private System.Data.IDbConnection db;
        private int KitchenID;
        public SalesRepository()
        {

            KitchenID = Convert.ToInt16(System.Web.HttpContext.Current.Session["KitchenID"]);
        }

        public int UpdateSales(List<Services.SalesItems.SalesItems> SalesItem)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;
            //string Documentno = "IVN-0001";
            sql = " INSERT INTO TH_Sales(IssueTo,IssueDate,Remarks,KitchenID)VALUES (@IssueTo,@IssueDate,@Remarks," + KitchenID + ") " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    SalesItem[0].IssueTo,
                    SalesItem[0].IssueDate,
                    SalesItem[0].Remarks


                }).SingleOrDefault();
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("") > 0)
                {

                }
            }
            foreach (SalesItems SalesItems in SalesItem)
            {

                sql = " Insert into TD_Sales (SalesID,ItemID,Quantity,Rate) " +
                          " Values (" + ID + ",@ItemID,@Quantity,@Rate)";


                db.Execute(sql, new
                {
                    SalesItems.ItemID,
                    SalesItems.Quantity,
                    SalesItems.Rate
                });
            }
            return ID;
        }


        public int UpdateIssueReturn(List<Core.Domain.IssueReturn.IssueReturnDetail> IssueReturnItem, Core.Domain.IssueReturn.IssueReturn IssueReturnHeader)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql;

            //First Add Header 
            int ID = 0;

            sql = " INSERT INTO TH_IssueReturn(IssueNo,RDate,KitchenID,ReturnBy,Remarks)VALUES (0,@RDate,1,@ReturnBy,@Remarks) " +
                                "Select Cast(SCOPE_IDENTITY() AS int)";


            try
            {
                ID = db.Query<int>(sql, new
                {
                    IssueReturnHeader.RDate,
                    IssueReturnHeader.ReturnBy,
                    IssueReturnHeader.Remarks

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
            foreach (IssueReturnDetail obj in IssueReturnItem)
            {

                sql = " Insert into TD_IssueReturn (ReturnID,ItemID,Quantity,Rate) " +
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
        
    }
}
   
