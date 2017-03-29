using System;
using System.Collections.Generic;
namespace SimpleInvoices.ViewModels{
    public class InvoiceReq{
        public InvoiceReq(){
            products=new List<ProductViewReq>();
        }
        public DateTime date{get;set;}
        public int  customer{get;set;}
        public int biller {get;set;}
        public List<ProductViewReq> products {get;set;}
        public string note{get;set;}

    }
}