using Impendulo.Common.ScheduleAvailablityAlgorithm;
using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Scheduling.ScheduleSummary
{
    public partial class frmScheduleSummary : MetroFramework.Forms.MetroForm
    {
        private ScheduleToDisplay CurrentScheduleToDisplay { get; set; }
        private enum ScheduleToDisplay : int
        {
            CompletedCourses = 0,
            CoursesInProgress = 1,
            CoursesNotYetStarted = 2
        }
        public frmScheduleSummary()
        {
            InitializeComponent();
            CurrentScheduleToDisplay = ScheduleToDisplay.CoursesInProgress;
        }

        private void frmScheduleSummary_Load(object sender, EventArgs e)
        {
            refreshDepartments();
            refreshCuriculums();
            refreshCourses();
            refreshCourseSchedule();
        }

        #region refresh Controls

        private void refreshCourseSchedule()
        {
            this.populateCourseSchedule();
        }
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
        #endregion
        private void populateVenues()
        {
            if (currentlyScheduledCoursesBindingSource.Count > 0)
            {
                List<CurrentlyScheduledCourses> l = (List<CurrentlyScheduledCourses>)currentlyScheduledCoursesBindingSource.List;
                venueBindingSource.DataSource = (from a in l
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
        #region Populate Methods
        private void populateCourseSchedule()
        {
            currentlyScheduledCoursesBindingSource.Clear();
            using (var Dbconnection = new MCDEntities())
            {

                switch (CurrentScheduleToDisplay)
                {
                    case ScheduleToDisplay.CompletedCourses:
                        currentlyScheduledCoursesBindingSource.DataSource = (from a in Dbconnection.GetAllCourseSchedulesWhichHavePassedCompletionDate()
                                                                             select new CurrentlyScheduledCourses()
                                                                             {
                                                                                 CourseStartDate = a.StartDate,
                                                                                 CourseEndDate = a.EndDate,
                                                                                 CourseID = a.CourseID,
                                                                                 CourseName = a.CourseName,
                                                                                 FacilitatorID = a.FacilitatorID,
                                                                                 FacilitatorName = a.FacilitatorName,
                                                                                 VenueID = a.VenueID,
                                                                                 VenueName = a.Venue,
                                                                                 AmountEnrolled = (int)a.AmountEnrolled,
                                                                                 CurriculumCourseID = a.CurriculumCourseID,
                                                                                 ScheduleLocationID = a.ScheduleLocationID,
                                                                                 ScheduleLocation = a.ScheduleLocation
                                                                             }).ToList<CurrentlyScheduledCourses>();
                        break;
                    case ScheduleToDisplay.CoursesInProgress:
                        currentlyScheduledCoursesBindingSource.DataSource = (from a in Dbconnection.GetAllCourseSchedulesWhichCurrentlyInProgress()
                                                                             select new CurrentlyScheduledCourses()
                                                                             {
                                                                                 CourseStartDate = a.StartDate,
                                                                                 CourseEndDate = a.EndDate,
                                                                                 CourseID = a.CourseID,
                                                                                 CourseName = a.CourseName,
                                                                                 FacilitatorID = a.FacilitatorID,
                                                                                 FacilitatorName = a.FacilitatorName,
                                                                                 VenueID = a.VenueID,
                                                                                 VenueName = a.Venue,
                                                                                 AmountEnrolled = (int)a.AmountEnrolled,
                                                                                 CurriculumCourseID = a.CurriculumCourseID,
                                                                                 ScheduleLocationID = a.ScheduleLocationID,
                                                                                 ScheduleLocation = a.ScheduleLocation
                                                                             }).ToList<CurrentlyScheduledCourses>();
                        break;
                    case ScheduleToDisplay.CoursesNotYetStarted:
                        currentlyScheduledCoursesBindingSource.DataSource = (from a in Dbconnection.GetAllCourseSchedulesWhichHaveNotStartedYet()
                                                                             select new CurrentlyScheduledCourses()
                                                                             {
                                                                                 CourseStartDate = a.StartDate,
                                                                                 CourseEndDate = a.EndDate,
                                                                                 CourseID = a.CourseID,
                                                                                 CourseName = a.CourseName,
                                                                                 FacilitatorID = a.FacilitatorID,
                                                                                 FacilitatorName = a.FacilitatorName,
                                                                                 VenueID = a.VenueID,
                                                                                 VenueName = a.Venue,
                                                                                 AmountEnrolled = (int)a.AmountEnrolled,
                                                                                 CurriculumCourseID = a.CurriculumCourseID,
                                                                                 ScheduleLocationID = a.ScheduleLocationID,
                                                                                 ScheduleLocation = a.ScheduleLocation
                                                                             }).ToList<CurrentlyScheduledCourses>();
                        break;
                }
                refershFacilitators();
                refresfVenues();
            };




        }
        private void populateFacilitators()
        {
            if (currentlyScheduledCoursesBindingSource.Count > 0)
            {
                List<CurrentlyScheduledCourses> l = (List<CurrentlyScheduledCourses>)currentlyScheduledCoursesBindingSource.List;
                individualBindingSource.DataSource = (from a in l
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

        #endregion

        private void lookupDepartmentBindingSource_PositionChanged(object sender, EventArgs e)
        {

        }

        private void curriculumBindingSource_PositionChanged(object sender, EventArgs e)
        {

        }

        private void lookupDepartmentBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {

        }

        private void curriculumBindingSource_ListChanged(object sender, ListChangedEventArgs e)
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

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refershFacilitators();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        private void radGroupByCourses_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScheduleToDisplay = (ScheduleToDisplay)(Convert.ToInt32(((RadioButton)sender).Tag));
            refreshCourseSchedule();
        }

        private void btnpicFilterByCourse_Click(object sender, EventArgs e)
        {

        }

        private void btnpicFilterByVenue_Click(object sender, EventArgs e)
        {

        }

        private void btnpicFilterByFacilitator_Click(object sender, EventArgs e)
        {

        }

        private void togDisplayOnSite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void togDisplayOffSite_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
