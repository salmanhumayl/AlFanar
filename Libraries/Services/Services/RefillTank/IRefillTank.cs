using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.RefillTank
{
    public interface IRefillTank
    {
        List<Core.Domain.RefillTank> GetList();
        int Insert(Core.Domain.RefillTank model);

    }
}
