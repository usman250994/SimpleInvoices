using SimpleInvoices.ViewModels;
using SimpleInvoices.Utils;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SimpleInvoices.BLL{
    public class CustomField{
        protected readonly InvoiceContext _db;
        public CustomField(InvoiceContext context){
            _db=context;
        }

         public List<CustomFieldRes> getCustomFields(string tableName)
        {
            List<CustomFieldRes> toReturn =new List<CustomFieldRes>();
            var db=_db;           
            var fields=db.customFields.Where(c=>c.enable==true && c.tableName.Equals(tableName)).Include(c=>c.FieldValues).ToList();
            if(fields.Count>0)
            {
                foreach(var entity in fields)
                {
                    toReturn.Add(new CustomFieldRes{
                        fieldName=entity.fieldName,
                         fieldValue=(entity.FieldValues.FirstOrDefault()!=null)?entity.FieldValues.FirstOrDefault().value:" "
                    });
                }
            }
            return toReturn;
        }
         public List<CustomFieldEditRes> getCustomFieldsById(int id)
        {
            List<CustomFieldEditRes> toReturn =new List<CustomFieldEditRes>();
            List<CustomFields> fields=new List<CustomFields>();
            var db=_db;
          if(id==0)
          {
fields=db.customFields.Where(c=>c.enable==true).Include(c=>c.FieldValues ).ToList(); 
          }

            else
            {
             fields=db.customFields.Where(c=>c.Id.Equals(id) &&c.enable==true ).Include(c=>c.FieldValues ).ToList();
            }
            if(fields.Count>0)
            {
                foreach(var entity in fields)
                {
                    toReturn.Add(new CustomFieldEditRes{
                        fieldName=entity.fieldName,
                        tableName=entity.tableName,
                        id=entity.Id
                    });
                }
            }
            return toReturn;
        }
        public BaseResponse editCustomField(CustomFieldReq customField){
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            var fields=db.customFields.Where(c=>c.Id.Equals(customField.id) && c.enable==true).FirstOrDefault();
            fields.fieldName=customField.fieldName;
            if(db.SaveChanges()>0){
                toReturn.status=1;
                toReturn.developerMessage="CustomField Edited Successfully";
            }
            return toReturn;
        }
        public BaseResponse deleteCustomField(int id){
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            var customField=db.customFields.Where(c=>c.Id.Equals(id)).Include(c=>c.FieldValues).FirstOrDefault();
            customField.enable=false;
            
            foreach(var item in customField.FieldValues)
            {
                item.enable=false;
            
            }
            if(db.SaveChanges()>0){
                toReturn.status=1;
                toReturn.developerMessage="Custom field Deleted Successfully";
            }
            return toReturn;
        }

        public BaseResponse addCustomField(CustomFieldReq customField){
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            db.customFields.Add(new CustomFields{
                fieldName=customField.fieldName,
                tableName=customField.tableName,
                enable=true
                
            });
            if(db.SaveChanges()==1){
                toReturn.developerMessage="Custom Field Added Successfully";
                toReturn.status=1;
            }
            else
            {
                toReturn.developerMessage="Custom Field cannot be added";
                toReturn.status=2;

            }
            return toReturn;
        }
    }
}