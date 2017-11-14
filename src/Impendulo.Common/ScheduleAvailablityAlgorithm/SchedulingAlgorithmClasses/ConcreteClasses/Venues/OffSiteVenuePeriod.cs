using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm

{
    public class OffSiteVenuePeriod : AbstractPeriod
    {
        public OffSiteVenuePeriod(DateTime StartDate, DateTime EndDate, int PeriodID, string Description, string VenueAssociatedCompanyName) : base(StartDate, EndDate, PeriodID, Description)
        {
            VenueAssociatedCompany = VenueAssociatedCompanyName;
        }

        public int VenueID { get { return this.PeriodID; } }
        public string VenueName { get { return this.Description; } }
        public string VenueAssociatedCompany { get; set; }

    }
}