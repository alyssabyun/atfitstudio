using Atfit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Atfit.Controllers;

namespace Atfit.Services
{
    public class EmailService
    {
        public EmailService()
        {
        }

        public void SendEmail(EmailModel email)
        {
            email.ToEmail = Config.Current.EmailDeveloper.Address;
            email.ToName = Config.Current.EmailDeveloper.DisplayName;
            email.Subject = string.Format("[Web] " + email.Subject);

            var body = PreMailer.Net.PreMailer.MoveCssInline(email.Message, false);
            email.Message = body.Html;

            var replyToAddress = GetReplyToAddress(email.ReplyToAddress);

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(email.FromEmail, email.FromName);

                message.ReplyToList.Add(replyToAddress);

                message.To.Add(new MailAddress(email.ToEmail, email.ToName));
                message.Subject = email.Subject;
                message.IsBodyHtml = true;
                message.Body = email.Message;
                using (var client = new SmtpClient())
                {
                    client.Send(message);
                }
            }
        }

        /// <summary>
        /// Get the reply to address if valid, otherwise null
        /// </summary>
        /// <param name="replyToAddress"></param>
        /// <returns></returns>
        private MailAddress GetReplyToAddress(string replyToAddress)
        {
            if (string.IsNullOrWhiteSpace(replyToAddress))
            {
                return null;
            }

            MailAddress address = null;

            try
            {
                address = new MailAddress(replyToAddress);
            }
            catch (System.ArgumentException)
            {
                // we swallow this exception on purpose
                // because it's just a reply to address and the user probabaly entered their address wrong.
            }
            catch (System.FormatException)
            {
                // we swallow this exception on purpose
                // because it's just a reply to address and the user probabaly entered their address wrong.
            }

            return address;
        }
    }
}