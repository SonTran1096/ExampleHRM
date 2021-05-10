using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.WebApp.Models;
using HRM.WebApp.IServices;
using HRM.WebApp.Services;
using HRM.WebApi.Model;
using HRM.WebApp.Models.ViewModel;

namespace HRM.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public ActionResult Index(string keyword)
        {
            if(keyword == null)
            {
                keyword = "";
            }
            else
            {
                ViewBag.Keyword = keyword;
            }
            var listProduct = _productService.GetListProduct(keyword);
            return View(listProduct);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(viewModel);

                var result = _productService.InsertProduct(viewModel);
                if(result.ResponseCode == 1)
                {
                    TempData["result"] = "Thêm mới sản phẩm thành công";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Thêm mới sản phẩm thất bại");
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _productService.GetProductById(id);
            return View(result);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            try
            {
                var result = _productService.UpdateProduct(viewModel);
                if(result.ResponseCode == 1)
                {
                    TempData["result"] = "Cập nhật sản phẩm thành công";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {

            return View(new ProductViewModel()
            {
                ProductId = id
            });
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductViewModel viewModel)
        {
            try
            {
                var result = _productService.DeleteProduct(viewModel.ProductId);
                if(result.ResponseCode == 1)
                {
                    TempData["result"] = "Xóa sản phẩm thành công";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Xóa sản phẩm thất bại");
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }
    }
}
