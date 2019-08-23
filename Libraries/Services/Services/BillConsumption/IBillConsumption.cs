using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BillConsumption
{
    public interface IBillConsumption
    {
        List<Services.BillConsumption.BillConsumptionViewModel> GetListView();
        LastReadingDetial GetLastReadingDetail(string ContractNo);

        decimal GetBillOutstanding(string ContractNo);

        int Insert(Services.BillConsumption.BillConsumptionAddViewModel model, ref string ErrorMsg);
    }
}
