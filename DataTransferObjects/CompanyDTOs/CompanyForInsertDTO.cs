using DataTransferObjects.CompanyContactDTOs;
using DataTransferObjects.LocationDTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransferObjects.CompanyDTOs
{
    public class CompanyForInsertDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public List<LocationInsertCompanyDTO> Locations { get; set; }
        public List<ContactToInsertDTO> Contacts { get; set; }
    }
}
