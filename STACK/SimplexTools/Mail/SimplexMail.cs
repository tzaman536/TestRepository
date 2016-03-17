using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using log4net;

namespace PhenixTools.Mail
{
    public class PhenixMail
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PhenixMail));

        public static void SendMail(string subject, string body, string to)
        {
            try
            {
                logger.Info("Sending mail.....");
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
                //SMTP_PORT = 25
                // send is the password for send@simplessys.co
                // smtp server name : localhost
                // userid: send@simplexsys.co
                //NetworkCredential nc = new NetworkCredential(send@simplexsys.co, "send");
                NetworkCredential nc = new NetworkCredential(ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM_CRED"]);
                smtp.Credentials = nc;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }

        }
    }
}
