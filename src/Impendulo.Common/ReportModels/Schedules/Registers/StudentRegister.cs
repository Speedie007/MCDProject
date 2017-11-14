using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ReportModels.Schedules.Registers
{
    public class StudentRegister
    {
        public string Company { get; set; }
        public string StudentName { get; set; }
        public string IDNumber { get; set; }
        public string Department { get; set; }
        public string CurriculumName { get; set; }
        public string Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CostCode { get; set; }
        public int Duration { get; set; }
        public string Confirmed { get; set; }
    }
}
