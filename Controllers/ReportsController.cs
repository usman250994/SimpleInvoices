using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers
{
    public class ReportsController:BaseController
    {
        public ReportsController(InvoiceContext context):base(context){

        }

        [Route("api/reports/totalsales"), HttpPost]
        public ToSalesRes totalSales ()
        {
           return new BLL.Reports(_db).totalSales();
        }
        [Route("api/reports/totalsalescustomerwise"), HttpPost]
        public TotalSalesCustomerRes totalSalesCustomerWise ()
        {
           return new BLL.Reports(_db).totalCustomerSales();
        }
        [Route("api/reports/totaltaxes"), HttpPost]
        public double totalTaxes ()
        {
           return new BLL.Reports(_db).totalTaxesReport();
        }
        [Route("api/reports/totalproduct"), HttpPost]
        public List<productSoldRes> totalProduct ()
        {
           return new BLL.Reports(_db).productSold();
        }
        [Route("api/reports/customerproduct"), HttpPost]
        public List<productCustomerRes> customerWiseProduct ()
        {
           return new BLL.Reports(_db).productCustomer();
        }
        [Route("api/reports/totalsalesbiller"), HttpPost]
        public BillerSalesRes totalSalesBiller ()
        {
           return new BLL.Reports(_db).billerSales();
        }
   /* 
     [Route("api/reports/totalsales"), HttpPost]
        public InvoiceRes totalSales ()
        {
           // return new BLL.Biller(_db).addBiller(biller);
        }
        */
    }
}