﻿using htcustomer.entity;
using htcustomer.service.Interfaces;
using htcustomer.service.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace htcustomer.web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }

        // GET: Category
        public ActionResult Index()
        {
            var model = categoryService.GetAllCategories();
            return View(model);
        }

        public ActionResult Add(CategoryViewModel category)
        {
            categoryService.Add(category);
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int categoryId)
        {
            categoryService.Delete(categoryId);
            return Json("Success");
        }        
        public JsonResult Update(CategoryViewModel category)
        {
            categoryService.Edit(category);
            return Json("Success");
        }

    }
}