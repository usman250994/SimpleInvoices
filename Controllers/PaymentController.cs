using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SimpleInvoices.ViewModels;
namespace SimpleInvoices.Controllers
{
    public class PaymentController : BaseController
    {
        public PaymentController(InvoiceContext context) : base(context)
        {

        }
        [Route("api/payment/createpayment"), HttpPost]
        public BaseResponse createPayment([FromBody] PaymentReq payment)
        {
            return new BLL.Payment(_db).createPayment(payment);
        }
        [RouteAttribute("api/payment/createPayementType"),HttpPost]
        public BaseResponse createPaymentType([FromBody] PaymentTypeReq PayTypeReq){
            return new BLL.Payment(_db).createPaymentType(PayTypeReq);
        }
         [RouteAttribute("api/payment/listpayment"),HttpPost]
        public List<PaymentRes> getPaymentResponse([FromBody] int id ){
            return new BLL.Payment(_db).getPayment(id);
        }

    }
}