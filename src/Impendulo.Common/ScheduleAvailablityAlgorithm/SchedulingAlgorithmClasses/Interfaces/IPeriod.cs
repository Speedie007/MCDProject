using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
     public interface IPeriod: IDisposable
    {
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        int PeriodID { get; }
        string Description { get; }
    }
}
