using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class Stock
    {
        
        public static void UpdateStock(int itemid, int KitchenID, decimal Quantity, string DocType)
        {
            var obj = new Stock();
            var StockItem = obj.FindStock(itemid, KitchenID);

            if (DocType == "I")
            {
                 

            }
        }


        public StockItem FindStock(int itemid, int KitchenID)
        {
              IRepository = new GroupRepository();
              var obj = IRepository.Find(itemid, KitchenID);
              
             return obj;
            
        }



    }

    public class StockItem
    {
        public int ItemID { get; set; }
        public int KitchenID { get; set; }
        public float Quantity { get; set; }
        public float Rate { get; set; }
    }
        