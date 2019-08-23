using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.RefillTank
{
    
    public class RefillTankService : IRefillTank
    {
        private RefillTankRepository IRepository;
        public List<Core.Domain.RefillTank> GetList()
        {
            IRepository = new RefillTankRepository();
            return IRepository.GetList();
        }

        public int Insert(Core.Domain.RefillTank model)
        {
            IRepository = new RefillTankRepository();
            return IRepository.Insert(model);
        }
    }
}
