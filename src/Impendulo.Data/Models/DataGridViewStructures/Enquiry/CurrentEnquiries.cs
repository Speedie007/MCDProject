using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Data.Models.DataGridViewStructures.Enquiry
{
    public class CurrentEnquiries
    {
        public int EnquiryID { get; set; }
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