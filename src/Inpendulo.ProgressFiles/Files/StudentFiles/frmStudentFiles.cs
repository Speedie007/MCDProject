using Impendulo.Common;
using Impendulo.Data.Models;
using Impendulo.Development.Company;
using Impendulo.Development.Students;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;
using Impendulo.Common.Verifiction;
using Impendulo.Data.Models.DataGridViewStructures.Schedules.CompanyFiles;
using Impendulo.Data.Models.DataGridViewStructures.Students.CompanyFiles;
using Impendulo.Data.Models.DataGridViewStructures.Enquiry.CompanyFiles;
using MetroFramework;
using Impendulo.Development.Enquiry.NewEnquiry;
using Impendulo.Development.Enquiry;
using Impendulo.Development.Enrollment;
using Impendulo.Development.Company.CompanyDetails;
using Impendulo.Development.ContactDetails;
using Impendulo.Developments.Reports.Reports.Schedules;
using Impendulo.Development.Email;

namespace Impendulo.Development.Files.Files.StudentFiles
{
    public partial class frmStudentFiles : MetroForm
    {
        #region Internal Classes
        //public class CompanyEnquiries
        //{
        //    public int EnquiryID { get; set; }
        //    public int ProgressFileID { get; set; }
        //    public string ContactPerson { get; set; }
        //    public string Department { get; set; }
        //    public string Curriculum { get; set; }
        //    public DateTime LastUpdated { get; set; }
        //    public string Status { get; set; }
        //    public Boolean InitialConsultationComplete { get; set; }
        //    public Boolean InitialCurriculumEnquiryDocumentationSent { get; set; }
        //    public int QuanityToEnroll { get; set; }
        //    public int QuantityCurrentlyEnrolled { get; set; }

        //}
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

        public ProgressFile CurrentlySelectedStudentProgressfile { get; private set; }
        public Student CurrentlySelectedStudent { get; private set; }
        private CurrentTab CurrentlySelectedTab { get; set; }
        private ScheduleToDisplay CurrentScheduleToDisplay { get; set; }

        private CustomSortableBindingList<Schedules> CurrentlySelectedSchedules { get; set; }
        private List<Impendulo.Data.Models.Enquiry> EnquiryCache = new List<Impendulo.Data.Models.Enquiry>();
        private CustomSortableBindingList<CompanyEnquiries> CurrentlySelectedEnquiries { get; set; }
        public Employee CurrentEmployeeLoggedIn { get; set; }
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
        public frmStudentFiles(Employee CurrentEmployeeLoggedIn)
        {
            this.CurrentEmployeeLoggedIn = CurrentEmployeeLoggedIn;

            InitializeComponent();
            //tabMain.SelectTab(0);
            CurrentlySelectedTab = CurrentTab.Enqiury;
        }

        private void frmCompanyFiles_Load(object sender, EventArgs e)
        {
            //this.refreshStudentFile();

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

                    this.refreshEnquiries();
                    break;
            }
        }
        #endregion

        #region Progress Files

        #region Refresh Methods
        private void refreshStudentFile()
        {
            this.populateStudentFile();
        }
        #region Populate Methods

