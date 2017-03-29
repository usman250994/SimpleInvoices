using System;
using System.Collections.Generic;

namespace SimpleInvoices {
    public class Ledgers:identity {
        public Ledgers(){
            payment=new List<Payment>();
            ledgerDetails=new List<LedgerDetails>();
        }
        public string invoiceName{get;set;}
        public DateTime createdDate{get;set;}
        public DateTime deliveryDate{get;set;}
        public DateTime dueDate {get;set;}
        public double amount {get;set;}
        public double balance {get;set;}
        public Customer customer{get;set;}
        public Billers biller{get;set;}
        public List<LedgerDetails> ledgerDetails {get;set;} 
        public List<Payment> payment {get;set;}
        public string note {get;set;}
        
    }
}