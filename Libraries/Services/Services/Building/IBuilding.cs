using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.Building
{
    public interface IBuilding
    {
        List<Core.Domain.Building> GetList();
        List<Services.Building.BuildingViewModel> GetListView();
        int InsertBuilding(Core.Domain.Building model, ref string ErrorMsg);
        void Update(Core.Domain.Building model);


        Core.Domain.Building Find(int id);

  
  
    }
}
