using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers{

    public class CustomerController:BaseController{
        public CustomerController(InvoiceContext context):base(context){

        }
        

        [Route("api/customer/create"), HttpPost]
        public BaseResponse addCustomer ([FromBody] UserViewReq customer)
        {
            return new BLL.Customers(_db).addCustomer(customer);
        }
        [Route("api/customer/edit"), HttpPost]
        public BaseResponse editCustomer ([FromBody] UserViewReq customer)
        {
            return new BLL.Customers(_db).editCustomer(customer);
        }
        [Route("api/customer/delete"), HttpPost]
        public BaseResponse deleteCustomer ([FromBody] int customer)
        {
            return new BLL.Customers(_db).deleteCustomer(customer);
        }
       
        [Route("api/customer/getCustomers"), HttpPost]
        public List<UserViewRes> getCustomers ([FromBody] int customer)
        {
            return new BLL.Customers(_db).getCustomers(customer);
        }
        
    }
}