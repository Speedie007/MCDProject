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
    
    public partial class GetAllEnrollmentsPerStudent_Result
    {
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public Nullable<int> AmountEnrolled { get; set; }
        public string ScheduleStartDate { get; set; }
        public string ScheduleCompletionDate { get; set; }
        public string CourseLocation { get; set; }
        public string EnrollmentProgressCurrentState { get; set; }
        public int CurriculumCourseEnrollmentID { get; set; }
    }
}