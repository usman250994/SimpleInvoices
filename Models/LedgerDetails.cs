using System.Collections.Generic;

namespace SimpleInvoices
{
    public class LedgerDetails
    {
        public LedgerDetails(){
            designs=new List<Design>();
        }
        public int ledgersId {get;set;}
        public Ledgers ledgers{get;set;}
        public int productId {get;set;}
        public product product {get;set;}
        public double quantity{get;set;}
        public Taxes tax {get;set;}
        public List<Design> designs {get;set;}
    }
}