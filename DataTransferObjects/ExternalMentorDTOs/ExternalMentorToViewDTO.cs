using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ExternalMentorDTOs
{
    public class ExternalMentorToViewDTO
    {
        public long MentorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ExternalMentorContactDTO> Contacts { get; set; }
    }
}
