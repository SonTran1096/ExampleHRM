using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private IProductDataAccess _productService;
        //public ProductsController(IProductDataAccess productService)
        //{
        //    _productService = productService;
        //}
        //// GET: api/Products
        //[HttpGet]
        //public IEnumerable<ProductModel> Get()
        //{
        //    return _productService.Gets();
        //}

        //// GET api/Products/5
        //[HttpGet("{id}")]
        //public ProductModel Get(int id)
        //{
        //    return _productService.Get(id);
        //}

        //// POST api/Products
        //[HttpPost]
        //public ProductModel Post([FromBody] ProductModel product)
        //{
        //    if (ModelState.IsValid)
        //        return _productService.Save(product);
        //    return null;
        //}

        //// PUT api/Products/5
        //[HttpPut("{id}")]
        //public ProductModel Put(int id, [FromBody] ProductModel product)
        //{
        //    if (ModelState.IsValid)
        //        return _productService.Save(product);
        //    return null;
        //}

        //// DELETE api/Products/5
        //[HttpDelete("{id}")]
        //public string Delete(int id)
        //{
        //    return _productService.Delete(id);
        //}
    }
}
