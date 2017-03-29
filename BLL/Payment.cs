using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices;
using SimpleInvoices.ViewModels;
namespace SimpleInvoices.BLL
{
    public class Payment
    {
        private readonly InvoiceContext _db;
        public Payment(InvoiceContext context)
        {
            _db = context;
        }
        public BaseResponse createPayment(PaymentReq req)
        {
            BaseResponse toReturn = new BaseResponse();
            var db = _db;
            var invoice = db.ledgers.Where(c => c.enable && c.Id.Equals(req.invoiceId)).FirstOrDefault();
            var payType = db.paymentTypes.Where(c => c.enable == true && c.Id.Equals(req.paymentTypeId)).FirstOrDefault();
            SimpleInvoices.Payment pay = new SimpleInvoices.Payment();
            pay.amount = req.amount;
            pay.paymentTypes = payType;
            pay.enable = true;
            pay.createdDate=DateTime.Now;
            db.payment.Add(pay);
            invoice.payment.Add(pay);
            db.SaveChanges();
            invoice.balance = invoice.balance - req.amount;
            db.SaveChanges();
            return toReturn;
        }

        public List<PaymentRes> getPayment(int id)
        {
            var db = _db;
            List<PaymentRes> toReturn = new List<PaymentRes>();
            List<SimpleInvoices.Payment> pay = new List<SimpleInvoices.Payment>();
            SimpleInvoices.Payment singlePay=new SimpleInvoices.Payment();
            List<Ledgers> invoice = new List<Ledgers>();
           
                invoice = db.ledgers.Where(c => c.enable == true).Include(c => c.payment).ThenInclude(c=>c.paymentTypes)
                .Include(c=>c.customer)
                .Include(c=>c.biller)
                .Include(c=>c.ledgerDetails).ThenInclude(c=>c.tax)
                .ToList();
            
            foreach (var entity in invoice)
            {
                foreach(var item in entity.payment){
                    if(id==0){
                toReturn.Add(new PaymentRes
                {
                    amount = item.amount,
                    invoiceId = entity.Id,
                    payType = (item.paymentTypes!=null)?item.paymentTypes.name:"",
                    billerName = entity.biller.name,
                    customerName = entity.customer.name,
                    id = item.Id,
                    payDate=item.createdDate
                });
                }
                else if(item.Id==id){
                    toReturn.Add(new PaymentRes
                {
                    amount = item.amount,
                    invoiceId = entity.Id,
                    payType = (item.paymentTypes!=null)?item.paymentTypes.name:"",
                    billerName = entity.biller.name,
                    customerName = entity.customer.name,
                    id = item.Id,
                    payDate=item.createdDate
                });
                    
                }
                }
            }
            return toReturn;

        }

        #region PayementType

        public BaseResponse createPaymentType(PaymentTypeReq req)
        {
            BaseResponse toReturn = new BaseResponse();
            var db = _db;
            db.paymentTypes.Add(new PaymentTypes
            {
                name = req.name
            });
            if (db.SaveChanges() > 0)
            {
                toReturn.status = 1;
                toReturn.developerMessage = "Payment type created Successfully";
            }
            return toReturn;
        }
        public List<PaymentTypeRes> getPaymentType()
        {
            List<PaymentTypeRes> toReturn = new List<PaymentTypeRes>();
            var db = _db;
            var payType = db.paymentTypes.Where(c => c.enable == true).ToList();
            foreach (var item in payType)
            {
                toReturn.Add(new PaymentTypeRes
                {
                    id = item.Id,
                    name = item.name
                });
            }
            return toReturn;
        }
        public BaseResponse editPaymentType(PaymentTypeReq req)
        {
            BaseResponse toReturn = new BaseResponse();
            var db = _db;
            var payType = db.paymentTypes.Where(c => c.enable == true && c.Id.Equals(req.id)).FirstOrDefault();
            payType.name = req.name;


            if (db.SaveChanges() > 0)
            {
                toReturn.status = 1;
                toReturn.developerMessage = "Pay type Edited Successfully";
            }
            else
            {
                toReturn.status = 2;
                toReturn.developerMessage = "Unable to edit paytype";
            }

            return toReturn;
        }
        #endregion
    }










}