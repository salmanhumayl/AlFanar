using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.IssueReturn
{
    public class IssueReturn
    {
        public int id { get; set; }
        public int IssueNo { get; set; }
        public DateTime RDate { get; set; }
        public string ReturnBy { get; set; }
        public decimal KitchenID { get; set; }
        public string Remarks { get; set; }
        public List<IssueReturnDetail> IssueReturnDetail { get; set; }
    }

    public class IssueReturnDetail
    {
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

    }
}
