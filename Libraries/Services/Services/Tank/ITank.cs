using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
namespace Services.Tank
{
    public interface ITank
    {
        List<Core.Domain.Tank> GetList();

        List<TankAsofConsumption> GetTotalConsumption();
        void InsertTank(Core.Domain.Tank model);

        void Update(Core.Domain.Tank model);


        Core.Domain.Tank Find(int id);
    }
}
