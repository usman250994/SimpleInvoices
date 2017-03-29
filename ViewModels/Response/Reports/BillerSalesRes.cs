using System.Collections.Generic;

namespace SimpleInvoices.ViewModels
{
    public class BillerSalesRes
    {

        public BillerSalesRes()
        {
            billerAmount = new List<BillerNameAmount>();
        }
        public List<BillerNameAmount> billerAmount { get; set; }
        public double total { get; set; }

    }

    public class BillerNameAmount
    {
        public string name { get; set; }
        public double amount { get; set; }
    }
}