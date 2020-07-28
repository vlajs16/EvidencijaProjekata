using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObjects.CompanyDTOs
{
    public class CompanyChangePasswordDTO
    {
        [Required]
        public string Password { get; set; }
    }
}
