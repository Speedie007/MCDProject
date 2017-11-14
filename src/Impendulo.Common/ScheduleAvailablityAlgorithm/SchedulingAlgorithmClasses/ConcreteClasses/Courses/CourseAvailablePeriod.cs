using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Impendulo.Common.Enum;
using Impendulo.Data.Models;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public class CourseAvailablePeriod : AbstactCourseAvaiablePeriod, ISearchResultSet
    {

        private List<EnumDayOfWeeks> _SetOfDaysOfWeekCourseCanBeScheulded = new List<EnumDayOfWeeks>();

        private List<AvailableCoursePeriodByFacilitatorResultSet> _ListOfAvailableCoursePeriodByFacilitator = new List<AvailableCoursePeriodByFacilitatorResultSet>();

        public CourseAvailablePeriod(DateTime CourseSearchPeriodStartDate, DateTime CourseSearchPeriodEndDate, int CourseID, string CourseDescription, int CourseDuration, EnumPreLoadType PreLoadingType, List<EnumDayOfWeeks> SetOfDaysOfWeekCourseCanBeScheulded, List<int> FacilitatorFilterList = null, List<int> OnSiteFilterList = null, List<int> OffSiteFilterList = null, int OffSiteID = 0) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription, CourseDuration, PreLoadingType, FacilitatorFilterList, OnSiteFilterList, OffSiteFilterList, OffSiteID)
        {
            this._SetOfDaysOfWeekCourseCanBeScheulded = SetOfDaysOfWeekCourseCanBeScheulded;
        }

        //public CourseAvailablePeriod(DateTime CourseSearchPeriodStartDate, DateTime CourseSearchPeriodEndDate, int CourseID, string CourseDescription, int CourseDuration, List<EnumDayOfWeeks> SetOfDaysOfWeekCourseCanBeScheulded, int OffSiteID = 0) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription, CourseDuration, OffSiteID)
        //{
        //    this._SetOfDaysOfWeekCourseCanBeScheulded = SetOfDaysOfWeekCourseCanBeScheulded;
        //    //this.SetAvailableCoursePeriodForFacilitators();
        //}

        public List<AvailableCoursePeriodByFacilitatorResultSet> ListOfAvailableCoursePeriodByFacilitator
        {
            get { return _ListOfAvailableCoursePeriodByFacilitator; }
            set { _ListOfAvailableCoursePeriodByFacilitator = value; }
        }

        private List<EnumDayOfWeeks> SetOfDaysOfWeekCourseCanBeScheulded
        {
            get
            {
                return _SetOfDaysOfWeekCourseCanBeScheulded;
            }
        }

        //private Boolean IsFirstDateCanBeScheduled(DateTime CurrentDay)
        //{
        //    Boolean Rtn = false;


        //    EnumDayOfWeeks DayOfTheWeek = CustomDateTime.CustomDayOfTheWeek(CurrentDay.DayOfWeek);
        //    int AmountOfDayFound = (from a in SetOfDaysOfWeekCourseCanBeScheulded
        //                            where a == DayOfTheWeek//a.HasFlag(DayOfTheWeek)
        //                            select a).ToList<EnumDayOfWeeks>().Count();
        //    if (AmountOfDayFound > 0)
        //    {
        //        Rtn = true;
        //    }

        //    return Rtn;

        //}

        private void SetAvailableCoursePeriodForFacilitators()
        {
            foreach (FacilitatorPeriod FAP in FacilitatorAvailablePeriods)
            {
                DateTime INITIAL_DATE = FAP.StartDate;

                //Get First Dat That Scheduling Can Start
                DateTime FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FAP.StartDate;
                while (FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date < FAP.EndDate.Date)
                {
                    Boolean IsFirstDayFound = false;
                    while (!IsFirstDayFound)
                    {
                        EnumDayOfWeeks DayOfTheWeek = CustomDateTime.CustomDayOfTheWeek(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.DayOfWeek);
                        if ((from a in SetOfDaysOfWeekCourseCanBeScheulded
                             where a == DayOfTheWeek//a.HasFlag(DayOfTheWeek)
                             select a).ToList<EnumDayOfWeeks>().Count() > 0)
                        {
                            IsFirstDayFound = true;
                        }
                        else
                        {
                            FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.AddDays(1);
                        }
                    }


                    //DateTime must be inclusive of the initial date so 1 day must be subtracted from the returned possible end date.
                    DateTime EndDateForCurrentPossibleSchedule = CustomDateTime.getCustomDateTime(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date.AddDays(-1), CourseDuration, SetOfDaysOfWeekCourseCanBeScheulded);
                    if (EndDateForCurrentPossibleSchedule.Date <= FAP.EndDate.Date)
                    {

                        //Add Schedule Option To List
                        ListOfAvailableCoursePeriodByFacilitator.Add(new AvailableCoursePeriodByFacilitatorResultSet()
                        {
                            CourseID = this.CourseID,
                            CourseName = this.CourseName,
                            FacilitatorID = FAP.FacillitatorID,
                            FacilitatorName = FAP.FacilitatorName,
                            CourseStartDate = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date,
                            CourseEndDate = EndDateForCurrentPossibleSchedule.Date
                        });
                        FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = EndDateForCurrentPossibleSchedule.Date.AddDays(1);

                    }
                    else
                    {
                        FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FAP.EndDate.Date;
                    }

                }

            }
        }
        public List<AvailableOnSitePeriods> GetAllAvailableOnStiePeriods()
        {

            List<AvailableOnSitePeriods> AOSP = new List<AvailableOnSitePeriods>();
            DateTime FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = CourseSearchPeriodStartDate.Date;
            while (FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date < CourseSearchPeriodEndDate.Date)
            {
                // Ensure that this course can start on this day... eg Monday through Thursday

                Boolean IsFirstDayFound = false;
                while (!IsFirstDayFound)
                {
                    EnumDayOfWeeks DayOfTheWeek = CustomDateTime.CustomDayOfTheWeek(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.DayOfWeek);
                    if ((from a in SetOfDaysOfWeekCourseCanBeScheulded
                         where a == DayOfTheWeek//a.HasFlag(DayOfTheWeek)
                         select a).ToList<EnumDayOfWeeks>().Count() > 0)
                    {
                        IsFirstDayFound = true;
                    }
                    else
                    {
                        FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.AddDays(1);
                    }
                }
                //Inclusive of the start date ( that why subtrtact day from the start date.
                DateTime EndDateForCurrentPossibleSchedule = CustomDateTime.getCustomDateTime(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date.AddDays(-1), CourseDuration, SetOfDaysOfWeekCourseCanBeScheulded);


                ///get all possible cominations for the period
                foreach (FacilitatorPeriod FP in FacilitatorAvailablePeriods.OrderBy(a => a.StartDate))
                {
                    //Deterine if the Period is greater than the course period(Course Duration) else cant schedule in this period.
                    if (FP.GetAmountOfDaysInPeriod() >= CourseDuration)
                    {
                        //Determine if the facilitator could fall into this course period 
                        if (FP.StartDate.Date <= FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date && EndDateForCurrentPossibleSchedule.Date <= FP.EndDate.Date)
                        {
                            // this period can fit into this course period.
                            //List<OnSiteVenuePeriod> OnSiteVenueAvailablePeriods
                            foreach (OnSiteVenuePeriod OSVAP in OnSiteVenueAvailablePeriods)
                            {
                                //Deterine if the Period is greater than the course period(Course Duration) else cant schedule in this period.
                                if (OSVAP.GetAmountOfDaysInPeriod() >= CourseDuration)
                                {
                                    if (OSVAP.StartDate <= FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date && EndDateForCurrentPossibleSchedule.Date <= OSVAP.EndDate.Date)
                                    {
                                        AOSP.Add(new AvailableOnSitePeriods()
                                        {
                                            CourseID = base.CourseID,
                                            CourseName = base.CourseName,
                                            CourseStartDate = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date,
                                            CourseEndDate = EndDateForCurrentPossibleSchedule.Date,
                                            FacilitatorID = FP.FacillitatorID,
                                            FacilitatorName = FP.FacilitatorName,
                                            VenueID = OSVAP.VenueID,
                                            VenueName = OSVAP.VenueName,
                                            VenueMaxCapicaty = OSVAP.VenueMaxCapicaty
                                        });
                                    }
                                }
                            }
                        }
                    }


                }
                FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = EndDateForCurrentPossibleSchedule.AddDays(1);

            }
            return AOSP;
        }

        public List<AvailableOffSitePeriods> GetAllAvailableOffSitePeriods()
        {
            List<AvailableOffSitePeriods> AOSP = new List<AvailableOffSitePeriods>();
            DateTime FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = CourseSearchPeriodStartDate.Date;
            while (FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date < CourseSearchPeriodEndDate.Date)
            {
                // Ensure that this course can start on this day... eg Monday through Thursday
                while (FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date < CourseSearchPeriodEndDate.Date)
                {
                    Boolean IsFirstDayFound = false;
                    while (!IsFirstDayFound)
                    {
                        EnumDayOfWeeks DayOfTheWeek = CustomDateTime.CustomDayOfTheWeek(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.DayOfWeek);
                        if ((from a in SetOfDaysOfWeekCourseCanBeScheulded
                             where a == DayOfTheWeek//a.HasFlag(DayOfTheWeek)
                             select a).ToList<EnumDayOfWeeks>().Count() > 0)
                        {
                            IsFirstDayFound = true;
                        }
                        else
                        {
                            FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.AddDays(1);
                        }
                    }
                    //Inclusive of the start date ( that why subtrtact day from the start date.
                    DateTime EndDateForCurrentPossibleSchedule = CustomDateTime.getCustomDateTime(FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date.AddDays(-1), CourseDuration, SetOfDaysOfWeekCourseCanBeScheulded);


                    ///get all possible cominations for the period
                    foreach (FacilitatorPeriod FP in FacilitatorAvailablePeriods.OrderBy(a => a.StartDate))
                    {
                        //Deterine if the Period is greater than the course period(Course Duration) else cant schedule in this period.
                        if (FP.GetAmountOfDaysInPeriod() >= CourseDuration)
                        {
                            //Determine if the facilitator could fall into this course period 
                            if (FP.StartDate.Date <= FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date && EndDateForCurrentPossibleSchedule.Date <= FP.EndDate.Date)
                            {
                                // this period can fit into this course period.
                                //List<OnSiteVenuePeriod> OnSiteVenueAvailablePeriods
                                foreach (OffSiteVenuePeriod OSVAP in OffSiteVenueAvailablePeriods)
                                {
                                    //Deterine if the Period is greater than the course period(Course Duration) else cant schedule in this period.
                                    if (OSVAP.GetAmountOfDaysInPeriod() >= CourseDuration)
                                    {
                                        if (OSVAP.StartDate <= FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date && EndDateForCurrentPossibleSchedule.Date <= OSVAP.EndDate.Date)
                                        {
                                            AOSP.Add(new AvailableOffSitePeriods()
                                            {
                                                CourseID = base.CourseID,
                                                CourseName = base.CourseName,
                                                CourseStartDate = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.Date,
                                                CourseEndDate = EndDateForCurrentPossibleSchedule.Date,
                                                FacilitatorID = FP.FacillitatorID,
                                                FacilitatorName = FP.FacilitatorName,
                                                VenueID = OSVAP.VenueID,
                                                VenueName = OSVAP.VenueName,
                                                VenueAssociatedCompany = OSVAP.VenueAssociatedCompany
                                            });
                                        }
                                    }
                                }
                            }
                        }


                    }
                    FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET = FIRST_DAY_CAN_BE_SCHEDULED_FROM_SET.AddDays(1);
                }
            }
            return AOSP;
        }
        //public List<AvailableOnSitePeriods> GetAllAvailableOnStiePeriods()
        //{
        //    List<AvailableOnSitePeriods> AOSP = new List<AvailableOnSitePeriods>();

        //    foreach (AvailableCoursePeriodByFacilitatorResultSet ACPBFRS in ListOfAvailableCoursePeriodByFacilitator)
        //    {
        //        foreach (OnSiteVenuePeriod OSVAP in OnSiteVenueAvailablePeriods)
        //        {

        //            if (ACPBFRS.CourseStartDate.Date >= OSVAP.StartDate.Date && ACPBFRS.CourseEndDate.Date < OSVAP.EndDate.Date)
        //            {
        //                AOSP.Add(new AvailableOnSitePeriods()
        //                {
        //                    CourseID = ACPBFRS.CourseID,
        //                    CourseName = ACPBFRS.CourseName,
        //                    CourseStartDate = ACPBFRS.CourseStartDate,
        //                    CourseEndDate = ACPBFRS.CourseEndDate,
        //                    FacilitatorID = ACPBFRS.FacilitatorID,
        //                    FacilitatorName = ACPBFRS.FacilitatorName,
        //                    VenueID = OSVAP.VenueID,
        //                    VenueName = OSVAP.VenueName,
        //                    VenueMaxCapicaty = OSVAP.VenueMaxCapicaty
        //                });
        //            }
        //        }
        //    }

        //    return AOSP;
        //}

        //public List<AvailableOffSitePeriods> GetAllAvailableOffSitePeriods()
        //{
        //    List<AvailableOffSitePeriods> AOSP = new List<AvailableOffSitePeriods>();

        //    foreach (AvailableCoursePeriodByFacilitatorResultSet ACPBFRS in ListOfAvailableCoursePeriodByFacilitator)
        //    {
        //        foreach (OffSiteVenuePeriod OSVAP in OffSiteVenueAvailablePeriods)
        //        {

        //            if (ACPBFRS.CourseStartDate.Date >= OSVAP.StartDate.Date && ACPBFRS.CourseEndDate.Date <= OSVAP.EndDate.Date)
        //            {
        //                AOSP.Add(new AvailableOffSitePeriods()
        //                {
        //                    CourseID = ACPBFRS.CourseID,
        //                    CourseName = ACPBFRS.CourseName,
        //                    CourseStartDate = ACPBFRS.CourseStartDate,
        //                    CourseEndDate = ACPBFRS.CourseEndDate,
        //                    FacilitatorID = ACPBFRS.FacilitatorID,
        //                    FacilitatorName = ACPBFRS.FacilitatorName,
        //                    VenueID = OSVAP.VenueID,
        //                    VenueName = OSVAP.VenueName,
        //                    VenueAssociatedCompany = OSVAP.VenueAssociatedCompany
        //                });
        //            }
        //        }
        //    }

        //    return AOSP;
        //}




        //private VenueAvailableDateSet GetVenueForFacilitatorAvailableDateSet(DateTime StartDate, DateTime EndDate)
        //{

        //    VenueAvailableDateSet Rtn = null;
        //    foreach (VenueAvailableDateSet VADS in VenueAvailableDateSets)
        //    {
        //        if (VADS.StartDate.Date <= StartDate.Date && VADS.EndDate.Date >= EndDate.Date)
        //        {
        //            Rtn = VADS;
        //        }
        //    }
        //    return Rtn;
        //}
    }
}