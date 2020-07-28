using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProjectContract
    {
        public long ProjectContractID { get; set; }
        public Project Project { get; set; }
        public DateTime SigningDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CompanySigner { get; set; }
        public Employee InternalSigner { get; set; }
    }
}
