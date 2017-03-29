using System;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers
{
    public class loginController:BaseController
    {
        public loginController(InvoiceContext context):base(context){}

        [Route("api/login/biller"), HttpPost]
        public BaseResponse loginUser ([FromBody] loginReq req){
            Console.WriteLine(req.email+"    "+ req.password);
            return new BLL.Login(_db).loginUser(req);
        }
    }
}