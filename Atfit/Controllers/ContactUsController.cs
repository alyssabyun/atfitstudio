using Atfit.Models;
using Atfit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Atfit.Controllers
{
    public class ContactUsController : Controller
    {
        //
        // GET: /ContactUs/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new EmailModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EmailModel m)
        {
            if (TryValidateModel(m) && !m.IsSpam)
            {
                var email = new EmailModel();

                email.FromEmail = Config.Current.EmailNoReply.Address;
                email.FromName = m.FromName;
                email.Subject = m.Subject;
                email.Message = m.Message;
                try
                {
                    var mailAddr = new MailAddress(m.FromEmail);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("FromEmail", "Please input valid email address");
                    return View(m);
                }

                email.ReplyToAddress = m.FromEmail;

                var emailService = new EmailService();
                emailService.SendEmail(email);

                return View("EmailSent");
            }
            return View(m);
        }
    }
}
