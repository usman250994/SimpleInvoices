namespace SimpleInvoices.Utils {
    public class Misc {

        public const int RESPONSE_OK = 200;
        public const int RESPONSE_BAD_REQUEST = 400;

        public const string NULL_REQUEST = "The request is empty.";
        public static SimpleInvoices.ViewModels.BaseResponse getResponse(int responseCode, string responseMessage) {
            return new SimpleInvoices.ViewModels.BaseResponse()
            {
                status=responseCode,
                developerMessage=responseMessage
            };
        }
    }
}