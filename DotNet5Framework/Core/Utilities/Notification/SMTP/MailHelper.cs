using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Notification.SMTP
{
    public class MailHelper
    {
        private static string smtpServer = "mail.cetinmehmet.com";
        private static int smtpPort = 587;
        private static string from = "iletisim@cetinmehmet.com";
        private static string fromPassword = "Zqc=9Lq_2I9j@T0:";
        private static bool isBodyHtml = true;
        private static NetworkCredential networkCredential = new NetworkCredential(from, fromPassword);
        public static void SendMail(List<string> tos, string subject, string body)
        {
            return;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer, smtpPort);

            mail.From = new MailAddress(from);
            foreach (string to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            //SmtpServer.UseDefaultCredentials = true;
            if (networkCredential != null)
                SmtpServer.Credentials = networkCredential;

            SmtpServer.Send(mail);

        }
        public static void SendMail(string to, string subject, string body)
        {
            return;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer, smtpPort);

            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;

            //SmtpServer.UseDefaultCredentials = true;
            if (networkCredential != null)
                SmtpServer.Credentials = networkCredential;

            SmtpServer.Send(mail);

        }
    }
}
