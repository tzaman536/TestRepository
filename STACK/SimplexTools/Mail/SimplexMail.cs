using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace PhenixTools.Mail
{
    public class PhenixMail
    {

        public static void SendMail(string subject, string body, string to)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.Subject = subject;
                msg.From = new MailAddress(ConfigurationManager.AppSettings["MAIL_FROM"]);
                msg.Body = body;
                msg.To.Add(new MailAddress(to));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SMTP_SERVER"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM_CRED"]);
                smtp.Credentials = nc;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
