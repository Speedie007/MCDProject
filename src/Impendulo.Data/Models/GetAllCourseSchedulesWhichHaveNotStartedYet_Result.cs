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
    
    public partial class GetAllCourseSchedulesWhichHaveNotStartedYet_Result
    {
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string FacilitatorName { get; set; }
        public string Venue { get; set; }
        public Nullable<int> AmountEnrolled { get; set; }
        public string CourseName { get; set; }
        public string ScheduleLocation { get; set; }
        public int FacilitatorID { get; set; }
        public int VenueID { get; set; }
        public int CurriculumCourseID { get; set; }
        public int CourseID { get; set; }
        public int ScheduleLocationID { get; set; }
    }
}