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
    
    public partial class GetAllStudentsRelatedToScheduledCourse_Result
    {
        public string IDNumber { get; set; }
        public string StudentName { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string ScheduleStatus { get; set; }
        public int FacilitatorID { get; set; }
        public int CurriculumCourseEnrollmentID { get; set; }
        public int EnrollmentID { get; set; }
        public int EnquiryID { get; set; }
        public int ProgressFileID { get; set; }
        public int CurriculumCourseID { get; set; }
    }
}
