using Impendulo.Common.Enum;
using Impendulo.Common.FileHandeling;
using Impendulo.Data;
using Impendulo.Data.Models;
using Impendulo.Deployment.Company;
using Impendulo.Deployment.Company.CompanyDetails;
using Impendulo.Deployment.Email;
using Impendulo.Deployment.Scheduling;
using Impendulo.Deployment.Scheduling.ConfirmScheduledCourse;
using Impendulo.Deployment.Students;
using Impendulo.Deployments.Assessments.ApprenticeshipAssessments.AddNewReport;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Enrollment
{
    public partial class frmEnrollmentProgress : MetroForm
    {

        /*Diff Modes that the form can be loaded
         1. Listed by current Enquiry - which list all enrollments for the the specific enquiry
         2. Lists by current Equiry and Enrollment which lists only current enrollemnt associated with the a specific enquiry
         3. All Enrollments (First 50)
         4. Enrolment filter by search page.
         */
        //private Boolean IsLoadingPreRequisiteEnrollment = false;
        public EnumDepartments CurrentSelectedDepartment { get; set; }
        public Employee CurrentEmployeeLoggedIn { get; set; }
        private int CurrentEnrollmentID { get; set; }
        private int CurrentEnrollmentPreRequisiteID { get; set; }

        private int CurrentCurriculumCourseEnrollmentID { get; set; }
        public int CurrentEquiryID { get; set; }
        private Data.Models.Enrollment CurrentEnrollment { get; set; }

        private List<CurriculumCourseEnrollment> AllPreRequisiteEnrollments;
        public frmEnrollmentProgress(int CurrentEnrollmentID)
        {
            this.CurrentEnrollmentID = CurrentEnrollmentID;
            AllPreRequisiteEnrollments = new List<CurriculumCourseEnrollment>();
            CurrentEnrollmentPreRequisiteID = 0;
            CurrentCurriculumCourseEnrollmentID = 0;
            InitializeComponent();
            splitContainerUpdateCurriculumCourseDetails.Panel1Collapsed = false;
            splitContainerUpdateCurriculumCourseDetails.Panel2Collapsed = true;
        }

        private void frmEnrollmentInProgressV2_Load(object sender, EventArgs e)
        {
            //loadup Doucment Types
            refreshEnrollmentDocumentTypes();
            //Load up Current Enrollment
            refreshEnrollment();
            showAddCourseForEnrollment();
            refreshEnrollmentDocuments();
        }

        private void showAddCourseForEnrollment()
        {
            splitContainerAddAndRevertCourse.Panel1Collapsed = false;
            splitContainerAddAndRevertCourse.Panel2Collapsed = true;
        }

        private void hideAddCourseForEnrollment()
        {
            splitContainerAddAndRevertCourse.Panel1Collapsed = true;
            splitContainerAddAndRevertCourse.Panel2Collapsed = false;
        }

        #region Resfresh Methods
        private void refreshEnrollmentDocumentTypes()
        {
            this.populateEnrollmentDocumentTypes();
        }
        private void refreshEnrollment()
        {
            this.populateEnrollment();

        }
        private void refershEnrollmentPreRequisites()
        {
            this.populateEnrollmentPreRequisites();
        }

        private void refreshEnrollmentDocuments()
        {
            this.populateEnrollmentFiles();
        }
        #endregion

        #region Populate Methods
        private void populateEnrollmentFiles()
        {

            using (var Dbconnection = new MCDEntities())
            {
                List<EnrollmentFiles> EF = new List<EnrollmentFiles>();

                int filterID = Convert.ToInt32(cboAttachmentType.SelectedValue);
                if (filterID == (int)Common.Enum.EnumEnrollentDocumentTypes.ID_Documents)
                {
                    Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(enrollmentBindingSourceMain.Current);
                    var ListOfFiles = (from a in Dbconnection.StudentIDDocuments
                                       where a.StudentID == EnrollmentObj.StudentEnrollment.StudentID
                                       select new
                                       {
                                           FileID = a.File.FileID,
                                           FileName = a.File.FileName,
                                           FileExtension = a.File.FileExtension,
                                           EnrollentDocumentType = "Student ID Document",
                                           EnrollmentDocumentTypeID = Common.Enum.EnumEnrollentDocumentTypes.ID_Documents

                                       }).ToList();

                    foreach (var obj in ListOfFiles)
                    {
                        EF.Add(new EnrollmentFiles()
                        {
                            FileID = obj.FileID,
                            FileName = obj.FileName,
                            FileExtension = obj.FileExtension,
                            EnrollmentDocumentTypeID = (int)Common.Enum.EnumEnrollentDocumentTypes.ID_Documents,
                            EnrollentDocumentType = obj.EnrollentDocumentType
                        });
                    }
                }
                else
                {
                    var ListOfFiles = (from a in Dbconnection.EnrollmentDocuments
                                       where a.EnrollmentID == CurrentEnrollmentID &&
                                        a.LookupEnrollmentDocumentTypeID == filterID
                                       select new
                                       {
                                           FileID = a.File.FileID,
                                           FileName = a.File.FileName,
                                           FileExtension = a.File.FileExtension,
                                           EnrollentDocumentType = a.LookupEnrollentDocumentType.EnrollmentDocumentType,
                                           EnrollmentDocumentTypeID = a.LookupEnrollmentDocumentTypeID

                                       }).ToList();

                    //List<File> AllFiles = new List<File>();
                    foreach (var obj in ListOfFiles)
                    {
                        EF.Add(new EnrollmentFiles()
                        {
                            FileID = obj.FileID,
                            FileName = obj.FileName,
                            FileExtension = obj.FileExtension,
                            EnrollmentDocumentTypeID = obj.EnrollmentDocumentTypeID,
                            EnrollentDocumentType = obj.EnrollentDocumentType
                        });
                    }
                }


                enrollmentFilesBindingSource.DataSource = EF;

            };
        }

        private void populateEnrollmentDocumentTypes()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupEnrollentDocumentTypeBindingSource.DataSource = (from a in Dbconnection.LookupEnrollentDocumentTypes
                                                                       select a).ToList<LookupEnrollentDocumentType>();
            };

        }
        private void populateEnrollment()
        {
            int EnrollmentToLoadID = 0;
            using (var Dbconnection = new MCDEntities())
            {
                //Load PreRequisteEnrollment
                if (CurrentEnrollmentPreRequisiteID != 0)
                {
                    EnrollmentToLoadID = this.CurrentEnrollmentPreRequisiteID;
                }
                else//Load Enrollment
                {
                    EnrollmentToLoadID = this.CurrentEnrollmentID;
                }
                CurrentEnrollment = (from a in Dbconnection.Enrollments
                                     where a.EnrollmentID == EnrollmentToLoadID
                                     select a)
                                    .Include(a => a.StudentEnrollment)
                                    .Include(a => a.StudentEnrollment.Student.StudentAssociatedCompanies)
                                    .Include(a => a.StudentEnrollment.Student.StudentAssociatedCompanies.Select(b => b.Company))
                                    .Include(a => a.StudentEnrollment.Student)
                                    .Include(a => a.StudentEnrollment.Student.Individual)
                                    .Include(a => a.StudentEnrollment.Student.Individual.ContactDetails)
                                    //.Include(a => a.CurriculumEnquiry)
                                    .Include(a => a.CurriculumCourseEnrollments)
                                    //LookupEnrollmentProgressState
                                    .Include(a => a.CurriculumCourseEnrollments.Select(b => b.LookupEnrollmentProgressState))
                                    .Include(a => a.CurriculumCourseEnrollments.Select(b => b.Schedules))
                                    .Include(a => a.CurriculumCourseEnrollments.Select(b => b.CurriculumCourse).Select(c => c.CurriculumCourseMinimumMaximum))
                                    .Include(a => a.CurriculumCourseEnrollments.Select(b => b.CurriculumCourse.CurriculumCourseDayCanBeScheduleds))
                                    .Include(a => a.Curriculum)
                                    .FirstOrDefault<Data.Models.Enrollment>();

                if (CurrentEnrollmentPreRequisiteID != 0)
                {

                    List<CurriculumCourseEnrollment> CurriculumCourseEnrollmentTemp = new List<CurriculumCourseEnrollment>();
                    foreach (CurriculumCourseEnrollment CurriculumCourseEnrollmentObj in CurrentEnrollment.CurriculumCourseEnrollments)
                    {
                        //             //trying code
                        //             CurrentEnrollment.CurriculumCourseEnrollments.Clear();
                        //             Dbconnection.Entry(blog)
                        //.Collection("Posts")
                        //.Query()
                        //.Where(p => p.Tags.Contains("entity-framework")
                        //.Load();
                        //             //end trying code

                        if (CurriculumCourseEnrollmentObj.CurriculumCourseEnrollmentID == this.CurrentCurriculumCourseEnrollmentID)
                        {
                            CurriculumCourseEnrollmentTemp.Add(CurriculumCourseEnrollmentObj);
                        }
                    }
                    CurrentEnrollment.CurriculumCourseEnrollments.Clear();
                    foreach (CurriculumCourseEnrollment CurriculumCourseEnrollmentObj in CurriculumCourseEnrollmentTemp)
                    {
                        CurrentEnrollment.CurriculumCourseEnrollments.Add(CurriculumCourseEnrollmentObj);
                    }

                    //SetControls

                }
                else
                {

                }

                enrollmentBindingSourceMain.DataSource = CurrentEnrollment;

            };
        }
        private void setControls()
        {
            if (CurrentEnrollmentPreRequisiteID != 0)
            {
                //btnSelectCourses.Enabled = false;
            }
            else
            {
                //check that all PreRequisites are iether exempt or completed.

                //if ()
                //{

                //}
                // btnSelectCourses.Enabled = true;
            }
        }
        private void populateEnrollmentPreRequisites()
        {
            int EnrollmentToLoadID = 0;
            using (var Dbconnection = new MCDEntities())
            {
                //Load PreRequisteEnrollment
                if (CurrentEnrollmentPreRequisiteID != 0)
                {
                    EnrollmentToLoadID = this.CurrentEnrollmentPreRequisiteID;
                }
                else//Load Enrollment
                {
                    EnrollmentToLoadID = this.CurrentEnrollmentID;
                }
                AllPreRequisiteEnrollments = (from a in Dbconnection.CurriculumCourseEnrollments
                                                  //from b in a.CurriculumCourse.Curriculum

                                                  //from b in a.CurriculumCourseEnrollments
                                                  //from c in b.CurriculumCourse.Course
                                              where a.Enrollment.EnrolmentParentID == EnrollmentToLoadID
                                              select a)
                                                .Include(a => a.Enrollment)
                                                .Include(a => a.CurriculumCourse)
                                                .Include(a => a.CurriculumCourse.Course)
                                                .Include(a => a.CurriculumCourse.Curriculum)
                                                .Include(a => a.LookupEnrollmentProgressState)
                                                .ToList<CurriculumCourseEnrollment>();
            };


            curriculumCourseEnrollmentPreRequisiteCourseBindingSource.DataSource = AllPreRequisiteEnrollments;
        }

        #endregion

        #region Controls Event Methods

        #endregion



        private void enrollmentBindingSourceMain_DataSourceChanged(object sender, EventArgs e)
        {
            refershEnrollmentPreRequisites();

        }

        private void dgvEnrollmentPreRequisites_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    CurriculumCourseEnrollment CurriculumCourseEnrollmentObj = (CurriculumCourseEnrollment)row.DataBoundItem;

                    if (!(CurriculumCourseEnrollmentObj.LookupEnrollmentProgressStateID == (int)EnumEnrollmentProgressStates.Excempt))
                    {
                        row.Cells[colPreRequisiteCourseProcessPreRequisiteCourse.Index].Value = "[ Edit ]";

                        row.Cells[colPreRequisiteCourseExemmptionStatus.Index].Value = "[ Exempt Course ]";
                        row.Cells[colPreRequisiteCourseExemmptionStatus.Index].Tag = 0;
                    }
                    else
                    {
                        row.Cells[colPreRequisiteCourseProcessPreRequisiteCourse.Index].Value = "";
                        row.Cells[colPreRequisiteCourseExemmptionStatus.Index].Value = "[ Re-Instate ]";
                        row.Cells[colPreRequisiteCourseExemmptionStatus.Index].Tag = 1;
                    }
                    row.Cells[colPreRequisiteCourseCurriculumName.Index].Value = CurriculumCourseEnrollmentObj.CurriculumCourse.Curriculum.CurriculumName.ToString();
                    row.Cells[colPreRequisiteCourseName.Index].Value = CurriculumCourseEnrollmentObj.CurriculumCourse.Course.CourseName.ToString();
                    row.Cells[colPreRequisiteCourseStatus.Index].Value = CurriculumCourseEnrollmentObj.LookupEnrollmentProgressState.EnrollmentProgressCurrentState.ToString();
                    //
                    //CurriculumCourse CurriculumCourseObj = (from a in EnrollmentObj.CurriculumCourseEnrollments
                    //                                        where a.CurriculumCourse.CurriculumID == EnrollmentObj.CurriculumID
                    //                                        select a.CurriculumCourse)
                    //                                        .FirstOrDefault<CurriculumCourse>();
                    //row.Cells[colApprenticeshipPreRequisiteCourse.Index].Value = CurriculumCourseObj.Course.CourseName.ToString();
                    //row.Cells[colApprenticeshipPrerequisteProcessingStatus.Index].Value = EnrollmentObj.LookupEnrollmentProgressState.EnrollmentProgressCurrentState.ToString();
                }
            }
        }



        private void dgvEnrollmentPreRequisites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Enrollment PreRequisiteEnrollment = ((CurriculumCourseEnrollment)curriculumCourseEnrollmentPreRequisiteCourseBindingSource.Current).Enrollment;
            CurriculumCourseEnrollment CurriculumCourseEnrollmentObj = ((CurriculumCourseEnrollment)curriculumCourseEnrollmentPreRequisiteCourseBindingSource.Current);

            switch (e.ColumnIndex)
            {
                case 3:
                    this.CurrentEnrollmentPreRequisiteID = CurriculumCourseEnrollmentObj.Enrollment.EnrollmentID;
                    this.CurrentCurriculumCourseEnrollmentID = CurriculumCourseEnrollmentObj.CurriculumCourseEnrollmentID;
                    this.refreshEnrollment();
                    this.hideAddCourseForEnrollment();
                    break;
                case 4:
                    if (Convert.ToInt32(dgvEnrollmentPreRequisites.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag) == 0)
                    {
                        using (frmEnrollmentException frm = new frmEnrollmentException())
                        {
                            frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                            frm.EnquiryID = CurrentEquiryID;
                            frm.SelectedCurriculumCourseEnrollment = (CurriculumCourseEnrollment)curriculumCourseEnrollmentPreRequisiteCourseBindingSource.Current;
                            frm.ShowDialog();
                        }
                    }
                    else
                    {

                    }

                    refreshEnrollment();
                    break;
            }
        }

        private void btnSwitchBackToParentEnrollment_Click(object sender, EventArgs e)
        {
            if (CheckIfAllPreRequisitieCoursesAreCompleted())
            {
                using (frmEnrollmentCourseSelection frm = new frmEnrollmentCourseSelection())
                {
                    frm.CurrentEnrollemnt = this.CurrentEnrollment;
                    frm.ShowDialog();
                    refreshEnrollment();
                }
            }
        }

        private Boolean CheckIfAllPreRequisitieCoursesAreCompleted()
        {
            Boolean Rtn = true;
            foreach (CurriculumCourseEnrollment CCE in curriculumCourseEnrollmentPreRequisiteCourseBindingSource.List)
            {
                if (CCE.LookupEnrollmentProgressStateID == (int)EnumEnrollmentProgressStates.In_Progress || CCE.LookupEnrollmentProgressStateID == (int)EnumEnrollmentProgressStates.New_Enrollment)
                {
                    MessageBox.Show(CCE.CurriculumCourse.Curriculum.CurriculumName + " - " + CCE.CurriculumCourse.Course.CourseName + " Is Incomplete - Schedule and process the course first once completed, proceed to configure the courses for the enrollment.", "Process Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return Rtn;

        }
        private void dgvEnrollmentCourseMain_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    CurriculumCourseEnrollment CurriculumCourseEnrollmentObj = (CurriculumCourseEnrollment)row.DataBoundItem;
                    ObservableListSource<Schedule> Schedules = (ObservableListSource<Schedule>)CurriculumCourseEnrollmentObj.Schedules;

                    row.Cells[colCourseEnrollmentMainCourseName.Index].Value = CurriculumCourseEnrollmentObj.CurriculumCourse.Course.CourseName.ToString();
                    row.Cells[colCourseEnrollmentMainStatus.Index].Value = CurriculumCourseEnrollmentObj.LookupEnrollmentProgressState.EnrollmentProgressCurrentState.ToString();

                    /*Course Has Been Scheduled
                     * **************************/
                    if ((from a in Schedules
                         where a.CurriculumCourseEnrollmentID == CurriculumCourseEnrollmentObj.CurriculumCourseEnrollmentID
                         select a).ToList<Schedule>().Count > 0)
                    {
                        Schedule ScheduleObj = (from a in Schedules
                                                where a.CurriculumCourseEnrollmentID == CurriculumCourseEnrollmentObj.CurriculumCourseEnrollmentID
                                                orderby a.ScheduleID
                                                select a).LastOrDefault<Schedule>();

                        row.Cells[colInProgressSelectedCourseMustSchedule.Index].Value = "[ Edit ]";
                        row.Cells[colInProgressSelectedCourseMustSchedule.Index].Tag = "EDIT";

                        row.Cells[colSelectedEnrollemntStartDate.Index].Value = ScheduleObj.ScheduleStartDate.ToString("D");
                        row.Cells[colSelectedEnrollemntEndDate.Index].Value = ScheduleObj.ScheduleCompletionDate.ToString("D");

                        if (CurriculumCourseEnrollmentObj.LookupEnrollmentProgressStateID == (int)Common.Enum.EnumEnrollmentProgressStates.In_Progress)
                        {

                            switch ((EnumScheduleStatuses)CurriculumCourseEnrollmentObj.Schedules.FirstOrDefault().LookupScheduleStatus.ScheduleStatusID)
                            {

                                case EnumScheduleStatuses.Reserved:
                                    row.Cells[colAddEditCourseReport.Index].Value = "[ Confirm Reservation ]";
                                    row.Cells[colAddEditCourseReport.Index].Tag = "ConfirmReservation";
                                    row.Cells[colScheduleConfirmed.Index].Value = "N";
                                    break;

                                case EnumScheduleStatuses.Confirmed:
                                    row.Cells[colAddEditCourseReport.Index].Value = "[ Complete Report ]";
                                    row.Cells[colAddEditCourseReport.Index].Tag = "CompleteReport";
                                    row.Cells[colScheduleConfirmed.Index].Value = "Y";
                                    break;

                            }

                            //colAddEditCourseReport

                        }
                        else if (CurriculumCourseEnrollmentObj.LookupEnrollmentProgressStateID == (int)Common.Enum.EnumEnrollmentProgressStates.Complete)
                        {
                            row.Cells[colAddEditCourseReport.Index].Value = "[ Update Report ]";
                            row.Cells[colAddEditCourseReport.Index].Tag = "UpdateReport";
                        }
                    }
                    else
                    {
                        /*TO-DO
                         * Must Sort out the Squencing for if a course is required to be re scheduled
                         * ****************************************************************************/

                        /*Course  Not Yet Scheduled
                         * **************************/
                        row.Cells[colInProgressSelectedCourseMustSchedule.Index].Value = "[ Schedule Course ]";
                        row.Cells[colInProgressSelectedCourseMustSchedule.Index].Tag = "SCHEDULE";
                        //row.Cells[colInProgressSelectedCourseMustSchedule.Index].Tag = "EDIT";
                        row.Cells[colSelectedEnrollemntStartDate.Index].Value = "Not Yet Scheduled";
                        row.Cells[colSelectedEnrollemntEndDate.Index].Value = "Not Yet Scheduled";
                        row.Cells[colScheduleConfirmed.Index].Value = "(NA)";
                    }


                }
            }

        }

        private void btnSelectCourses_Click(object sender, EventArgs e)
        {
            using (frmEnrollmentCourseSelection frm = new frmEnrollmentCourseSelection())
            {
                frm.CurrentEnrollemnt = this.CurrentEnrollment;
                frm.ShowDialog();
                refreshEnrollment();
            }
        }

        private void btnStudentInformation_Click(object sender, EventArgs e)
        {
            using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentEnrollment.StudentEnrollment.StudentID))
            {
                //frm.CurrentStudentID = CurrentEnrollment.Student.StudentID;
                frm.ShowDialog();
                if (frm.CurrentSelectedStudent != null)
                {
                    Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(enrollmentBindingSourceMain.Current);
                    EnrollmentObj.StudentEnrollment.Student = frm.CurrentSelectedStudent;
                    enrollmentBindingSourceMain.ResetCurrentItem();
                }
            }
        }

        private void btnBackToMaunEnrollmentTop_Click(object sender, EventArgs e)
        {
            this.CurrentEnrollmentPreRequisiteID = 0;
            this.refreshEnrollment();
        }
        private List<EnumDayOfWeeks> GetDayThatCurriculumCourseCanBeScheduled(CurriculumCourseEnrollment CurrentSelectedCurriculumCourseEnrollment)
        {
            List<EnumDayOfWeeks> DaysCanSchedule = new List<EnumDayOfWeeks>();
            //sets the Date One Day Ahead
            foreach (CurriculumCourseDayCanBeScheduled CCDCBS in CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseDayCanBeScheduleds)
            {
                DaysCanSchedule.Add((EnumDayOfWeeks)CCDCBS.DayOfWeekID);
            };
            return DaysCanSchedule;
        }
        //private DateTime EnsureSchedulableDate(DateTime SelectedDate)
        //{
        //    Boolean IsValidDay = false;

        //    while (!IsValidDay)
        //    {
        //        EnumDayOfWeeks d = Common.CustomDateTime.CustomDayOfTheWeek(SelectedDate.DayOfWeek);
        //        if (GetDayThatCurriculumCourseCanBeScheduled().Contains(d))
        //        {
        //            IsValidDay = true;
        //        }
        //        else
        //        {
        //            SelectedDate.AddDays(1);
        //        }
        //    }
        //    return SelectedDate;
        //}
        private void dgvEnrollmentCourseMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CurriculumCourseEnrollment CurrentSelectedCurriculumCourseEnrollment = (CurriculumCourseEnrollment)curriculumCourseEnrollmentsMainCoursesBindingSource.Current;

            switch (e.ColumnIndex)
            {

                case 0:


                    if (dgvEnrollmentCourseMain.Rows[e.RowIndex].Cells[0].Tag.Equals("EDIT"))
                    {
                        splitContainerUpdateCurriculumCourseDetails.Panel1Collapsed = true;
                        splitContainerUpdateCurriculumCourseDetails.Panel2Collapsed = false;
                        CurriculumCourseEnrollment CCE = (CurriculumCourseEnrollment)curriculumCourseEnrollmentsMainCoursesBindingSource.Current;
                        txtUpdateCurriulumCourseEnrollemntCost.Text = Convert.ToInt32(CCE.CourseCost).ToString();
                    }
                    else
                    {
                        //If if selected Course does not have a Parent Course then it can be scheduled any time forward from today
                        if (CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseParentID == 0)
                        {
                            //TODO: Open Scheduling Form - Pass Currently Selected CurriculumCourseEnrollment Object.

                            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

                            using (frmScheduleCurriculumCourseWizard frm = new frmScheduleCurriculumCourseWizard(
                                InitialStartDate: dt.Date.AddDays(1),
                                CurrentSelectedCurriculumCourseEnrollment: CurrentSelectedCurriculumCourseEnrollment
                                ))
                            {
                                frm.ShowDialog();
                            }
                        }
                        else
                        {
                            /*Has PreRequisite Course*/
                            //Step 1
                            //Determine if all PreRequisite Courses have been Scheduled.
                            if (DetermineIfAllPreRequisiteCourseHaveBeenScheduled(
                                _CurriculumCourseID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourseID,
                                _EnrollmentID: CurrentSelectedCurriculumCourseEnrollment.EnrollmentID))
                            {
                                //IF All Course Pre-Requiesties Are Scheduled then:
                                //TODO: Open Scheduling Form - Pass the Selected Curriculum CourseEnrollment Object
                                int PreRequisiteCurriculumCourseID = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.CurriculumCourseParentID;

                                Schedule PreRequisiteSchedule = (from a in CurrentEnrollment.CurriculumCourseEnrollments
                                                                 from c in a.Schedules
                                                                 where a.CurriculumCourseID == PreRequisiteCurriculumCourseID
                                                                 select c).FirstOrDefault<Schedule>();


                                using (var Dbconnection = new MCDEntities())
                                {
                                    DateTime dt = DateTime.Now;
                                    //Boolean hhh = Convert.ToBoolean((Dbconnection.DetermineIfTheFirstCousrseToBeScheduled(
                                    //       curriculumID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Curriculum.CurriculumID,
                                    //       enrollmentID: CurrentSelectedCurriculumCourseEnrollment.EnrollmentID,
                                    //       curriculumCourseID_ToCheck: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourseID).First().Value));

                                    if (!Convert.ToBoolean((Dbconnection.DetermineIfTheFirstCousrseToBeScheduled(
                                          curriculumID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Curriculum.CurriculumID,
                                          enrollmentID: CurrentSelectedCurriculumCourseEnrollment.EnrollmentID,
                                          curriculumCourseID_ToCheck: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourseID).First().Value)))
                                    {
                                        System.Data.Entity.Core.Objects.ObjectResult<DateTime?> CC = Dbconnection.GetCurriculumCourseEnrollmentPreRequisiteEndDate(
                                        curriculumCourseID: CurrentSelectedCurriculumCourseEnrollment.CurriculumCourseID,
                                        enrollmentID: CurrentSelectedCurriculumCourseEnrollment.EnrollmentID);
                                        dt = CC.FirstOrDefault<DateTime?>().Value;
                                    }

                                    if (dt <= DateTime.Now.Date)
                                    {
                                        dt = DateTime.Now.Date;
                                    }
                                    using (frmScheduleCurriculumCourseWizard frm = new frmScheduleCurriculumCourseWizard(
                                        InitialStartDate: dt.AddDays(1),
                                        CurrentSelectedCurriculumCourseEnrollment: CurrentSelectedCurriculumCourseEnrollment
                                        ))
                                    {
                                        frm.lblWziardHeading.Text = CurrentSelectedCurriculumCourseEnrollment.CurriculumCourse.Course.CourseName;
                                        frm.ShowDialog();
                                    }
                                };

                            }
                        }
                    }

                    //this.CurrentEnrollmentPreRequisiteID = CurriculumCourseEnrollmentObj.Enrollment.EnrollmentID;
                    //this.CurrentCurriculumCourseEnrollmentID = CurriculumCourseEnrollmentObj.CurriculumCourseEnrollmentID;
                    this.refreshEnrollment();
                    this.hideAddCourseForEnrollment();
                    break;
                case 1:

                    switch (dgvEnrollmentCourseMain.Rows[e.RowIndex].Cells[1].Tag.ToString())
                    {
                        case "CompleteReport":
                            //CurrentSelectedCurriculumCourseEnrollment
                            using (frmAddNewApprenticeshipAssessmentReport frm = new frmAddNewApprenticeshipAssessmentReport(CurrentlySelectedCurriculumCourseEnrollment: CurrentSelectedCurriculumCourseEnrollment))
                            {
                                frm.ShowDialog();
                                this.refreshEnrollment();
                            }
                            break;

                        case "ConfirmReservation":
                            using (frmConfirmScheduledCourse frm = new frmConfirmScheduledCourse(CurrentSelectedCurriculumCourseEnrollment.Schedules.FirstOrDefault().ScheduleID, CurrentSelectedCurriculumCourseEnrollment.EnrollmentID))
                            {
                                frm.ShowDialog();
                                this.refreshEnrollment();
                            }
                            break;
                            /* row.Cells[colAddEditCourseReport.Index].Value = "[ Confirm Reservation ]";
                                        row.Cells[colAddEditCourseReport.Index].Tag = "ConfirmReservation";
                                        */
                    }

                    //            row.Cells[colAddEditCourseReport.Index].Value = "[ Complete Report ]";
                    //        row.Cells[colAddEditCourseReport.Index].Tag = "CompleteReport";
                    //}else if (CurriculumCourseEnrollmentObj.LookupEnrollmentProgressStateID == (int)Common.Enum.EnumEnrollmentProgressStates.Complete)
                    //{
                    //    row.Cells[colAddEditCourseReport.Index].Value = "[ Update Report ]";
                    //    row.Cells[colAddEditCourseReport.Index].Tag = "UpdateReport";
                    break;
            }
        }



        private Boolean DetermineIfAllPreRequisiteCourseHaveBeenScheduled(int _CurriculumCourseID, int _EnrollmentID)
        {
            Boolean Rtn = true;
            IEnumerable<CurriculumCourse> PreRequisiteCoursesNotScheduled;
            using (var Dbconnection = new MCDEntities())
            {
                PreRequisiteCoursesNotScheduled = (from a in Dbconnection.GetCurriculumCoursePreRequisiteCourseNotYetScheduled(_CurriculumCourseID, _EnrollmentID)
                                                   select a)
                                                  .ToList<CurriculumCourse>();

                if (PreRequisiteCoursesNotScheduled.Count<CurriculumCourse>() > 0)
                {
                    int iCounter = 1;
                    Rtn = false;
                    String ErrorMessage = "Before proceeding to schedule this course please schedule the following course first, as these are Pre-Requisite Courses for the currently selected course:\n\n";
                    foreach (CurriculumCourse CC in PreRequisiteCoursesNotScheduled)
                    {
                        Dbconnection.Entry(CC).Reference(a => a.Course).Load();
                        ErrorMessage += iCounter + " - " + CC.Course.CourseName + "\n";
                        iCounter++;
                    }
                    MessageBox.Show(ErrorMessage, "Pre-Requisite Course To Schedule", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            };


            return Rtn;
        }

        private void btnRevertBackToMainEnrollment_Click(object sender, EventArgs e)
        {
            this.CurrentEnrollmentPreRequisiteID = 0;
            this.showAddCourseForEnrollment();
            this.refreshEnrollment();
            this.switchBackToScheduleList();
        }

        private void dgvEnrollmentCourseMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void btnUpdateCurriculumCourseEnrollmentCost_Click(object sender, EventArgs e)
        {
            CurriculumCourseEnrollment CCE = (CurriculumCourseEnrollment)curriculumCourseEnrollmentsMainCoursesBindingSource.Current;

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        Dbconnection.CurriculumCourseEnrollments.Attach(CCE);
                        CCE.CourseCost = Convert.ToInt32(txtUpdateCurriulumCourseEnrollemntCost.Text);
                        Dbconnection.Entry(CCE).State = EntityState.Modified;

                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        //commit transaction
                        dbTran.Commit();
                        Close();
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

        private void btnBackToSelctedCoursesSchedule_Click(object sender, EventArgs e)
        {
            switchBackToScheduleList();
        }
        private void switchBackToScheduleList()
        {
            splitContainerUpdateCurriculumCourseDetails.Panel1Collapsed = false;
            splitContainerUpdateCurriculumCourseDetails.Panel2Collapsed = true;
            curriculumCourseEnrollmentsMainCoursesBindingSource.ResetCurrentItem();
        }

        private void btnAddNewFile_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {

        }

        private void cboAttachmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshEnrollmentDocuments();
        }

        private void dgvEnrollmentFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    folderBrowserDialogToDownloadFile.ShowDialog();
                    if (System.IO.Directory.Exists(folderBrowserDialogToDownloadFile.SelectedPath))
                    {
                        EnrollmentFiles EF = (EnrollmentFiles)enrollmentFilesBindingSource.Current;
                        Data.Models.File f = Common.FileHandeling.FileHandeling.GetFile(EF.FileID);
                        MemoryStream ms = new MemoryStream(f.FileImage);
                        //write to file
                        FileStream file = new FileStream(folderBrowserDialogToDownloadFile.SelectedPath + "\\" + f.FileName + "." + f.FileExtension, FileMode.Create, FileAccess.Write);

                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                    }
                    break;
            }
        }

        private void enrollmentBindingSourceMain_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        private void enrollmentBindingSourceMain_PositionChanged(object sender, EventArgs e)
        {

        }

        private void enrollmentBindingSourceMain_ListChanged(object sender, ListChangedEventArgs e)
        {
            Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(enrollmentBindingSourceMain.Current);
            if (EnrollmentObj != null)
            {
                Boolean IsCompany = false;
                foreach (StudentAssociatedCompany SAC in EnrollmentObj.StudentEnrollment.Student.StudentAssociatedCompanies)
                {
                    if (SAC.IsCurrentCompany)
                    {
                        IsCompany = true;
                    }
                }
                if (IsCompany)
                {
                    txtCompanyNameDisplay.Text = EnrollmentObj.StudentEnrollment.Student.StudentAssociatedCompanies.Where(a => a.IsCurrentCompany == true).FirstOrDefault().Company.CompanyName;
                }
                else
                {
                    txtCompanyNameDisplay.Text = "Private - " + EnrollmentObj.StudentEnrollment.Student.Individual.FullName;
                }
            }
        }

        private void btnpicEmailClient_Click(object sender, EventArgs e)
        {
            Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(enrollmentBindingSourceMain.Current);

            using (frmEmailMessageV2 frm = new frmEmailMessageV2())
            {
                frm.txtMessageSubject.Text = "MCD Communication - Follow up on Enrollment - " + EnrollmentObj.StudentEnrollment.Student.Individual.FullName;
                frm.AddToEmailContact(new List<Individual>() { (Individual)EnrollmentObj.StudentEnrollment.Student.Individual });
                frm.txtMessageSubject.ReadOnly = true;
                frm.ShowDialog();
            }
        }

        private void btnpicREviewUpdateCompanyInfo_Click(object sender, EventArgs e)
        {
            Data.Models.Enrollment EnrollmentObj = (Data.Models.Enrollment)(enrollmentBindingSourceMain.Current);
            if (EnrollmentObj != null)
            {
                Boolean IsCompany = false;
                foreach (StudentAssociatedCompany SAC in EnrollmentObj.StudentEnrollment.Student.StudentAssociatedCompanies)
                {
                    if (SAC.IsCurrentCompany)
                    {
                        IsCompany = true;
                    }
                }
                if (IsCompany)
                {
                    using (ViewEditCompanyDetails frm = new ViewEditCompanyDetails(EnrollmentObj.StudentEnrollment.Student.StudentAssociatedCompanies.Where(a => a.IsCurrentCompany == true).FirstOrDefault().Company.CompanyID))
                    {
                        //frm.txtCompaniesFilterCriteria.Text = EnrollmentObj.StudentEnrollment.Student.StudentAssociatedCompanies.Where(a => a.IsCurrentCompany == true).FirstOrDefault().Company.CompanyName;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Student is private no Company Details Avaiable, Allocate the student to a company by configuring the student details.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnpicAddFiles_Click(object sender, EventArgs e)
        {
            List<Data.Models.File> UploadedFiles = Common.FileHandeling.FileHandeling.UploadFile(
              UseMultipleFileSelect: true,
              AutomaicallyAddFileToDatabase: false,
              ImagesOnly: false);

            LookupEnrollentDocumentType EDT = null;
            using (var Dbconnection = new MCDEntities())
            {
                EDT = (from a in Dbconnection.LookupEnrollentDocumentTypes
                       where a.LookupEnrollmentDocumentTypeID == (int)Common.Enum.EnumEnrollentDocumentTypes.Facilitator_Report
                       select a).FirstOrDefault<LookupEnrollentDocumentType>();
            };

            int iLookupEnrollmentDocumentTypeID = Convert.ToInt32(cboAttachmentType.SelectedValue);
            if (iLookupEnrollmentDocumentTypeID != (int)Common.Enum.EnumEnrollentDocumentTypes.ID_Documents)
            {

                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            foreach (Data.Models.File f in UploadedFiles)
                            {
                                EnrollmentDocument ED = new EnrollmentDocument()
                                {
                                    LookupEnrollmentDocumentTypeID = iLookupEnrollmentDocumentTypeID,
                                    File = f,
                                    FileID = 0,
                                    EnrollmentID = CurrentEnrollmentID
                                };
                                Dbconnection.EnrollmentDocuments.Add(ED);
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
            else
            {
                MetroMessageBox.Show(this, "To Link the ID documents, update the student profile by using the student update button.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            refreshEnrollmentDocuments();
            //foreach (EnrollmentDocument EDItem in CurrentEnrollment.EnrollmentDocuments)
            //{
            //    Dbconnection.EnrollmentDocuments.Attach(EDItem);
            //    Dbconnection.Entry(EDItem).Reference(a => a.LookupEnrollentDocumentType).Load();
            //}
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (curriculumCourseEnrollmentsMainCoursesBindingSource.Count > 0)
            {
                CurriculumCourseEnrollment CurrentSelectedCurriculumCourseEnrollment =
                         (CurriculumCourseEnrollment)curriculumCourseEnrollmentsMainCoursesBindingSource.Current;

                DialogResult Rtn = MetroMessageBox.Show(this, "All Scheduled Coures Will Be Confirmed, Are You Sure you Wish To Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Rtn == DialogResult.Yes)
                {
                    if (CurrentSelectedCurriculumCourseEnrollment.Schedules.Count > 0)
                    {
                        using (frmConfirmScheduledCourse frm = new frmConfirmScheduledCourse(CurrentSelectedCurriculumCourseEnrollment.Schedules.FirstOrDefault().ScheduleID, CurrentSelectedCurriculumCourseEnrollment.EnrollmentID, true))
                        {

                            frm.ShowDialog();
                            this.refreshEnrollment();
                        }

                    }
                }
            }

        }
    }
}
