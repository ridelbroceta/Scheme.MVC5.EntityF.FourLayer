using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;


namespace Apl.UI.Facade
{

    public class AppFacadeException : ApplicationException
    {
        public AppFacadeException(string message) : base("AppFacade: " + message) { }
    }

    public class AppFacade
    {

        private static void SendMailCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // here is where I get the error message mentioned above       
            }
            ((SmtpClient)sender).Dispose();
        }
 
        public static void SendEmailBasic(string address, string subject, string body)
        {
            if (WebConfigFacade.CanSend())
            {
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string from = smtpSection.From;
                var email = new MailMessage();
                email.To.Add(new MailAddress(address));
                email.From = new MailAddress(from);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                var smtp = new SmtpClient();
                smtp.Send(email);
                email.Dispose(); 
                smtp.Dispose();
            }
            else throw new AppFacadeException(string.Format("SendEmailBasic:{0}", "Error"));
        }
    }


}