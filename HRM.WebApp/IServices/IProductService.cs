using HRM.WebApi.Model;
using HRM.WebApp.Models;
using HRM.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductModel = HRM.WebApi.Model.ProductModel;

namespace HRM.WebApp.IServices
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetListProduct(string KeyWord);
        ProductViewModel GetProductById(int productId);
        ResponseResult InsertProduct(ProductViewModel viewModel);
        ResponseResult UpdateProduct(ProductViewModel viewModel);
        ResponseResult DeleteProduct(int productId);
    }
}
