using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="User Name")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password and Confirmation password do not match ...")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
