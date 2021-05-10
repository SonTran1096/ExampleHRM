using HRM.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.WebApi.IDataAccess
{
    public interface IProductDataAccess
    {
        IEnumerable<ProductModel> GetListProduct(string keyword);
        ProductModel GetProductWithId(int productId);
        ResponseResult InsertProduct(ProductModel model);
        ResponseResult UpdateProduct(ProductModel model);
        ResponseResult DeleteProduct(int productId);
    }
}
