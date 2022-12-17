using Microsoft.AspNetCore.Mvc;
using ShopApp.BusinessLayer.Concrete;
using ShopApp.DataAccessLayer.EntityFramework;
using ShopApp.EntityLayer.Concrete;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductDAL());

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = productManager.GetProductDetails((int)id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDetailsModel productDetails = new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories.Select(x => x.Category).ToList()
            };

            return View(productDetails);
        }

        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 3;

            ProductListModel productList = new ProductListModel()
            {
                Products = productManager.GetProductsByCategory(category, page, pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = productManager.GetCountByCategory(category),
                    CurrentCategory = category
                }
            };

            return View(productList);
        }
    }
}