        private void populateStudentFile()
        {

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedStudentProgressfile = (from a in Dbconnection.ProgressFiles
                                                        where a.StudentProgressFile.StudentID == CurrentlySelectedStudent.StudentID
                                                        select a)
                                                        .Include(a => a.StudentProgressFile)
                                                        .FirstOrDefault<ProgressFile>();
                this.gbFileSections.Enabled = true;
                txtStudentName.Text = this.CurrentlySelectedStudent.Individual.FullName;
                txtStudentProgressFileID.Text = CurrentlySelectedStudentProgressfile.ProgressFileID.ToString();
                if (CurrentlySelectedStudentProgressfile != null)
                {
                    gbFileSections.Enabled = true;
                    gbEnquiryContact.Enabled = true;
                    btnpicViewEnquiryEnrollments.Visible = true;
                    btnpicViewEnquiryHistory.Visible = true;
                    btnpicNewEnquiryNotification.Visible = true;
                    btnNewEnquiryEnrollment.Visible = true;
                    if (CurrentlySelectedStudentProgressfile.Enquiries.Count > 0)
                    {
                        gbInProgressContactDetails.Enabled = true;
                        gbAllEnquiries.Enabled = true;
                    }
                    else
                    {
                        gbInProgressContactDetails.Enabled = false;
                        gbAllEnquiries.Enabled = false;
                    }
                }
                else
                {
                    gbFileSections.Enabled = false;
                    gbEnquiryContact.Enabled = false;
                    btnpicViewEnquiryEnrollments.Visible = false;
                    btnpicViewEnquiryHistory.Visible = false;
                    btnpicNewEnquiryNotification.Visible = false;
                    btnNewEnquiryEnrollment.Visible = false;
                }
                this.loadCurrentTab();

            };

        }

        #endregion
        #endregion
        #endregion

        #region Tab Pages

        #region Enquiry Tab

        #region Refresh Methods
        private void refreshEnquiryContactDetails()
        {
            populateEnquiryContactDetails();
        }
        private void refreshEnquiries()
        {

            using (var Dbconnection = new MCDEntities())
            {

                EnquiryCache = (from a in Dbconnection.Enquiries
                                where a.ProgressFileID == CurrentlySelectedStudentProgressfile.ProgressFileID
                                select a)
                                .Include(a => a.ProgressFile)
                                .Include(a => a.ProgressFile.CompanyProgressFile)
                                .Include(a => a.ProgressFile.CompanyProgressFile.Company)
                                .Include(a => a.Curriculum)
                                .Include(b => b.Curriculum.LookupDepartment)
                                .Include(b => b.Enrollments)
                                .Include(b => b.LookupEnquiryStatus)
                                .Include(b => b.Individuals)
                                .Include(b => b.Individuals.Select(c => c.ContactDetails))
                                .Include(b => b.Individuals.Select(c => c.ContactDetails.Select(d => d.LookupContactType)))
                                .ToList<Impendulo.Data.Models.Enquiry>();
            };

            populateEnquiries();
        }
        #endregion
        #region Popuate Methods
        private void populateEnquiryContactDetails()
        {
            if (companyEnquiriesBindingSource.Count > 0)
            {
                CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
                contactDetailBindingSource.DataSource = (from a in EnquiryCache
                                                         from b in a.Individuals
                                                         where a.EnquiryID == CE.EnquiryID
                                                         select b).FirstOrDefault<Individual>().ContactDetails.ToList<ContactDetail>();
                if (!CE.InitialConsultationComplete)
                {
                    btnCompleteInitialConsulation.Enabled = true;
                }
                else
                {
                    btnCompleteInitialConsulation.Enabled = false;
                }
            }

        }
        private void populateEnquiryTextBoxControls(CompanyEnquiries EnquiryObj)
        {
            if (EnquiryObj != null)
            {
                txtEnquiryRefNum.Text = EnquiryObj.EnquiryID.ToString();
                txtCurrentContactPerson.Text = EnquiryObj.ContactPerson;
                txtDateEnquiryLastUpdated.Text = EnquiryObj.LastUpdated.ToString("f");
            }
            else
            {
                txtEnquiryRefNum.Text = "";
                txtCurrentContactPerson.Text = "";
                txtDateEnquiryLastUpdated.Text = "";
                contactDetailBindingSource.Clear();
            }



        }
        private void populateEnquiries()
        {

            using (var Dbconnection = new MCDEntities())
            {

                companyEnquiriesBindingSource.DataSource = new CustomSortableBindingList<CompanyEnquiries>(
                    (from a in EnquiryCache
                     select new CompanyEnquiries
                     {
                         EnquiryID = a.EnquiryID,
                         DepartmentID = a.Curriculum.DepartmentID,
                         ProgressFileID = a.ProgressFileID,
                         ContactPerson = a.Individuals.FirstOrDefault().FullName,
                         Department = a.Curriculum.LookupDepartment.DepartmentName,
                         Curriculum = a.Curriculum.CurriculumName,
                         InitialConsultationComplete = a.InitialConsultationComplete,
                         InitialCurriculumEnquiryDocumentationSent = a.InitialCurriculumEnquiryDocumentationSent,
                         LastUpdated = a.LastUpdated,
                         QuanityToEnroll = a.EnrollmentQuanity,
                         QuantityCurrentlyEnrolled = a.Enrollments.Where(b => b.EnrolmentParentID == 0).Count(),
                         Status = a.LookupEnquiryStatus.EnquiryStatus,
                     }).ToList<CompanyEnquiries>());
                if (EnquiryCache.Count > 0)
                {
                    gbInProgressContactDetails.Enabled = true;
                    gbAllEnquiries.Enabled = true;
                }
                else
                {
                    gbInProgressContactDetails.Enabled = false;
                    gbAllEnquiries.Enabled = false;
                }
                this.refreshEnquiryContactDetails();
            };

        }
        #endregion
        #region Control Event Methods

        private void btnViewCompanyDeailts_Click(object sender, EventArgs e)
        {
            using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentlySelectedStudent.StudentID))
            {
                frm.ShowDialog();
            }
        }

        private void btnpicAddNewEnquiry_Click(object sender, EventArgs e)
        {

            using (frmNewEnquiryV2 frm1 = new frmNewEnquiryV2(
                            IsProgressComanyProgressFileEnquiry: false,
                            IsStudentFileEnquiry: true,
                            CurrentlySelectedCompany: null,
                            CurrentlySelectedIndividual: CurrentlySelectedStudent.Individual))
            {
                frm1.ShowDialog();
                this.refreshEnquiries();
            }





        }

        private void btnpicViewEnquiryEnrollments_Click(object sender, EventArgs e)
        {
            if (companyEnquiriesBindingSource.Count > 0)
            {
                CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
                Impendulo.Data.Models.Enquiry E = (Impendulo.Data.Models.Enquiry)EnquiryCache.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Impendulo.Data.Models.Enquiry>();

                //View Current Enrollments
                //ensure that the Enrollments are refershed

                //IF any enrollments exists then open Selection list else Do Nothing.
                if (CE.QuantityCurrentlyEnrolled > 0)
                {
                    //Open thje list of linked Enrollments that are in progress
                    using (frmEnrollmentSelectionForEquiry frm = new frmEnrollmentSelectionForEquiry(E))
                    {
                        frm.ShowDialog();
                        if (frm.SelectedEnrollmentID != 0)
                        {
                            using (frmEnrollmentProgress innerFrm = new frmEnrollmentProgress(frm.SelectedEnrollmentID))
                            {
                                innerFrm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                                innerFrm.CurrentEquiryID = E.EnquiryID;
                                innerFrm.CurrentSelectedDepartment = (Impendulo.Common.Enum.EnumDepartments)E.Curriculum.DepartmentID;
                                innerFrm.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "There are currently no enrollments for this enquiry.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "There Are Currently No Enquiries to view any Enrollments for!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnpicViewEnquiryHistory_Click(object sender, EventArgs e)
        {
            if (companyEnquiriesBindingSource.Count > 0)
            {
                CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
                using (frmEquiryViewHistory frm = new frmEquiryViewHistory(CE.EnquiryID))
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "There Are No Enquiries to view any history for!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btnpicNewEnquiryNotification_Click(object sender, EventArgs e)
        {
            //if ()
            //{


            //}else
            //{
            //   // MetroMessageBox.Show(this, "Currently no enrollments for the company, please add enquiry first before attempting to gen the student!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnNewEnquiryEnrollment_Click(object sender, EventArgs e)
        {
            if (companyEnquiriesBindingSource.Count > 0)
            {
                CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
                Impendulo.Data.Models.Enquiry E = (Impendulo.Data.Models.Enquiry)EnquiryCache.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Impendulo.Data.Models.Enquiry>();

                if (E.InitialConsultationComplete)
                {

                    //gbInProgressEnquiryEnrrolmentQueries.Enabled = true;
                    DialogResult Rtn = MetroMessageBox.Show(this, "Do you have a copy of the individuals ID document or relevant details, These details are rquired to process initial enrollment! Else Select No and send an email Notification to the contact requesting these details.", "ID Document Requirement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //= MessageBox.Show("Do you have a copy of the individuals ID document or relevant details, These details are rquired to process initial enrollment! Else Select No and send an email Notification to the contact requesting these details.", "ID Document Requirement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (Rtn == DialogResult.Yes)
                    {

                        if (CE.QuantityCurrentlyEnrolled < CE.QuanityToEnroll)
                        {
                            using (frmApprenticeshipEnrollmentFormV2 frm = new frmApprenticeshipEnrollmentFormV2(E, false))
                            {

                                frm.ShowDialog();
                                if (frm.IsSuccessfullySaved)
                                {
                                    E.Enrollments.Add(frm.CurrentEnrollment);
                                    populateEnquiries();

                                    DialogResult Rtn1 = MessageBox.Show("Do you wish to process this new enrollment now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (Rtn1 == DialogResult.Yes)
                                    {
                                        using (frmEnrollmentProgress innerFrm = new frmEnrollmentProgress(frm.CurrentEnrollment.EnrollmentID))
                                        {
                                            innerFrm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                                            innerFrm.CurrentEquiryID = CE.EnquiryID;
                                            //innerFrm.CurrentSelectedDepartment = (Impendulo.Common.Enum.EnumDepartments)CE.Curriculum.DepartmentID;
                                            innerFrm.ShowDialog();
                                        }
                                    }

                                    gbInProgressContactDetails.Enabled = true;
                                    gbAllEnquiries.Enabled = true;


                                }
                                else
                                {
                                    MetroMessageBox.Show(this, "Enrolment Not Completed!", "Enrollment Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "All Requied Enrollment HAve Been Completed For this Query!", "Maximum Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Initial Consultation Is Not Yet Completed, Please complete before proceeding with the enrollment!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("Initial Consultation Is Not Yet Completed, Please complete before proceeding with the enrollment!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //gbInProgressEnquiryEnrrolmentQueries.Enabled = false;
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Currently no enrollments for the company, please add enquiry first before attempting to enroll the student!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAddEnquiryContactDetail_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    ContactDetail CD = (ContactDetail)contactDetailBindingSource.Current;

                    CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
                    Impendulo.Data.Models.Enquiry E = (Impendulo.Data.Models.Enquiry)EnquiryCache.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Impendulo.Data.Models.Enquiry>();
                    Individual ECFromCache = E.Individuals.First();

                    using (var Dbconnection = new MCDEntities())
                    {
                        using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                        {
                            try
                            {
                                //CRUD Operations
                                /*
			                    * this steps follow to both entities
			                    * 
			                    * 1 - create instance of entity with relative primary key
			                    * 
			                    * 2 - add instance to context
			                    * 
			                    * 3 - attach instance to context
			                    */

                                // 1
                                Individual I = new Individual { IndividualID = ECFromCache.IndividualID };
                                // 2
                                Dbconnection.Individuals.Add(I);
                                // 3
                                Dbconnection.Individuals.Attach(I);

                                // 1
                                ContactDetail cd = new ContactDetail { ContactDetailID = frm.CurrentDetail.ContactDetailID };
                                // 2
                                Dbconnection.ContactDetails.Add(cd);
                                // 3
                                Dbconnection.ContactDetails.Attach(cd);

                                // like previous method add instance to navigation property
                                I.ContactDetails.Add(cd);
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

                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                        Dbconnection.Entry(frm.CurrentDetail).Reference(a => a.LookupContactType).Load();
                    };
                    ECFromCache.ContactDetails.Add(frm.CurrentDetail);
                    refreshEnquiryContactDetails();
                }
            }
        }

        private void btnRemoveEnquiryContactDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnuUpdateEnquiryContactDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeEnquiryConactPerson_Click(object sender, EventArgs e)
        {

        }

        private void btnFilterEnquiryByEnquiryID_Click(object sender, EventArgs e)
        {

        }

        private void btnFilterEnquiriesByCurriculum_Click(object sender, EventArgs e)
        {

        }
        private void dgvCurrentEnquiry_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var EnquiryObj = (CompanyEnquiries)(row.DataBoundItem);
                    populateEnquiryTextBoxControls(EnquiryObj);

                }
            }
        }
        private void companyEnquiriesBindingSource_PositionChanged(object sender, EventArgs e)
        {
            var EnquiryObj = (CompanyEnquiries)(companyEnquiriesBindingSource.Current);
            populateEnquiryTextBoxControls(EnquiryObj);
            refreshEnquiryContactDetails();

        }
        private void dgvCurrentEnquiry_Sorted(object sender, EventArgs e)
        {
            var EnquiryObj = (CompanyEnquiries)(companyEnquiriesBindingSource.Current);
            populateEnquiryTextBoxControls(EnquiryObj);
            refreshEnquiryContactDetails();
        }
        #endregion

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
                                                                                                       //from b in a.CompanyStudentProgressFiles
                                                                                                       where a.StudentID == CurrentlySelectedStudent.StudentID
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

                StudentReturnDetails StudentReturnDetails = (StudentReturnDetails)studentBindingSource.Current;

                using (frmStudentAddUpdate frm = new frmStudentAddUpdate(StudentReturnDetails.StudentID))
                {
                    frm.ShowDialog();
                    refreshCompanyStudents();
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
                    new CustomSortableBindingList<GetAllEnrollmentsPerStudent_Result>(Dbconnection.GetAllEnrollmentsPerStudent(OfProgressFiles.VerifyStudentProgressFile(this.CurrentlySelectedStudent.StudentID)).ToList<GetAllEnrollmentsPerStudent_Result>());
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
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection. GetAllScheduledCoursesWhichAreCompletedForSelectedStudent(OfProgressFiles.VerifyStudentProgressFile(CurrentlySelectedStudent.StudentID))
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
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection.GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedStudent (OfProgressFiles.VerifyStudentProgressFile(CurrentlySelectedStudent.StudentID))
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
                        CurrentlySelectedSchedules = new CustomSortableBindingList<Schedules>((from a in Dbconnection. GetAllScheduledCoursesWhichAreNotYetStartedForSelectedStudent(OfProgressFiles.VerifyStudentProgressFile(CurrentlySelectedStudent.StudentID))
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
            using (frmStudentRegister frm = new frmStudentRegister())
            {
                frm.ShowDialog();
            }
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
            /*
                string FilterStr = ((Impendulo.Data.Models.LookupDepartment)lookupDepartmentBindingSource.Current).DepartmentName.ToLower();
            List<SectionalView> FilteredList = new List<SectionalView>((from a in CurrentlySelectedCourses
                                                                        where a.DepartmentName.ToLower().Contains(FilterStr)
                                                                        select a).ToList<SectionalView>());
                                                                        

            this.LoadFilterItems(FilteredList);
            */
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
        #endregion

        private void btnpicFileSearch_Click(object sender, EventArgs e)
        {
            using (frmStudentSearchForStudent frm = new frmStudentSearchForStudent(true))
            {
                frm.ShowDialog();
                if (frm.CurrentSelectedStudent != null)
                {

                    if (EnquiryCache != null) { EnquiryCache.Clear(); }
                    if (CurrentlySelectedEnquiries != null) { CurrentlySelectedEnquiries.Clear(); }

                    OfProgressFiles.VerifyStudentProgressFile(frm.CurrentSelectedStudent.StudentID);
                    this.CurrentlySelectedStudent = frm.CurrentSelectedStudent;

                    this.refreshStudentFile();
                    tabMain.SelectTab(0);
                }
            }
        }

        private void btnpicViewCompanyProfile_Click(object sender, EventArgs e)
        {
            if (CurrentlySelectedStudent != null)
            {
                using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentlySelectedStudent.StudentID))
                {
                    //frm.txtCompaniesFilterCriteria.Text = CurrentlySelectedCompany.CompanyName;
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

        private void btnCompleteInitialConsulation_Click(object sender, EventArgs e)
        {
            CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
            Impendulo.Data.Models.Enquiry E = (Impendulo.Data.Models.Enquiry)EnquiryCache.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Impendulo.Data.Models.Enquiry>();

            using (frmEnquiryInitialConsultation frm = new frmEnquiryInitialConsultation(E))
            {
                frm.EmployeeID = CurrentEmployeeLoggedIn.EmployeeID;
                frm.ShowDialog();
                this.refreshEnquiryContactDetails();
                this.populateEnquiries();
            }
        }

        private void dgvInprogressContactDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);
                    row.Cells[colInProgressContactType.Index].Value = ContactDetailObj.LookupContactType.ContactType.ToString();
                    if (ContactDetailObj.ContactTypeID == (int)Impendulo.Common.Enum.EnumContactTypes.Email_Address)
                    {
                        row.Cells[colInProgressContactDetailSendOption.Index].Value = "[ Send Email ]";
                    }
                    if (ContactDetailObj.ContactTypeID == (int)Impendulo.Common.Enum.EnumContactTypes.Cell_Number)
                    {
                        row.Cells[colInProgressContactDetailSendOption.Index].Value = "[ Send SMS ]";
                    }
                }
            }
        }

        private void dgvInprogressContactDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;

            CompanyEnquiries CE = (CompanyEnquiries)companyEnquiriesBindingSource.Current;
            Impendulo.Data.Models.Enquiry E = (Impendulo.Data.Models.Enquiry)EnquiryCache.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Impendulo.Data.Models.Enquiry>();

            Individual x = E.Individuals.FirstOrDefault();
            if (e.ColumnIndex == 2)
            {
                var ContactDetailObj = (ContactDetail)(gridView.Rows[e.RowIndex].DataBoundItem);
                if (ContactDetailObj != null)
                {
                    if (ContactDetailObj.ContactTypeID == (int)Impendulo.Common.Enum.EnumContactTypes.Email_Address)
                    {
                        using (frmEmailMessageV2 frm = new frmEmailMessageV2())
                        {
                            frm.txtMessageSubject.Text = "MCD Communication - Follow On Enquiry " + E.EnquiryID + " - (" + E.Curriculum.CurriculumName + ")";
                            frm.AddToEmailContact(new List<Individual>() { x });
                            frm.txtMessageSubject.ReadOnly = true;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }


    }
}


