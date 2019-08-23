using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;

namespace Services.Helper
{
    public class Common
    {
        public static int ExecuteQuery(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                int RowEffected = db.Execute(sql);
                return RowEffected;
            }
        }

        public static double ExecuteReader(string sql)
        {
            
            Double Qty=0;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString)) 
                {

               
                 System.Data.IDataReader Ireader = db.ExecuteReader(sql);

                Ireader.Read();
                //var Qty = reader.GetValue(0).ToString();
                //reader.GetInt32(reader.GetOrdinal("Kind")))

                try
                {
                    var obj = Ireader["Quantity"];
                    Qty = (double)obj;

                }
                catch (Exception)
                {
                    return 0;

                }
            }
            return Qty;





        }

        //Should be replace with <T>
        public static List<AsofStock> GetAsofStock(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<AsofStock>(sql).ToList();
                db.Close();
                return obj;

            }
                
        }

        public static List<StockLedger> GetStockLedger(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<StockLedger>(sql).ToList();
                db.Close();
                return obj;

            }

        }



        public static List<DailyIssuance> GetDailyIssuance(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<DailyIssuance>(sql).ToList();
                db.Close();
                return obj;

            }

        }


        public static List<PurchaseReturn> GetPurchaseReturn(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<PurchaseReturn>(sql).ToList();
                db.Close();
                return obj;

            }

        }

        public static List<DailyPurchase> GetDailyPurchase(string sql)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<DailyPurchase>(sql).ToList();
                db.Close();
                return obj;

            }

        }


        

        public static List<DayEndReport> GetDayEndReport(int CateogryID)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                string Sql = "Select* from V_DayEnd";
                if (CateogryID > 0)
                {
                    Sql += " Where CATID=" + CateogryID;
                }

                var obj = db.Query<DayEndReport>(Sql).ToList();
                db.Close();
                return obj;

            }

        }
        public static List<TotalValueCategoryWise> GetStockValueCategoryWise(int Kitchen)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                string Sql = "select V_StockAsOf.Cateogry as Category,Sum(Costofgood) as TotalValue from V_StockAsOf group by Cateogry";
                var obj = db.Query<TotalValueCategoryWise>(Sql).ToList();
                db.Close();
                return obj;
            }
        }

        public static List<TotalValueCategoryWise> GetConsumptionCategoryWise(int Kitchen)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                string Sql = "select Cat as Category,round(sum(costofgood),2) as TotalValue from V_Consumption group by Cat ";
                var obj = db.Query<TotalValueCategoryWise>(Sql).ToList();
                db.Close();
                return obj;
            }
        }
        

        public static decimal GetStockValue(int KitchenID)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@KitchenID", KitchenID);
            param.Add("@TotalStockValue", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);
            db.Open();
            db.Execute("GetStockValue", param, commandType: CommandType.StoredProcedure);
            db.Close();
            decimal netRate = param.Get<decimal>("@TotalStockValue");
            return netRate;
        }


        public static decimal GetIssueValue(int KitchenID)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@KitchenID", KitchenID);
            param.Add("@TotalIssueValue", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);
            db.Open();
            db.Execute("GetTotalIssue", param, commandType: CommandType.StoredProcedure);
            db.Close();
            decimal netRate = param.Get<decimal>("@TotalIssueValue");
            return netRate;
        }

        public static decimal GetPurchaseValue(int KitchenID)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@KitchenID", KitchenID);
            param.Add("@TotalPurchaseValue", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);
            db.Open();
            db.Execute("GetTotalPurchase", param, commandType: CommandType.StoredProcedure);
            db.Close();
            decimal netRate = param.Get<decimal>("@TotalPurchaseValue");
            return netRate;
        }


        public static decimal GetIssueReturnValue(int KitchenID)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@KitchenID", KitchenID);
            param.Add("@TotalReturnValue", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);
            db.Open();
            db.Execute("GetIssueReturnkValue", param, commandType: CommandType.StoredProcedure);
            db.Close();
            decimal netRate = param.Get<decimal>("@TotalReturnValue");
            return netRate;
        }

        public static decimal GetPurchaseReturnValue(int KitchenID)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@KitchenID", KitchenID);
            param.Add("@TotalReturnValue", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 10, scale: 2);
            db.Open();
            db.Execute("GetPurchaseReturnkValue", param, commandType: CommandType.StoredProcedure);
            db.Close();
            decimal netRate = param.Get<decimal>("@TotalReturnValue");
            return netRate;
        }
       
        public static void AddPrintQues(GraphicsState memStream)
        {
            System.Data.IDbConnection db;

            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            DynamicParameters param = new DynamicParameters();
            param.Add("@VarBinary", memStream);
            db.Open();
            db.Execute("printReceipt", param, commandType: CommandType.StoredProcedure);
            db.Close();
           
        }


        public static List<TransactionPanel> TransactionPanel(int KitchenID)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString))
            {
                var obj = db.Query<TransactionPanel>(" select * from V_TransactionPanel where kitchenID=1").ToList();
                db.Close();
                return obj;

            }

        }
    }
}

