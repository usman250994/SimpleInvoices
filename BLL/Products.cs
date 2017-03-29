using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleInvoices;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL{
    public class Products {
        private readonly InvoiceContext _db ;
        public Products (InvoiceContext context){
            _db=context;
        }
        public List<CustomFieldRes> getCustomFields()
        {
            List<CustomFieldRes> toReturn =new List<CustomFieldRes>();
            var db=_db;
            var fields=db.customFields.Where(c=>c.tableName.Equals("Products") && c.enable==true).ToList();
            if(fields.Count>0)
            {
                foreach(var entity in fields)
                {
                    toReturn.Add(new CustomFieldRes{
                        fieldName=entity.fieldName
                    });
                }
            }
            return toReturn;
        }
        public List<ProductViewRes> getProducts(int id)
        {
            var db=_db;
            List<ProductViewRes> toReturn =new List<ProductViewRes>();
            List<SimpleInvoices.product> productList=new List<SimpleInvoices.product>();
            List<CustomFields> fields=new List<CustomFields>();
            if(id==0)
            {
              productList=db.products.Where(c=> c.enable==true).Include(c=>c.customFields ).ToList();
              fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_PRODUCT)).Include(c=>c.FieldValues).ThenInclude(c=>c.product).ToList();
            }
            else
            {
             productList=db.products.Where(c=>c.Id.Equals(id)  && c.enable==true).Include(c=>c.customFields).ToList();
            fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_PRODUCT)).Include(c=>c.FieldValues).ThenInclude(c=>c.product).ToList();
            }
            
            if(productList.Count>0)
            {
                 foreach(var entity in productList)
                 {
                     var customFields =new List<CustomFieldRes>();
                     var design=new List<DesignViewReq>();
                   
                     toReturn.Add(new ProductViewRes(){
                         name=entity.name,
                         id=entity.Id,
                         color=entity.color,
                         note=entity.note,
                         description=entity.description,
                         price=entity.price,
                         createdOn=entity.createdOn,
                         customField=(fields!=null)?bll_getcustomFields(fields,entity):new List<CustomFieldRes>()
                     });
                 }
            }
            else{
                return toReturn;
            }
            return toReturn;
        }
