using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleInvoices {

public class product:identity {
    public product(){
        customFields=new List<CustomFields>();
        ledgerDetails=new List<LedgerDetails>();
    }
    public string name {get;set;}
    public string color {get;set;}
    public double price {get;set;}
    public string note {get;set;}
    public string description {get;set;}
    public double unitPrice{get;set;}
    public List<CustomFields> customFields {get;set;}
    public DateTime createdOn {get;set;}
    public List<LedgerDetails> ledgerDetails{get;set;}
    
    }
}