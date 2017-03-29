using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL{

    public class Customers {
        private readonly InvoiceContext _db ;
        public Customers (InvoiceContext context){
            _db=context;
        }
      
       #region get invoice 
        public List<UserViewRes> getCustomers (int id)
        {
            try{
            var db=_db;
            List<UserViewRes> toReturn =new List<UserViewRes>();
            List<CustomFields> fields=new List<CustomFields>();
            List<SimpleInvoices.Customer> userList=new List<SimpleInvoices.Customer>();
            if(id==0)
            {
              userList=db.customer.Where(c=>c.enable==true).ToList();
              fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_CUSTOMER)).Include(c=>c.FieldValues).ThenInclude(c=>c.customer).ToList();
              
            }
            else
            {
             userList=db.customer.Where(c=>c.Id.Equals(id)  && c.enable==true).ToList();
              fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_CUSTOMER)&&c.enable==true).Include(c=>c.FieldValues).ThenInclude(c=>c.customer).ToList();
            }
            
            if(userList.Count>0)
            {
                
                foreach(var entity in userList)
                {
                    double total=0;
                    double paid=0;
                    double owing=0;
                    var ledger=db.ledgers.Where(c=>c.enable==true).Include(c=>c.customer).Where(c=>c.customer==entity).FirstOrDefault();
                  if(ledger!=null){
                    total=ledger.amount;
                    owing=ledger.balance;
                    paid=total-owing;
                    Console.WriteLine("Customer added"); 
                    }
                    
                    toReturn.Add(new UserViewRes(){
                        name=entity.name,
                        contact=entity.contact,
                        id=entity.Id,
                        email=entity.email,
                        address=entity.address,
                        customfields=(fields!=null)?bll_getcustomFields(fields,entity):new List<CustomFieldRes>(),
                        total=total,
                        paid=paid,
                        owing=owing,
                        sholder=entity.sholder,
                        chest=entity.chest,
                        upperWaist=entity.upperWaist,
                        waist=entity.waist,
                        lowerWaist=entity.lowerWaist,
                        hips=entity.lowerWaist,
                           armHole=entity.armHole,
                           fullSleveLength=entity.fullSleveLength,
                           bicep=entity.bicep,
                           forearm=entity.forearm,
                           wrist=entity.wrist,
                           longShirtLength=entity.longShirtLength,
                           shortShirtLength=entity.shortShirtLength,
                           chaak=entity.chaak,
                            daaman=entity.daaman,
                            frontNeckDepth=entity.frontNeckDepth,
                            frontNeckWidth=entity.frontNeckWidth,
                            thigh=entity.thigh,
                            kneeCap=entity.kneeCap,
                            calf=entity.calf,
                            ankle=entity.ankle,
                            pantLength=entity.pantLength,
                            measurementType=entity.measurementType,
                            imagepath=entity.imagepath
                    });
                }
            }
            else{
                return toReturn;
            }
            return toReturn;
            }
            catch(Exception ex){
                return new List<UserViewRes>();
            }
        }
        #endregion
        public List<CustomFieldRes> bll_getcustomFields(List<SimpleInvoices.CustomFields> customField,Customer customer){
            List<CustomFieldRes> toReturn =new List<CustomFieldRes>();
            
            for(int i=0;i<customField.Count;i++){
                var fieldValue=(customField[i].FieldValues.Where(c=> c.customer.Id.Equals(customer.Id)).FirstOrDefault()!=null)?
                customField[i].FieldValues.Where(c=>c.customer==customer).FirstOrDefault().value:" ";
                toReturn.Add(new CustomFieldRes{
                    fieldName=customField[i].fieldName,
                    fieldValue=fieldValue
                });
              //  toReturn[i].fieldValue=(customField[i].FieldValues.Where(c=>c.customBillers==customer).FirstOrDefault()!=null)?
              //  customField[i].FieldValues.Where(c=>c.customBillers==customer).FirstOrDefault().value:" ";
            }
            
            return toReturn;
        }
        public BaseResponse addCustomer(UserViewReq customer)
        {
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            var Customer=db.customer.Where(c=>c.email.Equals(customer.email)).FirstOrDefault();
            if(Customer==null)
            {
                SimpleInvoices.Customer cust=new SimpleInvoices.Customer();

                cust.name=customer.name;
                cust.address=customer.address;
                cust.sholder=customer.sholder;
                cust.contact=customer.contact;
                cust.email=customer.email;
                cust.enable=Constant.USER_ACTIVE;
                cust.chest=customer.chest;
                cust.upperWaist=customer.upperWaist;
                cust.waist=customer.waist;
                cust.lowerWaist=customer.lowerWaist;
                cust.hips=customer.hips;
                cust.armHole=customer.armHole;
                cust.fullSleveLength=customer.fullSleveLength;
                cust.sleeveLength=customer.sleeveLength;
                cust.bicep=customer.bicep;
                cust.forearm=customer.forearm;
                cust.wrist=customer.wrist;
                cust.longShirtLength=customer.longShirtLength;
                cust.shortShirtLength=customer.shortShirtLength;
                cust.chaak=customer.chaak;
                cust.daaman=customer.daaman;
                cust.frontNeckDepth=customer.frontNeckDepth;
                cust.frontNeckWidth=customer.frontNeckWidth;
                cust.backNeckDepth=customer.backNeckDepth;
                cust.backNeckWidth=customer.backNeckWidth;
                cust.thigh=customer.thigh;
                cust.kneeCap=customer.kneeCap;
                cust.calf=customer.calf;
                cust.ankle=customer.ankle;
                cust.pantLength=customer.pantLength;
                cust.imagepath=customer.imagepath;
                if(customer.customFields.Count>0)
                {
                    foreach(var entity in customer.customFields)
                    {
                        string name=entity.fieldName;
                        var field=db.customFields.Where(c=>c.tableName.Equals("Customer") && c.fieldName.Equals(name) && c.enable==true).FirstOrDefault();
                   field.FieldValues.Add(new FieldValue {
                        value=entity.fieldValue,
                        customer=cust,
                        enable=true 
                    });
                    
                   List<FieldValue> fieldValue=new List<FieldValue>();
                   fieldValue=field.FieldValues;
                    db.FieldValues.AddRange(fieldValue);
                    }
                    
                }
                db.customer.Add(cust);
                if(db.SaveChanges()>=1)
                {
                    toReturn.status=1;
                    toReturn.developerMessage="Customer has been created";
                }
                else
                {
                    toReturn.status=2;
                    toReturn.developerMessage="Customer can not been created";
                }
            }
            else
            {
                toReturn.status=-2;
                toReturn.developerMessage="customer is already Created";
            }
            return toReturn;
        }

        public BaseResponse editCustomer(UserViewReq customer)
        {
            var db=_db;
            BaseResponse toReturn =new BaseResponse();
            var entity =db.customer.Where(c=>c.Id.Equals(customer.id) && c.enable==true).FirstOrDefault();
            if(entity!=null)
            {
                entity.address=customer.address;
                entity.contact=customer.contact;
                entity.email=customer.email;
                entity.name=customer.name;
                 entity.chest=customer.chest;
                 entity.sholder=customer.sholder;
                entity.upperWaist=customer.upperWaist;
                entity.waist=customer.waist;
                entity.lowerWaist=customer.lowerWaist;
                entity.hips=customer.hips;
                entity.armHole=customer.armHole;
                entity.fullSleveLength=customer.fullSleveLength;
                entity.sleeveLength=customer.sleeveLength;
                entity.bicep=customer.bicep;
                entity.forearm=customer.forearm;
                entity.wrist=customer.wrist;
                entity.longShirtLength=customer.longShirtLength;
                entity.shortShirtLength=customer.shortShirtLength;
                entity.chaak=customer.chaak;
                entity.daaman=customer.daaman;
                entity.frontNeckDepth=customer.frontNeckDepth;
                entity.frontNeckWidth=customer.frontNeckWidth;
                entity.backNeckDepth=customer.backNeckDepth;
                entity.backNeckWidth=customer.backNeckWidth;
                entity.thigh=customer.thigh;
                entity.kneeCap=customer.kneeCap;
                entity.calf=customer.calf;
                entity.ankle=customer.ankle;
                entity.pantLength=customer.pantLength;
                  foreach(var item in customer.customFields){
                    var customFields=db.customFields.Where(c=>c.fieldName.Equals(item.fieldName) && c.tableName.Equals(Constant.TABLE_CUSTOMER)).Include(c=>c.FieldValues).FirstOrDefault();
                    customFields.FieldValues.Where(c=>c.customer.Id.Equals(customer.id)).FirstOrDefault().value=item.fieldValue;

                }
                if(db.SaveChanges()==1)
                {
                    toReturn.status=1;
                    toReturn.developerMessage="Edit customer Successfully";

                }
                else
                {
                    toReturn.status=2;
                    toReturn.developerMessage="unable to Edit customer";
                }
            }
            else
            {
                toReturn.status=-1;
                toReturn.developerMessage="Cannot find user with that name";
            }
            return toReturn;

        }

        public BaseResponse deleteCustomer(int id)
        {
            BaseResponse toReturn =new BaseResponse();
            var db=_db;
            var entity=db.customer.Where(c=>c.Id.Equals(id) && c.enable==true).FirstOrDefault();
            if(entity!=null)
            {
            entity.enable=false;
             var entit = db.FieldValues.Where(c=>c.customer.Id.Equals(entity.Id)).ToList();
            foreach(var item in entit)
            {
                item.enable=false;
            }
            if(db.SaveChanges()==1)
            {
                toReturn.status=1;
                toReturn.developerMessage="Delete Customer Successfull";
            }
            else
            {
                 toReturn.status=2;
                toReturn.developerMessage="Unable to delete customer";
            }
            }
            else{
                toReturn.status=-1;
                toReturn.developerMessage="Unable to find customer with id "+id;
            }
            return toReturn;
        }

    }
}