using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.StockAdjustment
{
   public  class StockAdjustment
    {
        public int id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AdjDate { get; set; }
        public int ItemID { get; set; }
        public string  KitchenID { get; set; }
        [Required]
        public string Reason { get; set; }

        public int Quantity { get; set; }

        public string Remarks { get; set; }
        public decimal Rate { get; set; }

        public double nCurrentStock { get; set; } //Readonly 
    }
}
