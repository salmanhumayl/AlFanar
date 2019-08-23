using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.OpeningBalance;
using Core.Domain.StockAdjustment;

namespace Services.Building
{
    public class BuildingService : IBuilding
    {
        private BuildingRepositary IRepository;

       
        public Core.Domain.Building Find(int id)
        {
            IRepository = new BuildingRepositary();
            var obj = IRepository.Find(id);
            return obj;
        }

      

     
        public List<Core.Domain.Building> GetList()
        {
            IRepository = new BuildingRepositary();
            return IRepository.GetList();
        }

        public List<BuildingViewModel> GetListView()
        {
            IRepository = new BuildingRepositary();
            return IRepository.GetListView();
        }

        public int InsertBuilding(Core.Domain.Building model, ref string ErrorMsg)
        {
            IRepository = new BuildingRepositary();
            return IRepository.InsertBuilding(model, ref ErrorMsg);

        }

        public void Update(Core.Domain.Building model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            IRepository = new BuildingRepositary();
            IRepository.Update(model);
        }

       
    }
}
