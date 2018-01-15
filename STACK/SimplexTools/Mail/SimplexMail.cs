using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using log4net;
using Simplex.Tools.AppSettings;

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
                
                string mailFrom = AppSettingsHandler.GetAppSettingsValue("MAIL_FROM");

                string smtpServer = AppSettingsHandler.GetAppSettingsValue("SMTP_SERVER");
                string smtpPort = AppSettingsHandler.GetAppSettingsValue("SMTP_PORT");
                string mailCred = AppSettingsHandler.GetAppSettingsValue("MAIL_FROM_CRED");



                
                MailMessage msg = new MailMessage();
                msg.Subject = subject;
                msg.From = new MailAddress(mailFrom);
                msg.Body = body;
                msg.To.Add(new MailAddress(to));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpServer;
                smtp.Port = int.Parse(smtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                //SMTP_PORT = 25
                // send is the password for send@simplessys.co
                // smtp server name : localhost
                // userid: send@simplexsys.co
                //NetworkCredential nc = new NetworkCredential(send@simplexsys.co, "send");
                NetworkCredential nc = new NetworkCredential(mailFrom, mailCred);
                smtp.Credentials = nc;

                logger.InfoFormat("Mail from: {0}", mailFrom);
                logger.InfoFormat("SMTP Server: {0}",smtpServer);
                logger.InfoFormat("SMTP Port: {0}", smtpPort);
                logger.InfoFormat("Mail Cred: {0}", mailCred);

                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }

        }
    }
}
