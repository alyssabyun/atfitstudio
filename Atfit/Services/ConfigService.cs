using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace Atfit.Services
{
    public static class Config
    {
        public static ConfigService Current = new ConfigService();
    }
    public class ConfigService
    {
        private NameValueCollection Settings { get; set; }

        public MailAddress EmailDeveloper { get; private set; }
        public MailAddress EmailWebsite { get; private set; }
        public MailAddress EmailNoReply { get; private set; }

        private MailAddress GetMailAddressFromConfig(string configKey)
        {
            var email = Settings[configKey].Split('|');
            return new MailAddress(email[1], email[0]);
        }

        public ConfigService()
        {
            Settings = WebConfigurationManager.GetSection("settings") as NameValueCollection;

            EmailWebsite = GetMailAddressFromConfig("Email.Website");
            EmailDeveloper = GetMailAddressFromConfig("Email.Developer");
            EmailNoReply = GetMailAddressFromConfig("Email.NoReply");
        }
    }
}