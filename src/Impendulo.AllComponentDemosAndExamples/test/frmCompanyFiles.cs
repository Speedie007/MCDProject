using Impendulo.Common;

using Impendulo.Data.Models;
using Impendulo.Development.Company;
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
using System.Collections.ObjectModel;
using Impendulo.Common.Verifiction;
using Impendulo.Data.Models.DataGridViewStructures.Schedules.CompanyFiles;
using Impendulo.Data.Models.DataGridViewStructures.Students.CompanyFiles;
using Impendulo.Development.Students;
using Impendulo.Development.Company.CompanyDetails;

namespace Inpendulo.Development.Files.Files.CompanyFiles
{
    public partial class frmCompanyFiles : MetroForm
    {
        #region Internal Classes
        //internal class StudentReturnDetails
        //{
        //    public int StudentID { get; set; }
        //    public string IndividualFirstName { get; set; }
        //    public string IndividualLastname { get; set; }
        //    public string StudentIDNumber { get; set; }
        //    public string StudentPassportNumber { get; set; }
        //    public DateTime StudentlInitialDate { get; set; }
        //}
        //public class Schedules
        //{
        //    public string Department { get; set; }
        //    public string CurriculumName { get; set; }
        //    public string CourseName { get; set; }
        //    public int AmountEnrolled { get; set; }
        //    public DateTime StartDate { get; set; }
        //    public DateTime EndDate { get; set; }
        //    public string ScheduledLocation { get; set; }
        //    public string VenueName { get; set; }
        //    public string FacilitatorName { get; set; }
        //    public int VenueID { get; set; }
        //    public int FacilitactorID { get; set; }
        //    public int LocationID { get; set; }
        //    public int CurriculumID { get; set; }
        //}
        #endregion
        #region Global Variables
        public ProgressFile CurrentlySelectedCompanyProgressfile { get; private set; }
        public Company CurrentlySelectedCompany { get; private set; }
        private CurrentTab CurrentlySelectedTab { get; set; }
        private ScheduleToDisplay CurrentScheduleToDisplay { get; set; }

        private CustomSortableBindingList<Schedules> CurrentlySelectedSchedules { get; set; }
        #endregion

        #region Enum Properties
        private enum ScheduleToDisplay : int
        {
            CompletedCourses = 0,
            CoursesInProgress = 1,
            CoursesNotYetStarted = 2
        }
        private enum CurrentTab : int
        {
            Student = 1,
            Enrollments = 2,
            Schedules = 3,
            Reports = 4,
            Enqiury = 5

        }

        #endregion
        public frmCompanyFiles()
        {
            InitializeComponent();
            CurrentlySelectedTab = CurrentTab.Enqiury;
        }

        private void frmCompanyFiles_Load(object sender, EventArgs e)
        {
            this.refreshCompanyFile();
        }

