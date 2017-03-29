using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL
{
    public class Reports
    {
        private readonly InvoiceContext _db;
        public Reports(InvoiceContext context)
        {
            _db = context;
        }

        public ToSalesRes totalSales()
        {
            int quantity = 0;
            double total = 0;
            ToSalesRes toReturn = new ToSalesRes();
            var db = _db;
            var invoice = db.ledgers.ToList();
            quantity = invoice.Count;
            foreach (var item in invoice)
            {
                total += item.amount;
            }
            toReturn.invoiceName = "Invoice";
            toReturn.numberOfTimes = quantity;
            toReturn.totalSales = total;
            return toReturn;
        }


        public TotalSalesCustomerRes totalCustomerSales()
        {
            TotalSalesCustomerRes toReturn = new TotalSalesCustomerRes();

            var db = _db;
            var invoices = db.ledgers.Include(c => c.customer).GroupBy(c => c.customer).ToList();
            foreach (var item in invoices)
            {
                CustomerAmount customers = new CustomerAmount();
                foreach (var entity in item)
                {
                    customers.name = entity.customer.name;
                    customers.amount += entity.amount;
                }

                toReturn.customer.Add(customers);
                toReturn.total += customers.amount;
            }
            return toReturn;
        }

        public List<productSoldRes> productSold()
        {
            List<productSoldRes> toReturn = new List<productSoldRes>();
            var db = _db;
            var legderDetails = db.ledgerDetails.Include(p => p.product).GroupBy(p => p.product);
            foreach (var entity in legderDetails)
            {
                toReturn.Add(new productSoldRes
                {
                    quantity = entity.Count(),
                    productName = entity.FirstOrDefault().product.name

                });
            }
            return toReturn;
        }

        public List<productCustomerRes> productCustomer()
        {
            List<productCustomerRes> toReturn = new List<productCustomerRes>();
            var db = _db;
            productCustomerRes res = new productCustomerRes();
            var ledger = db.ledgers.Include(c => c.customer)
            .Include(c => c.ledgerDetails)
            .ThenInclude(c => c.product)
            .GroupBy(c => c.customer);
            foreach (var items in ledger)
            {
                foreach (var entity in items)
                {
                    res.customerName = entity.customer.name;
                    res.quantity = entity.ledgerDetails.Count;
                    foreach (var iteration in entity.ledgerDetails)
                    {
                        res.productName = iteration.product.name;
                        toReturn.Add(res);
                    }
                }
            }
            return toReturn;
        }
       
        public double totalTaxesReport()
        {

            double totalAmount = 0;
            double totalTax = 0;
            double toReturn = 0;
            var db = _db;
            var invoices = db.ledgers.Include(c => c.ledgerDetails).ThenInclude(c => c.product).ToList();
            foreach (var item in invoices)
            {
                totalAmount = 0;
                totalTax = 0;
                foreach (var entity in item.ledgerDetails)
                {
                    totalAmount += entity.quantity * entity.product.price;
                }
                totalTax = (item.amount - totalAmount);
                toReturn += totalTax;
            }
            return toReturn;
        }
        public BillerSalesRes billerSales(){
            BillerSalesRes toReturn=new BillerSalesRes();
            var db=_db;
            var invoices=db.ledgers.Include(c=>c.biller).GroupBy(c=>c.biller);
             foreach (var item in invoices)
            {
                BillerNameAmount biller = new BillerNameAmount();
                foreach (var entity in item)
                {
                    biller.name = entity.biller.name;
                    biller.amount += entity.amount;
                }

                toReturn.billerAmount.Add(biller);
                toReturn.total += biller.amount;
            }

            return toReturn;
        }
    }
}