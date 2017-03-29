using System;
using System.Collections.Generic;

namespace SimpleInvoices.ViewModels{
    public class ProductViewRes{
        public ProductViewRes(){
            designs=new List<DesignViewReq>();
            customField=new List<CustomFieldRes>();
        }
        public double quantity{get;set;}
        public int id {get;set;}
        public string name {get;set;}
        public string color {get;set;}
        public string note {get;set;}
        public string description {get;set;}
        public double price {get;set;}
        public double taxPercent{get;set;}
        public string enable {get;set;}
        public DateTime createdOn {get;set;}
        public List<CustomFieldRes> customField {get;set;}
        public List<DesignViewReq> designs{get;set;}
    }
}