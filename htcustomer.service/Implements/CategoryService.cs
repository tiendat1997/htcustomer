using htcustomer.entity;
using htcustomer.repository;
using htcustomer.service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<TblCategory> categoryRepo;

        public CategoryService(IRepository<TblCategory> _categoryRepo)
        {
            this.categoryRepo = _categoryRepo;
        }

        public bool Add(TblCategory newCategory)
        {
            if (newCategory != null)
            {
                newCategory.Disable = false;
                categoryRepo.Insert(newCategory);
                categoryRepo.Save();
                return true;
            }
            return false;
        }

        public bool Delete(int categoryId)
        {
            if (categoryId > 0)
            {
                var category = categoryRepo.GetByID(categoryId);
                if (category != null)
                {
                    category.Disable = true;
                    categoryRepo.Edit(category);
                    categoryRepo.Save();
                }
                return true;
            }
            return false;
        }

        public bool Edit(TblCategory category)
        {
            if (category != null)
            {
                if (category.CategoryID > 0)
                {
                    category.Disable = false;
                    categoryRepo.Edit(category);
                    categoryRepo.Save();
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<TblCategory> GetAllCategories()
        {
            return categoryRepo.Gets().Where(c => !c.Disable.Value);
        }
    }
}
