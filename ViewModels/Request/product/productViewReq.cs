using System.Collections.Generic;

namespace SimpleInvoices.ViewModels{
    public class ProductViewReq{
        public ProductViewReq()
        {
            customField =new List<CustomFieldRes>();
            designs=new List<DesignViewReq>();
        }
        public int id {get;set;}
        public int taxId {get;set;}
        public double quantity{get;set;}
        public string name {get;set;}
        public string color {get;set;}
        public string note {get;set;}
        public string description {get;set;}
        public double price {get;set;}
        public List<CustomFieldRes> customField {get;set;}
        public List<DesignViewReq> designs{get;set;}
        
    }
}