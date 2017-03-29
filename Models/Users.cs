
using System.ComponentModel.DataAnnotations;

namespace SimpleInvoices {

    public class identity{
        [Key]
        public int Id {get;set;}
        public bool enable {get;set;}
    }
    public class Users:identity{
        public string name {get;set;}
        public string password {get;set;}
    }

}