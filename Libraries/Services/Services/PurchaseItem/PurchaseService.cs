using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PurchaseItem
{
    public class PurchaseService
    {
        public int UpdatePurchase(List<Core.Domain.Purchase.PurchaseDetail> PurchaseItem, Core.Domain.Purchase.Purchase PurchaseHeader)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.UpdatePurchase(PurchaseItem, PurchaseHeader);
        }

        public int UpdatePurchaseReturn(List<Core.Domain.PurchaseReturn.PurchaseReturnDetail> ReturnItem, Core.Domain.PurchaseReturn.PurchaseReturn ReturnHeader)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.UpdatePurchaseReturn(ReturnItem, ReturnHeader);
        }

        public int UpdatePurchaseReturnByInvoice(Services.PurchaseItem.PurchaseReturnViewModel model)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.UpdatePurchaseReturnByInvoice(model);
        }
        

        public List<PurchaseReturnItem> GetPurchaseDetail(string InvoiceNo)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.GetPurchaseDetail(InvoiceNo);
        }
        public PurchaseReturnHeader GetPurchaseHeader(string InvoiceNo)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.GetPurchaseHeader(InvoiceNo);
        }

        public int ValidateInvoice(string InvoiceNo)
        {
            PurchaseRepository IRepository = new PurchaseRepository();
            return IRepository.ValidateInvoice(InvoiceNo);
        }


    }
}
