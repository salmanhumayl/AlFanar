using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace Services.Category
{
    public class CategoryRepository
    {
        private System.Data.IDbConnection db;
        public List<Core.Domain.Category> GetList()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Category>("Select * from tblCategory order by Name").ToList();
            db.Close();
            return obj;
        }

        public Core.Domain.Category Find(int id)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            var obj = db.Query<Core.Domain.Category>("Select * from tblCategory Where id=" + id).FirstOrDefault();
            db.Close();
            return obj;
        }
        public void Insert(Core.Domain.Category  model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " INSERT INTO tblcategory(Name) " +
                              " VALUES (@Name)";

            db.Execute(sql, new
            {
                model.Name
            });
            db.Close();
        }


        public void Update(Core.Domain.Category model)
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["cnConsumption"].ConnectionString);
            string sql = " Update tblCategory Set Name=@Name Where id=@ID";

            db.Execute(sql, new
            {
                
                model.Name,
                model.ID
            });
            
            db.Close();
        }


    }
}
