using htcustomer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<TblCategory> GetAllCategories();
        bool Edit(TblCategory category);
        bool Add(TblCategory newCategory);
        bool Delete(int categoryId);

    }
}
