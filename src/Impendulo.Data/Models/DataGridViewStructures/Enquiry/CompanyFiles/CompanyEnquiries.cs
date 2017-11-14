using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Data.Models.DataGridViewStructures.Enquiry.CompanyFiles
{
    public class CompanyEnquiries
    {
        public int EnquiryID { get; set; }
        public int DepartmentID { get; set; }
        public int ProgressFileID { get; set; }
        public string ContactPerson { get; set; }
        public string Department { get; set; }
        public string Curriculum { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Status { get; set; }
        public Boolean InitialConsultationComplete { get; set; }
        public Boolean InitialCurriculumEnquiryDocumentationSent { get; set; }
        public int QuanityToEnroll { get; set; }
        public int QuantityCurrentlyEnrolled { get; set; }
    }
}
