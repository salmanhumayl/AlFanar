using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Building
{


    public class BuildingViewModel
    {
        public int ID { get; set; }
        public string BuildingNo { get; set; }
        public string TankName { get; set; }
       

    }


    public class ItemViewModel
    {
        public int ID { get; set; }
        public string Cateogry { get; set; }
        public string Brand { get; set; }
        public string ItemName { get; set; }

        public string Unit { get; set; }


    }

    public class ItemStockViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public string ItemArtURL { get; set; }

        public decimal CurrentStock { get; set; }
        public decimal CostofGood { get; set; }



    }

    public class OpeningBalanceViewModel
    {

        public int id { get; set; }
        public string  itemCode { get; set; }
        public string Cateogory { get; set; }
        public string Name { get; set; }

        public DateTime OpbalDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
