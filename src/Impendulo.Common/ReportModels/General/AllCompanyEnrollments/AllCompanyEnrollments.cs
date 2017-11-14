using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ReportModels.General.AllCompanyEnrollments
{
    public class AllCompanyEnrollments
    {
        public string DepartmentName { get; set; }
        public string CurriculumName { get; set; }
        public string CourseName { get; set; }
        public int AmountEnrolled { get; set; }
        public string ScheduledStartDate { get; set; }
        public string ScheduledCompletionDate { get; set; }
        public string Location { get; set; }
        public string Client { get; set; }
    }
}
