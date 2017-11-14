using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Impendulo.Common.ScheduleAvailablityAlgorithm
{
    public abstract class AbstactCourseAvaiablePeriod : AbstractPeriod
    {
        private List<IPeriod> SetOfBookedPeriods = new List<IPeriod>();
        private List<IPeriod> SetOfAvailablePeriods = new List<IPeriod>();
        private List<Facilitator> SetOfAllAvailableFacilitstors = new List<Facilitator>();
        private List<Venue> SetOfAllAvailableOnSiteVenues = new List<Venue>();
        private List<Address> SetOfAllAvailableOffSiteVenues = new List<Address>();
        private List<int> FacilitatorFilterList = new List<int>();
        private List<int> OnSiteFilterList = new List<int>();
        private List<int> OffSiteFilterList = new List<int>();

        public List<FacilitatorPeriod> FacilitatorBookedPeriods
        {
            get
            {
                List<FacilitatorPeriod> Rtn = (from a in SetOfBookedPeriods.OfType<FacilitatorPeriod>()
                                               select a).ToList<FacilitatorPeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<FacilitatorPeriod>();
            }
        }
        public List<FacilitatorPeriod> FacilitatorAvailablePeriods
        {
            get
            {
                List<FacilitatorPeriod> Rtn = (from a in SetOfAvailablePeriods.OfType<FacilitatorPeriod>()
                                               select a).ToList<FacilitatorPeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<FacilitatorPeriod>();
            }
        }
        public List<OnSiteVenuePeriod> OnSiteVenueAvailablePeriods
        {
            get
            {
                List<OnSiteVenuePeriod> Rtn = (from a in SetOfAvailablePeriods.OfType<OnSiteVenuePeriod>()
                                               select a).ToList<OnSiteVenuePeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<OnSiteVenuePeriod>();
            }
        }
        public List<Individual> AvailableFacilitators
        {
            get
            {
                return this.SetOfAllAvailableFacilitstors.Select(a => a.Individual).ToList<Individual>();
            }
        }
        public List<Venue> AvailableOnSiteVenues
        {
            get
            {
                return this.SetOfAllAvailableOnSiteVenues;
            }
        }
        public List<Address> AvailableOffSiteVenues
        {
            get
            {
                return this.SetOfAllAvailableOffSiteVenues;
            }
        }
        public List<OnSiteVenuePeriod> OnSiteVenueBookedPeriods
        {
            get
            {
                List<OnSiteVenuePeriod> Rtn = (from a in SetOfBookedPeriods.OfType<OnSiteVenuePeriod>()
                                               select a).ToList<OnSiteVenuePeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<OnSiteVenuePeriod>();
            }
        }
        public List<OffSiteVenuePeriod> OffSiteVenueAvailablePeriods
        {
            get
            {
                List<OffSiteVenuePeriod> Rtn = (from a in SetOfAvailablePeriods.OfType<OffSiteVenuePeriod>()
                                                select a).ToList<OffSiteVenuePeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<OffSiteVenuePeriod>();
            }
        }
        public List<OffSiteVenuePeriod> OffSiteVenueBookedPeriods
        {
            get
            {
                List<OffSiteVenuePeriod> Rtn = (from a in SetOfBookedPeriods.OfType<OffSiteVenuePeriod>()
                                                select a).ToList<OffSiteVenuePeriod>();
                if (Rtn != null)
                    return Rtn;
                else
                    return new List<OffSiteVenuePeriod>();
            }
        }
        public int CourseID { get { return this.PeriodID; } }
        public string CourseName { get { return this.Description; } }
        private int _CourseDuration;
        public int CourseDuration { get { return _CourseDuration; } }
        public DateTime CourseSearchPeriodStartDate { get { return base.StartDate; } }
        public DateTime CourseSearchPeriodEndDate { get { return base.EndDate; } }
        private int OffSiteCompanyID { get; set; }
        private EnumPreLoadType PrelodaingType { get; set; }

        public AbstactCourseAvaiablePeriod(
            DateTime CourseSearchPeriodStartDate,
            DateTime CourseSearchPeriodEndDate,
            int CourseID,
            string CourseDescription,
            int CourseDuration,
            EnumPreLoadType PreLoadingType,
            List<int> FacilitatorFilterList = null,
            List<int> OnSiteFilterList = null,
            List<int> OffSiteFilterList = null,
            int OffSiteID = 0
            ) : base(CourseSearchPeriodStartDate, CourseSearchPeriodEndDate, CourseID, CourseDescription)
        {
            this._CourseDuration = CourseDuration;
            this.OffSiteCompanyID = OffSiteID;
            this.FacilitatorFilterList = FacilitatorFilterList;
            this.OnSiteFilterList = OnSiteFilterList;
            this.OffSiteFilterList = OffSiteFilterList;
            this.PrelodaingType = PreLoadingType;
            this.LoadFacilitators();
            this.LoadOnSiteVenues();
            this.LoadOffSiteVenues();
            //_SetOfDaysOfWeekCourseCanBeScheulded = SetOfDaysOfWeekCourseCanBeScheulded;
        }
        //public abstract FacilitatorPeriod GetAllFacilitatorBookedPeriods();
        //public abstract FacilitatorPeriod GetAllFacilitatorAvaiablePeriods();
        //public abstract OnSiteVenuePeriod GetAllOnSiteBookedPeriods();
        //public abstract OnSiteVenuePeriod GetAllOnSiteAvailableOnSitePeriods();
        //public abstract OffSiteVenuePeriod GetAllOffSiteBookedPeriods();
        //public abstract OffSiteVenuePeriod GetAllOffSiteAvaiablePeriods();

        //private void SetAllDayThatCourseCanBeSchuled()
        //{
        //    List<CurriculumCourseDayCanBeScheduled> CurriculumCourseDayCanBeScheduledDays = new List<CurriculumCourseDayCanBeScheduled>();

        //    CurriculumCourse CC = null;
        //    using (var Dbconnection = new MCDEntities())
        //    {
        //        CC = (from a in Dbconnection.CurriculumCourses
        //              where a.CurriculumCourseID == this.CourseID
        //              select a)
        //                     .Include(a => a.Course)
        //                     .FirstOrDefault<CurriculumCourse>();

        //        CurriculumCourseDayCanBeScheduledDays = (from a in CC.CurriculumCourseDayCanBeScheduleds
        //                                                 select a).ToList<CurriculumCourseDayCanBeScheduled>();

        //    };

        //    List<EnumDayOfWeeks> EnumDayOfWeeksList = new List<EnumDayOfWeeks>();
        //    foreach (CurriculumCourseDayCanBeScheduled CurriculumCourseDayCanBeScheduledDayObj in CurriculumCourseDayCanBeScheduledDays)
        //    {
        //        EnumDayOfWeeksList.Add((EnumDayOfWeeks)CurriculumCourseDayCanBeScheduledDayObj.DayOfWeekID);
        //    }
        //}

        private void DetermineAvailableDates(List<IPeriod> BookedDatePeriods)
        {
            /*Steps that Determine the Avaiable Dates For a set
             * 1. General formulation of  aset Follows: { Iniital DateSet + (N Amount Of DateSets - 1) + Last Date Set }
             * Example is as follows: Given STARTDATE: 2017-06-01 and ENDDATE: 2017-01-30 for the 
             * 2. Proccess Flow: 
             * 2.1: - Initial DateSet is: The Period from the Starting Date to FIRST D
             * 
             * */


            /*Ensure the DATASET is sorted*/
            List<IPeriod> SortedDataSet = (from a in BookedDatePeriods
                                           orderby a.StartDate, a.EndDate
                                           select a).ToList<IPeriod>();

            /*Initilise the Sorting Parameters*/




            /*Section One: Find the Initial DateSet*/

            //First Date Set From List from the data Set
            IPeriod FirstDateSet = SortedDataSet.First<IPeriod>();

            //If start date is the same a Initial Start Date of the Search Period then there is no Start Period.
            if (CourseSearchPeriodStartDate.Date < FirstDateSet.StartDate.Date)
            {
                if (FirstDateSet is FacilitatorPeriod)
                {
                    SetOfAvailablePeriods.Add(new FacilitatorPeriod(
                        StartDate: this.CourseSearchPeriodStartDate.Date,
                        EndDate: FirstDateSet.StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description));

                }
                if (FirstDateSet is OnSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OnSiteVenuePeriod(
                        StartDate: CourseSearchPeriodStartDate.Date,
                        EndDate: FirstDateSet.StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description,
                         VenueMaxCapicaty: ((OnSiteVenuePeriod)FirstDateSet).VenueMaxCapicaty)
                    );
                }
                if (FirstDateSet is OffSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OffSiteVenuePeriod(
                        StartDate: CourseSearchPeriodStartDate.Date,
                        EndDate: FirstDateSet.StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description,
                         VenueAssociatedCompanyName: ((OffSiteVenuePeriod)FirstDateSet).VenueAssociatedCompany)
                    );
                }
            }

            /*Step Two Get All Possible dates in between*/
            for (int iIndex = 0; iIndex < SortedDataSet.Count - 1; iIndex++)
            {

                if (SortedDataSet[iIndex] is FacilitatorPeriod)
                {
                    SetOfAvailablePeriods.Add(new FacilitatorPeriod(
                        StartDate: SortedDataSet[iIndex].EndDate.Date.AddDays(1),
                        EndDate: SortedDataSet[iIndex + 1].StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description
                        ));

                }
                if (SortedDataSet[iIndex] is OnSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OnSiteVenuePeriod(
                       StartDate: SortedDataSet[iIndex].EndDate.Date.AddDays(1),
                        EndDate: SortedDataSet[iIndex + 1].StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description
                        , VenueMaxCapicaty: ((OnSiteVenuePeriod)SortedDataSet[iIndex]).VenueMaxCapicaty));
                }
                if (SortedDataSet[iIndex] is OffSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OffSiteVenuePeriod(
                       StartDate: SortedDataSet[iIndex].EndDate.Date.AddDays(1),
                        EndDate: SortedDataSet[iIndex + 1].StartDate.AddDays(-1),
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description
                        , VenueAssociatedCompanyName: ((OffSiteVenuePeriod)SortedDataSet[iIndex]).VenueAssociatedCompany));
                }
            }

            /*Step 3 Get Last Remaining Period if exisits*/
            IPeriod LastDateSet = SortedDataSet.Last<IPeriod>();
            if (LastDateSet.EndDate.Date < this.CourseSearchPeriodEndDate.Date)
            {
                if (LastDateSet is FacilitatorPeriod)
                {
                    SetOfAvailablePeriods.Add(new FacilitatorPeriod(
                        StartDate: LastDateSet.EndDate.Date.AddDays(1),
                        EndDate: this.CourseSearchPeriodEndDate.Date,
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description
                        ));

                }
                if (LastDateSet is OnSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OnSiteVenuePeriod(
                        StartDate: LastDateSet.EndDate.Date.AddDays(1),
                        EndDate: this.CourseSearchPeriodEndDate.Date,
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description,
                        VenueMaxCapicaty: ((OnSiteVenuePeriod)LastDateSet).VenueMaxCapicaty));
                }
                if (LastDateSet is OffSiteVenuePeriod)
                {

                    SetOfAvailablePeriods.Add(new OffSiteVenuePeriod(
                        StartDate: LastDateSet.EndDate.Date.AddDays(1),
                        EndDate: this.CourseSearchPeriodEndDate.Date,
                        PeriodID: FirstDateSet.PeriodID,
                        Description: FirstDateSet.Description,
                        VenueAssociatedCompanyName: ((OffSiteVenuePeriod)LastDateSet).VenueAssociatedCompany));
                }
            }


        }
        private void LoadOnSiteVenues()
        {
            using (var Dbconnection = new MCDEntities())
            {
                /*Step 1:
                 * Load All Available Venues That Can Host This Course.
                 * **************************************************************/

                if (OnSiteFilterList != null)
                {
                    SetOfAllAvailableOnSiteVenues = (from a in Dbconnection.Venues
                                                     from b in a.VenueAssociatedCourses
                                                     where b.CourseID == this.CourseID
                                                      && OnSiteFilterList.Contains(a.VenueID)
                                                     select a)
                                                 .ToList<Venue>();
                }
                else
                {
                    SetOfAllAvailableOnSiteVenues = (from a in Dbconnection.Venues
                                                     from b in a.VenueAssociatedCourses
                                                     where b.CourseID == this.CourseID
                                                     select a)
                                                  .ToList<Venue>();
                }

                if (SetOfAllAvailableFacilitstors.Count == 0 && PrelodaingType == EnumPreLoadType.PreloadingOnSite)
                {
                    System.Windows.Forms.MessageBox.Show("There Are No Venues Linked To Support This Course, Close And Configure the Venues for this Course First before proceeding.", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                /*Step 2
                 * Get 
                 * All Current Scheduled Courses in this Search Period For THIS Course selected.
                 * *******************************************************************************/
                List<int> AllOnSiteVenuesAvailable = (from a in SetOfAllAvailableOnSiteVenues
                                                      select a.VenueID).ToList<int>();
                var SetOfVenueCurrentlyBookedPeriods = (from a in Dbconnection.Schedules
                                                        where
                                                           a.ScheduleStartDate >= CourseSearchPeriodStartDate.Date &&
                                                           a.ScheduleCompletionDate <= CourseSearchPeriodEndDate.Date &&
                                                           AllOnSiteVenuesAvailable.Contains(a.OnSiteSchedule.VenueID)
                                                        select new
                                                        {
                                                            VenueID = a.OnSiteSchedule.VenueID,
                                                            ScheduleStartDate = a.ScheduleStartDate,
                                                            ScheduleCompletionDate = a.ScheduleCompletionDate,
                                                            VenueName = a.OnSiteSchedule.Venue.VenueName,
                                                            VenueMaxCapicity = a.OnSiteSchedule.Venue.VenueMaxCapacity

                                                        }).OrderBy(a => a.ScheduleStartDate)
                                                                    .ThenBy(a => a.ScheduleCompletionDate)
                                                                        .Distinct()
                                                                            .ToList();

                /*Step 3
                 * Set All Booked and Avaiable Periods 
                 * ***************************************/


                /*Step 3.1 Sets Booked Period For Each Facilitrator
                 * If None Exists in this Period then Sets all Avaiable Facilitators Periods to the same as the Search Period START and END Periods.
                 * ******************************************/
                if (SetOfVenueCurrentlyBookedPeriods.Count > 0)
                {
                    foreach (var SelectedVenueBookedPeriod in SetOfVenueCurrentlyBookedPeriods)
                    {
                        SetOfBookedPeriods.Add(new OnSiteVenuePeriod(
                            StartDate: SelectedVenueBookedPeriod.ScheduleStartDate.Date,
                            EndDate: SelectedVenueBookedPeriod.ScheduleCompletionDate.Date,
                            PeriodID: SelectedVenueBookedPeriod.VenueID,
                             Description: SelectedVenueBookedPeriod.VenueName,
                              VenueMaxCapicaty: SelectedVenueBookedPeriod.VenueMaxCapicity
                            ));
                    }
                    /*Step 3.1.1 Detertermine the Avaiable Periods foeach Facilitator
                     * ****************************************************************/

                    //Get all Possible Venues that don't have anything booked
                    foreach (Venue v in SetOfAllAvailableOnSiteVenues)
                    {
                        List<IPeriod> VP = (from a in SetOfBookedPeriods.OfType<OnSiteVenuePeriod>()
                                            where a.VenueID == v.VenueID
                                            //&& a.VenueLocation == EnumScheduleLocations.Onsite
                                            select a).ToList<IPeriod>();
                        if (VP.Count > 0)
                        {
                            //Process the set of periods to determine the Avaiable Periods of Facilitator
                            DetermineAvailableDates(VP);
                        }
                        else
                        {
                            /*None Exists so Current Facilitator are available the full period of the search.
                            ********************************************************************************/
                            SetOfAvailablePeriods.Add(new OnSiteVenuePeriod(
                            StartDate: CourseSearchPeriodStartDate.Date,
                            EndDate: CourseSearchPeriodEndDate.Date,
                             PeriodID: v.VenueID,
                             Description: v.VenueName,
                             VenueMaxCapicaty: v.VenueMaxCapacity
                            ));
                        }
                    }

                }
                else
                {
                    /*None Exists so All Facilitators are available the full period of the search.
                     * ****************************************************************************/
                    foreach (Venue v in SetOfAllAvailableOnSiteVenues)
                    {
                        SetOfAvailablePeriods.Add(new OnSiteVenuePeriod(
                           StartDate: CourseSearchPeriodStartDate.Date,
                           EndDate: CourseSearchPeriodEndDate.Date,
                            PeriodID: v.VenueID,
                            Description: v.VenueName,
                            VenueMaxCapicaty: v.VenueMaxCapacity
                           ));
                    }
                }
            };
        }
        private void LoadOffSiteVenues()
        {
            using (var Dbconnection = new MCDEntities())
            {
                /*Step 1:
                 * Load All Available Venues That Can Host This Course.
                 * **************************************************************/
                if (OffSiteFilterList != null)
                {
                    SetOfAllAvailableOffSiteVenues = (from a in Dbconnection.Companies
                                                      from b in a.Addresses
                                                      where a.CompanyID == this.OffSiteCompanyID
                                                       && OffSiteFilterList.Contains(b.AddressID)
                                                      select b)
                                                 .ToList<Address>();
                }
                else
                {
                    SetOfAllAvailableOffSiteVenues = (from a in Dbconnection.Companies
                                                      from b in a.Addresses
                                                      where a.CompanyID == this.OffSiteCompanyID
                                                      select b)
                                                  .ToList<Address>();
                }

                if (SetOfAllAvailableOffSiteVenues.Count == 0 && PrelodaingType == EnumPreLoadType.PreLoadingOffSite)
                {
                    System.Windows.Forms.MessageBox.Show("There Are No Available Address Location Configured for this Company To Indicate were this This Course will be conducted, Close And Configure the Company Address/Locations First for this Company First before proceeding.", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                /*Step 2
                 * Get 
                 * All Current Scheduled Companies in this Search Period For THIS Course selected.
                 * *******************************************************************************/
                List<int> AllOffSiteVenuesAvailable = (from a in SetOfAllAvailableOffSiteVenues
                                                       select a.AddressID).ToList<int>();

                var SetOfVenueCurrentlyBookedPeriods = (from a in Dbconnection.Schedules
                                                        from b in a.OffSiteSchedule.Address.Companies
                                                        where
                                                           a.ScheduleStartDate >= CourseSearchPeriodStartDate.Date &&
                                                           a.ScheduleCompletionDate <= CourseSearchPeriodEndDate.Date &&
                                                           AllOffSiteVenuesAvailable.Contains(a.OffSiteSchedule.AddressID)
                                                        select new
                                                        {
                                                            VenueID = a.OffSiteSchedule.AddressID,
                                                            ScheduleStartDate = a.ScheduleStartDate,
                                                            ScheduleCompletionDate = a.ScheduleCompletionDate,
                                                            VenueName = a.OffSiteSchedule.Address.AddressDescription,
                                                            CompanyName = b.CompanyName
                                                        }).OrderBy(a => a.ScheduleStartDate)
                                                                    .ThenBy(a => a.ScheduleCompletionDate)
                                                                        .Distinct()
                                                                            .ToList();

                /*Step 3
                 * Set All Booked and Avaiable Periods 
                 * ***************************************/


                /*Step 3.1 Sets Booked Period For Each Facilitrator
                 * If None Exists in this Period then Sets all Avaiable Facilitators Periods to the same as the Search Period START and END Periods.
                 * ******************************************/
                if (SetOfVenueCurrentlyBookedPeriods.Count > 0)
                {
                    foreach (var SelectedVenueBookedPeriod in SetOfVenueCurrentlyBookedPeriods)
                    {
                        SetOfBookedPeriods.Add(new OffSiteVenuePeriod(
                            StartDate: SelectedVenueBookedPeriod.ScheduleStartDate.Date,
                            EndDate: SelectedVenueBookedPeriod.ScheduleCompletionDate.Date,
                            PeriodID: SelectedVenueBookedPeriod.VenueID,
                             Description: SelectedVenueBookedPeriod.VenueName,
                             VenueAssociatedCompanyName: SelectedVenueBookedPeriod.CompanyName
                            ));
                    }
                    /*Step 3.1.1 Detertermine the Avaiable Periods foeach Facilitator
                     * ****************************************************************/

                    //Get all Possible Venues that don't have anything booked
                    foreach (Address v in SetOfAllAvailableOffSiteVenues)
                    {
                        List<IPeriod> VP = (from a in SetOfBookedPeriods.OfType<OffSiteVenuePeriod>()
                                            where a.VenueID == v.AddressID
                                            select a).ToList<IPeriod>();
                        if (VP.Count > 0)
                        {
                            //Process the set of periods to determine the Avaiable Periods of Facilitator
                            DetermineAvailableDates(VP);
                        }
                        else
                        {
                            /*None Exists so Current Facilitator are available the full period of the search.
                            ********************************************************************************/
                            string CompanyName = v.Companies.FirstOrDefault<Company>().CompanyName;
                            SetOfAvailablePeriods.Add(new OffSiteVenuePeriod(
                            StartDate: CourseSearchPeriodStartDate.Date,
                            EndDate: CourseSearchPeriodEndDate.Date,
                             PeriodID: v.AddressID,
                             Description: v.AddressDescription,
                             VenueAssociatedCompanyName: CompanyName
                            ));
                        }
                    }

                }
                else
                {
                    /*None Exists so All Facilitators are available the full period of the search.
                     * ****************************************************************************/
                    foreach (Address v in SetOfAllAvailableOffSiteVenues)
                    {
                        string CompanyName = v.Companies.FirstOrDefault<Company>().CompanyName;
                        SetOfAvailablePeriods.Add(new OffSiteVenuePeriod(
                           StartDate: CourseSearchPeriodStartDate.Date,
                           EndDate: CourseSearchPeriodEndDate.Date,
                            PeriodID: v.AddressID,
                            Description: v.AddressDescription,
                            VenueAssociatedCompanyName: CompanyName
                           ));
                    }
                }


            };
        }
        private void LoadFacilitators()
        {

            using (var Dbconnection = new MCDEntities())
            {
                /*Step 1:
                 * Load All Available Facilitators That Can Persent This Course.
                 * **************************************************************/
                //If Null it is unfiltered ELSE Uses Filter List To Filter By Facilitator.....
                if (FacilitatorFilterList != null)
                {
                    SetOfAllAvailableFacilitstors = (from a in Dbconnection.Facilitators
                                                     from b in a.FacilitatorAssociatedCourses
                                                     where b.CourseID == this.CourseID
                                                     && FacilitatorFilterList.Contains(a.FacilitatorID)
                                                     select a)
                                                 .Include(a => a.Individual)
                                                 .ToList<Facilitator>();
                }
                else
                {
                    SetOfAllAvailableFacilitstors = (from a in Dbconnection.Facilitators
                                                     from b in a.FacilitatorAssociatedCourses
                                                     where b.CourseID == this.CourseID
                                                     select a)
                                                 .Include(a => a.Individual)
                                                 .ToList<Facilitator>();
                }

                if (SetOfAllAvailableFacilitstors.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("There Are No Facilitators Linked To Support This Course, Close And Configure the Facilitators for this Course First before proceeding.", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }


                /*Step 2
                 * Get 
                 * All Current Scheduled Courses in this Search Period For THIS Course selected.
                 * *******************************************************************************/
                List<int> AllFacilitatorsAvailable = (from a in SetOfAllAvailableFacilitstors
                                                      select a.FacilitatorID).ToList<int>();
                var SetOfFacilitatorCurrentlyBookedPeriods = (from a in Dbconnection.Schedules
                                                              where
                                                                 a.ScheduleStartDate >= CourseSearchPeriodStartDate.Date &&
                                                                 a.ScheduleCompletionDate <= this.CourseSearchPeriodEndDate.Date &&
                                                                 AllFacilitatorsAvailable.Contains(a.FacilitatorID)
                                                              select new
                                                              {
                                                                  FacilitatorID = a.FacilitatorID,
                                                                  ScheduleStartDate = a.ScheduleStartDate,
                                                                  ScheduleCompletionDate = a.ScheduleCompletionDate,
                                                                  FullName = a.Facilitator.Individual.IndividualFirstName + " " + a.Facilitator.Individual.IndividualLastname

                                                              }).OrderBy(a => a.ScheduleStartDate)
                                                                    .ThenBy(a => a.ScheduleCompletionDate)
                                                                        .Distinct()
                                                                            .ToList();

                /*Step 3
                 * Set All Booked and Avaiable Periods 
                 * ***************************************/


                /*Step 3.1 Sets Booked Period For Each Facilitrator
                 * If None Exists in this Period then Sets all Avaiable Facilitators Periods to the same as the Search Period START and END Periods.
                 * ******************************************/
                if (SetOfFacilitatorCurrentlyBookedPeriods.Count > 0)
                {
                    //adds All Booked FAcilitator Periods into the "SelectedFacilitatorBookedPeriod" DataStore(LIST)
                    foreach (var SelectedFacilitatorBookedPeriod in SetOfFacilitatorCurrentlyBookedPeriods)
                    {
                        SetOfBookedPeriods.Add(new FacilitatorPeriod(
                            StartDate: SelectedFacilitatorBookedPeriod.ScheduleStartDate.Date,
                            EndDate: SelectedFacilitatorBookedPeriod.ScheduleCompletionDate.Date,
                            PeriodID: SelectedFacilitatorBookedPeriod.FacilitatorID,
                             Description: SelectedFacilitatorBookedPeriod.FullName
                            ));
                    }
                    /*Step 3.1.1 Detertermine the Avaiable Periods foeach Facilitator
                     * ****************************************************************/

                    //Get all Possible Facilitators that don't have anything booked
                    //List<int> AllFacilitatorsAvailable = (from a in SetOfAllAvailableFacilitstors
                    //                                      select a.FacilitatorID).ToList<int>();
                    foreach (Facilitator f in SetOfAllAvailableFacilitstors)
                    {
                        List<IPeriod> FP = (from a in SetOfBookedPeriods.OfType<FacilitatorPeriod>()
                                            where a.FacillitatorID == f.FacilitatorID
                                            select a).ToList<IPeriod>();
                        if (FP.Count > 0)
                        {
                            //Process the set of periods to determine the Avaiable Periods of Facilitator
                            DetermineAvailableDates(FP);
                        }
                        else
                        {
                            /*None Exists so Current Facilitator are available the full period of the search.
                            ********************************************************************************/
                            SetOfAvailablePeriods.Add(new FacilitatorPeriod(
                            StartDate: this.CourseSearchPeriodStartDate.Date,
                            EndDate: this.CourseSearchPeriodEndDate.Date,
                             PeriodID: f.FacilitatorID,
                             Description: f.Individual.FullName
                            ));
                        }
                    }

                }
                else
                {
                    /*None Exists so All Facilitators are available the full period of the search.
                     * ****************************************************************************/
                    foreach (Facilitator f in SetOfAllAvailableFacilitstors)
                    {
                        SetOfAvailablePeriods.Add(new FacilitatorPeriod(
                            StartDate: this.CourseSearchPeriodStartDate.Date,
                            EndDate: this.CourseSearchPeriodEndDate.Date,
                             PeriodID: f.FacilitatorID,
                             Description: f.Individual.FullName
                            ));
                    }
                }
            };
        }

    }
}