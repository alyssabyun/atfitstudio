using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Atfit.Models
{
    public class ReferFriendModel
    {
        [Required]
        public string FriendName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Name { get; set; }
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