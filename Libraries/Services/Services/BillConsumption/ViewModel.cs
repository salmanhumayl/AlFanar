using Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BillConsumption
{
    class ViewModel
    {
    }


    public class BillConsumptionAddViewModel
    {
        public Core.Domain.BillConsumption BillHeader { get; set; }

        public CustomerDetailViewModel CustomerDetail { get; set; }

        public LastReadingDetial ReadingDetail { get; set; }

        public decimal CustomerOutStanding { get; set; }
        public BillConsumptionAddViewModel()
        {
    //        BillHeader = new Core.Domain.BillConsumption();

        }
    }

    public class LastReadingDetial
    {
        public DateTime CurrentReadingPeriodTo { get; set; }
        public int CurrentReading { get; set; }
    }

    public class BillConsumptionViewModel
    {
        public int id { get; set; }
        public string ContractNo { get; set; }
        public string InvoiceNo { get; set; }

        public DateTime CurrentReadingPeriodFrom { get; set; }
        public DateTime CurrentReadingPeriodTo { get; set; }
        public DateTime CurrentReadingDate { get; set; }
        public decimal PreviousReading { get; set; }
        public decimal CurrentReading { get; set; }

        public decimal Consumption { get; set; }

        public decimal Rate { get; set; }

        public decimal ServiceCharges { get; set; }

        public string CustomerName { get; set; }
        public string BuildingNo { get; set; }
        public string  FlatNo { get; set; }
    }
}
