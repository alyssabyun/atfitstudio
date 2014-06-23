using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Atfit.Models
{
    public class EmailModel
    {
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        [Required]
        public string FromName { get; set; }
        [Required]
        public string FromEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public string ReplyToAddress { get; set; }
        public string AntiSpam { get; set; }
        public bool IsSpam
        {
            get
            {
                return !string.IsNullOrEmpty(AntiSpam);
            }
        }
    }
}