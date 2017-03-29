using System.Linq;
using SimpleInvoices.ViewModels;

namespace SimpleInvoices.BLL
{
   
    public class Login
    {
         private readonly InvoiceContext _db;
         public Login(InvoiceContext context){
            _db=context;
         }
        public BaseResponse loginUser(loginReq login){
            var db=_db;
            BaseResponse toReturn=new BaseResponse();
            var biller=db.biller.Where(c=>c.email.Equals(login.email) && c.password.Equals(login.password)).FirstOrDefault();
            if(biller !=null)
            {
                toReturn.status=1;
                toReturn.developerMessage="User Exists";
            }
            else{
                toReturn.status=0;
                toReturn.developerMessage="biller with this name and id does not exists";
            }
            return toReturn;
        }
        
    }
}