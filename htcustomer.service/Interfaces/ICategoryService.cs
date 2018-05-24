using htcustomer.entity;
using htcustomer.service.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAllCategories();
        void Edit(CategoryViewModel category);
        void Add(CategoryViewModel newCategory);
        void Delete(int categoryId);

    }
}
