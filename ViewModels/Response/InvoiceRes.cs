using System;
using System.Collections.Generic;
using SimpleInvoices.ViewModels;
namespace SimpleInvoices{
    public class InvoiceRes{
        public InvoiceRes(){
            product=new List<ProductViewRes>();
        }
        public int id {get;set;}
        public int customerId {get;set;}
        public string custName{get;set;}
        public int billerId{get;set;}
        public string billerName {get;set;}
        public List<ProductViewRes> product {get;set;}
        public double price {get;set;}
        public string note {get;set;}
        public DateTime delivery{get;set;}
        public DateTime date{get;set;}
        public double balance{get;set;}
    }
}