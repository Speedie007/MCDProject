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
    using System.Collections.Generic;
    
    public partial class CurriculumCourseDayCanBeScheduled
    {
        public int CurriculumCourseDayCanBeScheduledID { get; set; }
        public int DayOfWeekID { get; set; }
        public int CurriculumCourseID { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
    
        public virtual CurriculumCourse CurriculumCourse { get; set; }
        public virtual LookupDayOfWeek LookupDayOfWeek { get; set; }
    }
}
