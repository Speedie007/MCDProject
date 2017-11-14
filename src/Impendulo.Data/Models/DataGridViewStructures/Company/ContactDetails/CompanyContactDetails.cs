using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Data.Models.DataGridViewStructures.Company.ContactDetails
{
    public class CompanyContactDetails
    {
        public int ContactDetailID { get; set; }
        public string ContactDetailValue { get; set; }
        public int ContactTypeID { get; set; }
        public string ContactType { get; set; }
    }
}
