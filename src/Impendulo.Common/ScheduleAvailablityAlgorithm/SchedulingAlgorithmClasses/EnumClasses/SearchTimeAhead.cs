using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public enum SearchTimeAhead : int
    {
        OneMonth = 1,
        ThreeMonths = 3,
        SixMonths = 6,
        NineMonths = 9,
        TwelveMonths = 12,
        TwentyFourMonths = 24
    }
}