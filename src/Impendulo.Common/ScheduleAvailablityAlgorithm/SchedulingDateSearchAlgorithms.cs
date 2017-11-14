using Impendulo.Common;
using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class SchedulingDateSearchAlgorithms
    {

        //public enum SearchTimeAhead : int
        //{
        //    OneMonth = 1,
        //    ThreeMonths = 3,
        //    SixMonth = 6,
        //    NineMonths = 9,
        //    TwelveMonths = 12,
        //    TwentyFourMonths = 24

        //}
        public SchedulingDateSearchAlgorithms(
            int CourseIDBookingFor,
            DateTime InitialDateToStartFrom,
            SearchTimeAhead AmountOfSearchTimeAhead,
             int CurriculumCourseID,
              EnumScheduleLocations ScheduleLocation)
        {
            this.CourseIDBookingFor = CourseIDBookingFor;
            this.InitalDate = InitialDateToStartFrom;
            this.AmountOfSearchTimeAhead = AmountOfSearchTimeAhead;
            this.CurriculumCourseID = CurriculumCourseID;

            FacilitatorBookedDateSets = new List<FacilitatorBookedDateSet>();
            FacilitatorAvailableDateSets = new List<FacilitatorAvailableDateSet>();

            VenueBookedDateSets = new List<VenueBookedDateSet>();
            VenueAvailableDateSets = new List<VenueAvailableDateSet>();

            //use this one for Off-Site 
            CourseByFacilitatorAvailableDateSets = new List<CourseByFacilitatorAvailableDateSet>();
            CourseByFacilitatorAndVenueAvailableDateSets = new List<CourseByFacilitatorAndVenueAvailableDateSet>();

            SetBookedDatesForFacilitators();
            SetBookedDatesForVenues();

            SetAvailableDatesForFacilitators();
            SetAvailableDatesForVenues();
            switch (ScheduleLocation)
            {
                case EnumScheduleLocations.Onsite:
                    SetPossibleCurriculumCourseDatesWithFacilitatorOnly();
                    SetPossibleCurriculumCourseDatesWithFacilitatorAndVenue();
                    break;
                case EnumScheduleLocations.OffSite:
                    SetPossibleCurriculumCourseDatesWithFacilitatorOnly();
                    break;
            }


        }

        public List<FacilitatorBookedDateSet> FacilitatorBookedDateSets { get; set; }
        public List<FacilitatorAvailableDateSet> FacilitatorAvailableDateSets { get; set; }

        public List<VenueBookedDateSet> VenueBookedDateSets { get; set; }
        public List<VenueAvailableDateSet> VenueAvailableDateSets { get; set; }

        public List<CourseByFacilitatorAvailableDateSet> CourseByFacilitatorAvailableDateSets { get; set; }
        public List<CourseByFacilitatorAndVenueAvailableDateSet> CourseByFacilitatorAndVenueAvailableDateSets { get; set; }


        private SearchTimeAhead AmountOfSearchTimeAhead { get; set; }
        private int CourseIDBookingFor { get; set; }
        private DateTime InitalDate { get; set; }
        private DateTime CurrentSearchDate { get; set; }

        private int CurriculumCourseID { get; set; }


        private FacilitatorBookedDateSet IsDateInAnySet(List<FacilitatorBookedDateSet> SetsOfBookedDatesPerFacilitator, DateTime DateIndex)
        {
            FacilitatorBookedDateSet Rtn = null;

            foreach (FacilitatorBookedDateSet FBDS in SetsOfBookedDatesPerFacilitator)
            {
                if ((FBDS.StartDate.Date <= DateIndex.Date) && (FBDS.EndDate.Date >= DateIndex))
                {
                    return FBDS;
                }
            }

            return Rtn;
        }
        private VenueBookedDateSet IsDateInAnySet(List<VenueBookedDateSet> SetsOfBookedDatesPreVenue, DateTime DateIndex)
        {
            VenueBookedDateSet Rtn = null;

            foreach (VenueBookedDateSet VBDS in SetsOfBookedDatesPreVenue)
            {
                if ((VBDS.StartDate.Date <= DateIndex.Date) && (VBDS.EndDate.Date >= DateIndex))
                {
                    return VBDS;
                }
            }

            return Rtn;
        }

        public class VenueBookedDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int VenueID { get; set; }
            public VenueBookedDateSet(int VenueID, DateTime StartDate, DateTime EndDate)
            {
                this.VenueID = VenueID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }
        public class VenueAvailableDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int VenueID { get; set; }
            public VenueAvailableDateSet(int VenueID, DateTime StartDate, DateTime EndDate)
            {
                this.VenueID = VenueID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }

        public class CourseByFacilitatorAvailableDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int FacilitatorID { get; set; }
            public int CurriculumCourseID { get; set; }
            public string CourseName { get; set; }
            public CourseByFacilitatorAvailableDateSet(string CourseName, int FacilitatorID, int CurriculumCourseID, DateTime StartDate, DateTime EndDate)
            {
                this.CourseName = CourseName;
                this.FacilitatorID = FacilitatorID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
                this.CurriculumCourseID = CurriculumCourseID;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }
        public class CourseByFacilitatorAndVenueAvailableDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int FacilitatorID { get; set; }
            public int CurriculumCourseID { get; set; }
            public string CourseName { get; set; }
            public int VenueID { get; set; }
            public CourseByFacilitatorAndVenueAvailableDateSet(string CourseName, int VenueID, int FacilitatorID, int CurriculumCourseID, DateTime StartDate, DateTime EndDate)
            {
                this.VenueID = VenueID;
                this.CourseName = CourseName;
                this.FacilitatorID = FacilitatorID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
                this.CurriculumCourseID = CurriculumCourseID;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }
        public class FacilitatorAvailableDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int FacilitatorID { get; set; }
            public FacilitatorAvailableDateSet(int FacilitatorID, DateTime StartDate, DateTime EndDate)
            {
                this.FacilitatorID = FacilitatorID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }
        public class FacilitatorBookedDateSet
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int FacilitatorID { get; set; }
            public FacilitatorBookedDateSet(int FacilitatorID, DateTime StartDate, DateTime EndDate)
            {
                this.FacilitatorID = FacilitatorID;
                this.StartDate = StartDate;
                this.EndDate = EndDate;
            }

            public int GetAmountOfDaysInSet()
            {
                int Rtn;
                Rtn = EndDate.Date.Subtract(StartDate).Days;
                return Rtn;
            }

        }

        private List<Facilitator> GetAllFacilitatorsForSelectedCourse()
        {
            List<Facilitator> Rtn = new List<Facilitator>();
            using (var Dbconnection = new MCDEntities())
            {

                List<Facilitator> f = (from a in Dbconnection.Facilitators
                                       from b in a.FacilitatorAssociatedCourses
                                       where b.CourseID == CourseIDBookingFor
                                       select a).ToList<Facilitator>();
                if (f != null)
                {
                    foreach (Facilitator FacilitatorObj in f)
                    {
                        Rtn.Add(FacilitatorObj);
                    }
                }
            };
            return Rtn;
        }
        private List<Venue> GetAllVenuesForSelectedCourse()
        {
            List<Venue> Rtn = new List<Venue>();
            using (var Dbconnection = new MCDEntities())
            {
                List<Venue> v = (from a in Dbconnection.Venues
                                 from b in a.VenueAssociatedCourses
                                 where b.CourseID == CourseIDBookingFor
                                 select a).ToList<Venue>();
                if (v != null)
                {
                    foreach (Venue VenueObj in v)
                    {
                        Rtn.Add(VenueObj);
                    }
                }
            };

            return Rtn;
        }

        private void SetAvailableDatesForFacilitators()
        {
            DateTime SearchEndDate;
            SearchEndDate = this.InitalDate.Date.AddMonths((int)this.AmountOfSearchTimeAhead).Date;

            //For EAch Facilitator
            if (FacilitatorBookedDateSets.Count > 0)
            {
                foreach (int FacilitatorID in (from a in FacilitatorBookedDateSets
                                               select a.FacilitatorID).Distinct<int>().ToList<int>())
                {
                    //Steps through their Set of Booked Dates to Determine Available DateSets
                    List<FacilitatorBookedDateSet> FBDS = (from a in FacilitatorBookedDateSets
                                                           where a.FacilitatorID == FacilitatorID
                                                           orderby a.StartDate
                                                           select a).ToList<FacilitatorBookedDateSet>();
                    // {
                    DateTime INITIAL_DATE = this.InitalDate.Date;
                    DateTime CURRENT_SEARCH_INDEX_DATE = this.InitalDate.Date;

                    //Initial Check To Catch the first Day - IF It Booked
                    FacilitatorBookedDateSet FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPerFacilitator: FBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    ////If Null No DataSet Found.
                    while (FBDS_RETURNED_AS_FOUND != null)
                    {
                        //Date Index Is Currently In With In A Booked Set.
                        FacilitatorAvailableDateSets.Add(new FacilitatorAvailableDateSet(FacilitatorID, INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.Date));
                        INITIAL_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                        CURRENT_SEARCH_INDEX_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                        FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPerFacilitator: FBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    }


                    //Checks Each Day Ahead to See If It Falls Into A Booked Time Period.
                    while (CURRENT_SEARCH_INDEX_DATE < SearchEndDate)
                    {
                        //Check if the Current Start Date Is In Any Of the Booked Date Sets 
                        FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPerFacilitator: FBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE.Date.AddDays(1));
                        //If Null No DataSet Found.
                        if (FBDS_RETURNED_AS_FOUND == null)
                        {
                            //Current DateIndex Not In Any Booked Date Sets
                            CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
                        }
                        else
                        {
                            //Date Index Is Currently In With In A Booked Set.
                            FacilitatorAvailableDateSets.Add(new FacilitatorAvailableDateSet(FacilitatorID, INITIAL_DATE.Date, FBDS_RETURNED_AS_FOUND.StartDate.AddDays(-1)));
                            INITIAL_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                            CURRENT_SEARCH_INDEX_DATE = FBDS_RETURNED_AS_FOUND.EndDate.Date;
                            // CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
                        }

                    }
                    FBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPerFacilitator: FBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    //Capture the Last Possiable Dte Set Avaiable
                    if (FBDS_RETURNED_AS_FOUND == null)
                    {
                        //Date Index Is Currently In With In A Booked Set.
                        FacilitatorAvailableDateSets.Add(new FacilitatorAvailableDateSet(FacilitatorID, INITIAL_DATE.Date, CURRENT_SEARCH_INDEX_DATE.Date));
                    }
                }
            }
            else
            {
                foreach (Facilitator f in GetAllFacilitatorsForSelectedCourse())
                {
                    FacilitatorAvailableDateSets.Add(new FacilitatorAvailableDateSet(f.FacilitatorID, InitalDate.Date, this.InitalDate.Date.AddMonths((int)this.AmountOfSearchTimeAhead).Date));
                }

            }


        }
        private void SetAvailableDatesForVenues()
        {
            DateTime SearchEndDate;
            SearchEndDate = this.InitalDate.Date.AddMonths((int)this.AmountOfSearchTimeAhead).Date;

            //For EAch Facilitator
            if (VenueBookedDateSets.Count > 0)
            {
                foreach (int VenueID in (from a in VenueBookedDateSets
                                         select a.VenueID).Distinct<int>().ToList<int>())
                {
                    //Steps through their Set of Booked Dates to Determine Available DateSets
                    List<VenueBookedDateSet> VBDS = (from a in VenueBookedDateSets
                                                     where a.VenueID == VenueID
                                                     orderby a.StartDate
                                                     select a).ToList<VenueBookedDateSet>();
                    // {
                    DateTime INITIAL_DATE = this.InitalDate.Date;
                    DateTime CURRENT_SEARCH_INDEX_DATE = this.InitalDate.Date;

                    //Initial Check To Catch the first Day - IF It Booked
                    VenueBookedDateSet VBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPreVenue: VBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    ////If Null No DataSet Found.
                    while (VBDS_RETURNED_AS_FOUND != null)
                    {
                        //Date Index Is Currently In With In A Booked Set.
                        VenueAvailableDateSets.Add(new VenueAvailableDateSet(VenueID, INITIAL_DATE.Date, VBDS_RETURNED_AS_FOUND.StartDate.Date));
                        INITIAL_DATE = VBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                        CURRENT_SEARCH_INDEX_DATE = VBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                        VBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPreVenue: VBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    }


                    //Checks Each Day Ahead to See If It Falls Into A Booked Time Period.
                    while (CURRENT_SEARCH_INDEX_DATE < SearchEndDate)
                    {
                        //Check if the Current Start Date Is In Any Of the Booked Date Sets 
                        VBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPreVenue: VBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE.Date.AddDays(1));
                        //If Null No DataSet Found.
                        if (VBDS_RETURNED_AS_FOUND == null)
                        {
                            //Current DateIndex Not In Any Booked Date Sets
                            CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
                        }
                        else
                        {
                            //Date Index Is Currently In With In A Booked Set.
                            VenueAvailableDateSets.Add(new VenueAvailableDateSet(VenueID, INITIAL_DATE.Date, VBDS_RETURNED_AS_FOUND.StartDate.AddDays(-1)));
                            INITIAL_DATE = VBDS_RETURNED_AS_FOUND.EndDate.Date.AddDays(1);
                            CURRENT_SEARCH_INDEX_DATE = VBDS_RETURNED_AS_FOUND.EndDate.Date;
                            // CURRENT_SEARCH_INDEX_DATE = CURRENT_SEARCH_INDEX_DATE.AddDays(1);
                        }

                    }
                    VBDS_RETURNED_AS_FOUND = IsDateInAnySet(SetsOfBookedDatesPreVenue: VBDS, DateIndex: CURRENT_SEARCH_INDEX_DATE);
                    //Capture the Last Possiable Dte Set Avaiable
                    if (VBDS_RETURNED_AS_FOUND == null)
                    {
                        //Date Index Is Currently In With In A Booked Set.
                        VenueAvailableDateSets.Add(new VenueAvailableDateSet(VenueID, INITIAL_DATE.Date, CURRENT_SEARCH_INDEX_DATE.Date));
                    }
                }
            }
            else
            {
                foreach (Venue v in GetAllVenuesForSelectedCourse())
                {
                    VenueAvailableDateSets.Add(new VenueAvailableDateSet(v.VenueID, InitalDate.Date, this.InitalDate.Date.AddMonths((int)this.AmountOfSearchTimeAhead).Date));
                }

            }


        }

        private void SetBookedDatesForFacilitators()
        {
            using (var Dbconnection = new MCDEntities())
            {
                foreach (Facilitator AvailableFacilitaor in GetAllFacilitatorsForSelectedCourse())
                {
                    var SetOfFacilitatorSchedules = (from a in Dbconnection.Schedules
                                                     where a.ScheduleStartDate >= InitalDate.Date &&
                                                     a.FacilitatorID == AvailableFacilitaor.FacilitatorID
                                                     select new
                                                     {
                                                         FacilitatorID = a.FacilitatorID,
                                                         ScheduleStartDate = a.ScheduleStartDate,
                                                         ScheduleCompletionDate = a.ScheduleCompletionDate

                                                     }).OrderBy(a => a.ScheduleStartDate).Distinct().ToList();
                    foreach (var FaciliatorBookedDates in SetOfFacilitatorSchedules)
                    {
                        FacilitatorBookedDateSet FBDS = new FacilitatorBookedDateSet(
                            FacilitatorID: FaciliatorBookedDates.FacilitatorID,
                             StartDate: FaciliatorBookedDates.ScheduleStartDate,
                             EndDate: FaciliatorBookedDates.ScheduleCompletionDate);
                        //add To List
                        FacilitatorBookedDateSets.Add(FBDS);
                    }
                }
            };
        }
        private void SetBookedDatesForVenues()
        {
            using (var Dbconnection = new MCDEntities())
            {
                foreach (Venue AvailableVenue in GetAllVenuesForSelectedCourse())
                {
                    var SetOfVenueUtilisedDates = (from a in Dbconnection.Schedules
                                                   where a.ScheduleStartDate >= InitalDate.Date &&
                                                   a.Venue.VenueID == AvailableVenue.VenueID
                                                   select new
                                                   {
                                                       VenueID = a.Venue.VenueID,
                                                       VenueUtilisedFromDate = a.ScheduleStartDate,
                                                       VenueUtilisedTillDate = a.ScheduleCompletionDate

                                                   }).OrderBy(a => a.VenueUtilisedFromDate).Distinct().ToList();
                    foreach (var VenueBookedDates in SetOfVenueUtilisedDates)
                    {
                        VenueBookedDateSet VBDS = new VenueBookedDateSet(
                            VenueID: VenueBookedDates.VenueID,
                             StartDate: VenueBookedDates.VenueUtilisedFromDate,
                             EndDate: VenueBookedDates.VenueUtilisedTillDate);
                        //add To List
                        VenueBookedDateSets.Add(VBDS);
                    }
                }
            };
        }

        private void SetPossibleCurriculumCourseDatesWithFacilitatorOnly()
        {
            List<CurriculumCourseDayCanBeScheduled> CurriculumCourseDayCanBeScheduledDays = new List<CurriculumCourseDayCanBeScheduled>();

            CurriculumCourse CC = null;
            using (var Dbconnection = new MCDEntities())
            {
                CC = (from a in Dbconnection.CurriculumCourses
                      where a.CurriculumCourseID == this.CurriculumCourseID
                      select a)
                             .Include(a => a.Course)
                             .FirstOrDefault<CurriculumCourse>();

                CurriculumCourseDayCanBeScheduledDays = (from a in CC.CurriculumCourseDayCanBeScheduleds
                                                         select a).ToList<CurriculumCourseDayCanBeScheduled>();

            };

            List<EnumDayOfWeeks> EnumDayOfWeeksList = new List<EnumDayOfWeeks>();
            foreach (CurriculumCourseDayCanBeScheduled CurriculumCourseDayCanBeScheduledDayObj in CurriculumCourseDayCanBeScheduledDays)
            {
                EnumDayOfWeeksList.Add((EnumDayOfWeeks)CurriculumCourseDayCanBeScheduledDayObj.DayOfWeekID);
            }

            foreach (FacilitatorAvailableDateSet FADS in FacilitatorAvailableDateSets)
            {
                DateTime INITIAL_DATE = FADS.StartDate;

                //Get First Dat That Scheduling Can Start
                DateTime FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FADS.StartDate;
                while (FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date < FADS.EndDate.Date)
                {
                    Boolean IsFirstDayFound = false;
                    while (!IsFirstDayFound)
                    {
                        if (IsFirstDateCanBeScheduled(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET, CurriculumCourseDayCanBeScheduledDays))
                        {
                            IsFirstDayFound = true;
                        }
                        else
                        {
                            FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.AddDays(1);
                        }
                    }

                    DateTime dd = new DateTime(2017, 07, 13);
                    dd = dd.AddDays(2);
                    //DateTime must be inclusive of the initial date so 1 day must be subtracted from the returned possible end date.
                    DateTime EndDateForCurrentPossibleSchedule = CustomDateTime.getCustomDateTime(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET, CC.Duration, EnumDayOfWeeksList).AddDays(-1);
                    if (EndDateForCurrentPossibleSchedule.Date <= FADS.EndDate.Date)
                    {
                        //Add Schedule Option To List
                        CourseByFacilitatorAvailableDateSets.Add(new CourseByFacilitatorAvailableDateSet(

                            CourseName: CC.Course.CourseName,
                            FacilitatorID: FADS.FacilitatorID,
                             CurriculumCourseID: this.CurriculumCourseID,
                              StartDate: FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date,
                               EndDate: EndDateForCurrentPossibleSchedule.Date));
                        FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = EndDateForCurrentPossibleSchedule.Date.AddDays(1);

                    }
                    else
                    {
                        FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FADS.EndDate.Date;
                    }

                }

            }
        }

        private void SetPossibleCurriculumCourseDatesWithFacilitatorAndVenue()
        {
            foreach (CourseByFacilitatorAvailableDateSet CBFADS in CourseByFacilitatorAvailableDateSets)
            {
                VenueAvailableDateSet VENUE_DATE_SET_FOUND = GetVenueForFacilitatorAvailableDateSet(StartDate: CBFADS.StartDate, EndDate: CBFADS.EndDate);
                if (VENUE_DATE_SET_FOUND != null)
                {

                    CourseByFacilitatorAndVenueAvailableDateSets.Add(new CourseByFacilitatorAndVenueAvailableDateSet(
                        CourseName: CBFADS.CourseName,
                        VenueID: VENUE_DATE_SET_FOUND.VenueID,
                        FacilitatorID: CBFADS.FacilitatorID,
                        CurriculumCourseID: CBFADS.CurriculumCourseID,
                         StartDate: CBFADS.StartDate,
                         EndDate: CBFADS.EndDate));
                }
            }
        }

        private VenueAvailableDateSet GetVenueForFacilitatorAvailableDateSet(DateTime StartDate, DateTime EndDate)
        {
            
            VenueAvailableDateSet Rtn = null;
            foreach (VenueAvailableDateSet VADS in VenueAvailableDateSets)
            {
                if (VADS.StartDate.Date <= StartDate.Date && VADS.EndDate.Date >= EndDate.Date)
                {
                    Rtn = VADS;
                }
            }
            return Rtn;
        }

        private Boolean IsFirstDateCanBeScheduled(DateTime CurrentDay, List<CurriculumCourseDayCanBeScheduled> CurriculumCourseDayCanBeScheduledList)
        {
            Boolean Rtn = false;


            EnumDayOfWeeks DayOfTheWeek = CustomDateTime.CustomDayOfTheWeek(CurrentDay.DayOfWeek);
            int AmountOfDayFound = (from a in CurriculumCourseDayCanBeScheduledList
                                    where a.DayOfWeekID == (int)DayOfTheWeek
                                    select a).ToList<CurriculumCourseDayCanBeScheduled>().Count();
            if (AmountOfDayFound > 0)
            {
                Rtn = true;
            }

            return Rtn;

        }
    }
}
