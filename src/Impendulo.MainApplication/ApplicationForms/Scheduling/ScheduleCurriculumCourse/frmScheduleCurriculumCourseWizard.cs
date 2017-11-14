using Impendulo.Common.Enum;
using Impendulo.Common.ScheduleAvailablityAlgorithm;

using Impendulo.Data.Models;
using Impendulo.Deployment.Company;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static Impendulo.Common.ScheduleAvailablityAlgorithm.SchedulingDateSearchAlgorithms;

namespace Impendulo.Deployment.Scheduling
{
    public partial class frmScheduleCurriculumCourseWizard : MetroForm
    {
        private Boolean mustSaveItems = false;

        private SearchTimeAhead CurrentPeriodAhead { get; set; }

        public Schedule CurrentScheduleConfiguration { get; set; }

        private DateTime PossibleStartDate { get; set; }

        private int CurrentlySelectedItemRowIndex { get; set; }

        private enumLastGridUsed LastGridUsed { get; set; }


        // private EnumScheduleLocations CurrentSiteSelection { get; set; }
        public Employee CurrentEmployeeLoggedIn
        {
            get;
            set;
        }
        /// <summary>
        /// Currently Selected CurriculumCourseEnrollment Object - with the Following collections loaded
        /// 1. -
        /// </summary>
        private CurriculumCourseEnrollment CurrentSelectedCurriculumCourseEnrollment { get; set; }


        /// <summary>
        /// Course Obj - contains the Linked Facilitators for the course and Venues to select from
        /// </summary>
        private CurriculumCourse CurrentlySelectedCurriculumCourseToSchedule { get; set; }

        public int CurrentPosition { get; set; }
        public frmScheduleCurriculumCourseWizard(DateTime InitialStartDate, CurriculumCourseEnrollment CurrentSelectedCurriculumCourseEnrollment)
        {
            CurrentlySelectedItemRowIndex = 0;
            LastGridUsed = enumLastGridUsed.notYetSelectedAnyField;
            this.CurrentSelectedCurriculumCourseEnrollment = CurrentSelectedCurriculumCourseEnrollment;
            //PossibleStartDate = Common.CustomDateTime.getCustomDateTime(
            //                                   CurrentDate: DateTime.Now.Date.AddDays(1),
            //                                   AmountDaysToAdd: 0,
            //                                   DaysCanSchedule: GetDayThatCurriculumCourseCanBeScheduled());
            PossibleStartDate = InitialStartDate;
            CurrentPeriodAhead = SearchTimeAhead.ThreeMonths;
            InitializeComponent();
            //dtScheduleFromCustomStartDateSelector.Format = DateTimePickerFormat.Custom;
            //dtScheduleFromCustomStartDateSelector.CustomFormat = "MMMM dd, yyyy - dddd";
            dtScheduleFromCustomStartDateSelector.MinDate = PossibleStartDate.Date;

        }

        private void frmScheduleCurriculumCourseWizard_Load(object sender, EventArgs e)
        {
            if (CurrentScheduleConfiguration == null)
            {
                CurrentScheduleConfiguration = new Schedule()
                {
                    ScheduleLocationID = (int)EnumScheduleLocations.Onsite,
                    CurriculumCourseEnrollmentID = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourseEnrollmentID,
                    EnrollmentID = CurrentSelectedCurriculumCourseEnrollment.EnrollmentID,
                    ScheduleStatusID = (int)EnumScheduleStatuses.Reserved
                };
            }
            dtScheduleFromCustomStartDateSelector.SelectionStart = PossibleStartDate.Date;
            if (CurrentEmployeeLoggedIn == null)
            {
                /*
             * Thismust be Commmented out or removed in the production version this is just for Develpoement Testing.
             */
                using (var Dbconnection = new MCDEntities())
                {
                    CurrentEmployeeLoggedIn = (from a in Dbconnection.Employees
                                               select a).FirstOrDefault<Employee>();
                };

                /***************************************************************************************/
                // MessageBox.Show("It is Required that you be logged in to use the feature.\n Login and try again!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this.Close();

            }

            //Loads the Wizard
            this.CurrentPosition = 0;
            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();

            //pre populates the required information to determine the Available dates
            this.prePopulateCurrentlySelectedCourse();
        }

