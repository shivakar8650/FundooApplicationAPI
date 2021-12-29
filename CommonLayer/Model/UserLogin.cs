using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email Id is required")]
        [DataType(DataType.Text)]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password contain six character")]
        public string Password { get; set; }
    }
}
