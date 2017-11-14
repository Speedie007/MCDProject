using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Impendulo.Data.Models;
using System.Data.Entity;
using Impendulo.Common.Enum;
using Impendulo.Common.FileHandeling;
using Impendulo.Development.Students;
using MetroFramework;
using System.Data.Entity.Validation;

namespace Impendulo.Development.Enrollment
{
    public partial class frmApprenticeshipEnrollmentFormV2 : MetroForm
    {



        int iCurrentPosition = 0;
        //MCDEntities Dbconnection;
        //Student StudentObj;
        public Data.Models.Enrollment CurrentEnrollment { get; }
        public Student CurrentSelectedStudent { get; set; }
        private Data.Models.Enquiry _CurrentEnquiry { get; set; }
        public Data.Models.Enquiry CurrentEnquiry { get { return _CurrentEnquiry; } }
        public Boolean IsCompanyEnrollment { get; private set; }
        public int CompanyProgressFileID { get; set; }

        public Boolean IsSuccessfullySaved { get; set; }

        private Boolean MustSaveItems = false;

        public Employee CurrentEmployeeLoggedIn
        {
            get;
            set;
        }

        public frmApprenticeshipEnrollmentFormV2(Data.Models.Enquiry CurrentEnquiry, Boolean bIsCompanyEnrollment, int CompanyProgressFileID = 0)
        {
            this._CurrentEnquiry = CurrentEnquiry;
            this.IsCompanyEnrollment = IsCompanyEnrollment;
            this.CompanyProgressFileID = CompanyProgressFileID;
            this.CurrentEnrollment = new Data.Models.Enrollment()
            {
                EnrolmentParentID = 0,
                EnquiryID = this.CurrentEnquiry.EnquiryID,
                ProgressFileID = 0,
                EnrollmentExcempt = false,
                LookupEnrollmentProgressStateID = (int)Common.Enum.EnumEnrollmentProgressStates.New_Enrollment,
                CurriculumID = CurrentEnquiry.CurriculumID,
                DateIntitiated = DateTime.Now.Date,
                ApprienticeshipEnrollment = new ApprienticeshipEnrollment()
                {
                    DateUpdated = DateTime.Now.Date,
                    LookupSectionalEnrollmentTypeID = (int)EnumSectionalEnrollmentTypes.Section_13
                }


            };
            InitializeComponent();
            //this.txtSummaryFinaciallyResponsible.Text = "Private";
            lblSummaryEnrollmentSelectionType.Text = lblCurentlySelectedEnrollmentType.Text;
            IsSuccessfullySaved = false;

        }

        private void frmAddUpdateStudent_Load(object sender, EventArgs e)
        {


            if (CurrentEmployeeLoggedIn == null)
            {
                /*
             * Thismust be Commmented out or removed in the production version this is just for Develpoement Testing.
             */
                using (var Dbconnection = new MCDEntities())
                {
                    CurrentEmployeeLoggedIn = (from a in Dbconnection.Employees
                                               where a.EmployeeID == 11075
                                               select a).FirstOrDefault<Employee>();
                };

                /***************************************************************************************/
                // MessageBox.Show("It is Required that you be logged in to use the feature.\n Login and try again!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this.Close();

            }



            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();

            this.populateLookupEnrollentDocumentType();
        }

