using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Purchase_Requisition;

namespace Services.Quotation
{
    public class QuotationService : IQuotation
    {
        private QuotationRepository IRepository;
        public void GenerateQuotation(List<PRDetail> model)
        {
            IRepository = new QuotationRepository();
            IRepository.GenerateQuotation(model);

        }

        public List<QuotationItemDetail> GetQuotationDetail(int QutationID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetQuotationDetail(QutationID);
            return obj;
        }

        public List<QuotationItemDetail> GetQuotationItemDetailPRSupplierWise(int PRID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetQuotationItemDetailPRSupplierWise(PRID);
            return obj;


        }

        public List<GeneratedQuatations> GetQuotations(int PRID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetQuotations(PRID);
            return obj;
        }

        public List<QuaotedSupplier> GetSupplier(int PRID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetSupplier(PRID);
            return obj;
        }

        public List<QuaotedSupplierRates> GetSupplierQuoatedRate(int PRID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetSupplierQuoatedRate(PRID);
            return obj;
        }

        public List<QuaotedTotal> GetSupplierQuoatedTotal(int PRID)
        {
            IRepository = new QuotationRepository();
            var obj = IRepository.GetSupplierQuoatedTotal(PRID);
            return obj;
        }

        public bool IsQuationGenerated(int PRID)
        {
            IRepository = new QuotationRepository();
            return IRepository.IsQuationGenerated(PRID);

        }
    }
}
