using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObjects
{
    public class EmployeeForLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "You must enter password with 4 to 20 chars")]
        public string Password { get; set; }
    }
}