        #region Pre-Populate Information
        private void prePopulateCurrentlySelectedCourse()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedCurriculumCourseToSchedule = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse;
                Dbconnection.CurriculumCourses.Attach(CurrentlySelectedCurriculumCourseToSchedule);
                //loads the days that can be scheduled.
                if (!Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Collection(a => a.CurriculumCourseDayCanBeScheduleds).IsLoaded)
                {
                    Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Collection(a => a.CurriculumCourseDayCanBeScheduleds).Load();
                }
                //load the course that associated with the Curriculum Course
                if (!Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Reference<Course>(a => a.Course).IsLoaded)
                {
                    Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Reference<Course>(a => a.Course).Load();
                }

                //if (!Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Collection(a => a.Course.VenueAssociatedCourses).IsLoaded)
                //{
                //    Dbconnection.Entry(CurrentlySelectedCurriculumCourseToSchedule).Collection(a => a.Course.VenueAssociatedCourses).Load();
                //}


            };
        }
        #endregion

        #region Wizard Comopnents
        #region "Navigation Controls"
        private void navigateForward()
        {
            if (ValidateForwardStep())
            {
                if (CurrentPosition + 1 < MainflowLayoutPanel.Controls.Count)
                {
                    //if step validation is passed the next window is display by incrementing the CurrentPosition Counter.
                    CurrentPosition++;
                }
                else
                {
                    //on last step which will close if the finish b=button is selected.
                    DialogResult res = MessageBox.Show("Are Details Correct?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (DialogResult.Yes == res)
                    {
                        this.mustSaveItems = true;
                        this.Close();
                    }
                }
                this.setCenterDisplayPanels();
                this.setNavigationControls();
                this.loadupStep();
            }
        }
        private void navigateBackwards()
        {
            if (ValidateBackwardStep())
            {
                if (CurrentPosition - 1 >= 0)
                {
                    CurrentPosition--;
                }
                else
                {

                    //CurrentPosition = 5;
                }
            }

            //Hide All Panels inside the MainFlowPanel
            //MainflowLayoutPanel
            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();
        }


        private void setNavigationControls()
        {
            if (CurrentPosition == 0)
            {
                btnPreviousSection.Visible = false;
                btnNextSection.Text = "Next";
            }
            else
            {
                if (CurrentPosition == MainflowLayoutPanel.Controls.Count - 1)
                {
                    btnNextSection.Text = "Save";

                }
                else
                {
                    btnNextSection.Text = "Next";

                }
                btnPreviousSection.Visible = true;
            }
            int iAmountOfSteps = 0;
            foreach (var Control in tableLayoutPanel5.Controls)
            {
                if (Control is Label)
                {
                    iAmountOfSteps++;
                    //NavigationPanel
                    var lblObj = (Label)Control;
                    if (Convert.ToInt32(lblObj.Tag.ToString()) == CurrentPosition)
                    {
                        lblObj.Font = new Font(lblObj.Font, FontStyle.Bold);
                        lblObj.Margin = new Padding(6, 7, 3, 3);
                    }
                    else
                    {
                        lblObj.Font = new Font(lblObj.Font, FontStyle.Regular);
                        lblObj.Margin = new Padding(3, 7, 3, 3);
                    }
                }
            }
            double dblPercentageComplete = (((Convert.ToDouble(CurrentPosition + 1) / Convert.ToDouble(iAmountOfSteps))) * 100);
            wizardStepProgressBar.Value = Convert.ToInt32(dblPercentageComplete);

        }
        private void setCenterDisplayPanels()
        {
            foreach (Control gbControl in MainflowLayoutPanel.Controls)
            {
                if (gbControl is GroupBox)
                {
                    var gbObj = (GroupBox)gbControl;
                    gbObj.Hide();
                }
            }
            foreach (Control Control in MainflowLayoutPanel.Controls)
            {
                if (Control is GroupBox)
                {
                    var gbObj = (GroupBox)Control;
                    if (Convert.ToInt32(gbObj.Tag.ToString()) == CurrentPosition)
                    {
                        gbObj.Show();
                        gbObj.Width = MainflowLayoutPanel.Width;
                        gbObj.Height = MainflowLayoutPanel.Height;
                    }
                    else
                    {
                        gbObj.Hide();
                        gbObj.Width = 0;
                        gbObj.Height = 0;
                    }
                }
            }
        }
        #endregion

        #region Wizard Methods
        private void loadupStep()
        {
            switch (CurrentPosition)
            {
                case 0:
                    //Student Details
                    this.loadupStepOne();
                    break;
                case 1:
                    //Select Company if Off Site
                    this.loadupStepTwo();
                    break;
                case 2:
                    //Select Date
                    this.loadupStepThree();
                    break;
                case 3:
                    this.loadupStepFour();
                    break;
                case 4:
                    this.loadupStepFive();
                    break;
                case 5:
                    this.loadupStepSix();
                    break;
                case 6:
                    this.loadupStepSeven();
                    break;
                case 7:
                    this.loadupStepEight();
                    break;
                default:

                    break;
            }

        }
        private void btnPreviousSection_Click(object sender, EventArgs e)
        {
            navigateBackwards();
        }

        private void btnNextSection_Click(object sender, EventArgs e)
        {
            navigateForward();
        }
        private Boolean ValidateForwardStep()
        {

            Boolean bRtn = true;
            switch (CurrentPosition)
            {
                case 0:
                    //Select On-Site or Off-Site
                    if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
                    {
                        CurrentPosition++;
                        //CurrentScheduleConfiguration.Companies.Clear();
                    }


                    break;
                case 1:
                    //Select Compnay if Off Site

                    break;
                case 2:
                    //Schedule Selection
                    if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
                    {

                    }

                    if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.OffSite)
                    {

                    }
                    break;
                case 3:
                    break;

                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

                default:
                    bRtn = false;
                    break;
            }

            return bRtn;
        }
        private Boolean ValidateBackwardStep()
        {

            Boolean bRtn = true;
            switch (CurrentPosition)
            {
                case 0:



                    break;
                case 1:

                    break;
                case 2:
                    //Select Compnay if Off Site
                    if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
                    {
                        CurrentPosition--;
                        //CurrentScheduleConfiguration.Companies.Clear();
                    }
                    break;
                case 3:
                    break;

                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;

                default:
                    bRtn = false;
                    break;
            }

            return bRtn;
        }

        #region "Each Wizard Page Loadup"
        private void loadupStepOne()
        {

        }

        private void loadupStepTwo()
        {
        }

        private void loadupStepThree()
        {

            dgvAvailableDateToSchedule.SelectTab(1);
            // dgvAvailableDateToSchedule.SelectTab(2);

            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                this.refreshStartAndEndDates();
                this.refreshPossibleSchedulingDates();
                this.refreshCurrentlyScheduledDates();
                splitContainerAvailableDates.Panel1Collapsed = false;
                splitContainerAvailableDates.Panel2Collapsed = true;
                splitContainerVenueFilterControls.Panel1Collapsed = false;
                splitContainerVenueFilterControls.Panel2Collapsed = true;

            }
            else
            {
                this.refreshStartAndEndDates();
                this.refreshPossibleSchedulingDates(OffSiteID: CurrentlySelectedCompany.CompanyID);
                this.refreshCurrentlyScheduledDates();
                splitContainerAvailableDates.Panel1Collapsed = true;
                splitContainerAvailableDates.Panel2Collapsed = false;
                splitContainerVenueFilterControls.Panel1Collapsed = true;
                splitContainerVenueFilterControls.Panel2Collapsed = false;
            }


        }
        #region ScheduleFilters


        private void populateOnsiteFilters(CourseAvailablePeriod ScheduleAlgorthim)
        {
            this.populateFacilitatorFilter(ScheduleAlgorthim);
            this.populateOnSiteFilter(ScheduleAlgorthim);
        }

        private void populateOffSiteFilters(CourseAvailablePeriod ScheduleAlgorthim)
        {
            this.populateFacilitatorFilter(ScheduleAlgorthim);
            this.populateOffSiteFilter(ScheduleAlgorthim);
        }
        private void populateFacilitatorFilter(CourseAvailablePeriod ScheduleAlgorthim)
        {
            individualBindingSource.DataSource = ScheduleAlgorthim.AvailableFacilitators;
        }
        private void populateOnSiteFilter(CourseAvailablePeriod ScheduleAlgorthim)
        {
            venueBindingSourceFilterList.DataSource = ScheduleAlgorthim.AvailableOnSiteVenues;
        }
        private void populateOffSiteFilter(CourseAvailablePeriod ScheduleAlgorthim)
        {
            addressBindingSourceFilterList.DataSource = ScheduleAlgorthim.AvailableOffSiteVenues;
        }

        #endregion
        private DateTime EnsureSchedulableDate(DateTime SelectedDate)
        {
            Boolean IsValidDay = false;
            //DateTime DateTimeToSelect = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, 0, 0, 0);
            while (!IsValidDay)
            {
                EnumDayOfWeeks d = Common.CustomDateTime.CustomDayOfTheWeek(SelectedDate.DayOfWeek);
                if (GetDayThatCurriculumCourseCanBeScheduled().Contains(d))
                {
                    IsValidDay = true;
                }
                else
                {
                    SelectedDate = SelectedDate.Date.AddDays(1);
                }
            }
            return SelectedDate;
        }
        private void refreshCurrentlyScheduledDates()
        {
            using (var Dbconnection = new MCDEntities())
            {
                //CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseMinimumMaximum.CurriculumCourseMaximum
                //Dbconnection.CurriculumCourseEnrollments.Attach(CurrentSelectedCurriculumCourseEnrollment);
                //Dbconnection.Entry(CurrentSelectedCurriculumCourseEnrollment).Reference("CurriculumCourse.CurriculumCourseMinimumMaximum").Load();
                int MaxPerCourse = 0;
                if (CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseMinimumMaximum.CurriculumCourseMaximum != 0)
                {
                    MaxPerCourse = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseMinimumMaximum.CurriculumCourseMaximum;

                }
                //CurrentlyScheduledCourses
                List<CurrentlyScheduledCourses> CSC = new List<CurrentlyScheduledCourses>();



                switch (CurrentScheduleConfiguration.ScheduleLocationID)
                {
                    case (int)Common.Enum.EnumScheduleLocations.Onsite:

                        List<GetAllOnSiteScheduledCourses_Result> AOSSC_Onsite = Dbconnection.GetAllOnSiteScheduledCourses(
                        scheduleLocationID: CurrentScheduleConfiguration.ScheduleLocationID,
                        curriculumCourseID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseID,
                        startDate: EnsureSchedulableDate(dtScheduleFromCustomStartDateSelector.SelectionStart.Date),// dtScheduleFromCustomStartDateSelector.SelectionStart.Date, 
                        endDate: dtScheduleFromCustomStartDateSelector.SelectionStart.Date.AddMonths((int)CurrentPeriodAhead))
                        .ToList<GetAllOnSiteScheduledCourses_Result>();



                        foreach (GetAllOnSiteScheduledCourses_Result item in AOSSC_Onsite.Where(a => a.AmountEnrolled < MaxPerCourse))
                        {
                            CSC.Add(new CurrentlyScheduledCourses()
                            {
                                CourseStartDate = item.ScheduleStartDate.Date,
                                CourseEndDate = item.ScheduleCompletionDate.Date,
                                CourseName = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Course.CourseName,
                                CourseID = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CourseID,
                                FacilitatorID = item.IndividualID,
                                FacilitatorName = item.FacilitatorName,
                                VenueID = item.VenueID,
                                VenueName = item.VenueName,
                                AmountEnrolled = Convert.ToInt32(item.AmountEnrolled)
                            });
                        }

                        currentlyScheduledCoursesBindingSource.DataSource = CSC.ToList();
                        break;
                    case (int)Common.Enum.EnumScheduleLocations.OffSite:

                        List<GetAllOffSiteScheduledCourses_Result> AOSSC_OffSite = Dbconnection.GetAllOffSiteScheduledCourses(
                             scheduleLocationID: CurrentScheduleConfiguration.ScheduleLocationID,
                             curriculumCourseID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseID,
                             startDate: EnsureSchedulableDate(dtScheduleFromCustomStartDateSelector.SelectionStart.Date),// dtScheduleFromCustomStartDateSelector.SelectionStart.Date, 
                             endDate: dtScheduleFromCustomStartDateSelector.SelectionEnd.Date).ToList<GetAllOffSiteScheduledCourses_Result>();

                        //CurrentlyScheduledCourses
                        // List<CurrentlyScheduledCourses> CSC = new List<CurrentlyScheduledCourses>();
                        foreach (GetAllOffSiteScheduledCourses_Result item in AOSSC_OffSite.Where(a => a.AmountEnrolled < MaxPerCourse))
                        {
                            CSC.Add(new CurrentlyScheduledCourses()
                            {
                                CourseStartDate = item.ScheduleStartDate.Date,
                                CourseEndDate = item.ScheduleCompletionDate.Date,
                                CourseName = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Course.CourseName,
                                CourseID = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CourseID,
                                FacilitatorID = item.IndividualID,
                                FacilitatorName = item.FacilitatorName,
                                VenueID = item.VenueID,
                                VenueName = item.VenueName,
                                AmountEnrolled = Convert.ToInt32(item.AmountEnrolled)
                            });
                        }
                        currentlyScheduledCoursesBindingSource.DataSource = CSC.ToList();
                        break;
                }

                //CurrentlyScheduledCourses

            };
        }
        private void refreshPossibleSchedulingDates(List<int> FacilitatorFilterList = null, List<int> OnSiteVenueFilterList = null, List<int> OffSiteFitlerList = null, int OffSiteID = 0)
        {
            Common.EnumPreLoadType TypeOfLoading = Common.EnumPreLoadType.PreloadingOnSite;
            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.OffSite)
            {
                TypeOfLoading = Common.EnumPreLoadType.PreLoadingOffSite;
            }
            CourseAvailablePeriod ScheduleAlgorthim = new CourseAvailablePeriod(
                CourseSearchPeriodStartDate: dtScheduleFromCustomStartDateSelector.SelectionRange.Start.Date,
                CourseSearchPeriodEndDate: dtScheduleFromCustomStartDateSelector.SelectionRange.Start.AddMonths((int)CurrentPeriodAhead).Date,
                CourseID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CourseID,
                CourseDescription: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Course.CourseName,
                CourseDuration: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Duration,
                SetOfDaysOfWeekCourseCanBeScheulded: GetDayThatCurriculumCourseCanBeScheduled(),
                PreLoadingType: TypeOfLoading,
                FacilitatorFilterList: FacilitatorFilterList,
                OnSiteFilterList: OnSiteVenueFilterList,
                OffSiteFilterList: OffSiteFitlerList,
                OffSiteID: OffSiteID);//Company ID
            switch ((EnumScheduleLocations)CurrentScheduleConfiguration.ScheduleLocationID)
            {
                case (EnumScheduleLocations.Onsite):
                    availableOnSitePeriodsBindingSource.DataSource = ScheduleAlgorthim.GetAllAvailableOnStiePeriods().OrderBy(a => a.CourseStartDate).ThenBy(a => a.FacilitatorName).ToList();
                    populateOnsiteFilters(ScheduleAlgorthim);
                    break;
                case (EnumScheduleLocations.OffSite):
                    availableOffSitePeriodsBindingSource.DataSource = ScheduleAlgorthim.GetAllAvailableOffSitePeriods().OrderBy(a => a.CourseStartDate).ThenBy(a => a.FacilitatorName).ToList();
                    populateOffSiteFilters(ScheduleAlgorthim);
                    break;
            }



        }
        private List<EnumDayOfWeeks> GetDayThatCurriculumCourseCanBeScheduled()
        {
            List<EnumDayOfWeeks> DaysCanSchedule = new List<EnumDayOfWeeks>();
            //sets the Date One Day Ahead
            foreach (CurriculumCourseDayCanBeScheduled CCDCBS in CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseDayCanBeScheduleds)
            {
                DaysCanSchedule.Add((EnumDayOfWeeks)CCDCBS.DayOfWeekID);
            };
            return DaysCanSchedule;
        }
        private void loadupStepFour()
        {

        }
        private void loadupStepFive()
        {

        }
        private void loadupStepSix()
        {

        }
        private void loadupStepSeven()
        {

        }
        private void loadupStepEight()
        {

        }


        #endregion

        #endregion

        #endregion

        private void picSelectOnSite_Click(object sender, EventArgs e)
        {
            picSelectOnSite.BackColor = Color.Gainsboro;
            picSelectOffSite.BackColor = Color.Transparent;
            CurrentScheduleConfiguration.ScheduleLocationID = (int)EnumScheduleLocations.Onsite;
            lblCurentlySelectedSiteType.Text = "On-Site";
        }

        private void picSelectOffSite_Click(object sender, EventArgs e)
        {
            picSelectOnSite.BackColor = Color.Transparent;
            picSelectOffSite.BackColor = Color.Gainsboro;
            CurrentScheduleConfiguration.ScheduleLocationID = (int)EnumScheduleLocations.OffSite;
            lblCurentlySelectedSiteType.Text = "Off-Site";
        }
        private void refreshCompanyInfo()
        {
            this.populateCompanyInfo();
        }

        private void populateCompanyInfo()
        {
            //if (CurrentScheduleConfiguration.Companies.Count > 0)
            //{
            //    companyBindingSource.DataSource = (from a in CurrentScheduleConfiguration.Companies
            //                                       select a).ToList<Data.Models.Company>();
            //}
            //else
            //{
            //    companyBindingSource.Clear();
            //}
        }
        private Data.Models.Company CurrentlySelectedCompany { get; set; }

        private void btnSearchForCompany_Click(object sender, EventArgs e)
        {
            using (frmCompanySearch frm = new frmCompanySearch())
            {
                frm.ShowDialog();
                if (frm.CurrentCompany != null)
                {
                    //this.CurrentScheduleConfiguration.Clear();
                    CurrentlySelectedCompany = frm.CurrentCompany;

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Companies.Attach(CurrentlySelectedCompany);

                        Dbconnection.Entry(CurrentlySelectedCompany).Collection(a => a.Addresses).Load();
                    };
                    companyBindingSource.DataSource = CurrentlySelectedCompany;

                    addressBindingSource.DataSource = (from a in CurrentlySelectedCompany.Addresses
                                                       select a).ToList<Data.Models.Address>();

                }
                else
                {
                    companyBindingSource.DataSource = null;

                    addressBindingSource.DataSource = null;
                    //this.CurrentScheduleConfiguration.Companies.Clear();
                }
            };
            this.refreshCompanyInfo();
        }

        private void btnReviewCompanyDetails_Click(object sender, EventArgs e)
        {
            if (companyBindingSource.Count > 0)
            {
                //using (frmCompany frm = new frmCompany())
                //{
                //    frm.txtCompaniesFilterCriteria.Text = ((Data.Models.Company)companyBindingSource.Current).CompanyName.ToString();
                //    frm.ShowDialog();
                //    this.refreshCompanyInfo();
                //}

            }
        }

        private void dtScheduleFromCustomStartDateSelector_DateChanged(object sender, DateRangeEventArgs e)
        {
            refreshStartAndEndDates();
            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                refreshPossibleSchedulingDates();
                refreshCurrentlyScheduledDates();

            }
            else
            {
                refreshPossibleSchedulingDates(OffSiteID: CurrentlySelectedCompany.CompanyID);
                refreshCurrentlyScheduledDates();
            }

        }

        private void TimePeriodAhead_CheckedChanged(object sender, EventArgs e)
        {
            MetroRadioButton x = (MetroRadioButton)sender;
            CurrentPeriodAhead = (SearchTimeAhead)Convert.ToInt32(x.Tag);
            refreshStartAndEndDates();
            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                refreshPossibleSchedulingDates();
                refreshCurrentlyScheduledDates();
            }
            else
            {
                refreshPossibleSchedulingDates(OffSiteID: CurrentlySelectedCompany.CompanyID);
                refreshCurrentlyScheduledDates();
            }


        }

        private void dtScheduleFromCustomStartDateSelector_DateSelected(object sender, DateRangeEventArgs e)
        {

        }

        private void btnReCalculateSchedule_Click(object sender, EventArgs e)
        {
            refreshStartAndEndDates();
            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                refreshPossibleSchedulingDates();
                refreshCurrentlyScheduledDates();
            }
            else
            {
                refreshPossibleSchedulingDates(OffSiteID: CurrentlySelectedCompany.CompanyID);
                refreshCurrentlyScheduledDates();
            }
        }
        private void refreshStartAndEndDates()
        {
            txtScheduleStartDate.Text = dtScheduleFromCustomStartDateSelector.SelectionStart.ToString("D");
            txtScheduleEndDate.Text = dtScheduleFromCustomStartDateSelector.SelectionEnd.AddMonths((int)CurrentPeriodAhead).ToString("D");
        }

        private void ResetLastCheckedItem()
        {
            if (LastGridUsed != enumLastGridUsed.notYetSelectedAnyField)
            {
                switch (LastGridUsed)
                {
                    case enumLastGridUsed.currentlyavailable:
                        dgvCurrentlyScheduledDates.Rows[CurrentlySelectedItemRowIndex].Cells[0].Value = false;
                        break;
                    case enumLastGridUsed.onsite:
                        dgvAvailableOnSiteCoursesToSchedule.Rows[CurrentlySelectedItemRowIndex].Cells[0].Value = false;
                        break;
                    case enumLastGridUsed.offsite:
                        dgvAvailableOffSiteCoursesToSchedule.Rows[CurrentlySelectedItemRowIndex].Cells[0].Value = false;
                        break;
                }
            }


        }
        private void resetAvailableOnsitePeriodSelection()
        {

            foreach (DataGridViewRow row in dgvAvailableOnSiteCoursesToSchedule.Rows)
            {
                row.Cells[0].Value = false;

            }
        }
        private void resetAvailableOffsitePeriodSelection()
        {
            foreach (DataGridViewRow row in dgvAvailableOffSiteCoursesToSchedule.Rows)
            {
                row.Cells[0].Value = false;
            }
        }
        private void resetCurrentScheduledPeriodSelection()
        {
            foreach (DataGridViewRow row in dgvCurrentlyScheduledDates.Rows)
            {
                row.Cells[0].Value = false;
            }
        }
        private void resetAllScheduleSelectionDataGridView()
        {
            resetAvailableOnsitePeriodSelection();
            resetAvailableOffsitePeriodSelection();
            resetCurrentScheduledPeriodSelection();
        }
        private void dgvCoursesToSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // AvailableOnSitePeriods x = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;
            if (e.ColumnIndex == 0)
            {
                ResetLastCheckedItem();
                LastGridUsed = enumLastGridUsed.onsite;
                CurrentlySelectedItemRowIndex = e.RowIndex;
                // resetAllScheduleSelectionDataGridView();
                var gridView = (DataGridView)sender;

                //AvailableOnSitePeriods CurrentSelectedperiod = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;

                gridView.Rows[e.RowIndex].Cells[colAvailableDateToScheduleSelectCourse.Index].Value = true;
            }
        }

        private enum enumLastGridUsed
        {
            offsite,
            onsite,
            currentlyavailable,
            notYetSelectedAnyField
        }
        private void dgvCurrentlyScheduledDates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                ResetLastCheckedItem();
                LastGridUsed = enumLastGridUsed.currentlyavailable;
                CurrentlySelectedItemRowIndex = e.RowIndex;

                //resetAllScheduleSelectionDataGridView();
                var gridView = (DataGridView)sender;

                //AvailableOnSitePeriods CurrentSelectedperiod = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;

                gridView.Rows[e.RowIndex].Cells[colCurrentlySechuldedCourseSelector.Index].Value = true;

            }
        }
        private class SelectedSechuledParameters
        {
            public int FacilitatorID { get; set; }
            public int VenueID { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

        }
        private SelectedSechuledParameters GetSelectedSchedule()
        {
            SelectedSechuledParameters Rtn = null;
            foreach (DataGridViewRow row in dgvAvailableOnSiteCoursesToSchedule.Rows)
            {
                var chkValue = row.Cells[colAvailableDateToScheduleSelectCourse.Index].Value;
                if (chkValue != null)
                {
                    if ((Boolean)chkValue)
                    {
                        AvailableOnSitePeriods AOSP = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;
                        Rtn = new SelectedSechuledParameters()
                        {
                            VenueID = AOSP.VenueID,
                            FacilitatorID = AOSP.FacilitatorID,
                            StartDate = AOSP.CourseStartDate,
                            EndDate = AOSP.CourseEndDate
                        };
                    }
                }
            }
            foreach (DataGridViewRow row in dgvAvailableOffSiteCoursesToSchedule.Rows)
            {
                var chkValue = row.Cells[colAvailableOffSiteDateToScheduleSelectCourse.Index].Value;
                if (chkValue != null)
                {
                    if ((Boolean)chkValue)
                    {
                        AvailableOffSitePeriods AOSP = (AvailableOffSitePeriods)availableOffSitePeriodsBindingSource.Current;
                        Rtn = new SelectedSechuledParameters()
                        {
                            VenueID = AOSP.VenueID,
                            FacilitatorID = AOSP.FacilitatorID,
                            StartDate = AOSP.CourseStartDate,
                            EndDate = AOSP.CourseEndDate
                        };
                    }
                }
            }
            foreach (DataGridViewRow row in dgvCurrentlyScheduledDates.Rows)
            {
                var chkValue = row.Cells[colCurrentlySechuldedCourseSelector.Index].Value;
                if (chkValue != null)
                {
                    if ((Boolean)chkValue)
                    {
                        CurrentlyScheduledCourses CSC = (CurrentlyScheduledCourses)currentlyScheduledCoursesBindingSource.Current;
                        Rtn = new SelectedSechuledParameters()
                        {
                            VenueID = CSC.VenueID,
                            FacilitatorID = CSC.FacilitatorID,
                            StartDate = CSC.CourseStartDate,
                            EndDate = CSC.CourseEndDate
                        };
                    }
                }
            }


            return Rtn;
        }
        private void frmScheduleCurriculumCourseWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mustSaveItems)
            {

                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            SelectedSechuledParameters SSP = GetSelectedSchedule();
                            if (SSP != null)
                            {
                                //CRUD Operations
                                CurrentScheduleConfiguration.FacilitatorID = SSP.FacilitatorID;
                                CurrentScheduleConfiguration.ScheduleStartDate = SSP.StartDate.Date;
                                CurrentScheduleConfiguration.ScheduleCompletionDate = SSP.EndDate;
                                if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
                                {
                                    CurrentScheduleConfiguration.OnSiteSchedule = new OnSiteSchedule()
                                    {
                                        DateLastModified = DateTime.Now,
                                        VenueID = SSP.VenueID
                                    };
                                }
                                if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.OffSite)
                                {
                                    CurrentScheduleConfiguration.OffSiteSchedule = new OffSiteSchedule()
                                    {
                                        DateLastModified = DateTime.Now,
                                        AddressID = SSP.VenueID
                                    };
                                }
                                Dbconnection.Entry(CurrentScheduleConfiguration).State = EntityState.Added;
                                // CurrentSelectedCurriculumCourseEnrollment.Schedules.Add();
                                Dbconnection.CurriculumCourseEnrollments.Attach(CurrentSelectedCurriculumCourseEnrollment);
                                CurrentSelectedCurriculumCourseEnrollment.LookupEnrollmentProgressStateID = (int)EnumEnrollmentProgressStates.In_Progress;
                                Dbconnection.Entry(CurrentSelectedCurriculumCourseEnrollment).Reference(a => a.LookupEnrollmentProgressState).Load();
                                Dbconnection.Entry(CurrentSelectedCurriculumCourseEnrollment).State = EntityState.Modified;

                            }

                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();
                            //commit transaction
                            dbTran.Commit();


                        }
                        catch (Exception ex)
                        {
                            if (ex is DbEntityValidationException)
                            {
                                foreach (DbEntityValidationResult entityErr in ((DbEntityValidationException)ex).EntityValidationErrors)
                                {
                                    foreach (DbValidationError error in entityErr.ValidationErrors)
                                    {
                                        MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //Rollback transaction if exception occurs
                            dbTran.Rollback();
                        }
                    }
                };

            }
        }

        private void dgvAvailableOffSiteCoursesToSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AvailableOnSitePeriods x = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;
            if (e.ColumnIndex == 0)
            {
                ResetLastCheckedItem();
                LastGridUsed = enumLastGridUsed.offsite;
                CurrentlySelectedItemRowIndex = e.RowIndex;
                // resetAllScheduleSelectionDataGridView();
                var gridView = (DataGridView)sender;

                //AvailableOnSitePeriods CurrentSelectedperiod = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;
                resetAllScheduleSelectionDataGridView();
                gridView.Rows[e.RowIndex].Cells[colAvailableOffSiteDateToScheduleSelectCourse.Index].Value = true;


            }
        }






        private void btnpicFilterFacilitator_Click(object sender, EventArgs e)
        {

            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                refreshPossibleSchedulingDates(FacilitatorFilterList: new List<int>() { Convert.ToInt32(cboFacilitatorFilterList.SelectedValue) });
            }
            else
            {
                refreshPossibleSchedulingDates(
                    FacilitatorFilterList: new List<int>() { Convert.ToInt32(cboFacilitatorFilterList.SelectedValue) },
                     OffSiteID: CurrentlySelectedCompany.CompanyID);
            }
        }

        private void btnpicFilterVenues_Click(object sender, EventArgs e)
        {
            if (CurrentScheduleConfiguration.ScheduleLocationID == (int)EnumScheduleLocations.Onsite)
            {
                refreshPossibleSchedulingDates(OnSiteVenueFilterList: new List<int>() { Convert.ToInt32(cboOnsiteVenueFilterList.SelectedValue) });
            }
            else
            {
                refreshPossibleSchedulingDates(
                                                OffSiteFitlerList: new List<int>() { Convert.ToInt32(cboOffsiteVenueFilterList.SelectedValue) },
                                                OffSiteID: CurrentlySelectedCompany.CompanyID);

            }

        }

        private void resetCompanyAddressSelectedDataGridView()
        {
            foreach (DataGridViewRow row in dgvCompanyAddressSelection.Rows)
            {
                row.Cells[0].Value = false;
            }
        }
        private void dgvCompanyAddressSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //colAddressSelection
            if (e.ColumnIndex == 0)
            {
                if (addressBindingSource.Count > 0)
                {
                    resetCompanyAddressSelectedDataGridView();
                    var gridView = (DataGridView)sender;

                    //AvailableOnSitePeriods CurrentSelectedperiod = (AvailableOnSitePeriods)availableOnSitePeriodsBindingSource.Current;

                    gridView.Rows[e.RowIndex].Cells[colAddressSelection.Index].Value = true;
                }

            }
        }

        private void dgvAvailableDateToSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvAvailableDateToSchedule_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvAvailableDateToSchedule_Click(object sender, EventArgs e)
        {
            if (dgvAvailableDateToSchedule.SelectedIndex == 0)
            {
                lblCurrenltySelectedIndicator.Text = "Currently Viewing - Available Courses to Schedule";
            }
            else
            {
                lblCurrenltySelectedIndicator.Text = "Currently Viewing - Existing Course Scheduled";
            }
        }
    }
}
