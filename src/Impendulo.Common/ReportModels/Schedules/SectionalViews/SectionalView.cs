using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ReportModels.Schedules.SectionalViews
{
    public class SectionalView
    {
        public string ReportHeading { get; set; }
        public int AmountEnrolled { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public string CostCode { get; set; }
        public string FacilitatorName { get; set; }
        public string VenueName { get; set; }
        public int FacilitatorID { get; set; }
        public int VenueID { get; set; }
        public int CurriculumCourseID { get; set; }
        public int CourseID { get; set; }
        public int DepartmentID { get; set; }
        public int LookupScheduleLocationID { get; set; }
        public string EnrollmentProgressCurrentState { get; set; }
        public int LookupEnrollmentProgressStateID { get; set; }
    }
}
