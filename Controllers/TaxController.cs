

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers{
    public class TaxController:BaseController{
        
        public TaxController(InvoiceContext context):base(context){

        }
         [Route("api/tax/create"), HttpPost]
        public BaseResponse addTaxes ([FromBody] TaxViewReq tax)
        {
            return new BLL.Tax(_db).addTax(tax);
        }
        [Route("api/tax/gettaxes"), HttpPost]
        public List<TaxViewRes> getTaxes ([FromBody] int id)
        {
            return new BLL.Tax(_db).getAllTax(id);
        }
        [Route("api/tax/edittaxes"), HttpPost]
        public BaseResponse editTaxes ([FromBody] TaxViewReq tax)
        {
            return new BLL.Tax(_db).editTax(tax);
        }
        [Route("api/tax/deletetaxes"), HttpPost]
        public BaseResponse deleteTaxes ([FromBody] int id)
        {
            return new BLL.Tax(_db).deleteTax(id);
        }
    }
}