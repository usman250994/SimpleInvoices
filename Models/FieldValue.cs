namespace SimpleInvoices{
    public class FieldValue:identity {
        public string value {get;set;}
        public Customer customer{get;set;}
        public Billers billers {get;set;}
        public product product {get;set;}
        
        public CustomFields customfields {get;set;}
      
       
    }
}