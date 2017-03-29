using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SimpleInvoices.Controllers{
    public class ProductController:BaseController{
        public ProductController(InvoiceContext context):base(context)
        {
        }
        [Route("api/product/addproduct"), HttpPost]
        public ViewModels.BaseResponse addProduct([FromBody] ViewModels.ProductViewReq product){
            return new BLL.Products(_db).addProduct(product);
        }
        [Route("api/product/editproduct"), HttpPost]
        public ViewModels.BaseResponse editproduct([FromBody] ViewModels.ProductViewReq product){
            return new BLL.Products(_db).editProduct(product);
        }
        [Route("api/product/deleteproduct"), HttpPost]
        public ViewModels.BaseResponse deleteproduct([FromBody] int product){
            return new BLL.Products(_db).deleteProduct(product);
        }
        [Route("api/product/getproduct"), HttpPost]
        public List<ViewModels.ProductViewRes> getproduct([FromBody] int product){
            return new BLL.Products(_db).getProducts(product);
        }
    }
}