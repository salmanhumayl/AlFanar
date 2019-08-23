using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Services.RefillTank
{
   public  class RefillTankRepository
    {
        private System.Data.IDbConnection db;
        public List<Core.Domain.RefillTank> GetList()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.RefillTank>("Select * from tblUnit").ToList();
            db.Close();
            return obj;
        }


        public int Insert(Core.Domain.RefillTank model)
        {
            int ID = 0;
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " INSERT INTO tblRefillTank(TankId,FillingDate,Quantity) " +
                              " VALUES (@TankId,@FillingDate,@Quantity)" +
                               " Select Cast(SCOPE_IDENTITY() AS int)";
            try
            {
                ID = db.Query<int>(sql, new
                {
                    model.TankId,
                    model.FillingDate,
                    model.Quantity
                    

                }).SingleOrDefault();

            }
            catch (Exception e)
            {
                db.Close();
                return 0;

            }
            db.Close();
            return ID;
    
        }
            
        

    }
}
