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
    public class ReferController : Controller
    {
        //
        // GET: /Refer/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ReferFriendModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ReferFriendModel m)
        {
            if(TryValidateModel(m) && !m.IsSpam)
            {
                var email = new EmailModel();

                email.FromEmail = Config.Current.EmailNoReply.Address;
                email.FromName = m.Name;
                email.Subject = "Refer a friend";
                email.Message = "Friend's name: " + m.FriendName + ",<br /> Friend's phone number: " + m.Phone + "<br /> My name: " + m.Name ;
                email.ReplyToAddress = email.FromEmail;

                var emailService = new EmailService();
                emailService.SendEmail(email);
                return View("Submitted");
            }
            return View(m);
        }
    }
}
