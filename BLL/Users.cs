using System;
using System.Linq;
using SimpleInvoices.ViewModels;
using SimpleInvoices;
using System.Collections.Generic;

namespace SimpleInvoices.BLL{
    public class Users {
        private readonly InvoiceContext _db;
        public Users (InvoiceContext context)
        {
            _db=context;
        }

        public List<LoginUsersViewRes> GetUsers(int id){
            List<LoginUsersViewRes> toReturn=new List<LoginUsersViewRes>();
            var db=_db;
            List<SimpleInvoices.Users> users=new List<SimpleInvoices.Users>(); 
            if(id==0){
             users=db.users.Where(c=>c.enable==true).ToList();
            }
            else{
                users=db.users.Where(c=>c.Id==id && c.enable==true).ToList();
            }
            foreach(var entity in users){
                toReturn.Add(new LoginUsersViewRes{
                    name=entity.name,
                    id=entity.Id
                });
            }
            return toReturn;
        }
        public BaseResponse deleteUser(int id){
            BaseResponse toReturn=new BaseResponse();
            var db=_db;
            db.users.Where(c=>c.Id==id).FirstOrDefault().enable=false;
            if(db.SaveChanges()>0){
                toReturn.status=1;
                toReturn.developerMessage="User Successfully deleted";
            }
            else{
                toReturn.status=2;
                toReturn.developerMessage="unable to delete User";
            }
            return toReturn;
        }
        public BaseResponse addUsers (RegisterUserReq user)
        {
          string token = "";
			BaseResponse response = new BaseResponse();
            try
            {

                var db = _db;
                
                var isRegistered = db.users.Where(c=>c.name.Equals(user.name));

                


                if (isRegistered.Count() == 0)
                {
                    Random rnd = new Random();
                    token = rnd.Next(100000, 999999).ToString();



                    Console.WriteLine("Hold here");
                    var insertUserObj = new SimpleInvoices.Users()
                    {
                      
                        password = Utils.Utils.CreateMD5(user.password),
                       
                        enable = Constant.USER_ACTIVE,
                       
                        name = user.name,
                     
                    };

                    Console.WriteLine("I'm here");

                    db.users.Add(insertUserObj);
                    response.status = db.SaveChanges();


                    if (response.status == 1)
                    {
                          response.status = 1;
                         response.developerMessage = "Account Created";
                    }

                    else
                    {
                        response.status = 2;
                        response.developerMessage = "Couldn't Register, Try Again Later!";
                    }
                }
                else
                {
                    var registeredUser = isRegistered.SingleOrDefault();

                    if (registeredUser.enable == Constant.USER_ACTIVE)
                    {
                        response.status = 1;
                        response.developerMessage = "Account Already Created";
                    }
                    else
                    {

                       
                        registeredUser.password = Utils.Utils.CreateMD5(user.password);
                        registeredUser.enable = Constant.USER_ACTIVE;
                      
                        db.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {
                response.developerMessage = ex.Message;
                response.status = 3;
            }
			return response;
        }
    }
}