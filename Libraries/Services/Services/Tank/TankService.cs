using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.Tank
{
    public class TankService : ITank
    {
        private TankRepository IRepository;
        public List<Core.Domain.Tank> GetList()
        {
            IRepository = new TankRepository();
            return IRepository.GetList();
        }

        public void InsertTank(Core.Domain.Tank model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("group");
            }
            IRepository = new TankRepository();
            IRepository.Insert(model);
        }

        public Core.Domain.Tank Find(int id)
        {
            IRepository = new TankRepository();
            var obj = IRepository.Find(id);
            return obj;
        }
        public void Update(Core.Domain.Tank model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            IRepository = new TankRepository();
            IRepository.Update(model);
        }

        public List<TankAsofConsumption> GetTotalConsumption()
        {
            IRepository = new TankRepository();
            return IRepository.GetTotalConsumption();
        }

      
    }
}
