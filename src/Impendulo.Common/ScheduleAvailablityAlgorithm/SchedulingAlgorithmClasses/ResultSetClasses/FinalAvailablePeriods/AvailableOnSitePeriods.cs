using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class AvailableOnSitePeriods: IResultSet
    {
        public DateTime CourseEndDate{ get; set; }

        public int CourseID{ get; set; }

        public string CourseName { get; set; }

        public DateTime CourseStartDate { get; set; }

        public int FacilitatorID { get; set; }

        public string FacilitatorName { get; set; }

        public int VenueID { get; set; }

        public string VenueName { get; set; }

        public int VenueMaxCapicaty { get; set; }
    }
}