using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Uniq.WebUI.Tools
{
    public class GeneralTool
    {
        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string URLConverter(string _text)
        {
            return _text.ToLower().Replace(" ", "-").Replace("ş", "s").Replace("ö", "o").Replace("ü", "u").Replace("ğ", "g").Replace("ç", "c").Replace("ı", "i");
        }

        public static void SendMail(string mail, string subject, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "agacanergun.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("test@agacanergun.com", "A5125297a");
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("test@agacanergun.com");
            mailMessage.To.Add(mail);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            smtpClient.Send(mailMessage);
        }
    }
}
