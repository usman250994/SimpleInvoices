using System;

namespace SimpleInvoices
{
    public class PaymentRes
    {
        public int id {get;set;}
        public int invoiceId{get;set;}
        public double amount  {get;set;}
        public string payType {get;set;}
        public DateTime payDate{get;set;}
        public string billerName{get;set;}
        public string customerName{get;set;}
    }
}