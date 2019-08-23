using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Quotation
{
    public interface IQuotation
    {
        void GenerateQuotation(List<Services.Purchase_Requisition.PRDetail> model);

        bool IsQuationGenerated(int PRID);
        List<QuotationItemDetail> GetQuotationItemDetailPRSupplierWise(int PRID);

        List<QuotationItemDetail> GetQuotationDetail(int QutationID);


        List<GeneratedQuatations> GetQuotations(int PRID);

        List<QuaotedSupplier> GetSupplier(int PRID);
        List<QuaotedSupplierRates> GetSupplierQuoatedRate(int PRID);

        List<QuaotedTotal> GetSupplierQuoatedTotal(int PRID);
        
    }
}
