using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class AvailableCoursePeriodByFacilitatorResultSet: IResultSet
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int FacilitatorID { get; set; }
        public string FacilitatorName { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
    }
}