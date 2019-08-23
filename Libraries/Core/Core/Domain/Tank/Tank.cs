using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Tank
    {
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }

        public decimal OpeningBalance { get; set; }
    }
}
