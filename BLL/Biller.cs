using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL
{
    public class Biller
    {
        private readonly InvoiceContext _db;
        public Biller(InvoiceContext context)
        {
            _db = context;
        }

        public List<BillerViewRes> getBiller(int id)
        {
            var db = _db;
            List<BillerViewRes> toReturn = new List<BillerViewRes>();

            List<SimpleInvoices.Billers> userList = new List<SimpleInvoices.Billers>();
            List<SimpleInvoices.CustomFields> fields = new List<SimpleInvoices.CustomFields>();
            if (id == 0)
            {
                userList = db.biller.Where(c => c.enable == true).ToList();
                fields = db.customFields.Where(c => c.enable == true && c.tableName.Equals(Constant.TABLE_BILLER)).Include(c => c.FieldValues).ThenInclude(c => c.billers).ToList();
            }
            else
            {
                userList = db.biller.Where(c => c.Id.Equals(id) && c.enable == true).ToList();
                fields = db.customFields.Where(c => c.enable == true && c.tableName.Equals(Constant.TABLE_BILLER)).Include(c => c.FieldValues).ThenInclude(c => c.billers).ToList();
            }

            if (userList.Count > 0)
            {
                foreach (var entity in userList)
                {
                    toReturn.Add(new BillerViewRes()
                    {
                        name = entity.name,
                        contact = entity.contact,
                        city = entity.city,
                        id = entity.Id,
                        email = entity.email,
                        address = entity.address,
                        customfields = (fields != null) ? bll_getcustomFields(fields, entity) : new List<CustomFieldRes>()
                    });
                }
            }
            else
            {
                return toReturn;
            }
            return toReturn;
        }
        public List<CustomFieldRes> bll_getcustomFields(List<SimpleInvoices.CustomFields> customField, Billers customer)
        {
            List<CustomFieldRes> toReturn = new List<CustomFieldRes>();

            for (int i = 0; i < customField.Count; i++)
            {
                var fieldValue = (customField[i].FieldValues.Where(c => c.billers.Id.Equals(customer.Id)).FirstOrDefault() != null) ?
                customField[i].FieldValues.Where(c => c.billers == customer).FirstOrDefault().value : " ";
                toReturn.Add(new CustomFieldRes
                {
                    fieldName = customField[i].fieldName,
                    fieldValue = fieldValue
                });
                //  toReturn[i].fieldValue=(customField[i].FieldValues.Where(c=>c.customBillers==customer).FirstOrDefault()!=null)?
                //  customField[i].FieldValues.Where(c=>c.customBillers==customer).FirstOrDefault().value:" ";
            }

            return toReturn;
        }
        public BaseResponse addBiller(BillerViewReq customer)
        {
            BaseResponse toReturn = new BaseResponse();
            var db = _db;
            var Customer = db.biller.Where(c => c.email.Equals(customer.email)).FirstOrDefault();
            if (Customer == null)
            {
                SimpleInvoices.Billers cust = new SimpleInvoices.Billers();

                cust.name = customer.name;
                cust.address = customer.address;
                cust.city = customer.city;
                cust.contact = customer.contact;
                cust.email = customer.email;
                cust.enable = Constant.USER_ACTIVE;
                cust.password=customer.password;
                if (customer.customFields.Count > 0)
                {
                    foreach (var entity in customer.customFields)
                    {
                        string name = entity.fieldName;
                        var field = db.customFields.Where(c => c.enable == true && c.tableName.Equals(Constant.TABLE_BILLER) && c.fieldName.Equals(name)).FirstOrDefault();
                        field.FieldValues.Add(new FieldValue
                        {
                            value = entity.fieldValue,
                            billers = cust,
                            enable = true
                        });

                        List<FieldValue> fieldValue = new List<FieldValue>();
                        fieldValue = field.FieldValues;
                        db.FieldValues.AddRange(fieldValue);
                    }

                }

                db.biller.Add(cust);
                if (db.SaveChanges() == 1)
                {
                    toReturn.status = 1;
                    toReturn.developerMessage = "Biller has been created";
                }
                else
                {
                    toReturn.status = 2;
                    toReturn.developerMessage = "Biller can not been created";
                }
            }
            else
            {
                toReturn.status = -2;
                toReturn.developerMessage = "Biller is already Created";
            }
            return toReturn;
        }

        public BaseResponse editBiller(BillerViewReq customer)
        {
            var db = _db;
            BaseResponse toReturn = new BaseResponse();
            var entity = db.biller.Where(c => c.Id.Equals(customer.id) && c.enable == true).FirstOrDefault();
            if (entity != null)
            {
                entity.address = customer.address;
                entity.contact = customer.contact;
                entity.city = customer.city;
                entity.email = customer.email;
                entity.name = customer.name;
                entity.password=customer.password;

                foreach (var item in customer.customFields)
                {
                    var customFields = db.customFields.Where(c => c.fieldName.Equals(item.fieldName) && c.tableName.Equals(Constant.TABLE_BILLER)).Include(c => c.FieldValues).FirstOrDefault();
                    customFields.FieldValues.Where(c => c.billers.Id.Equals(customer.id)).FirstOrDefault().value = item.fieldValue;

                }

                if (db.SaveChanges() == 1)
                {
                    toReturn.status = 1;
                    toReturn.developerMessage = "Edit customer Successfully";

                }
                else
                {
                    toReturn.status = 2;
                    toReturn.developerMessage = "unable to Edit customer";
                }
            }
            else
            {
                toReturn.status = -1;
                toReturn.developerMessage = "Cannot find user with that name";
            }
            return toReturn;

        }

        public BaseResponse deleteBiller(int id)
        {
            BaseResponse toReturn = new BaseResponse();
            var db = _db;
            var entity = db.biller.Where(c => c.Id.Equals(id) && c.enable == true).FirstOrDefault();
            if (entity != null)
            {
                entity.enable = false;
                var entit = db.FieldValues.Where(c => c.billers.Id.Equals(entity.Id)).ToList();
                foreach (var item in entit)
                {
                    item.enable = false;
                }

                if (db.SaveChanges() == 1)
                {
                    toReturn.status = 1;
                    toReturn.developerMessage = "Delete Biller Successfull";
                }
                else
                {
                    toReturn.status = 2;
                    toReturn.developerMessage = "Unable to delete biller";
                }
            }
            else
            {
                toReturn.status = -1;
                toReturn.developerMessage = "Unable to find customer with id " + id;
            }
            return toReturn;
        }

    }
}