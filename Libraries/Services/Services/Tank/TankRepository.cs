using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace Services.Tank
{
    public class TankRepository
    {
        private System.Data.IDbConnection db;
        public List<Core.Domain.Tank> GetList()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Tank>("Select * from tblTank").ToList();
            db.Close();
            return obj;
        }

        public Core.Domain.Tank Find(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Tank>("Select * from tblTank Where id=" + id).FirstOrDefault();
            db.Close();
            return obj;
        }

        public List<TankAsofConsumption> GetTotalConsumption()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string Sql = " Select tblTank.Name,tblTank.id,isnull(dbo.Get_TankConsumption(id), 0) as TotalComsumption from tblTank " +
                 " GROUP BY tblTank.Name,tblTank.id";

            var obj = db.Query<TankAsofConsumption>(Sql).ToList();

            db.Close();
            return obj;
        }



        public void Insert(Core.Domain.Tank model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " INSERT INTO tblTank(Name) " +
                              " VALUES (@Name)";

            db.Execute(sql, new
            {
                model.Name
            });
            db.Close();
        }


        public void Update(Core.Domain.Tank model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " Update tblBrand Set Name=@Name Where id=@ID";

            db.Execute(sql, new
            {

                model.Name,
                model.ID
            });

            db.Close();
        }
    }
}
