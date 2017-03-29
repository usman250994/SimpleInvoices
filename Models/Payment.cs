using System;
using System.Collections.Generic;

namespace SimpleInvoices {
    public class Payment:identity {
        public double amount {get;set;}
        public DateTime createdDate{get;set;}
        public PaymentTypes paymentTypes {get;set;}
    }
}