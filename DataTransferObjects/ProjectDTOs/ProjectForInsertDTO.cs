using DataTransferObjects.EmployeeDTOs;
using DataTransferObjects.ProjectProposalDTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectDTOs
{
    public class ProjectForInsertDTO
    {
        public long? ProjectID { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public EmployeeForProjectInsertDTO InternalMentor { get; set; }
        public EmployeeForProjectInsertDTO DecisionMaker { get; set; }
        public ProjectProposalForProjectInsertDTO ProjectProposal { get; set; }
    }
}
