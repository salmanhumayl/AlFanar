using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using Core.Domain;
using Core.Domain.OpeningBalance;
using System.Data;
using System.Web;
using System.IO;

namespace Services.Building
{
    public class BuildingRepositary
    {
        private System.Data.IDbConnection db;

        public List<BuildingViewModel> GetListView()
        {
            string sql = " Select a.id,a.BuildingNo,b.Name as TankName from tblBuilding a ,tbltank b " +
                         " Where a.TankID = b.id";



            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Services.Building.BuildingViewModel>(sql).ToList();
            db.Close();
            return obj;
        }

        public List<Core.Domain.Building> GetList()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Building>("Select * from tblBuilding").ToList();
            db.Close();
            return obj;
        }


        public Core.Domain.Building Find(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Building>("Select * from tblBuilding Where id=" + id).FirstOrDefault();
            db.Close();
            return obj;
        }
        

        
     
        public int InsertBuilding(Core.Domain.Building model ,ref string ErrorMsg)
        {
            int ID = 0;
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " INSERT INTO tblBuilding(BuildingNo,BuildingName,TankID) " +
                              " VALUES (@BuildingNo,@BuildingName,@TankID)" +
                               " Select Cast(SCOPE_IDENTITY() AS int)";
            try
            {
                ID = db.Query<int>(sql, new
                {
                    model.BuildingNo,
                    model.BuildingName,
                    model.TankID
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
            model.ID = ID; 
            db.Close();
          
            return ID;
        }


        public void Update(Core.Domain.Building model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " Update tblItem Set CategoryID=@CategoryID,BrandID=@BrandID,ItemCode=@ItemCode,Name=@Name,Unit=@Unit Where id=@ID";

            db.Execute(sql, new
            {
               
               model.ID
            });
            
            db.Close();
        }
       




    }
}