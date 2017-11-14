using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public interface IResultSet
    {
         DateTime CourseEndDate { get; set; }

         int CourseID { get; set; }

         string CourseName { get; set; }

         DateTime CourseStartDate { get; set; }

    }
}