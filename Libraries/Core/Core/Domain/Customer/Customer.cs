using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Domain
{
    public class Customer
    {
        [Required(ErrorMessage = " Required")]
        public string ContractNo { get; set; }
     
        [Required(ErrorMessage = " Required Name ")]
        public String CustomerName { get; set; }
        
        public String MobileNo { get; set; }

        public String FlatNo { get; set; }
        public int BuildingID { get; set; }
        public decimal Deposit { get; set; }

    }
}
