using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers
{
    public class InvoiceController:BaseController{
        public InvoiceController(InvoiceContext context):base(context){

        }
        [Route("api/invoice/createinvoice"),HttpPost]
        public BaseResponse createInvoice([FromBody] InvoiceReq invoice)
        {
            return new BLL.Invoice(_db).createInvoice(invoice);
        } 
        [Route("api/invoice/getinvoice"),HttpPost]
        public List<InvoiceRes> getInvoice([FromBody] int id){
            return new BLL.Invoice(_db).getAllInvoice(id);
        }
        [Route("api/invoice/deleteinvoice"),HttpPost]
        public BaseResponse deleteInvoice([FromBody] int id){
            return new BLL.Invoice(_db).deleteInvoice(id);
        }
        [Route("api/invoice/populatedropdown"),HttpPost]
        public List<UserDropdownRes> getDropDown([FromBody] String name)
        {
            return new BLL.Invoice(_db).getDropdownRes(name);
        } 
    }
}