using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Data.Models.DataGridViewStructures.Students.CompanyFiles
{
    public class StudentReturnDetails
    {
        public int StudentID { get; set; }
        public string IndividualFirstName { get; set; }
        public string IndividualLastname { get; set; }
        public string StudentIDNumber { get; set; }
        public string StudentPassportNumber { get; set; }
        public DateTime StudentlInitialDate { get; set; }
    }
}
