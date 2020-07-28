using DataTransferObjects.ProjectProposalDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectDTOs
{
    public class ProjectDetailsDTO
    {
        public long ProjectID { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public EmployeeDTO InternalMentor { get; set; }
        public EmployeeDTO DecisionMaker { get; set; }
        public ProjectProposalForProjectDetailsDTO ProjectProposal { get; set; }
    }
}
