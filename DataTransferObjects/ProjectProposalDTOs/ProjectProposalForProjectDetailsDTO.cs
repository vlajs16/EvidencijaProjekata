using DataTransferObjects.CompanyDTOs;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.ProjectCoveringSubjectDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectProposalDTOs
{
    public class ProjectProposalForProjectDetailsDTO
    {
        public long ProjectProposalID { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public ExternalMentorForProjectDetailsDTO ExternalMentor { get; set; }
        public CompanyForInsertProjectProposalDTO Company { get; set; }
        public List<ProjectCoveringSubjectForProjectDetailsDTO> Subjects { get; set; }
    }
}
