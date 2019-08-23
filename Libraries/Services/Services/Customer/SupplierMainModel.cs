using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Services.Customer
{
    class SupplierMainModel
    {
    }

    public class CustomerDetailViewModel
    {
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string FlatNo { get; set; }

        public string BuildingNo { get; set; }

        public decimal Deposit { get; set; }
        public string TankName { get; set; }
        public int TankID { get; set; }
    }

    public class CustomerViewModel
    {
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string FlatNo { get; set; }

        public string BuildingNo { get; set; }

        public string TankName { get; set; }

        public decimal Deposit { get; set; }
    }

    public class LoginSupplierModel
    {
        
        public string License { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}

