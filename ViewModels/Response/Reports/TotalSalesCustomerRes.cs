using System.Collections.Generic;

namespace SimpleInvoices.ViewModels
{
    public class TotalSalesCustomerRes
    {
        public TotalSalesCustomerRes(){
            customer=new List<CustomerAmount>();
        }
         public List<CustomerAmount> customer {get;set;}
         public double total {get;set;}
    }
    public class CustomerAmount {
        public string name {get;set;}
        public double amount {get;set;}
    }
}