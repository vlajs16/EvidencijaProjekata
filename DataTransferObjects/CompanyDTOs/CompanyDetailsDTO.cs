using DataTransferObjects.CompanyContactDTOs;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.LocationDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.CompanyDTOs
{
    public class CompanyDetailsDTO
    {
        public long CompanyID { get; set; }
        public string Name { get; set; }
        public List<LocationToViewCompanyDTO> Locations { get; set; }
        public List<ContactToViewDTO> Contacts { get; set; }
        public List<ExternalMentorToViewDTO> Mentors { get; set; }
    }
}
