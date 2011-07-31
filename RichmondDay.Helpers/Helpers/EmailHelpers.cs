using System;
using System.Collections.Generic;
using System.Net.Mail;


namespace RichmondDay.Helpers {
    public class EmailHelpers {
        public static void SendEmail(SmtpConfiguration smtpConfiguration, string toAddress, string fromAddress, string subject, string message) {
            SendEmail(smtpConfiguration, toAddress, fromAddress, subject, message, "");
        }

        public static void SendEmail(SmtpConfiguration smtpConfiguration, string toAddress, string fromAddress, string subject, string message, string sendGridCategory) {
            SendGridSmtpApiHeader header = new SendGridSmtpApiHeader();
            // if were on production add a sendgrid header
            if (!string.IsNullOrEmpty(sendGridCategory)) {
                if (smtpConfiguration.Server.Contains("sendgrid")) {
                    header.SetCategory(sendGridCategory);
                }
            }

            // if there is no from address specified, use the default one.
            if (string.IsNullOrEmpty(fromAddress))
                fromAddress = smtpConfiguration.DefaultFromAddress;

            // setup the message
            MailMessage email = new MailMessage();
            email.Subject = subject;
            email.Body = message;
            email.From = new MailAddress(fromAddress);
            email.To.Add(new MailAddress(toAddress));
            email.IsBodyHtml = true;
            if (smtpConfiguration.Server.Contains("sendgrid")) {
                email.Headers.Add("X-SMTPAPI", header.ToString());
            }

            // setup the smtpclient 
            // only need to add credentials if they are present (production)
            SmtpClient client = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port);
            if (!string.IsNullOrEmpty(smtpConfiguration.Username) && !string.IsNullOrEmpty(smtpConfiguration.Password)) {
                client.Credentials = new System.Net.NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);
            }
            
            // send the message
            client.Send(email);
        }

        public static void SendEmail(SmtpConfiguration smtpConfiguration, List<string> toAddresses, string fromAddress, string subject, string message, string sendGridCategory) {
            // if there is no from address specified, use the default one.
            if (string.IsNullOrEmpty(fromAddress))
                fromAddress = smtpConfiguration.DefaultFromAddress;
            string headers = "";

            if (smtpConfiguration.Server.Contains("sendgrid")) {
                SendGridSmtpApiHeader header = new SendGridSmtpApiHeader();
                foreach (var emailAddress in toAddresses) {
                    header.AddTo(emailAddress);
                }

                if (!string.IsNullOrEmpty(sendGridCategory))
                    header.SetCategory(string.Format("Daily Email for {0}", DateTime.Today));
            
                headers = header.ToString();
            }

            // setup the message
            MailMessage email = new MailMessage();
            email.Subject = subject;
            email.Body = message;
            email.From = new MailAddress(fromAddress);
            if (smtpConfiguration.Server.Contains("sendgrid")) {
                email.Headers.Add("X-SMTPAPI", headers);
                email.To.Add(new MailAddress("test@test.com"));
            } else {
                foreach (string address in toAddresses) {
                    email.To.Add(new MailAddress(address));
                }
            }
            email.IsBodyHtml = true;

            // setup the smtpclient 
            // only need to add credentials if they are present (production)
            SmtpClient client = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port);
            if (!string.IsNullOrEmpty(smtpConfiguration.Username) && !string.IsNullOrEmpty(smtpConfiguration.Password)) {
                client.Credentials = new System.Net.NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);
            }

            // send the message
            client.Send(email);
        }

        public static void SendEmail(SmtpConfiguration smtpConfiguration, List<string> toAddresses, string fromAddress, string subject, string message) {
            SendEmail(smtpConfiguration, toAddresses, fromAddress, subject, message, "");
        }
    }
}
