using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Services.Category
{
    public class CategoryService : ICategory
    {
        private CategoryRepository IRepository;
        public List<Core.Domain.Category> GetList()
        {
            IRepository = new CategoryRepository();
            return IRepository.GetList();
        }
        public void InsertCategory(Core.Domain.Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("group");
            }
            IRepository = new CategoryRepository();
            IRepository.Insert(model);
        }

        public Core.Domain.Category Find(int id)
        {
            IRepository = new CategoryRepository();
            var obj = IRepository.Find(id);
            return obj;
        }
        public void Update(Core.Domain.Category model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            IRepository = new CategoryRepository();
            IRepository.Update(model);
        }

    }
}
