using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.BillConsumption
{
    public class BillConsumptionService : IBillConsumption
    {
        private BillConsumptionRepository IRepository;

        public decimal GetBillOutstanding(string ContractNo)
        {
            IRepository = new BillConsumptionRepository();
            return IRepository.GetBillOutstanding(ContractNo);
        }

        public LastReadingDetial GetLastReadingDetail(string ContractNo)
        {
            IRepository = new BillConsumptionRepository();
            return IRepository.GetLastReadingDetail(ContractNo);
        }

        public List<BillConsumptionViewModel> GetListView()
        {
            IRepository = new BillConsumptionRepository();
            return IRepository.GetListView();
        }

        public int Insert(Services.BillConsumption.BillConsumptionAddViewModel model, ref string ErrorMsg)
        {
            IRepository = new BillConsumptionRepository();
            return IRepository.Insert(model,ref ErrorMsg);

        }
    }
}
