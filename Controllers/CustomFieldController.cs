using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.Controllers{
    public class CustomFieldController:BaseController{
        public CustomFieldController(InvoiceContext db):base(db){
        }
       [Route("api/customfield/getCustomField"),HttpPost]
        public List<CustomFieldRes> getCustomField([FromBody] string tableName)
        {
            return new BLL.CustomField(_db).getCustomFields(tableName);
        }

        [Route("api/customfield/addCustomField"),HttpPost]
        public BaseResponse addCustomField([FromBody] CustomFieldReq fields){
            return new BLL.CustomField(_db).addCustomField(fields);
        }
        [Route("api/customfield/editCustomField"),HttpPost]
        public BaseResponse editCustomField([FromBody] CustomFieldReq fields){
            return new BLL.CustomField(_db).editCustomField(fields);
        }
        [Route("api/customfield/deleteCustomFieldById"),HttpPost]
        public BaseResponse deleteCustomField([FromBody] int fields){
            return new BLL.CustomField(_db).deleteCustomField(fields);
        }
         [Route("api/customfield/getCustomFieldById"),HttpPost]
        public List<CustomFieldEditRes> getCustomFieldById([FromBody] int fields){
            return new BLL.CustomField(_db).getCustomFieldsById(fields);
        }
    }
}