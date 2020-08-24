using DataTransferObjects.CompanyDTOs;
using DataTransferObjects.ExternalMentorDTOs;
using DataTransferObjects.ProjectCoveringSubjectDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectProposalDTOs
{
    public class ProjectProposalForInsertDTO
    {
        public string Name { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public DateTime StartDateProjectProposal { get; set; }
        public int DaysDuration { get; set; }
        public ExternalMentorForInsertProjectDTO ExternalMentor { get; set; }
        public CompanyForInsertProjectProposalDTO Company { get; set; }
        public List<ProjectCoveringSubjectInsertDTO> Subjects { get; set; }
    }
}
