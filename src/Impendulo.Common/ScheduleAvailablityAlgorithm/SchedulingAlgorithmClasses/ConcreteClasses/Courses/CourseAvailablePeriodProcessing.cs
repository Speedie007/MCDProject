using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class CourseAvailablePeriodProcessing : AbstactCourseAvaiablePeriod
    {

        //private int _OffSiteID = 0;

        //public CourseAvailablePeriodProcessing(DateTime CourseSearchPeriodStartDate, DateTime CourseSearchPeriodEndDate, int CourseID, string CourseDescription, int OffSiteID = 0) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription, OffSiteID)
        //{
        //}
        //    public CourseAvailablePeriodProcessing(DateTime CourseSearchPeriodStartDate, DateTime CourseSearchPeriodEndDate, int CourseID, string CourseDescription,List<EnumDayOfWeeks> SetOfDaysOfWeekCourseCanBeScheduled, int OffSiteID = 0) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription, SetOfDaysOfWeekCourseCanBeScheduled,0)
        //{
        //this._OffSiteID = OffSiteID;

        //this.LoadAllBookedFacilitators();
        //this.LoadAllBookedVenues();
        ///*//check if there are any FFAcilitators booked in this period
        //* if there are then calculate the Avaiable Periods
        //* ELSE
        //* If None are booked then set the Start and End date for each available  facilitator to the The Initail/Start date of the Search and the end DATE for the search period.
        //*/
        //if ((from a in SetOfBookedPeriods select a).OfType<FacilitatorPeriod>().Count() > 0)
        //{
        //    this.DetermineAvailableDates((from a in SetOfBookedPeriods
        //                                  orderby a.StartDate, a.EndDate
        //                                  select a).OfType<FacilitatorPeriod>().ToList<IPeriod>());
        //}
        //else
        //{
        //    foreach (Facilitator AvailableFacilitaor in SetOfAllAvailableFacilitstors)
        //    {

        //        SetOfAvailablePeriods.Add(new FacilitatorAvailablePeriod(
        //        StartDate: base.CourseSearchPeriodStartDate.Date,
        //        EndDate: base.CourseSearchPeriodEndDate.Date,
        //        PeriodID: AvailableFacilitaor.FacilitatorID,
        //        Description: AvailableFacilitaor.Individual.IndividualFirstName + " " + AvailableFacilitaor.Individual.IndividualLastname));
        //    }
        //}
        ///*//check if there are any FFAcilitators booked in this period
        //* if there are then calculate the Avaiable Periods
        //* ELSE
        //* If None are booked then set the Start and End date for each available  facilitator to the The Initail/Start date of the Search and the end DATE for the search period.
        //*/
        //if ((from a in SetOfBookedPeriods select a).OfType<VenuePeriod>().Count() > 0)
        //{
        //    this.DetermineAvailableDates((from a in SetOfBookedPeriods
        //                                  orderby a.StartDate, a.EndDate
        //                                  select a).OfType<VenuePeriod>().ToList<IPeriod>());
        //}
        //else
        //{
        //    //foreach (Venue AvailableVenue in SetOfAllAvailableOnSiteVenues)
        //    //{
        //    //    EnumScheduleLocations LocationType = ((VenuePeriod)AvailableVenue).VenueLocation;
        //    //    SetOfAvailablePeriods.Add(new VenuePeriod(
        //    //    StartDate: StartDate.Date,
        //    //    EndDate: CourseSearchPeriodEndDate.Date,
        //    //    PeriodID: AvailableVenue.VenueID,
        //    //    Description: AvailableVenue.VenueName,
        //    //     VenueLocation: AvailableVenue.));
        //    //}
        //}
        //}

        // public abstract ISearchResultSet GetResultSet();
        //public override IPeriod IsDateInAnySet(List<IPeriod> SetsOfBookedDates, DateTime DateIndex)
        //{
        //    IPeriod Rtn = null;

        //    foreach (IPeriod BookedPeriod in SetsOfBookedDates)
        //    {
        //        if ((BookedPeriod.StartDate.Date <= DateIndex.Date) && (BookedPeriod.EndDate.Date >= DateIndex))
        //        {
        //            return BookedPeriod;
        //        }
        //    }
        //    return Rtn;

        //}



        //private void SetAvailableDates<T>(List<IPeriod> BookedDatePeriods)
        //{
        //    foreach (int ParentProcessingID in (from a in BookedDatePeriods
        //                                        select a.PeriodID).Distinct<int>().ToList<int>())
        //    {
        //        //Steps through their Set of Booked Dates to Determine Available DateSets vfor iteher the Facilitators or the Venue depending of which dataset is being passed through.
        //        List<IPeriod> BDP = (from a in BookedDatePeriods
        //                                 //ParentProcessingID is iether the FacilitatorID or the VenueID Depending on whioch type of set is being worked with.
        //                             where a.PeriodID == ParentProcessingID
        //                             orderby a.StartDate, a.EndDate
        //                             select a).ToList<IPeriod>();
        //        // {
        //        DateTime INITIAL_DATE = this.CourseSearchPeriodStartDate.Date;
        //        DateTime CURRENT_SEARCH_INDEX_DATE = this.CourseSearchPeriodStartDate.Date;


        //        //Initial Check To Catch the first Day - IF It Booked
        //        IPeriod FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDates: BDP, DateIndex: CURRENT_SEARCH_INDEX_DATE);
        //        ////If Null No DataSet Found.
        //        while (FBDS_RETURNED_AS_FOUND != null)
        //        {

        //            //Date Index Is Currently In With In A Booked Set.
        //            if (FBDS_RETURNED_AS_FOUND is FacilitatorPeriod)
        //            {
        //                this.SetOfAvailablePeriods.Add(new FacilitatorAvailablePeriod(INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.Date, PeriodID: ParentProcessingID, Description: FBDS_RETURNED_AS_FOUND.Description));
        //            }
        //            if (FBDS_RETURNED_AS_FOUND is VenuePeriod)
        //            {
        //                this.SetOfAvailablePeriods.Add(new VenueAvailablePeriod(INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.Date, PeriodID: ParentProcessingID, Description: FBDS_RETURNED_AS_FOUND.Description));
        //            }
        //            //this.SetOfAvailablePeriods.Add(new FacilitatorAvailableDateSet(FacilitatorID, INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.Date));
        //            INITIAL_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
        //            CURRENT_SEARCH_INDEX_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
        //            FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDates: BDP, DateIndex: CURRENT_SEARCH_INDEX_DATE);
        //        }


        //        //Checks Each Day Ahead to See If It Falls Into A Booked Time Period.
        //        while (CURRENT_SEARCH_INDEX_DATE.Date <= base.CourseSearchPeriodEndDate.Date)
        //        {
        //            //Check if the Current Start Date Is In Any Of the Booked Date Sets 
        //            FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDates: BDP, DateIndex: CURRENT_SEARCH_INDEX_DATE.Date.AddDays(1));
        //            //If Null No DataSet Found.
        //            if (FBDS_RETURNED_AS_FOUND == null)
        //            {
        //                //Current DateIndex Not In Any Booked Date Sets
        //                CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
        //            }
        //            else
        //            {
        //                ////FacilitatorAvailableDateSets.Add(new FacilitatorAvailableDateSet(FacilitatorID, INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.AddDays(-1)));
        //                ////INITIAL_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
        //                ////CURRENT_SEARCH_INDEX_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date;
        //                //Date Index Is Currently In With In A Booked Set.
        //                if (FBDS_RETURNED_AS_FOUND is FacilitatorPeriod)
        //                {
        //                    SetOfAvailablePeriods.Add(new FacilitatorAvailablePeriod(INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.AddDays(-1), PeriodID: ParentProcessingID, Description: FBDS_RETURNED_AS_FOUND.Description));
        //                }
        //                if (FBDS_RETURNED_AS_FOUND is VenuePeriod)
        //                {
        //                    SetOfAvailablePeriods.Add(new VenueAvailablePeriod(INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.AddDays(-1), PeriodID: ParentProcessingID, Description: FBDS_RETURNED_AS_FOUND.Description));
        //                }
        //                INITIAL_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
        //                CURRENT_SEARCH_INDEX_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date;
        //                // CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
        //            }

        //        }

        //        IPeriod LastDatePeriodInSet = (from a in BDP
        //                                       orderby a.StartDate, a.EndDate
        //                                       select a).Last<IPeriod>();

        //        if (LastDatePeriodInSet.EndDate.Date < base.CourseSearchPeriodEndDate.Date)
        //        {
        //            if (LastDatePeriodInSet is FacilitatorPeriod)
        //            {
        //                SetOfAvailablePeriods.Add(new FacilitatorAvailablePeriod(
        //                   StartDate: LastDatePeriodInSet.EndDate.Date,
        //                   EndDate: base.CourseSearchPeriodEndDate.Date,
        //                   PeriodID: ParentProcessingID,
        //                   Description: LastDatePeriodInSet.Description));
        //            }
        //            if (LastDatePeriodInSet is VenuePeriod)
        //            {
        //                EnumScheduleLocations CuurentLocation = ((VenuePeriod)LastDatePeriodInSet).VenueLocation;
        //                SetOfAvailablePeriods.Add(new VenuePeriod(
        //                   StartDate: INITIAL_DATE.Date,
        //                   EndDate: base.CourseSearchPeriodEndDate.Date,
        //                   PeriodID: ParentProcessingID,
        //                   Description: LastDatePeriodInSet.Description,
        //                    VenueLocation: CuurentLocation));
        //            }
        //        }
        //    }
        //}

        //protected void LoadAllBookedVenues()
        //{
        //    //Loads All OnSiteVenues
        //    using (var Dbconnection = new MCDEntities())
        //    {
        //        SetOfAllAvailableOnSiteVenues = (from a in Dbconnection.Venues
        //                                         from b in a.VenueAssociatedCourses
        //                                         where b.CourseID == this.CourseID
        //                                         select a).ToList<Venue>();
        //        foreach (Venue AvailableVenue in SetOfAllAvailableOnSiteVenues)
        //        {
        //            var SetOfVenueUtilisedDates = (from a in Dbconnection.Schedules
        //                                           where a.ScheduleStartDate >= this.CourseSearchPeriodStartDate.Date &&
        //                                           a.OnSiteSchedule.VenueID == AvailableVenue.VenueID
        //                                           select new
        //                                           {
        //                                               VenueID = a.OnSiteSchedule.VenueID,
        //                                               VenueUtilisedFromDate = a.ScheduleStartDate,
        //                                               VenueUtilisedTillDate = a.ScheduleCompletionDate,
        //                                               VenueName = a.OnSiteSchedule.Venue.VenueName

        //                                           }).OrderBy(a => a.VenueUtilisedFromDate).Distinct().ToList();
        //            foreach (var VenueBookedDates in SetOfVenueUtilisedDates)
        //            {
        //                OnSiteVenuePeriod VBDS = new OnSiteVenuePeriod(
        //                     StartDate: VenueBookedDates.VenueUtilisedFromDate,
        //                     EndDate: VenueBookedDates.VenueUtilisedTillDate,
        //                    PeriodID: VenueBookedDates.VenueID,
        //                     Description: VenueBookedDates.VenueName,
        //                      VenueLocation: EnumScheduleLocations.Onsite);
        //                //add To List
        //                SetOfBookedPeriods.Add(VBDS);
        //            }
        //        }
        //    };
        //}





        //protected void LoadAllBookedFacilitators()

        //{

        //    using (var Dbconnection = new MCDEntities())
        //    {
        //        SetOfAllAvailableFacilitstors = (from a in Dbconnection.Facilitators
        //                                         from b in a.FacilitatorAssociatedCourses
        //                                         where b.CourseID == this.CourseID
        //                                         select a).ToList<Facilitator>();
        //        foreach (Facilitator AvailableFacilitaor in (from a in SetOfAllAvailableFacilitstors select a).ToList<Facilitator>())
        //        {
        //            var SetOfFacilitatorSchedules = (from a in Dbconnection.Schedules
        //                                             where a.ScheduleStartDate >= CourseSearchPeriodStartDate.Date &&
        //                                             a.FacilitatorID == AvailableFacilitaor.FacilitatorID
        //                                             select new
        //                                             {
        //                                                 FacilitatorID = a.FacilitatorID,
        //                                                 ScheduleStartDate = a.ScheduleStartDate,
        //                                                 ScheduleCompletionDate = a.ScheduleCompletionDate,
        //                                                 FullName = a.Facilitator.Individual.IndividualFirstName + " " + a.Facilitator.Individual.IndividualLastname

        //                                             }).OrderBy(a => a.ScheduleStartDate).Distinct().ToList();
        //            foreach (var FaciliatorBookedDates in SetOfFacilitatorSchedules)
        //            {
        //                FacilitatorPeriod FBP = new FacilitatorPeriod(
        //                    StartDate: FaciliatorBookedDates.ScheduleStartDate,
        //                    EndDate: FaciliatorBookedDates.ScheduleCompletionDate,
        //                    PeriodID: FaciliatorBookedDates.FacilitatorID,
        //                    Description: FaciliatorBookedDates.FullName);
        //                //add To List
        //                SetOfBookedPeriods.Add(FBP);
        //            }
        //        }
        //    };
        //}
        public CourseAvailablePeriodProcessing(DateTime CourseSearchPeriodStartDate, DateTime CourseSearchPeriodEndDate, int CourseID, string CourseDescription, int CourseDuration, EnumPreLoadType PreLoadingType, List<int> FacilitatorFilterList = null, List<int> OnSiteFilterList = null, List<int> OffSiteFilterList = null, int OffSiteID = 0) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription, CourseDuration, PreLoadingType, FacilitatorFilterList, OnSiteFilterList, OffSiteFilterList, OffSiteID)
        {
        }
    }
}