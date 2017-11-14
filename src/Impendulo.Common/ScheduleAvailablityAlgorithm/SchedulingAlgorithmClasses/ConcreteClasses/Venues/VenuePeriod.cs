
using Impendulo.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class OnSiteVenuePeriod : AbstractPeriod
    {

        private int _VenueMaxQuantity = 0;
        public int VenueID { get { return this.PeriodID; } }
        public string VenueName { get { return this.Description; } }

        public int VenueMaxCapicaty
        {
            get
            {
                return _VenueMaxQuantity;
            }

            set
            {
                _VenueMaxQuantity = value;
            }
        }

        //private EnumScheduleLocations _VenueLocation = EnumScheduleLocations.Onsite;
        //public  EnumScheduleLocations VenueLocation { get { return _VenueLocation; } }
        public OnSiteVenuePeriod(DateTime StartDate, DateTime EndDate, int PeriodID, string Description, int VenueMaxCapicaty) : base(StartDate, EndDate, PeriodID, Description)
        {
            this.VenueMaxCapicaty = VenueMaxCapicaty;
            //this._VenueLocation = VenueLocation;
        }
    }
}