using DataTransferObjects.ScientificAreaDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectCoveringSubjectDTOs
{
    public class ProjectCoveringSubjectInsertDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ScientificAreaToInsertProjectProposalDTO ScientificArea { get; set; }
    }
}
