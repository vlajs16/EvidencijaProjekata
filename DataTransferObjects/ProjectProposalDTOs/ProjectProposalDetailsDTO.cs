using DataTransferObjects.CompanyDTOs;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.ProjectCoveringSubjectDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectProposalDTOs
{
    public class ProjectProposalDetailsDTO
    {
        public long ProjectProposalID { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public DateTime StartDateProjectProposal { get; set; }
        public int DaysDuration { get; set; }
        public ExternalMentorForProjectProposalDetailDTO ExternalMentor { get; set; }
        public CompanyForListDTO Company { get; set; }
        public List<ProjectCoveringSubjectViewDTO> Subjects { get; set; }
    }
}
