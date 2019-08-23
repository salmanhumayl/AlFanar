using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.OpeningBalance
{
    public class OpeningBalance
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OpbalDate { get; set; }
        public int ItemID { get; set; }
        public int KitchenID { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }
    }
}
