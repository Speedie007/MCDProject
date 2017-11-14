using Impendulo.Common;
using Impendulo.Common.ReportModels.Schedules.SectionalViews;
using Impendulo.Data.Models;
using Impendulo.Development.Reports.Reports.Schedules;
//using Impendulo.Development.Reports.Reports.Schedules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Scheduling.CourseScheduleSections
{
    public partial class frmCourseScheduleSections : MetroFramework.Forms.MetroForm
    {
        #region Global Variables
        private int CurrentSelectedIndexTabs { get; set; }

        private enumSelectedSubTab CurrenltySelectedConfirmedSubTab { get; set; }

        #endregion
        private enum enumSelectedSubTab
        {
            All_Confirmed_Not_Yet_Started,
            All_Confirmed_Currently_InProgress,
            All_Confirmed_Completed_Require_ReportFeedBack
        }

        private List<Individual> CurrentlyAvailableFacilitators { get; set; }
        private List<Venue> CurrentlyAvailableVenues { get; set; }

        private List<SectionalView> CurrentlySelectedCourses { get; set; }

        public frmCourseScheduleSections()
        {
            CurrentSelectedIndexTabs = 0;
            CurrentlySelectedCourses = new List<SectionalView>();
            CurrentlyAvailableFacilitators = new List<Individual>();
            CurrentlyAvailableVenues = new List<Venue>();

            CurrenltySelectedConfirmedSubTab = enumSelectedSubTab.All_Confirmed_Not_Yet_Started;
            InitializeComponent();
        }

        private void frmCourseScheduleSections_Load(object sender, EventArgs e)
        {
            splitContainerMain.Panel1Collapsed = true;


            populateDataGridView();
            refreshDepartments();
            refreshCuriculums();
            refreshCourses();
        }

        #region REfresh Methods

        private void refreshDepartments()
        {
            this.populateDepartments();
        }
        private void refreshCuriculums()
        {
            this.populateCurriculums();
        }
        private void refreshCourses()
        {
            populateCourses();
        }
        private void refershFacilitators()
        {

            this.populateFacilitators();
        }
        private void refresfVenues()
        {
            this.populateVenues();
        }

        private void refreshNotYetConfirmed()
        {
            this.populateNotYetConfirmed();
        }

        private void refreshAll_ConfirmedScheduleds_NOT_YET_STARTED()
        {
            this.populateAll_ConfirmedScheduleds_NOT_YET_STARTED();
        }

        private void refreshAllScheduledCourses_CURRENTLY_IN_PROGRESS()
        {
            populateAllScheduledCourses_CURRENTLY_IN_PROGRESS();
        }
        private void refreshAllScheduledCourses_CURRENTLY_COMPLETED_REQUIRE_REPORT_FEEDBACK()
        {
            populateAllScheduledCourses_CURRENTLY_COMPLETED_REQUIRE_rEPORT_FEEDBACK();
        }

        private void refreshAllScheduledCoursesWhichAreArchied()
        {
            populateAllScheduledCourses_CURRENTLY_COMPLETED_AND_ACRHIVED();
        }
        #endregion

        #region Populate Methods

        private void populateDepartments()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupDepartmentBindingSource.DataSource = (from a in Dbconnection.LookupDepartments
                                                            select a).ToList<LookupDepartment>();
            };
        }
        private void populateCurriculums()
        {
            using (var Dbconnection = new MCDEntities())
            {
                int DepartmentID = Convert.ToInt32(this.cboDepartment.SelectedValue);

                curriculumBindingSource.DataSource = (from a in Dbconnection.Curriculums
                                                      where a.DepartmentID == DepartmentID
                                                      select a).ToList<Curriculum>();
            };
        }
        private void populateCourses()
        {
            using (var Dbconnection = new MCDEntities())
            {
                if (curriculumBindingSource.Count > 0)
                {
                    int CurriculumID = Convert.ToInt32(this.cboCuriculum.SelectedValue);

                    courseBindingSource.DataSource = (from a in Dbconnection.Courses
                                                      from b in a.CurriculumCourses
                                                      where b.CurriculumID == CurriculumID
                                                      select a).ToList<Course>();
                }

            };
        }

        private void populateFacilitators()
        {
            if (CurrentlySelectedCourses.Count > 0)
            {
                //CustomSortableBindingList<SectionalView> l = (CustomSortableBindingList<SectionalView>)currentlyScheduledCoursesBindingSource.List;
                individualBindingSource.DataSource = (from a in CurrentlySelectedCourses
                                                      select new Individual()
                                                      {
                                                          IndividualID = a.FacilitatorID,
                                                          IndividualFirstName = a.FacilitatorName
                                                      }).Distinct<Individual>(new individualComparer()).ToList<Individual>();
            };

        }
        private class individualComparer : IEqualityComparer<Individual>
        {
            public bool Equals(Individual x, Individual y)
            {
                if (x.IndividualID == y.IndividualID)
                {
                    if (x.IndividualFirstName == y.IndividualFirstName)
                    {
                        return true;
                    }
                }
                return false;
            }

            public int GetHashCode(Individual obj)
            {
                Individual i = (Individual)obj;

                return i.IndividualID.GetHashCode() + i.IndividualFirstName.GetHashCode();
            }
        }
        private void populateVenues()
        {
            if (CurrentlySelectedCourses.Count > 0)
            {
                //CustomSortableBindingList<Schedules> l = (CustomSortableBindingList<Schedules>)currentlyScheduledCoursesBindingSource.List;
                venueBindingSource.DataSource = (from a in CurrentlySelectedCourses
                                                 select new Venue()
                                                 {
                                                     VenueID = a.VenueID,
                                                     VenueName = a.VenueName
                                                 }).Distinct<Venue>(new VenueComparer()).ToList<Venue>();
            };
        }
        private class VenueComparer : IEqualityComparer<Venue>
        {
            public bool Equals(Venue x, Venue y)
            {
                if (x.VenueID == y.VenueID)
                {
                    if (x.VenueName == y.VenueName)
                    {
                        return true;
                    }
                }
                return false;
            }



            public int GetHashCode(Venue obj)
            {
                Venue i = (Venue)obj;

                return i.VenueID.GetHashCode() + i.VenueName.GetHashCode();
            }


        }
        private void populateNotYetConfirmed()
        {


            //using (var Dbconnection = new MCDEntities())
            //{
            //    rEPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_ResultBindingSource.DataSource = new Common.CustomSortableBindingList<REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_Result>(
            //        Dbconnection.REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules().ToList<REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_Result>());

            //    foreach (REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_Result CSR in rEPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_ResultBindingSource.List)
            //    {

            //        CurrentlyAvailableVenues.Add(new Venue()
            //        {
            //            VenueID = (int)CSR.VenueID,
            //            VenueName = CSR.VenueName
            //        });

            //        CurrentlyAvailableFacilitators.Add(new Individual()
            //        {
            //            IndividualID = CSR.FacilitatorID,
            //            IndividualFirstName = CSR.FacilitatorName
            //        });
            //    }
            //};
            CurrentlyAvailableVenues.Clear();
            CurrentlyAvailableFacilitators.Clear();
            CurrentlySelectedCourses.Clear();

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedCourses.Clear();

                foreach (REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_Result x in Dbconnection.REPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules())
                {
                    CurrentlySelectedCourses.Add(new SectionalView()
                    {
                        AmountEnrolled = (int)x.AmountEnrolled,
                        CostCode = x.CostCode,
                        CourseID = x.CourseID,
                        CourseName = x.CourseName,
                        CurriculumCourseID = x.CurriculumCourseID,
                        CurriculumName = x.CurriculumName,
                        DepartmentID = x.DepartmentID,
                        DepartmentName = x.DepartmentName,
                        EndDate = x.EndDate,
                        //EnrollmentProgressCurrentState = ( DateTime) x.EndDate,
                        FacilitatorID = x.FacilitatorID,
                        FacilitatorName = x.FacilitatorName,
                        //LookupEnrollmentProgressStateID = x.,
                        LookupScheduleLocationID = x.LookupScheduleLocationID,
                        StartDate = x.StartDate,
                        VenueName = x.VenueName,
                        VenueID = (int)x.VenueID
                    });

                    CurrentlyAvailableVenues.Add(new Venue()
                    {
                        VenueID = (int)x.VenueID,
                        VenueName = x.VenueName
                    });
                    CurrentlyAvailableFacilitators.Add(new Individual()
                    {
                        IndividualID = (int)x.FacilitatorID,
                        IndividualFirstName = x.FacilitatorName
                    });
                }


                rEPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_ResultBindingSource.DataSource =
                       new Common.CustomSortableBindingList<SectionalView>(CurrentlySelectedCourses);
            };
            this.refresfVenues();
            this.refershFacilitators();
        }

        private void populateAll_ConfirmedScheduleds_NOT_YET_STARTED()
        {

            //using (var Dbconnection = new MCDEntities())
            //{
            //    rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_ResultBindingSource1.DataSource =
            //        new Common.CustomSortableBindingList<REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_Result>(
            //        Dbconnection.REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet().ToList<REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_Result>()
            //        );
            //};

            CurrentlyAvailableVenues.Clear();
            CurrentlyAvailableFacilitators.Clear();
            CurrentlySelectedCourses.Clear();

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedCourses.Clear();

                foreach (REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_Result x in Dbconnection.REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet().ToList<REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_Result>())
                {
                    CurrentlySelectedCourses.Add(new SectionalView()
                    {
                        AmountEnrolled = (int)x.AmountEnrolled,
                        CostCode = x.CostCode,
                        CourseID = x.CourseID,
                        CourseName = x.CourseName,
                        CurriculumCourseID = x.CurriculumCourseID,
                        CurriculumName = x.CurriculumName,
                        DepartmentID = x.DepartmentID,
                        DepartmentName = x.DepartmentName,
                        EndDate = x.EndDate,
                        //EnrollmentProgressCurrentState = ( DateTime) x.EndDate,
                        FacilitatorID = x.FacilitatorID,
                        FacilitatorName = x.FacilitatorName,
                        //LookupEnrollmentProgressStateID = x.,
                        LookupScheduleLocationID = x.LookupScheduleLocationID,
                        StartDate = x.StartDate,
                        VenueName = x.VenueName,
                        VenueID = (int)x.VenueID
                    });
                    CurrentlyAvailableVenues.Add(new Venue()
                    {
                        VenueID = (int)x.VenueID,
                        VenueName = x.VenueName
                    });
                    CurrentlyAvailableFacilitators.Add(new Individual()
                    {
                        IndividualID = (int)x.FacilitatorID,
                        IndividualFirstName = x.FacilitatorName
                    });
                }



                rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_ResultBindingSource1.DataSource =
                       new Common.CustomSortableBindingList<SectionalView>(CurrentlySelectedCourses);
            };
            this.refresfVenues();
            this.refershFacilitators();
        }

        private void populateAllScheduledCourses_CURRENTLY_IN_PROGRESS()
        {
            using (var Dbconnection = new MCDEntities())
            {
                CurrentlyAvailableVenues.Clear();
                CurrentlyAvailableFacilitators.Clear();
                CurrentlySelectedCourses.Clear();

                foreach (REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_Result x in Dbconnection.REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress()
                           .ToList<REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_Result>())
                {
                    CurrentlySelectedCourses.Add(new SectionalView()
                    {
                        AmountEnrolled = (int)x.AmountEnrolled,
                        CostCode = x.CostCode,
                        CourseID = x.CourseID,
                        CourseName = x.CourseName,
                        CurriculumCourseID = x.CurriculumCourseID,
                        CurriculumName = x.CurriculumName,
                        DepartmentID = x.DepartmentID,
                        DepartmentName = x.DepartmentName,
                        EndDate = x.EndDate,
                        //EnrollmentProgressCurrentState = ( DateTime) x.EndDate,
                        FacilitatorID = x.FacilitatorID,
                        FacilitatorName = x.FacilitatorName,
                        //LookupEnrollmentProgressStateID = x.,
                        LookupScheduleLocationID = x.LookupScheduleLocationID,
                        StartDate = x.StartDate,
                        VenueName = x.VenueName,
                        VenueID = (int)x.VenueID
                    });
                    CurrentlyAvailableVenues.Add(new Venue()
                    {
                        VenueID = (int)x.VenueID,
                        VenueName = x.VenueName
                    });
                    CurrentlyAvailableFacilitators.Add(new Individual()
                    {
                        IndividualID = (int)x.FacilitatorID,
                        IndividualFirstName = x.FacilitatorName
                    });
                }

                rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_ResultBindingSource1.DataSource =
                       new Common.CustomSortableBindingList<SectionalView>(CurrentlySelectedCourses);
            };
            this.refresfVenues();
            this.refershFacilitators();
        }
        private void populateAllScheduledCourses_CURRENTLY_COMPLETED_REQUIRE_rEPORT_FEEDBACK()
        {
            //using (var Dbconnection = new MCDEntities())
            //{
            //    rEPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource =
            //           new Common.CustomSortableBindingList<REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result>(
            //               Dbconnection.REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate().ToList<REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result>()
            //           );
            //};

            CurrentlyAvailableVenues.Clear();
            CurrentlyAvailableFacilitators.Clear();
            CurrentlySelectedCourses.Clear();

            using (var Dbconnection = new MCDEntities())
            {


                foreach (REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result x in Dbconnection.REPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate())
                {
                    CurrentlySelectedCourses.Add(new SectionalView()
                    {
                        AmountEnrolled = (int)x.AmountEnrolled,
                        CostCode = x.CostCode,
                        CourseID = x.CourseID,
                        CourseName = x.CourseName,
                        CurriculumCourseID = x.CurriculumCourseID,
                        CurriculumName = x.CurriculumName,
                        DepartmentID = x.DepartmentID,
                        DepartmentName = x.DepartmentName,
                        EndDate = x.EndDate,
                        //EnrollmentProgressCurrentState = ( DateTime) x.EndDate,
                        FacilitatorID = x.FacilitatorID,
                        FacilitatorName = x.FacilitatorName,
                        //LookupEnrollmentProgressStateID = x.,
                        LookupScheduleLocationID = x.LookupScheduleLocationID,
                        StartDate = x.StartDate,
                        VenueName = x.VenueName,
                        VenueID = (int)x.VenueID
                    });
                    CurrentlyAvailableVenues.Add(new Venue()
                    {
                        VenueID = (int)x.VenueID,
                        VenueName = x.VenueName
                    });
                    CurrentlyAvailableFacilitators.Add(new Individual()
                    {
                        IndividualID = (int)x.FacilitatorID,
                        IndividualFirstName = x.FacilitatorName
                    });
                }

                rEPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource =
                       new Common.CustomSortableBindingList<SectionalView>(CurrentlySelectedCourses);
            };
            this.refresfVenues();
            this.refershFacilitators();

        }
        private void populateAllScheduledCourses_CURRENTLY_COMPLETED_AND_ACRHIVED()
        {
            //using (var Dbconnection = new MCDEntities())
            //{
            //    rEPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource =
            //           new Common.CustomSortableBindingList<REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result>(
            //               Dbconnection.REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate()
            //               .ToList<REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result>()
            //           );
            //};

            CurrentlyAvailableVenues.Clear();
            CurrentlyAvailableFacilitators.Clear();
            CurrentlySelectedCourses.Clear();

            using (var Dbconnection = new MCDEntities())
            {


                foreach (REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_Result x in Dbconnection.REPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate())
                {
                    CurrentlySelectedCourses.Add(new SectionalView()
                    {
                        AmountEnrolled = (int)x.AmountEnrolled,
                        CostCode = x.CostCode,
                        CourseID = x.CourseID,
                        CourseName = x.CourseName,
                        CurriculumCourseID = x.CurriculumCourseID,
                        CurriculumName = x.CurriculumName,
                        DepartmentID = x.DepartmentID,
                        DepartmentName = x.DepartmentName,
                        EndDate = x.EndDate,
                        //EnrollmentProgressCurrentState = ( DateTime) x.EndDate,
                        FacilitatorID = x.FacilitatorID,
                        FacilitatorName = x.FacilitatorName,
                        //LookupEnrollmentProgressStateID = x.,
                        LookupScheduleLocationID = x.LookupScheduleLocationID,
                        StartDate = x.StartDate,
                        VenueName = x.VenueName,
                        VenueID = (int)x.VenueID
                    });
                    CurrentlyAvailableVenues.Add(new Venue()
                    {
                        VenueID = (int)x.VenueID,
                        VenueName = x.VenueName
                    });
                    CurrentlyAvailableFacilitators.Add(new Individual()
                    {
                        IndividualID = (int)x.FacilitatorID,
                        IndividualFirstName = x.FacilitatorName
                    });
                }
                rEPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource =
                       new Common.CustomSortableBindingList<SectionalView>(CurrentlySelectedCourses);
            };
            this.refresfVenues();
            this.refershFacilitators();
        }
        #endregion

        private void populateDataGridView()
        {
            switch (CurrentSelectedIndexTabs)
            {
                case 0:
                    refreshNotYetConfirmed();
                    break;

                case 1:
                    refreshConfirmedCourseSubTab();
                    break;

                case 2:
                    this.refreshAllScheduledCoursesWhichAreArchied();
                    break;
            }
        }
        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSelectedIndexTabs = metroTabControl1.SelectedIndex;
            populateDataGridView();
        }


        private void refreshConfirmedCourseSubTab()
        {
            switch (CurrenltySelectedConfirmedSubTab)
            {
                case enumSelectedSubTab.All_Confirmed_Not_Yet_Started:
                    refreshAll_ConfirmedScheduleds_NOT_YET_STARTED();
                    break;
                case enumSelectedSubTab.All_Confirmed_Currently_InProgress:
                    refreshAllScheduledCourses_CURRENTLY_IN_PROGRESS();
                    break;
                case enumSelectedSubTab.All_Confirmed_Completed_Require_ReportFeedBack:
                    refreshAllScheduledCourses_CURRENTLY_COMPLETED_REQUIRE_REPORT_FEEDBACK();
                    break;
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (printCurrentlySelectedListing.SelectedIndex)
            {
                case 0:
                    CurrenltySelectedConfirmedSubTab = enumSelectedSubTab.All_Confirmed_Not_Yet_Started;
                    break;

                case 1:
                    CurrenltySelectedConfirmedSubTab = enumSelectedSubTab.All_Confirmed_Currently_InProgress;
                    break;

                case 2:
                    CurrenltySelectedConfirmedSubTab = enumSelectedSubTab.All_Confirmed_Completed_Require_ReportFeedBack;
                    break;
            }
            refreshConfirmedCourseSubTab();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (frmSectionalView frm = new frmSectionalView(enumScheduleSectionialViewToDisplay.Confirmed_NotYetStarted))
            {
                frm.ShowDialog();
            }
        }

        private void btnPrintAllCurrentlyInProgressScheduledCourses_Click(object sender, EventArgs e)
        {
            using (frmSectionalView frm = new frmSectionalView(enumScheduleSectionialViewToDisplay.Confirmed_InProgress))
            {
                frm.ShowDialog();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (frmSectionalView frm = new frmSectionalView(enumScheduleSectionialViewToDisplay.Confirmed_REquireReports))
            {
                frm.ShowDialog();
            }
        }

        private void btnHideFilters_Click(object sender, EventArgs e)
        {
            btnShowFilters.Visible = true;
            btnHideFilters.Visible = false;

            splitContainerMain.Panel1Collapsed = true;
        }

        private void btnShowFilters_Click(object sender, EventArgs e)
        {
            btnHideFilters.Visible = true;
            btnShowFilters.Visible = false;

            splitContainerMain.Panel1Collapsed = false;


        }

        private void btnpicFilterDepartment_Click(object sender, EventArgs e)
        {

            /*
             * rEPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1
                    rEPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1
                    rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_ResultBindingSource1
                    rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_ResultBindingSource1
                    rEPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_ResultBindingSource


    */
            string FilterStr = ((Impendulo.Data.Models.LookupDepartment)lookupDepartmentBindingSource.Current).DepartmentName.ToLower();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.DepartmentName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());

            this.LoadFilterItems(FilteredList);


        }
        private void LoadFilterItems(List<SectionalView> FilterItems)
        {
            switch (CurrentSelectedIndexTabs)
            {
                case 0:
                    rEPORTS_GetAll_NOT_YET_CONFIRMED_CourseSchedules_ResultBindingSource.DataSource = FilterItems;
                    break;
                case 1:
                    switch (CurrenltySelectedConfirmedSubTab)
                    {
                        case enumSelectedSubTab.All_Confirmed_Not_Yet_Started:
                            rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichHaveNotStartedYet_ResultBindingSource1.DataSource = FilterItems;
                            break;
                        case enumSelectedSubTab.All_Confirmed_Currently_InProgress:
                            rEPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress_ResultBindingSource1.DataSource = FilterItems;
                            break;
                        case enumSelectedSubTab.All_Confirmed_Completed_Require_ReportFeedBack:
                            rEPORTS_GetAll_CONFIRMED_AND_NOT_YET_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource = FilterItems;
                            break;
                    }
                    break;
                case 2:
                    rEPORTS_GetAll_CONFIRMED_AND_COMPLETED_CourseSchedulesWhichHavePassedCompletionDate_ResultBindingSource1.DataSource = FilterItems;
                    break;
            }
        }
        private void btnpicFilterCurriculum_Click(object sender, EventArgs e)
        {
            /*
               string FilterStr = ((Impendulo.Data.Models.LookupDepartment)lookupDepartmentBindingSource.Current).DepartmentName.ToLower();
           List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                       where a.DepartmentName.ToLower().Contains(FilterStr)
                                                                       select a).ToList<SectionalView>());


           this.LoadFilterItems(FilteredList);
           */
            string FilterStr = ((Impendulo.Data.Models.Curriculum)curriculumBindingSource.Current).CurriculumName.ToLower();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.CurriculumName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());
            this.LoadFilterItems(FilteredList);
        }

        private void btnpicFilterByCourse_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Course)courseBindingSource.Current).CourseName.ToLower();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.CourseName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());
            this.LoadFilterItems(FilteredList);
        }

        private void btnpicFilterByFacilitator_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Individual)individualBindingSource.Current).FullName.ToLower().TrimEnd();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.FacilitatorName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());

            this.LoadFilterItems(FilteredList);

        }

        private void btnpicFilterByVenue_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Venue)venueBindingSource.Current).VenueName.ToLower();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.VenueName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());

            this.LoadFilterItems(FilteredList);
        }

        private void btnpicFilterDates_Click(object sender, EventArgs e)
        {
            DateTime FilterStartDate = dtScheduleFilterStartTime.Value.Date;
            DateTime FilterEndDate = dtScheduleFilterEndTime.Value.Date;
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.StartDate.Date >= FilterStartDate.Date && a.StartDate.Date <= dtScheduleFilterEndTime.Value.Date
                                                                        select a).ToList<SectionalView>());
            this.LoadFilterItems(FilteredList);
        }

        private void groupBox4_Enter_1(object sender, EventArgs e)
        {

        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refreshCuriculums();
            this.refreshCourses();
        }

        private void cboCuriculum_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refreshCourses();
        }
    }
}
