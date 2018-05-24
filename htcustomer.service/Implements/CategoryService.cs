using htcustomer.entity;
using htcustomer.repository;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel.Category;
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

        public void Add(CategoryViewModel category)
        {
            try
            {
                categoryRepo.Insert(new TblCategory { CategoryID = category.CategoryID, Name = category.Name, Disable = false });
                categoryRepo.Save();
            }catch(Exception ex)
            {

            }            
        }

        public void Delete(int categoryId)
        {
            try
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
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Edit(CategoryViewModel category)
        {
            try
            {
                categoryRepo.Edit(new TblCategory { CategoryID = category.CategoryID, Name = category.Name });
                categoryRepo.Save();
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return categoryRepo.Gets()
                .Where(c => c.Disable != true)
                .Select(c => new CategoryViewModel
                {
                    CategoryID = c.CategoryID,
                    Name = c.Name
                });
        }
    }
}
