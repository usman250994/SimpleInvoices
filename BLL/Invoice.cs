using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL{
    public class Invoice {
        public readonly InvoiceContext _db;
        double total=0;
        public Invoice (InvoiceContext context){
            _db=context;
        }

        

        public List<UserDropdownRes> getDropdownRes(string user){
            var db=_db;
            List<UserDropdownRes> dropdownRes=new List<UserDropdownRes>();
            if(user.ToLower().Equals("customer")){
                var usersList=db.customer.Where(c=>c.enable==true).ToList();
                foreach (var entity in usersList){
                    dropdownRes.Add(new UserDropdownRes{
                        id=entity.Id,
                        name=entity.name
                    });
                }
                
            }
            else if(user.ToLower().Equals("biller")){
                 var usersList=db.biller.Where(c=>c.enable==true).ToList();
                foreach (var entity in usersList){
                    dropdownRes.Add(new UserDropdownRes{
                        id=entity.Id,
                        name=entity.name
                    });
                }
            }
            else if(user.ToLower().Equals("product")){
                 var usersList=db.products.Where(c=>c.enable==true).ToList();
                 
                foreach (var entity in usersList){
                    dropdownRes.Add(new UserDropdownRes{
                        id=entity.Id,
                        name=entity.name
                    });
                }
            }
            return dropdownRes;
            
        }

        public BaseResponse createInvoice(InvoiceReq invoice){
            BaseResponse toReturn=new BaseResponse();
           var db=_db;
           
               Ledgers ledger=new Ledgers();
           ledger.biller=db.biller.Where(c=>c.Id.Equals(invoice.biller)).FirstOrDefault();
           ledger.customer=db.customer.Where(c=>c.Id.Equals(invoice.customer)).FirstOrDefault();
          // ledger.dueDate=invoice.dueDate;
           ledger.createdDate=DateTime.Now;
           ledger.deliveryDate=invoice.date;
           ledger.enable=true;
           ledger.ledgerDetails=getLedgerDetails(ledger,invoice,db);
           ledger.amount=total;
           ledger.balance=total;
           ledger.invoiceName="Invoice-"+ledger.Id;
           ledger.note=invoice.note;
           db.SaveChanges();
           ledger.invoiceName="Invoice-"+ledger.Id;
           if(db.SaveChanges()>0){
               toReturn.status=1;
               toReturn.developerMessage="Invoice Created Successfully";
           }
            return toReturn;
        }

        public List<LedgerDetails> getLedgerDetails(Ledgers ledger,InvoiceReq invoice,InvoiceContext db){
            
            List<LedgerDetails> toReturn=new List<LedgerDetails>();
            foreach(var item in invoice.products){
                Taxes tax=new Taxes();        
                if(item.taxId>0)
                {
                    tax=db.taxes.Where(c=>c.Id.Equals(item.taxId)).FirstOrDefault();
                }
                else{
                    tax=null;
                }
                 
                 List<Design> designList=new List<Design>();
                if(item.id==0){ 
                    var result= new  BLL.Products(db).addProduct(item);  
                    foreach(var design in item.designs)
                    {
                        Design _design=new Design();
                        
                            _design.name=design.name;
                            _design.color=design.color;
                            _design.cut=design.cut;
                            _design.fabric=design.fabric;
                            _design.note=design.note;
                            _design.enable=true;
                        designList.Add(_design);
                    }
                    item.id=result.status;
                    var product=db.products.Where(c=>c.Id==item.id).FirstOrDefault();
                    toReturn.Add(new LedgerDetails {
                    ledgers=ledger,
                    product=product,
                    quantity=item.quantity,
                    designs=designList,
                    tax=tax
                });
                double taxAmount=0;
                    if(item.taxId>0){
                    taxAmount=(item.quantity*item.price)*(tax.percent/100);
                    }
                total+=(item.quantity*product.price)+taxAmount;
                
                product.ledgerDetails=toReturn;
                }
                else
                {
                    var product = db.products.Where(c => c.Id == item.id).FirstOrDefault();
                    foreach(var design in item.designs)
                    {
                        Design _design=new Design();
                        
                            _design.name=design.name;
                            _design.color=design.color;
                            _design.cut=design.cut;
                            _design.fabric=design.fabric;
                            _design.note=design.note;
                        designList.Add(_design);
                    }
                    toReturn.Add(new LedgerDetails
                    {
                        ledgers = ledger,
                        product = product,
                        quantity=item.quantity,
                        designs=designList,
                        tax=tax
                    });
                    product.ledgerDetails=toReturn;
                     double taxAmount=0;
                    if(item.taxId>0){
                    taxAmount=(item.quantity*item.price)*(tax.percent/100);
                    }
                    total+=(item.quantity*product.price)+taxAmount;
                }
            }
            db.ledgerDetails.AddRange(toReturn);
            return toReturn;
        }
        public List<InvoiceRes> getAllInvoice(int id){
          //  Utils.Email em=new Utils.Email();
           // em.sendEmail("","");
            List<InvoiceRes> toReturn=new List<InvoiceRes>();
            List<Ledgers> ledgers=new List<Ledgers>();
            var db=_db;
            if(id==0){
                 ledgers=db.ledgers.Include(c=>c.biller)
                 .Include(c=>c.customer)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.product)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.tax)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.designs)
                 .ToList();
            }
            else{
                ledgers=db.ledgers.Where(c=>c.Id.Equals(id)).Include(c=>c.biller)
                .Include(c=>c.customer)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.product)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.tax)
                 .Include(c=>c.ledgerDetails).ThenInclude(c=>c.designs)
                 .ToList();
            }
            foreach(var entity in ledgers){
                InvoiceRes res=new InvoiceRes();
                res.id=entity.Id;
                res.billerId=entity.biller.Id;
                res.billerName=entity.biller.name;
                res.customerId=entity.customer.Id;
                res.custName=entity.customer.name;
                res.price=entity.amount;
                res.balance=entity.balance;
                res.delivery=entity.deliveryDate;
                res.date=entity.createdDate;
                
                foreach(var product in entity.ledgerDetails){
                    res.product.AddRange(new BLL.Products(db).getProductsWithDesigns(product.productId,product.quantity,product.tax, entity.Id));
                }
                res.note=entity.note;
                toReturn.Add(res);
            }
            return toReturn;
        }
        public List<ProductViewRes> getAllproduct(Ledgers legers){
            List<ProductViewRes> toReturn =new List<ProductViewRes>();
            foreach(var entity in legers.ledgerDetails){

            }
            return toReturn;
        }

        public BaseResponse deleteInvoice(int id){
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            var invoice=db.ledgers.Where(c=>c.Id.Equals(id)).Include(c=>c.ledgerDetails).FirstOrDefault();
            invoice.enable=false;
            if(db.SaveChanges()>0){
                toReturn.status=1;
                toReturn.developerMessage="invoice deleted Successfully";
            }
            return toReturn;
            
        }

    }
}