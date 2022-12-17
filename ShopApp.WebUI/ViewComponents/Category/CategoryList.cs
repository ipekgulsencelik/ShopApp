using Microsoft.AspNetCore.Mvc;
using ShopApp.BusinessLayer.Concrete;
using ShopApp.DataAccessLayer.EntityFramework;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.ViewComponents.Category
{
    public class CategoryList : ViewComponent
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());

        public IViewComponentResult Invoke()
        {
            var values = categoryManager.TGetList();

            CategoryListViewModel categoryList = new CategoryListViewModel()
            {
                SelectedCategory = RouteData.Values["category"]?.ToString(),
                Categories = values
            };

            return View(categoryList);
        }
    }
}
