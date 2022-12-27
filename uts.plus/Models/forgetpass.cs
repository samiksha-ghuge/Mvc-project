using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace uts.plus.Models
{
    public class forgetpass
    {
        [Required, EmailAddress, Display(Name = "Registered Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}