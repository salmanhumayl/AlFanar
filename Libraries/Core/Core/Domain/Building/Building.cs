using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Domain
{
    public class Building
    {
        public int ID { get; set; }
        public int TankID { get; set; }
        [Required]
        public string BuildingNo { get; set; }
        public string BuildingName { get; set; }
       

    }
}
