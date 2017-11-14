using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Data.Models.DataGridViewStructures.Company.Addresses
{
    public class CompanyAddresses
    {
        public int AddressID { get; set; }
        public string AddressType { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string Town { get; set; }
        public string Suburb { get; set; }
        public string AreaCode { get; set; }
        public Boolean IsDefault { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
    }
}
