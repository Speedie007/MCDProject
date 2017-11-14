using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public interface ISearchResultSet
    {
        //int CourseID { get; set; }
        //string CourseName { get; set; }
        //int FacillitatorID { get; set; }
        //string FacilitatorName { get; set; }
        //DateTime StartDate { get; }
        //DateTime EndDate { get; }

        //void SetAvailableCoursePeriodForFacilitators();
        List<AvailableOnSitePeriods> GetAllAvailableOnStiePeriods();
        List<AvailableOffSitePeriods> GetAllAvailableOffSitePeriods();

    }
}