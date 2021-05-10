using HRM.WebApi.Common;
using HRM.WebApi.Helpers;
using HRM.WebApi.IDataAccess;
using HRM.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.WebApi.DataAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        public IEnumerable<ProductModel> GetListProduct(string keyword)
        {
            var listProduct = new List<ProductModel>();

            List<SqlParameter> listParameter = new List<SqlParameter>();
            listParameter.Add(new SqlParameter("@KeyWord", keyword));

            listProduct = DBUtils.ExecuteSPList<ProductModel>("sp_getListProduct", listParameter);

            return listProduct;
        }
        public ProductModel GetProductWithId(int productId)
        {
            ProductModel model = null;
            try
            {
                var parameters = new List<SqlParameter> { new SqlParameter("@ProductId", productId) };
                model = DBUtils.ExecuteSP<ProductModel>("sp_getProductWithId", parameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return model;
        }

        public ResponseResult InsertProduct(ProductModel model)
        {
            ResponseResult result = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ProductName", model.ProductName));
                listParameter.Add(new SqlParameter("@Quantity", model.Quantity));
                listParameter.Add(new SqlParameter("@Price", model.Price));
                listParameter.Add(new SqlParameter("@PromoPrice", model.PromoPrice));
                listParameter.Add(new SqlParameter("@Status", model.Status));

                result = DBUtils.ExecuteSP<ResponseResult>("sp_InsertProduct", listParameter);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ResponseResult UpdateProduct(ProductModel model)
        {
            ResponseResult result = null;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@ProductId", model.ProductId));
                listParameter.Add(new SqlParameter("@ProductName", model.ProductName));
                listParameter.Add(new SqlParameter("@Quantity", model.Quantity));
                listParameter.Add(new SqlParameter("@Price", model.Price));
                listParameter.Add(new SqlParameter("@PromoPrice", model.PromoPrice));
                listParameter.Add(new SqlParameter("@Status", model.Status));

                result = DBUtils.ExecuteSP<ResponseResult>("sp_UpdateProduct", listParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ResponseResult DeleteProduct(int productId)
        {
            ResponseResult result = null;
            try
            {
                var parameters = new List<SqlParameter> { new SqlParameter("@ProductId", productId) };
                result = DBUtils.ExecuteSP<ResponseResult>("sp_DeleteProduct", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
