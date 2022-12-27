using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace uts.plus.Models
{
    public class PasswordDetail
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        /*[RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "8-15 char long,one uppercase,one lowercase, one digit")]*/
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }



    }
}