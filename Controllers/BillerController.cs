using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers{
    public class BillerController:BaseController{

        public BillerController(InvoiceContext context):base(context){
            
        }

        [Route("api/biller/create"), HttpPost]
        public BaseResponse addBiller ([FromBody] BillerViewReq biller)
        {
            return new BLL.Biller(_db).addBiller(biller);
        }
        [Route("api/biller/edit"), HttpPost]
        public BaseResponse editBiller ([FromBody] BillerViewReq biller)
        {
            return new BLL.Biller(_db).editBiller(biller);
        }
        [Route("api/biller/delete"), HttpPost]
        public BaseResponse deleteBiller ([FromBody] int biller)
        {
            return new BLL.Biller(_db).deleteBiller(biller);
        }
        [Route("api/biller/getBiller"), HttpPost]
        public List<BillerViewRes> getBiller ([FromBody] int biller)
        {
            return new BLL.Biller(_db).getBiller(biller);
        }

    }
}