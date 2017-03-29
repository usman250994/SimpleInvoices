using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;
namespace SimpleInvoices.Controllers
{
    public class UserController: BaseController
    {
        public UserController(InvoiceContext context):base(context)
        { }

         [Route("api/users/register"), HttpPost]
         public BaseResponse AddUser([FromBody] RegisterUserReq user)
		{
			return new BLL.Users(_db).addUsers(user);
		}
        [Route("api/users/get"), HttpPost]
         public List<LoginUsersViewRes> GetUsers([FromBody] int id)
		{
			return new BLL.Users(_db).GetUsers(id);
		}
        [Route("api/users/delete"), HttpPost]
         public BaseResponse deleteUsers([FromBody] int id)
		{
			return new BLL.Users(_db).deleteUser(id);
		}
    }
}