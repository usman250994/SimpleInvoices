using System.Collections.Generic;

namespace SimpleInvoices.ViewModels{
    public class ProductRes{
        public ProductRes(){
            designs=new List<DesignViewReq>();
        }
        public int id {get;set;}
        public string name {get;set;}
        public string color {get;set;}
        public string price {get;set;}
        public string note {get;set;}
        public string description {get;set;}
        public List<DesignViewReq> designs {get;set;}
        public string enable {get;set;}

    }
}