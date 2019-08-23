using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.Customer
{
    public class CustomerService : ICustomer
    {
        private CustomerRepository IRepository;

       
        public Core.Domain.Customer Find(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerDetailViewModel GetCustomerDetail(string ContractNo)
        {
            IRepository = new CustomerRepository();
            return IRepository.GetCustomerDetail(ContractNo);
        }

        public List<Core.Domain.Customer> GetList()
        {
            throw new NotImplementedException();
        }

        public List<Customer.CustomerViewModel> GetListView()
        {
            IRepository = new CustomerRepository();
            return IRepository.GetListView();
        }

      
       
       
        public int Insert(Core.Domain.Customer model, ref string ErrorMsg)
        {
            IRepository = new CustomerRepository();
            return IRepository.Insert(model, ref ErrorMsg);
        }

        public void Update(Core.Domain.Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
