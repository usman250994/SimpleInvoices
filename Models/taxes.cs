using System.Collections.Generic;

namespace SimpleInvoices {
    public class Taxes:identity {
        public string name {get;set;}
        public double percent {get;set;}
    }
}