        #region Common Functions
        private void loadCurrentTab()
        {
            switch (CurrentlySelectedTab)
            {
                case CurrentTab.Student:
                    this.refreshCompanyStudents();
                    break;
                case CurrentTab.Schedules:

                    refreshDepartments();
                    refreshCuriculums();
                    refreshCourses();
                    this.refreshSchedules();
                    break;
                case CurrentTab.Reports:

                    break;
                case CurrentTab.Enrollments:
                    this.refreshEnrollments();
                    break;
                case CurrentTab.Enqiury:

                    break;
            }
        }
        #endregion
        #region Student Tab
        #region Refresh Methods
        private void refreshCompanyStudents()
        {
            this.poulateCompanyStudents();
        }
        #endregion
        #region Populate Methods
        private void poulateCompanyStudents()
        {

            //new BindingList<User>(query);
            using (var Dbconnection = new MCDEntities())
            {
                studentBindingSource.DataSource = new CustomSortableBindingList<StudentReturnDetails>((from a in Dbconnection.StudentProgressFiles
                                                                                                       from b in a.CompanyStudentProgressFiles
                                                                                                       where b.CompanyProgressFileID == CurrentlySelectedCompanyProgressfile.ProgressFileID
                                                                                                       select new StudentReturnDetails
                                                                                                       {
                                                                                                           StudentID = a.Student.StudentID,
                                                                                                           IndividualFirstName = a.Student.Individual.IndividualFirstName,
                                                                                                           IndividualLastname = a.Student.Individual.IndividualLastname,
                                                                                                           StudentIDNumber = a.Student.StudentIDNumber,
                                                                                                           StudentPassportNumber = a.Student.StudentPassportNumber,
                                                                                                           StudentlInitialDate = a.Student.StudentlInitialDate
                                                                                                       })
                                                   .ToList<StudentReturnDetails>());

            };
        }
        #endregion
        #region Control Methods
        private void dgvStudnetDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    //Student StudentObj = (Student)(row.DataBoundItem);
                    //row.Cells[colStudentFirstName.Index].Value = StudentObj.Individual.IndividualFirstName.ToString();
                    //row.Cells[colStudentLAstName.Index].Value = StudentObj.Individual.IndividualLastname.ToString();
                }
            }

        }
        private void btnViewStudentDetails_Click(object sender, EventArgs e)
        {
            if (studentBindingSource.Count > 0)
            {
                Student CurrentlySelectedStudent = (Student)studentBindingSource.Current;
                using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentlySelectedStudent.StudentID))
                {
                    frm.ShowDialog();
                    CurrentlySelectedStudent.Individual.IndividualFirstName = frm.CurrentSelectedStudent.Individual.IndividualFirstName;
                    CurrentlySelectedStudent.Individual.IndividualLastname = frm.CurrentSelectedStudent.Individual.IndividualLastname;
                    CurrentlySelectedStudent.StudentIDNumber = frm.CurrentSelectedStudent.StudentIDNumber;
                    CurrentlySelectedStudent.StudentPassportNumber = frm.CurrentSelectedStudent.StudentPassportNumber;
                    studentBindingSource.ResetCurrentItem();
                }
            }
        }

        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            using (frmStudentAddUpdate frm = new frmStudentAddUpdate(0))
            {
                frm.ShowDialog();
                if (frm.CurrentSelectedStudent != null)
                {
                    /*Create  Student File And Link It To the Company
                     * ***********************************************/
                    OfProgressFiles.VerifyCompanyStudentProgressFile(frm.CurrentSelectedStudent.StudentID, this.CurrentlySelectedCompany.CompanyID);

                    /*Shows the Newly Added Student in the the list
                     * **********************************************/
                    if (studentBindingSource.Count > 0)
                    {
                        studentBindingSource.List.Insert(0, frm.CurrentSelectedStudent);
                        studentBindingSource.ResetBindings(true);
                    }
                    else
                    {
                        studentBindingSource.DataSource = new List<Student> { frm.CurrentSelectedStudent };
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Enrollment Tab
        #region Refresh Methods
        private void refreshEnrollments()
        {
            this.populateEnrolments();
        }
        #endregion

        #region Populate Methods
        private void populateEnrolments()
        {
            using (var Dbconnection = new MCDEntities())
            {
                getAllEnrollmentsPerCompany_ResultBindingSource.DataSource =
                    new CustomSortableBindingList<GetAllEnrollmentsPerCompany_Result>(Dbconnection.GetAllEnrollmentsPerCompany(OfProgressFiles.VerifyCompanyProgressFile(this.CurrentlySelectedCompany.CompanyID)).ToList<GetAllEnrollmentsPerCompany_Result>());
            };
        }
        #endregion
        #endregion

        #region Schedule Tab

        #region Refresh Method
        private void refreshSchedules()
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
        #region Populate Methods
        private void populateCourseSchedule()
        {
            currentlyScheduledCoursesBindingSource.Clear();
            using (var Dbconnection = new MCDEntities())
            {

                switch (CurrentScheduleToDisplay)
                {
                    case ScheduleToDisplay.CompletedCourses:
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection.GetAllScheduledCoursesWhichAreCompletedForSelectedCompany(OfProgressFiles.VerifyCompanyProgressFile(CurrentlySelectedCompany.CompanyID))
                                                                                               select new Schedules()
                                                                                               {
                                                                                                   Department = a.Department,
                                                                                                   CurriculumName = a.CurriculumName,
                                                                                                   CourseName = a.CourseName,
                                                                                                   AmountEnrolled = (int)a.AmountEnrolled,
                                                                                                   StartDate = a.StartDate,
                                                                                                   EndDate = a.EndDate,
                                                                                                   ScheduledLocation = a.ScheduledLocation,
                                                                                                   VenueName = a.VenueName,
                                                                                                   FacilitatorName = a.FacilitatorName,
                                                                                                   VenueID = a.VenueID,
                                                                                                   FacilitactorID = a.FacilitactorID,
                                                                                                   LocationID = a.LocationID,
                                                                                                   CurriculumID = a.CurriculumID
                                                                                               }).ToList<Schedules>());
                        currentlyScheduledCoursesBindingSource.DataSource = CurrentlySelectedSchedules;
                        break;
                    case ScheduleToDisplay.CoursesInProgress:
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection.GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedCompany(OfProgressFiles.VerifyCompanyProgressFile(CurrentlySelectedCompany.CompanyID))
                                                                                               select new Schedules()
                                                                                               {
                                                                                                   Department = a.Department,
                                                                                                   CurriculumName = a.CurriculumName,
                                                                                                   CourseName = a.CourseName,
                                                                                                   AmountEnrolled = (int)a.AmountEnrolled,
                                                                                                   StartDate = a.StartDate,
                                                                                                   EndDate = a.EndDate,
                                                                                                   ScheduledLocation = a.ScheduledLocation,
                                                                                                   VenueName = a.VenueName,
                                                                                                   FacilitatorName = a.FacilitatorName,
                                                                                                   VenueID = a.VenueID,
                                                                                                   FacilitactorID = a.FacilitactorID,
                                                                                                   LocationID = a.LocationID,
                                                                                                   CurriculumID = a.CurriculumID
                                                                                               }).ToList<Schedules>());
                        currentlyScheduledCoursesBindingSource.DataSource = CurrentlySelectedSchedules;
                        break;
                    case ScheduleToDisplay.CoursesNotYetStarted:
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection.GetAllScheduledCoursesWhichAreNotYetStartedForSelectedCompany(OfProgressFiles.VerifyCompanyProgressFile(CurrentlySelectedCompany.CompanyID))
                                                                                               select new Schedules()
                                                                                               {
                                                                                                   Department = a.Department,
                                                                                                   CurriculumName = a.CurriculumName,
                                                                                                   CourseName = a.CourseName,
                                                                                                   AmountEnrolled = (int)a.AmountEnrolled,
                                                                                                   StartDate = a.StartDate,
                                                                                                   EndDate = a.EndDate,
                                                                                                   ScheduledLocation = a.ScheduledLocation,
                                                                                                   VenueName = a.VenueName,
                                                                                                   FacilitatorName = a.FacilitatorName,
                                                                                                   VenueID = a.VenueID,
                                                                                                   FacilitactorID = a.FacilitactorID,
                                                                                                   LocationID = a.LocationID,
                                                                                                   CurriculumID = a.CurriculumID
                                                                                               }).ToList<Schedules>());
                        currentlyScheduledCoursesBindingSource.DataSource = CurrentlySelectedSchedules;
                        break;
                }
                refershFacilitators();
                refresfVenues();
            };




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
        private void populateFacilitators()
        {
            if (currentlyScheduledCoursesBindingSource.Count > 0)
            {
                CustomSortableBindingList<Schedules> l = (CustomSortableBindingList<Schedules>)currentlyScheduledCoursesBindingSource.List;
                individualBindingSource.DataSource = (from a in l
                                                      select new Individual()
                                                      {
                                                          IndividualID = a.FacilitactorID,
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
            if (currentlyScheduledCoursesBindingSource.Count > 0)
            {
                CustomSortableBindingList<Schedules> l = (CustomSortableBindingList<Schedules>)currentlyScheduledCoursesBindingSource.List;
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
        #endregion


        #region Events
        private void btnScheduleShowFilters_Click(object sender, EventArgs e)
        {
            splitContainerScheduleFilterDivider.Panel1Collapsed = false;
            btnScheduleShowFilters.Visible = false;
            btnScheduleHideFIlters.Visible = true;
        }

        private void btnScheduleHideFIlters_Click(object sender, EventArgs e)
        {
            splitContainerScheduleFilterDivider.Panel1Collapsed = true;
            btnScheduleHideFIlters.Visible = false;
            btnScheduleShowFilters.Visible = true;
        }

        private void btnSchedulePrintCourseRegister_Click(object sender, EventArgs e)
        {

        }
        private void btnpicFilterDepartment_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.LookupDepartment)lookupDepartmentBindingSource.Current).DepartmentName.ToLower();
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.Department.ToLower().Contains(FilterStr)
                                                                                                          select a).ToList<Schedules>());
        }

        private void btnpicFilterCurriculum_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Curriculum)curriculumBindingSource.Current).CurriculumName.ToLower();
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.CurriculumName.ToLower().Contains(FilterStr)
                                                                                                          select a).ToList<Schedules>());
        }

        private void btnpicFilterDates_Click(object sender, EventArgs e)
        {
            DateTime FilterStartDate = dtScheduleFilterStartTime.Value.Date;
            DateTime FilterEndDate = dtScheduleFilterEndTime.Value.Date;
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.StartDate.Date >= FilterStartDate && a.EndDate.Date <= dtScheduleFilterEndTime.Value.Date
                                                                                                          select a).ToList<Schedules>());
        }

        private void dtScheduleFilterStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtScheduleFilterEndTime.Value < dtScheduleFilterStartTime.Value)
            {
                dtScheduleFilterEndTime.Value = dtScheduleFilterStartTime.Value;
            }
        }

        private void dtScheduleFilterEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtScheduleFilterEndTime.Value < dtScheduleFilterStartTime.Value)
            {
                dtScheduleFilterStartTime.Value = dtScheduleFilterEndTime.Value;
            }
        }
        private void radGroupByCourses_CheckedChanged(object sender, EventArgs e)
        {
            CurrentScheduleToDisplay = (ScheduleToDisplay)(Convert.ToInt32(((RadioButton)sender).Tag));
            refreshSchedules();
        }
        private void btnpicFilterByCourse_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Course)courseBindingSource.Current).CourseName.ToLower();
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.CourseName.ToLower().Contains(FilterStr)
                                                                                                          select a).ToList<Schedules>());
        }

        private void btnpicFilterByFacilitator_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Individual)individualBindingSource.Current).FullName.ToLower();
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.FacilitatorName.ToLower().Contains(FilterStr)
                                                                                                          select a).ToList<Schedules>());
        }

        private void btnpicFilterByVenue_Click(object sender, EventArgs e)
        {
            string FilterStr = ((Impendulo.Data.Models.Venue)venueBindingSource.Current).VenueName.ToLower();
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where a.VenueName.ToLower().Contains(FilterStr)
                                                                                                          select a).ToList<Schedules>());
        }

        private void togDisplayOnSite_CheckedChanged(object sender, EventArgs e)
        {
            if (!togDisplayOnSite.Checked && !togDisplayOffSite.Checked)
            {
                togDisplayOffSite.Checked = true;
            }
            this.LoadScheduleBasedOnSiteLocation();
        }

        private void togDisplayOffSite_CheckedChanged(object sender, EventArgs e)
        {
            if (!togDisplayOnSite.Checked && !togDisplayOffSite.Checked)
            {
                togDisplayOnSite.Checked = true;
            }
            this.LoadScheduleBasedOnSiteLocation();
        }

        private void LoadScheduleBasedOnSiteLocation()
        {
            List<int> LocationToFilter = new List<int>();
            if (togDisplayOnSite.Checked)
            {
                LocationToFilter.Add(1);
            }
            if (togDisplayOffSite.Checked)
            {
                LocationToFilter.Add(2);
            }
            currentlyScheduledCoursesBindingSource.DataSource = new CustomSortableBindingList<Schedules>((from a in CurrentlySelectedSchedules
                                                                                                          where LocationToFilter.Contains(a.LocationID)
                                                                                                          select a).ToList<Schedules>());
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

        private void btnREsetSchedulesFilters_Click(object sender, EventArgs e)
        {
            currentlyScheduledCoursesBindingSource.DataSource = CurrentlySelectedSchedules;
        }
        #endregion
        #endregion
        #region Refresh Methods
        private void refreshCompanyFile()
        {
            this.populateCompanyFiles();
        }

        #endregion

        #region Populate Methods

        private void populateCompanyFiles()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedCompanyProgressfile = (from a in Dbconnection.ProgressFiles
                                                        where a.CompanyProgressFile.CompanyID == CurrentlySelectedCompany.CompanyID
                                                        select a)
                                                         .Include(a => a.CompanyProgressFile)
                                                         .FirstOrDefault<ProgressFile>();
                this.gbFileSections.Enabled = true;
                txtCompanyName.Text = this.CurrentlySelectedCompany.CompanyName;
                txtCompanyProgressFileID.Text = CurrentlySelectedCompanyProgressfile.ProgressFileID.ToString();
                if (CurrentlySelectedCompanyProgressfile != null)
                { gbFileSections.Enabled = true; }
                else { gbFileSections.Enabled = false; }
                this.loadCurrentTab();

            };

        }

        #endregion

        private void btnpicFileSearch_Click(object sender, EventArgs e)
        {
            using (frmCompanySearch frm = new frmCompanySearch())
            {
                frm.ShowDialog();
                if (frm.CurrentCompany != null)
                {
                    OfProgressFiles.VerifyCompanyProgressFile(frm.CurrentCompany.CompanyID);
                    this.CurrentlySelectedCompany = frm.CurrentCompany;
                    this.refreshCompanyFile();
                }
            }
        }

        private void btnpicViewCompanyProfile_Click(object sender, EventArgs e)
        {
            if (CurrentlySelectedCompany != null)
            {
                using (ViewEditCompanyDetails frm = new ViewEditCompanyDetails(CurrentlySelectedCompany.CompanyID))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void btnpicViewCompanyContacts_Click(object sender, EventArgs e)
        {

        }

        private void dgvStudnetDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// MainTab Index Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            {
                case 0:
                    CurrentlySelectedTab = CurrentTab.Enqiury;
                    break;
                case 1:
                    CurrentlySelectedTab = CurrentTab.Enrollments;
                    break;
                case 2:
                    CurrentlySelectedTab = CurrentTab.Schedules;
                    break;
                case 3:
                    CurrentlySelectedTab = CurrentTab.Reports;
                    break;
                case 4:
                    CurrentlySelectedTab = CurrentTab.Student;
                    break;
                default:
                    CurrentlySelectedTab = CurrentTab.Enqiury;
                    break;
            }
            loadCurrentTab();
        }

        private void dgvScheduledCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}

