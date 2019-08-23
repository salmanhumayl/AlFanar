using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
namespace Services.Category
{
    public interface ICategory
    {
        List<Core.Domain.Category> GetList();
        void InsertCategory(Core.Domain.Category model);
        void Update(Core.Domain.Category model);


        Core.Domain.Category Find(int id);

    }
}