        #region PerloadDropDownControls
        private void populateLookupEnrollentDocumentType()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupEnrollentDocumentTypeBindingSource.DataSource = (from a in Dbconnection.LookupEnrollentDocumentTypes
                                                                       select a).ToList<LookupEnrollentDocumentType>();
            };

        }
        #endregion
        #region Forms Sections

        #region Page 1 - Enrollment Type

        #region Refresh Methods



        #endregion

        #region Populate Methods

        #endregion

        #region Control Events

        private void radSelectSection13_CheckedChanged(object sender, EventArgs e)
        {
            lblCurentlySelectedEnrollmentType.Text = lblCurentlySelectedEnrollmentType.Text.Replace("28", "13");
            lblSummaryEnrollmentSelectionType.Text = lblCurentlySelectedEnrollmentType.Text;
            CurrentEnrollment.ApprienticeshipEnrollment.LookupSectionalEnrollmentTypeID = (int)EnumSectionalEnrollmentTypes.Section_13;
        }

        private void radSelectSection28_CheckedChanged(object sender, EventArgs e)
        {
            lblCurentlySelectedEnrollmentType.Text = lblCurentlySelectedEnrollmentType.Text.Replace("13", "28");
            lblSummaryEnrollmentSelectionType.Text = lblCurentlySelectedEnrollmentType.Text;
            CurrentEnrollment.ApprienticeshipEnrollment.LookupSectionalEnrollmentTypeID = (int)EnumSectionalEnrollmentTypes.Section_13;
        }

        #endregion

        #region Logic Control Methods



        #endregion
        #endregion

        #region Page 2  - Student Selection

        #region Refresh Methods
        private void refreshStudentSelection()
        {
            populateStudentSelection();
        }
        #endregion
        #region Populate Methods
        private void populateStudentSelection()
        {

            if (CurrentEnrollment.StudentEnrollment != null)
            {
                txtStudentFullName.Text = CurrentEnrollment.StudentEnrollment.Student.Individual.FullName;
                txtStudentIdNumber.Text = CurrentEnrollment.StudentEnrollment.Student.StudentIDNumber;
                txtStudentNember.Text = CurrentEnrollment.StudentEnrollment.StudentID.ToString();

                //Summary Fields
                txtSummaryFullName.Text = txtStudentFullName.Text;
                txtSummaryIDNumber.Text = txtStudentIdNumber.Text;
                txtSummaryStudentNumber.Text = txtStudentNember.Text;
                picbtnEditCurrentStudent.Visible = true;
            }
            else
            {
                picbtnEditCurrentStudent.Visible = false;
                txtStudentFullName.Clear();
                txtStudentIdNumber.Clear();
                txtStudentNember.Clear();
                //Summary Fields
                txtSummaryFullName.Clear();
                txtSummaryIDNumber.Clear();
                txtSummaryStudentNumber.Clear();

            }
        }
        #endregion

        #region Controls Events
        private void picSearchStudents_Click(object sender, EventArgs e)
        {
            List<Student> StudentExcemptionList = new List<Student>();

            using (var Dbconnection = new MCDEntities())
            {
                //Loadds the associated Collections in this case the Enrollment with their associated Students that are enrolled.
                //Dbconnection.CurriculumEnquiries.Attach(CurrentEnquiry);
                ////Load all enrollments that linked to the Enquiry Item.
                //Dbconnection.Entry(CurrentEnquiry).Collection(a => a.Enrollments).Load();
                //foreach (Data.Models.Enrollment EnrollmentObj in CurrentEnquiry.Enrollments)
                //{
                //    //Load the student linked to the enrollment
                //    // Dbconnection.Entry(EnrollmentObj).Reference(a => a.StudentEnrollment.Student).Load();
                //    StudentExcemptionList.Add(EnrollmentObj.StudentEnrollment.Student);
                //}
            };
            using (frmStudentSearchForStudent frm = new frmStudentSearchForStudent(true))
            {

                //Set the list of student that are already Enrolled for this Enquiry Item.
                frm.StudentExpceptionList = StudentExcemptionList;
                frm.ShowDialog();
                if (frm.CurrentSelectedStudent != null)
                {
                    if (CurrentEnrollment.StudentEnrollment == null)
                    {

                        CurrentEnrollment.StudentEnrollment = new StudentEnrollment()
                        {
                            StudentID = frm.CurrentSelectedStudent.StudentID,
                            DateUpdated = DateTime.Now.Date,
                            Student = frm.CurrentSelectedStudent
                        };
                    }
                    else
                    {
                        CurrentEnrollment.StudentEnrollment.StudentID = frm.CurrentSelectedStudent.StudentID;
                        CurrentEnrollment.StudentEnrollment.Student = frm.CurrentSelectedStudent;
                    }
                    picbtnEditCurrentStudent.Visible = true;
                }
                else
                {
                    CurrentEnrollment.StudentEnrollment = null;
                    picbtnEditCurrentStudent.Visible = false;
                }
                refreshStudentSelection();
            };
        }
        private void picbtnEditCurrentStudent_Click(object sender, EventArgs e)
        {
            using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentEnrollment.StudentEnrollment.StudentID))
            {
                frm.CurrentSelectedStudent = CurrentEnrollment.StudentEnrollment.Student;
                frm.ShowDialog();

                refreshStudentSelection();

            }
        }

        #endregion

        #region Logical Control Metods


        #endregion



        #endregion



        #region Page 3 - Financial Resposiblity

        //#region Refresh Methods

        //#endregion

        //#region Populate Methods

        //#endregion

        //#region Control Events
        //private void picSelectPrivate_Click(object sender, EventArgs e)
        //{
        //    picSelectCompany.BackColor = Color.Transparent;
        //    picSelectPrivate.BackColor = Color.Gainsboro;
        //    lblCurentlySelectedSiteType.Text = "Private";
        //    this.txtSummaryFinaciallyResponsible.Text = "Private";

        //    //CurrentEnrollment.EnrollmentPaymentTypeID = (int)EnumEnrollmentPaymentTypes.Private;
        //}

        //private void picSelectCompany_Click(object sender, EventArgs e)
        //{
        //    picSelectCompany.BackColor = Color.Gainsboro;
        //    picSelectPrivate.BackColor = Color.Transparent;

        //    lblCurentlySelectedSiteType.Text = "Company";
        //    this.txtSummaryFinaciallyResponsible.Text = "Company";
        //    //CurrentEnrollment.EnrollmentPaymentTypeID = (int)EnumEnrollmentPaymentTypes.Company;
        //}

        //#endregion

        //#region logical Control Methods

        //#endregion

        #endregion

        #region Page 4 - Documentation Upload

        #region Refresh Methods
        private void refreshDocumentUploads()
        {
            this.populateUploadedDocuments();
        }
        #endregion

        #region Populate Methods
        private void populateUploadedDocuments()
        {
            ////List<File> AllFiles = new List<File>();
            ////var tempAllFiles = 
            //////foreach (var item in tempAllFiles)
            //////{
            //////    AllFiles.Add(new Data.Models.File()
            //////    {
            //////        FileID = item.FileID,
            //////        FileName = item.FileName,
            //////        FileExtension = item.FileExtension,
            //////        DateCreated = item.DateCreated
            //////    });
            //////}
            enrollmentDocumentBindingSource.DataSource = (from a in CurrentEnrollment.EnrollmentDocuments
                                                          select a).ToList<EnrollmentDocument>();
        }
        #endregion

        #region Control Events
        private void btnpicAddEnrollmentDocument_Click(object sender, EventArgs e)
        {
            List<Data.Models.File> UploadedFiles = Common.FileHandeling.FileHandeling.UploadFile(
                UseMultipleFileSelect: true,
                AutomaicallyAddFileToDatabase: false,
                ImagesOnly: false);

            LookupEnrollentDocumentType EDT = (LookupEnrollentDocumentType)lookupEnrollentDocumentTypeBindingSource.Current;

            foreach (Data.Models.File f in UploadedFiles)
            {
                EnrollmentDocument ED = new EnrollmentDocument()
                {
                    LookupEnrollmentDocumentTypeID = Convert.ToInt32(cboDocumentType.SelectedValue),
                    File = f,
                    LookupEnrollentDocumentType = EDT
                };
                CurrentEnrollment.EnrollmentDocuments.Add(ED);
            }
            //foreach (EnrollmentDocument EDItem in CurrentEnrollment.EnrollmentDocuments)
            //{
            //    Dbconnection.EnrollmentDocuments.Attach(EDItem);
            //    Dbconnection.Entry(EDItem).Reference(a => a.LookupEnrollentDocumentType).Load();
            //}

            this.refreshDocumentUploads();
        }
        private void btnRemoveEnrollmentFile_Click(object sender, EventArgs e)
        {
            if (enrollmentDocumentBindingSource.Count > 0)
            {
                CurrentEnrollment.EnrollmentDocuments.RemoveAt(enrollmentDocumentBindingSource.Position);
                this.refreshDocumentUploads();
            }

        }
        #endregion

        #region logical Control Methods

        #endregion

        #endregion

        #region Page 5 - Summary Confirmation

        #region Refresh Methods

        #endregion

        #region Populate Methods

        #endregion

        #region Control Events



        #endregion

        #region logical Control Methods

        #endregion

        #endregion


        #endregion

        #region Wizard Comopnents
        #region "Navigation Controls"
        private void navigateForward()
        {
            if (ValidateStep())
            {
                if (iCurrentPosition + 1 < MainflowLayoutPanel.Controls.Count)
                {
                    //if step validation is passed the next window is display by incrementing the IcurrentPosition Counter.
                    iCurrentPosition++;
                }
                else
                {
                    //on last step which will close if the finish b=button is selected.
                    DialogResult res = MessageBox.Show("Are Details Correct?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (DialogResult.Yes == res)
                    {
                        MustSaveItems = true;
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
            if (iCurrentPosition - 1 >= 0)
            {
                iCurrentPosition--;
            }
            else
            {

                //iCurrentPosition = 5;
            }
            //Hide All Panels inside the MainFlowPanel
            //MainflowLayoutPanel
            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();
        }


        private void setNavigationControls()
        {
            if (iCurrentPosition == 0)
            {
                btnPreviousSection.Visible = false;
                btnNextSection.Text = "Next";
            }
            else
            {
                if (iCurrentPosition == MainflowLayoutPanel.Controls.Count - 1)
                {
                    btnNextSection.Text = "Process";
                    btnNextSection.ImageIndex = 2;
                }
                else
                {
                    btnNextSection.Text = "Next";
                    btnNextSection.ImageIndex = 0;
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
                    if (Convert.ToInt32(lblObj.Tag.ToString()) == iCurrentPosition)
                    {
                        lblObj.Font = new Font(lblObj.Font, FontStyle.Bold | FontStyle.Underline);
                    }
                    else
                    {
                        lblObj.Font = new Font(lblObj.Font, FontStyle.Regular);
                    }
                }
            }
            double dblPercentageComplete = (((Convert.ToDouble(iCurrentPosition + 1) / Convert.ToDouble(iAmountOfSteps))) * 100);
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
                    if (Convert.ToInt32(gbObj.Tag.ToString()) == iCurrentPosition)
                    {
                        gbObj.Show();
                        gbObj.Width = MainflowLayoutPanel.Width - 10;
                        gbObj.Height = MainflowLayoutPanel.Height - 10;
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
            switch (iCurrentPosition)
            {
                case 0:
                    this.loadupEnquiryContactSelectionType();
                    break;
                case 1:
                    this.loadupEnquiryContactSelection();
                    break;
                case 2:
                    this.loadupEnquiryCurriculumSelection();
                    break;
                case 3:
                    this.loadupEnquiryConfirmation();
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
        private Boolean ValidateStep()
        {

            Boolean bRtn = true;
            switch (iCurrentPosition)
            {
                case 0:
                    break;
                case 1:
                    if (CurrentEnrollment.StudentEnrollment == null)
                    {
                        bRtn = false;
                        MetroMessageBox.Show(this, "(Invalid Student Selection) - No Student Selected! Select student before proceeding.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;

                case 4:
                    break;
                case 5:
                    break;
                default:
                    bRtn = false;
                    break;
            }

            return bRtn;
        }

        #region "Each Wizard Page Loadup"
        private void loadupEnquiryContactSelectionType()
        {


        }
        private void loadupEnquiryContactSelection()
        {





        }
        private void loadupEnquiryCurriculumSelection()
        {



        }


        private void loadupEnquiryConfirmation()
        {

        }







        #endregion

        #endregion

        #endregion

        private void frmApprenticeshipEnrollmentFormV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MustSaveItems)
            {


                using (var Dbconnection = new MCDEntities())
                {

                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            ///Step 1 Begin - Saves Main Enrollment
                            Dbconnection.Students.Attach(CurrentEnrollment.StudentEnrollment.Student);
                            Dbconnection.Entry(CurrentEnrollment.ApprienticeshipEnrollment).State = EntityState.Added;


                            Dbconnection.Entry(CurrentEnrollment).State = EntityState.Added;
                            Dbconnection.Entry(CurrentEnrollment.StudentEnrollment).State = EntityState.Added;
                            Dbconnection.Entry(CurrentEnrollment.StudentEnrollment.Student).State = EntityState.Unchanged;
                            foreach (EnrollmentDocument ED in CurrentEnrollment.EnrollmentDocuments)
                            {
                                Dbconnection.Entry(ED).State = EntityState.Added;
                                Dbconnection.Entry(ED.File).State = EntityState.Added;
                                // ED.LookupEnrollentDocumentType = null;
                                Dbconnection.LookupEnrollentDocumentTypes.Attach(ED.LookupEnrollentDocumentType);
                                Dbconnection.Entry(ED.LookupEnrollentDocumentType).State = EntityState.Unchanged;
                            }

                            CurrentEnrollment.ProgressFileID = Impendulo.Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(CurrentEnrollment.StudentEnrollment.StudentID);


                            Dbconnection.Enrollments.Add(CurrentEnrollment);
                            Dbconnection.SaveChanges();
                            if (IsCompanyEnrollment)
                            {
                                Common.Verifiction.OfProgressFiles.VerifyCompanyStudentProgressFile(CurrentEnrollment.StudentEnrollment.StudentID, this.CompanyProgressFileID);

                            }
                            //End Step 1 
                            //Step 2 Get PreRequiestes Courses and and load them with the main enrollment

                            List<CurriculumPrequisiteCourse> CPC = (from a in Dbconnection.CurriculumPrequisiteCourses
                                                                    where a.CurriculumID == CurrentEnquiry.CurriculumID
                                                                    select a)
                                                                    .Include("CurriculumCourse")
                                                                    .ToList<CurriculumPrequisiteCourse>();

                            //Get All The Possible Cuuriculum that for partof the Pre-Requisites
                            List<int> PreRequisiteCurriculumID = (from a in CPC
                                                                  select a.CurriculumCourse.CurriculumID)
                                                                  .Distinct<int>()
                                                                  .ToList<int>();
                            //Create Enrollment for each possible Curriculum that that forms part of the list pre-requisite Courses.
                            foreach (int CurriculumIDForPreRquisiteCourseEnrollment in PreRequisiteCurriculumID)
                            {
                                //Creates a Enrollment Enrty for the Curriculum
                                Data.Models.Enrollment PreRequisisteCourseEnrollment = new Data.Models.Enrollment
                                {
                                    EnrolmentParentID = CurrentEnrollment.EnrollmentID,
                                    EnquiryID = this.CurrentEnquiry.EnquiryID,
                                     ProgressFileID = CurrentEnrollment.ProgressFileID,
                                    
                                    LookupEnrollmentProgressStateID = (int)EnumEnrollmentProgressStates.New_Enrollment,
                                    CurriculumID = CurriculumIDForPreRquisiteCourseEnrollment,
                                    StudentEnrollment = new StudentEnrollment()
                                    {
                                        StudentID = CurrentEnrollment.StudentEnrollment.StudentID,
                                        DateUpdated = DateTime.Now
                                    },
                                    DateIntitiated = DateTime.Now,
                                    EnrollmentExcempt = false
                                   
                                };
                                Dbconnection.Enrollments.Add(PreRequisisteCourseEnrollment);
                                Dbconnection.SaveChanges();
                                //links each pre-Requisite Course to the Curriculum Linked.
                                foreach (CurriculumCourse CurriculumCourseToLink in (from a in CPC
                                                                                     where a.CurriculumCourse.CurriculumID == CurriculumIDForPreRquisiteCourseEnrollment
                                                                                     select a.CurriculumCourse)
                                                   .Distinct<CurriculumCourse>().
                                                   ToList<CurriculumCourse>())
                                {
                                    Dbconnection.CurriculumCourseEnrollments.Add(new CurriculumCourseEnrollment
                                    {
                                        LookupEnrollmentProgressStateID = (int)EnumEnrollmentProgressStates.New_Enrollment,
                                        CurriculumCourseID = CurriculumCourseToLink.CurriculumCourseID,
                                        EnrollmentID = PreRequisisteCourseEnrollment.EnrollmentID,
                                        CourseCost = CurriculumCourseToLink.Cost // Set the linked course to the default Cost for the course
                                    });
                                }
                                //Saves the sub set of Pre-Requisite Curriculum and linked courses.
                                Dbconnection.SaveChanges();
                            }

                            EquiryHistory hist = new EquiryHistory
                            {
                                EnquiryID = CurrentEnquiry.EnquiryID,
                                EmployeeID = CurrentEmployeeLoggedIn.EmployeeID,
                                LookupEquiyHistoryTypeID = (int)EnumEquiryHistoryTypes.Enrollment_Student_Successfully_Enrolled,
                                DateEnquiryUpdated = DateTime.Now,
                                EnquiryNotes = "Initial Enrollment Completed Successfully for the the Following Individual - " + CurrentEnrollment.StudentEnrollment.Student.Individual.FullName.ToString() + "\n Enquiry Ref# - " + CurrentEnquiry.EnquiryID

                            };
                            Dbconnection.EquiryHistories.Add(hist);


                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            IsSuccessfullySaved = true;
                        }
                        catch (Exception ex)
                        {
                            if (ex is DbEntityValidationException)
                            {
                                foreach (DbEntityValidationResult entityErr in ((DbEntityValidationException)ex).EntityValidationErrors)
                                {
                                    foreach (DbValidationError error in entityErr.ValidationErrors)
                                    {
                                        //MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        MetroMessageBox.Show(this, error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MetroMessageBox.Show(this, ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //Rollback transaction if exception occurs
                            dbTran.Rollback();
                        }
                    }
                };





            }
        }

        private void dgvFileUploads_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    EnrollmentDocument EnrollmentDocumentObj = (EnrollmentDocument)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colFileName.Index].Value = EnrollmentDocumentObj.File.FileName.ToString();
                    row.Cells[colDocumentType.Index].Value = EnrollmentDocumentObj.LookupEnrollentDocumentType.EnrollmentDocumentType.ToString();
                    row.Cells[colFileExtension.Index].Value = EnrollmentDocumentObj.File.FileExtension.ToString();
                }
            }
        }

        private void cboDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainflowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
