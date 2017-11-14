using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ReportModels
{
    public class Enrollments
    {
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public int AmountEnrolled { get; set; }
        public string ScheduleStartDate { get; set; }
        public string ScheduleCompletionDate { get; set; }
        public string CourseLocation { get; set; }
        public string Client { get; set; }
        public int DeterminIfScheduledOrNot { get; set; }
    }
}