public List<ProductViewRes> getProductsWithDesigns(int id,double quantity,SimpleInvoices.Taxes tax,int ledger_id)
        {
            var db=_db;
            List<ProductViewRes> toReturn =new List<ProductViewRes>();
            List<SimpleInvoices.product> productList=new List<SimpleInvoices.product>();
            List<Design> designs=new List<Design>();
            List<CustomFields> fields=new List<CustomFields>();
            if(id==0)
            {
              productList=db.products.Where(c=> c.enable==true).Include(c=>c.customFields ).ToList();
              fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_PRODUCT) && c.enable==true).Include(c=>c.FieldValues).ThenInclude(c=>c.product).ToList();
              
            }
            else
            {
             productList=db.products.Where(c=>c.Id.Equals(id)  && c.enable==true).Include(c=>c.customFields).ToList();
            fields=db.customFields.Where(c=>c.tableName.Equals(Constant.TABLE_PRODUCT) && c.enable==true).Include(c=>c.FieldValues).ThenInclude(c=>c.product).ToList();
            }
            
            if(productList.Count>0)
            {
                 foreach(var entity in productList)
                 {
                     var customFields =new List<CustomFieldRes>();
                     var design=new List<DesignViewReq>();
                     var ledgersDetails=db.ledgerDetails.Where(c=>c.productId==entity.Id && c.ledgersId==ledger_id).Include(c=>c.designs)
                     .ToList();
                     foreach(var items in ledgersDetails){
                        designs=items.designs;
                        foreach(var des in designs){
                            design.Add(new DesignViewReq{
                                name=des.name,
                                cut=des.cut,
                                fabric=des.fabric,
                                color=des.color,
                                note=des.note
                            });
                        }
                     }         
                     toReturn.Add(new ProductViewRes(){
                         name=entity.name,
                         id=entity.Id,
                         color=entity.color,
                         note=entity.note,
                         description=entity.description,
                         price=entity.price,
                         customField=(fields!=null)?bll_getcustomFields(fields,entity):new List<CustomFieldRes>(),
                         designs=design,
                         quantity=quantity,
                         taxPercent=tax.percent,
                         createdOn=entity.createdOn
                     });
                 }
            }
            else{
                return toReturn;
            }
            return toReturn;
        }

        public List<CustomFieldRes> bll_getcustomFields(List<SimpleInvoices.CustomFields> customField,product customer){
            List<CustomFieldRes> toReturn =new List<CustomFieldRes>();
            
            for(int i=0;i<customField.Count;i++){
                var fieldValue=(customField[i].FieldValues.Where(c=> c.product.Id.Equals(customer.Id) && c.enable==true).FirstOrDefault()!=null)?
                customField[i].FieldValues.Where(c=>c.product==customer).FirstOrDefault().value:" ";
                toReturn.Add(new CustomFieldRes{
                    fieldName=customField[i].fieldName,
                    fieldValue=fieldValue
                });
            }
            
            return toReturn;
        }
        public BaseResponse addProduct(ProductViewReq product)
        {
            int productId=0;
            try{
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            SimpleInvoices.product dbProduct=new SimpleInvoices.product();
            List<Design> designLists=new List<Design>();
            string name=product.name;
            dbProduct=db.products.Where(c=>c.name.Equals(name) && c.enable==true).FirstOrDefault();
            Console.WriteLine("get Product");
            if(dbProduct==null)
            {
                SimpleInvoices.product prod=new SimpleInvoices.product();
                prod.name=product.name;
                prod.color=product.color;
                prod.note=product.note;
                prod.description=product.description;
                prod.price=product.price;
                prod.enable=Constant.USER_ACTIVE;
                prod.createdOn=DateTime.Now;
                prod.enable=true;
                if(product.customField.Count>0)
                {
                    Console.WriteLine("in first If");
                    foreach(var entity in product.customField)
                    {
                        string fieldname=entity.fieldName;
                        var field=db.customFields.Where(c=>c.tableName.Equals("Product") && c.fieldName.Equals(entity.fieldName)).FirstOrDefault();
                        field.FieldValues.Add(new FieldValue {
                        value=entity.fieldValue,
                        product=prod,
                        enable=true
                    });
                    List<FieldValue> fieldValue=new List<FieldValue>();
                   fieldValue=field.FieldValues;
                   db.FieldValues.AddRange(fieldValue);
                   Console.WriteLine("Field added");
                   }        
            }
                           productId=db.products.Add(prod).Entity.Id;
                 
                if(db.SaveChanges()>0)
                {
                    productId=prod.Id;
                    toReturn.status=productId;
                    toReturn.developerMessage="Product has been created";
                }
                else
                {
                    toReturn.status=productId;
                    toReturn.developerMessage="product can not been created";
                }
            }
            else
            {
                toReturn.status=-2;
                toReturn.developerMessage="Product is already Created";
            }

            return toReturn;
            }
            catch(Exception ex){
                Console.WriteLine("error "+ex);
                return new BaseResponse();
            }
        }
        public BaseResponse editProduct(ProductViewReq product)
        {
            var db=_db;
            BaseResponse toReturn =new BaseResponse();
            var entity =db.products.Where(c=>c.Id.Equals(product.id) && c.enable==true).FirstOrDefault();
            if(entity!=null)
            {
                entity.name=product.name;
                entity.note=product.note;
                entity.description=product.description;
                entity.price=product.price;
                entity.color=product.color;
foreach(var item in product.customField){
                    var customFields=db.customFields.Where(c=>c.fieldName.Equals(item.fieldName) && c.tableName.Equals(Constant.TABLE_PRODUCT)).Include(c=>c.FieldValues).FirstOrDefault();
                    customFields.FieldValues.Where(c=>c.product.Id.Equals(product.id)).FirstOrDefault().value=item.fieldValue;

                }


                if(db.SaveChanges()==1)
                {
                    toReturn.status=1;
                    toReturn.developerMessage="Edit Product Successfully";

                }
                else
                {
                    toReturn.status=2;
                    toReturn.developerMessage="unable to Edit Product";
                }
            }
            else
            {
                toReturn.status=-1;
                toReturn.developerMessage="Cannot find product with that name";
            }
            return toReturn;

        }

        public BaseResponse deleteProduct(int id)
        {
            BaseResponse toReturn =new BaseResponse();
            var db=_db;
            var entity=db.products.Where(c=>c.Id.Equals(id) && c.enable==true).FirstOrDefault();
            if(entity!=null)
            {
            entity.enable=false;
             var entit = db.FieldValues.Where(c=>c.product.Id.Equals(entity.Id)).ToList();
            foreach(var item in entit)
            {
                item.enable=false;
            }
            if(db.SaveChanges()==1)
            {
                toReturn.status=1;
                toReturn.developerMessage="Delete product Successfull";
            }
            else
            {
                 toReturn.status=2;
                toReturn.developerMessage="Unable to delete product";
            }
            }
            else{
                toReturn.status=-1;
                toReturn.developerMessage="Unable to find product with id "+id;
            }
            return toReturn;
        }

    }
}