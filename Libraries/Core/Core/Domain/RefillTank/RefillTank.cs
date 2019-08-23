using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
   public  class RefillTank
    {
        public int id { get; set; }
        public int TankId { get; set; }
        [DataType(DataType.Date)]
        public DateTime FillingDate { get; set; }

        public decimal Quantity { get; set; }

        public string RefNo { get; set; }
        public string Remarks { get; set; }


    }
}
