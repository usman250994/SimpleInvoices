using Braintree;
using System;
using System.Text;

using System.Net.Mail;


namespace SimpleInvoices.Utils
{
    public class Utils
    {
       
       
        public static DateTime getDateFromString(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd",
                                           System.Globalization.CultureInfo.InvariantCulture);
        }

       


        public static String calculateTimeDifference(String timestamp)
        {
            DateTime datetime;
            datetime = Convert.ToDateTime(timestamp);
            String date;

            TimeSpan span = DateTime.Now.Subtract(datetime);
            if (span.Days == 0)
            {
                if (span.Hours == 0)
                {
                    date = span.Minutes.ToString() + "m ago";
                }
                else
                {
                    date = span.Hours.ToString() + "hr ago";
                }
            }
            else if (span.Days >= 365)
            {
                date = "Year ago";
            }

            else if (span.Days == 1)
            {
                date = "Yesterday";
            }
            else if (span.Days <= 6 && span.Days > 0)
            {
                date = datetime.DayOfWeek.ToString();
            }
            else
            {
                // Same as datetime.ToShortDateString();
                date = datetime.ToString("d");
            }

            return date;

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


    }
}