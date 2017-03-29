using Microsoft.AspNetCore.Mvc;

namespace SimpleInvoices.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly InvoiceContext _db;
        public BaseController (InvoiceContext context){
            _db=context;
        }

    }
}