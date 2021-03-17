using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "Name must be less than 25 characters long ")]
        [Required(ErrorMessage = " Please enter your Name ! ")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Please enter your Email ! ")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }
        [Display(Name="Photo")]
        public string ExistPhotoPath { get; set; }

        public IFormFile Photo { get; set; }
    }
}
