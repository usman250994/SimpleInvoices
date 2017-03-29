namespace SimpleInvoices.ViewModels
{
    public class BaseResponse
    {
       public BaseResponse(){
      }

		public int status { get; set; }
		public string developerMessage { get; set; }
    }
}