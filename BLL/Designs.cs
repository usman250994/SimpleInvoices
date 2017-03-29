using System.Collections.Generic;
using SimpleInvoices;
using SimpleInvoices.ViewModels;

namespace invoicingSystem.BLL
{
    public class Designs
    {
        public readonly InvoiceContext _db;
        public  Designs(InvoiceContext context)
        {
            _db=context;
        }

        
    }
}