using System.Collections.Generic;

namespace SimpleInvoices.ViewModels{
    public class BillerViewRes{
        public BillerViewRes()
        {
            customfields=new List<CustomFieldRes>();
        }
         public int id {get;set;}
        public string name {get;set;}
        public string address {get;set;}
        public string contact {get;set;}
        public string email {get;set;}
        public string city {get;set;}
        public List<CustomFieldRes> customfields{get;set;}
    }
}