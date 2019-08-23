using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using Core.Domain;
namespace Services.Customer
{
    public class CustomerRepository
    {
        private System.Data.IDbConnection db;
 
        public CustomerRepository()
        {
           // db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);

        }

        public List<CustomerViewModel> GetListView()
        {
            string sql = " Select a.*,b.BuildingNo,C.Name as TankName from tblCustomer a ,tblBuilding b,tblTank c  " +
                         " where a.BuildingID=b.id And b.TankId=c.id";


            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Services.Customer.CustomerViewModel>(sql).ToList();
            db.Close();
            return obj;
        }

        public CustomerDetailViewModel GetCustomerDetail(string ContractNo)
        {
            string sql = " Select a.*,b.BuildingNo,c.Name as TankName,c.id as TankID  from tblCustomer a ,tblBuilding b,tblTank c " +
                         " Where a.BuildingID = b.id And b.TankID=C.ID" +
                         " And ContractNo = '" + ContractNo + "'";

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Services.Customer.CustomerDetailViewModel>(sql).SingleOrDefault();
            db.Close();
            return obj;
        }

        public int Insert(Core.Domain.Customer model, ref string ErrorMsg)
        {
            int ID = 0;
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " INSERT INTO tblCustomer(ContractNo,CustomerName,MobileNo,FlatNo,BuildingID,Deposit) " +
                              " VALUES (@ContractNo,@CustomerName,@MobileNo,@FlatNo,@BuildingID,@Deposit)";
                             
            try
            {
                ID = db.Execute(sql, new
                {
                    model.ContractNo,
                    model.CustomerName,
                    model.MobileNo,
                    model.FlatNo,
                    model.BuildingID,
                    model.Deposit
                });

            }
            catch (Exception e)
            {

                if (e.Message.IndexOf("PK_tblCustomer") > 0)
                {
                    ErrorMsg = "Contract No Already Exist....";

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
