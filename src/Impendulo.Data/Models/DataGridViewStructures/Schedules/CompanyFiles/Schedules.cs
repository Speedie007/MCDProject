using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Data.Models.DataGridViewStructures.Schedules.CompanyFiles
{
    public class Schedules
    {
        public string Department { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public int AmountEnrolled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ScheduledLocation { get; set; }
        public string VenueName { get; set; }
        public string FacilitatorName { get; set; }
        public int VenueID { get; set; }
        public int FacilitactorID { get; set; }
        public int LocationID { get; set; }
        public int CurriculumID { get; set; }
        public int CurriculumCourseEnrollmentID { get; set; }

    }
}
