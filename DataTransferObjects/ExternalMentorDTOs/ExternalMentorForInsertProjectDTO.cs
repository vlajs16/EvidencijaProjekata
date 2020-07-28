using DataTransferObjects.ExternalMentorDTOs.ExMentorContact;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ExternalMentorDTOs
{
    public class ExternalMentorForInsertProjectDTO
    {
        public int? MentorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<MentorContactToInsertDTO> Contacts { get; set; }
    }
}
