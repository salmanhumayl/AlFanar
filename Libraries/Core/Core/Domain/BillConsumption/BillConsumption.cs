using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class BillConsumption
    {
        public int id { get; set; }
        [Required]
        public string ContractNo { get; set; }
        public string InvoiceNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurrentReadingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurrentReadingPeriodFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurrentReadingPeriodTo { get; set; }
       
        
        public decimal CurrentReading { get; set; }
        [Required]
        public decimal Consumption { get; set; }
        [Required]
        public decimal Rate { get; set; }

        public decimal ? ServiceCharges { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }


        //  public BillPaymentDetail BillPayements { get; set; }

        // public BillConsumption()
        // {
        //   BillPayements = new BillPaymentDetail();
        // }

    }

   // public class BillPaymentDetail
   // {

     //   public int PaymentID { get; set; }
      //  public int BillID { get; set; }

       // public decimal BillAmount { get; set; }

        //public decimal PaidAmount { get; set; }
    //}


}
