using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Services.BillConsumption
{
    public class BillConsumptionRepository
    {
        private System.Data.IDbConnection db;
        public BillConsumptionRepository()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

        }

        public List<BillConsumptionViewModel> GetListView()
        {
            string sql = " Select a.*,b.CustomerName,b.FlatNo,c.BuildingNo from tblBillConsumption a, tblCustomer b, tblBuilding c " +
                         " Where a.ContractNo = b.ContractNo" +
                         " And b.BuildingID = c.ID";


            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Services.BillConsumption.BillConsumptionViewModel>(sql).ToList();
            db.Close();
            return obj;
        }


        

     public decimal GetBillOutstanding(string ContractNo)
        {
            string sql = " Select isnull(a.OutStandingBalance,0) " +
                         " + " +
                         " (Select isnull(Sum(BillAmount),0) from tblBillConsumption where ContractNo = '" + ContractNo + "') " +
                         " - " +
                         " (Select isnull(Sum(PaidAmount),0) from tblBillConsumption where ContractNo = '" + ContractNo + "') as  OutStanding" +
	                     " from tblCustomerOutstandingBal a where ContractNo = '" + ContractNo +"'";


            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query(sql).ToArray();
            db.Close();
            if (obj.Length > 0)
            {
                return Convert.ToDecimal(obj[0].OutStanding);
            }
            else
                return 0;
           
            
        }


        public LastReadingDetial GetLastReadingDetail(string ContractNo)
        {
            string sql = " Select top 1 CurrentReadingPeriodTo,CurrentReading from tblBillConsumption where ContractNo='" + ContractNo + "'" +
                         " Order by id desc";


            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Services.BillConsumption.LastReadingDetial>(sql).SingleOrDefault();
            db.Close();
            return obj;
            
            //CurrentReadingPeriodTo
        }

        public int Insert(Services.BillConsumption.BillConsumptionAddViewModel model, ref string ErrorMsg)
        {
            int ID = 0;
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

            decimal BillAmount = model.BillHeader.Consumption * model.BillHeader.Rate;
            string sql = " INSERT INTO tblBillConsumption(ContractNo,InvoiceNo,CurrentReadingPeriodFrom,CurrentReadingPeriodTo," +
                         " CurrentReadingDate,CurrentReading,Consumption,Rate,ServiceCharges,BillAmount,PaidAmount) " +
                              " VALUES (@ContractNo,@InvoiceNo,@CurrentReadingPeriodFrom,@CurrentReadingPeriodTo," +
                              " @CurrentReadingDate,@CurrentReading,@Consumption,@Rate,@ServiceCharges,@BillAmount,@PaidAmount)" +
                               " Select Cast(SCOPE_IDENTITY() AS int)";
            try
            {
                ID = db.Query<int>(sql, new
                {
                    model.CustomerDetail.ContractNo,
                    model.BillHeader.InvoiceNo,
                    model.BillHeader.CurrentReadingPeriodFrom,
                    model.BillHeader.CurrentReadingPeriodTo,
                    model.BillHeader.CurrentReadingDate,
                    
                    model.BillHeader.CurrentReading,
                    model.BillHeader.Consumption,
                    model.BillHeader.Rate,
                    model.BillHeader.ServiceCharges,
                    BillAmount,
                    model.BillHeader.PaidAmount,

                }).SingleOrDefault();

            }
            catch (Exception e)
            {

                if (e.Message.IndexOf("IX_tblItem-Name") > 0)
                {
                    ErrorMsg = "Item Already Exist....";

                }
                else
                {
                    ErrorMsg = e.Message;
                }

                db.Close();
                return 0;

            }
           
            return ID;
        }

    }
}
