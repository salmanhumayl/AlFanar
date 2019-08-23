using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SalesItems
{
   public  class SalesService
    {
       public int UpdateSales(List<Services.SalesItems.SalesItems> SalesItem)
        {
            SalesRepository IRepository = new SalesRepository();
            return IRepository.UpdateSales(SalesItem);
        }

        public int UpdateIssueReturn(List<Core.Domain.IssueReturn.IssueReturnDetail> IssueReturnItem, Core.Domain.IssueReturn.IssueReturn IssueReturnHeader)
        {
            SalesRepository IRepository = new SalesRepository();
            return IRepository.UpdateIssueReturn(IssueReturnItem, IssueReturnHeader);
        }

        
    }
}
