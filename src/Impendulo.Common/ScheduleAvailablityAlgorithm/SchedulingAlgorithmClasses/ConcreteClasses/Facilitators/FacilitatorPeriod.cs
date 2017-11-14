using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class FacilitatorPeriod : AbstractPeriod
    {
        public int FacillitatorID
        {
            get { return this.PeriodID; }
        }
        
        public string FacilitatorName { get { return this.Description; } }
        public FacilitatorPeriod(DateTime StartDate, DateTime EndDate, int PeriodID, string Description) : base(StartDate, EndDate, PeriodID, Description)
        {
        }

       
    }
}