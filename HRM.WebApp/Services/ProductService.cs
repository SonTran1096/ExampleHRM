using HRM.WebApi.DataAccess;
using HRM.WebApi.IDataAccess;
using HRM.WebApi.Model;
using HRM.WebApp.IServices;
using HRM.WebApp.Models;
using HRM.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductModel = HRM.WebApi.Model.ProductModel;

namespace HRM.WebApp.Services
{
    public class ProductService : IProductService
    {
        private IProductDataAccess _productDA;
        private IProductDataAccess ProductDA
        {
            get { return _productDA ?? (_productDA = new ProductDataAccess()); }
        }

        public IEnumerable<ProductViewModel> GetListProduct(string KeyWord)
        {
            try
            {
                var listProduct = ProductDA.GetListProduct(KeyWord);
                return listProduct.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    PromoPrice = p.PromoPrice,
                    Status = p.Status
                }).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public ProductViewModel GetProductById(int productId)
        {
            try
            {
                var model = ProductDA.GetProductWithId(productId);
                return new ProductViewModel
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    PromoPrice = model.PromoPrice,
                    Status = model.Status
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ResponseResult InsertProduct(ProductViewModel viewModel)
        {
            ResponseResult result = null;
            try
            {
                var dbModel = new ProductModel
                {
                    ProductName = viewModel.ProductName,
                    Quantity = viewModel.Quantity,
                    Price = viewModel.Price,
                    PromoPrice = viewModel.PromoPrice,
                    Status = viewModel.Status
                };
                return ProductDA.InsertProduct(dbModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public ResponseResult UpdateProduct(ProductViewModel viewModel)
        {
            ResponseResult result = null;
            try
            {
                var dbModel = new ProductModel
                {
                    ProductId = viewModel.ProductId,
                    ProductName = viewModel.ProductName,
                    Quantity = viewModel.Quantity,
                    Price = viewModel.Price,
                    PromoPrice = viewModel.PromoPrice,
                    Status = viewModel.Status
                };
                return ProductDA.UpdateProduct(dbModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResponseResult DeleteProduct(int productId)
        {
            ResponseResult result = null;
            try
            {
                result = ProductDA.DeleteProduct(productId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
