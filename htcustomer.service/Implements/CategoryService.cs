using htcustomer.entity;
using htcustomer.repository;
using htcustomer.repository.UnitOfWork;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TblCategory> categoryRepo;

        public CategoryService(IUnitOfWork unitOfWork, IRepository<TblCategory> _categoryRepo)
        {
            this.unitOfWork = unitOfWork;
            this.categoryRepo = _categoryRepo;
        }
        public void Add(CategoryViewModel category)
        {
            if (category == null) throw new ArgumentNullException("Null Agurment");
            categoryRepo.Insert(new TblCategory { Name = category.Name, Disable = false });
            unitOfWork.Save();
        }
        public void Delete(int categoryId)
        {
            var category = categoryRepo.GetByID(categoryId);
            if (category == null) throw new ArgumentNullException("Null Agurment");
            category.Disable = true;
            categoryRepo.Edit(category);
            unitOfWork.Save();
        }
        public void Edit(CategoryViewModel category)
        {
            if (category == null) throw new ArgumentNullException("Null Agurment");
            categoryRepo.Edit(new TblCategory { CategoryID = category.CategoryID, Name = category.Name });
            unitOfWork.Save();
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
