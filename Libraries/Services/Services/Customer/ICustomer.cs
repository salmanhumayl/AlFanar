using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;


namespace Services.Customer
{
    public interface ICustomer
    {
        List<Core.Domain.Customer> GetList();
        List<Services.Customer.CustomerViewModel> GetListView();
        int Insert(Core.Domain.Customer model, ref string ErrorMsg);
        void Update(Core.Domain.Customer model);

        CustomerDetailViewModel GetCustomerDetail(string ContractNo);

        Core.Domain.Customer Find(int id);




        
    }
}
