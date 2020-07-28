using DataTransferObjects.ScientificAreaDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects.ProjectCoveringSubjectDTOs
{
    public class ProjectCoveringSubjectViewDTO
    {
        public long ProjectCoveringSubjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ScientificAreaForListDTO ScientificArea { get; set; }
    }
}
