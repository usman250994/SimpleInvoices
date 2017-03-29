using System;

namespace SimpleInvoices.ViewModels{
    public class PaymentReq
    {
        
        public int invoiceId{get;set;}
        public double amount {get;set;} 
        public DateTime date {get;set;}
        public int paymentTypeId {get;set;}
    }
}