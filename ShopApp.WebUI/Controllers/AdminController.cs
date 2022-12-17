using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.BusinessLayer.Concrete;
using ShopApp.BusinessLayer.ValidationRule;
using ShopApp.DataAccessLayer.EntityFramework;
using ShopApp.EntityLayer.Concrete;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EFProductDAL());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());

        public IActionResult Index()
        {
            var products = productManager.TGetList();

            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            ProductValidator productValidator = new ProductValidator();

            ValidationResult results = productValidator.Validate(product);

            if (results.IsValid)
            {
                productManager.TAdd(product);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productManager.GetByIDWithCategories((int)id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageURL,
                SelectedCategories = product.ProductCategories.Select(x => x.Category).ToList(),
                Categories = categoryManager.TGetList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            Product entity = productManager.TGetByID(model.Id);

            if (entity == null)
            {
                return NotFound();
            }

            ProductValidator productValidator = new ProductValidator();

            ValidationResult results = productValidator.Validate(entity);

            if (results.IsValid)
            {
                var product = productManager.TGetByID(model.Id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;

                if (file != null)
                {
                    product.ImageURL = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                productManager.Update(product, categoryIds);

                return RedirectToAction("Index");
            }

            ViewBag.Categories = categoryManager.TGetList();

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var product = productManager.TGetByID(productId);

            if (product != null)
            {
                productManager.TDelete(product);
            }

            return RedirectToAction("Index");
        }

        public IActionResult CategoryList()
        {
            var categories = categoryManager.TGetList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            CategoryValidator categoryValidator = new CategoryValidator();

            ValidationResult results = categoryValidator.Validate(category);

            if (results.IsValid)
            {
                categoryManager.TAdd(category);

                return RedirectToAction("CategoryList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryManager.GetByIDWithProducts((int)id);

            if (category == null)
            {
                return NotFound();
            }
            
            CategoryModel model = new CategoryModel()
            {
                Id = category.ID,
                Name = category.Name,
                Description = category.Description,
                Products = category.ProductCategories.Select(p => p.Product).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            var category = categoryManager.TGetByID(model.ID);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;
            category.Description = model.Description;

            categoryManager.TUpdate(category);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var category = categoryManager.TGetByID(categoryId);

            if (category != null)
            {
                categoryManager.TDelete(category);
            }

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            categoryManager.DeleteFromCategory(categoryId, productId);

            return Redirect("/Admin/EditCategory/" + categoryId);
        }
    }
}
