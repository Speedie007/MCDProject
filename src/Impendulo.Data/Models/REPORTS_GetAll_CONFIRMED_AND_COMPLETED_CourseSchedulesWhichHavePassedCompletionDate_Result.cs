//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impendulo.Data.Models
{
    using System;
    
    public partial class REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result
    {
        public Nullable<int> AmountEnrolled { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public string FacilitatorName { get; set; }
        public string VenueName { get; set; }
        public int FacilitatorID { get; set; }
        public Nullable<int> VenueID { get; set; }
        public int CurriculumCourseID { get; set; }
        public int CourseID { get; set; }
        public int DepartmentID { get; set; }
        public int LookupScheduleLocationID { get; set; }
        public string EnrollmentProgressCurrentState { get; set; }
        public int LookupEnrollmentProgressStateID { get; set; }
        public string CostCode { get; set; }
    }
}
