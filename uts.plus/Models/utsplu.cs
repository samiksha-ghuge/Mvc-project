﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace uts.plus
{
    [MetadataType(typeof(validations))]
    public partial class utsplu
    {
        public string ConfirmPassword { get; set; }
    }


    public class validations
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Confirm password and Password field do not match")]

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Invalid.Must be 10 digit number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Pincode is required")]
        /*[MinLength(6, ErrorMessage ="Minimum 6 digits required")]*/
        [RegularExpression(@"^[0-9]{1,6}",ErrorMessage ="Minimum 6 digits required")]
        public string Pincode { get; set; }

        public string EmployeeType { get; set; }

        [Required]
        public Nullable<bool> DocumentsVerified { get; set; }
       

    }
}