using Impendulo.Data.Models;
using Impendulo.Data.Models.Enum;
using MetroFramework;
using MetroFramework.Controls;
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

namespace Impendulo.Developments.Assessments.ApprenticeshipAssessments.AddNewReport
{
    public partial class frmAddNewApprenticeshipAssessmentReport : MetroFramework.Forms.MetroForm
    {

        /*
         * REquired Pararmeters
         * *******************/
        //public int CurriculumCourseEnrollmentID { get; private set; }
        //public int CurriculumCourseID { get; private set; }
        public Assessment CurrentlySelectedAssessment { get; private set; }
        public CurriculumCourseEnrollment CurrentlySelectedCurriculumCourseEnrollment { get; private set; }
        //public int EnrollmentID { get; private set; }
        private List<AssessmentReport> AssessmentReportList { get; set; }
        /*END of Reuired Paramters
         * ***********************/
        private int iCurrentPosition { get; set; }

        public Employee CurrentEmployeeLoggedIn
        {
            get;
            set;
        }
        public Boolean IsSuccessfullySaved { get; set; }

        private Boolean MustSaveItems = false;
        public frmAddNewApprenticeshipAssessmentReport(CurriculumCourseEnrollment CurrentlySelectedCurriculumCourseEnrollment)
        {
            this.CurrentlySelectedCurriculumCourseEnrollment = CurrentlySelectedCurriculumCourseEnrollment;
            AssessmentReportList = new List<AssessmentReport>();

            iCurrentPosition = 0;
            InitializeComponent();

            /*Loadup the Selected Assesment
             * *****************************/

            using (var Dbconnection = new MCDEntities())
            {
                CurrentlySelectedAssessment = (from a in Dbconnection.Assessments
                                               where a.CurriculumCourseEnrollmentID == this.CurrentlySelectedCurriculumCourseEnrollment.CurriculumCourseEnrollmentID
                                               select a)
                                               .Include(a => a.AssessmentModules)
                                               .Include(a => a.AssessmentModules.Select(b => b.CurriculumCourseModule))
                                               .Include(a => a.AssessmentModules.Select(b => b.CurriculumCourseModule.Module))
                                               .Include(a => a.AssessmentModules.Select(b => b.AssessmentModuleActivities))
                                               //.Include(a => a.AssessmentModules.Select(b => b.LookupPracticalAssessmentStatus.PracticalAssessmentStatus))
                                               .FirstOrDefault<Assessment>();
                if (CurrentlySelectedAssessment == null)
                {
                    CurrentlySelectedAssessment = new Assessment();
                    Schedule CurrentConfiguredSchedule = (from a in Dbconnection.Schedules
                                                          where a.CurriculumCourseEnrollmentID == this.CurrentlySelectedCurriculumCourseEnrollment.CurriculumCourseEnrollmentID
                                                          select a).FirstOrDefault<Schedule>();
                    /*Set the start and end date controls on the first page.
                     * ***************************************************/
                    CurrentlySelectedAssessment.AssessmentDateStarted = CurrentConfiguredSchedule.ScheduleStartDate.Date;
                    CurrentlySelectedAssessment.AssessmentDateCompleted = CurrentConfiguredSchedule.ScheduleCompletionDate.Date;
                    CurrentlySelectedAssessment.AssessmentDateSubmitted = DateTime.Now;
                    CurrentlySelectedAssessment.CurriculumCourseEnrollmentID = this.CurrentlySelectedCurriculumCourseEnrollment.CurriculumCourseEnrollmentID;

                    foreach (CurriculumCourseModule CCM in (from a in Dbconnection.CurriculumCourseModules
                                                            where a.CurriculumCourseID == this.CurrentlySelectedCurriculumCourseEnrollment.CurriculumCourseID
                                                            select a)
                                                            .Include(a => a.Module)
                                                            .ToList<CurriculumCourseModule>())
                    {
                        CCM.ObjectState = Data.Models.Enum.EntityObjectState.Unchanged;
                        // CCM.CurriculumCours.Course
                        CurrentlySelectedAssessment.AssessmentModules.Add(new AssessmentModule()
                        {
                            CurriculumCourseModuleID = CCM.CurriculumCourseModuleID,
                            PracticalAssessmentStatusID = (int)Common.Enum.EnumPracticalAssessmentStatuses.Not_Yet_Competent,
                            TheoriticalAssessmentStatusID = (int)Common.Enum.EnumTheoriticalAssesmentStatuses.Not_Yet_Competent,
                            Notes = "",
                            AssessmentID = 0,
                            CurriculumCourseModule = CCM
                        });
                    }

                    //foreach (AssessmentModule AM in CurrentlySelectedAssessment.AssessmentModules)
                    //{
                    //    Dbconnection.Entry(AM).Reference(a => a.LookupPracticalAssessmentStatus).Load();
                    //    Dbconnection.Entry(AM).Reference(a => a.LookupTheoriticalAssesmentStatus).Load();
                    //}

                    /***********************************************
                     * Load the Currently Loaded Course Name
                     * *********************************************/
                    lblSelectedCourseName.Text = (from a in Dbconnection.CurriculumCourses
                                                  where a.CurriculumCourseID == this.CurrentlySelectedCurriculumCourseEnrollment.CurriculumCourseID
                                                  select a).FirstOrDefault<CurriculumCourse>().Course.CourseName;

                }
            };
            /*END of Loading Assessment
             * ************************/
        }

        private void frmAddNewApprenticeshipAssessmentReport_Load(object sender, EventArgs e)
        {
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
            /*Pre Loads the Lookup Types
             * ************************/
            this.populatePracticalAssessmentStatus();
            this.populateTheoriticalAssessmentStatus();
            this.preloadRecommendations();
            /*END of Loading Lookup Types
             * ****************************/

            /*Loads the different sections of the wizard
             * ********************************************/
            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();
            /*End of loading the sections of the wizard
             * *******************************************/


        }
        #region Pre Load Recommendations
        private void preloadRecommendations()
        {

            using (var Dbconnection = new MCDEntities())
            {
                foreach (LookupAssessmentRecommendation AR in Dbconnection.LookupAssessmentRecommendations.ToList<LookupAssessmentRecommendation>())
                {
                    MetroRadioButton rad = new MetroRadioButton();

                    rad.UseStyleColors = true;
                    if (AR.AssessmentRecommendationID == 2)
                    {
                        rad.Width = 165;
                    }
                    else if (AR.AssessmentRecommendationID == 3)
                    {
                        rad.Width = 140;
                    }

                    rad.Margin = new Padding(3, 5, 3, 0);
                    rad.Text = AR.AssessmentRecommendation;
                    rad.Tag = AR.AssessmentRecommendationID;

                    rad.Click += Rad_Click;

                    flowLayoutPanelFinalRecommendation.Controls.Add(rad);
                }
            };
        }

        private void Rad_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region PerloadDropDownControls
        private void populatePracticalAssessmentStatus()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupPracticalAssessmentStatusBindingSource.DataSource = (from a in Dbconnection.LookupPracticalAssessmentStatuses
                                                                           select a).ToList<LookupPracticalAssessmentStatus>();
            };
        }
        private void populateTheoriticalAssessmentStatus()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupTheoriticalAssesmentStatusBindingSource.DataSource = (from a in Dbconnection.LookupTheoriticalAssesmentStatuses
                                                                            select a).ToList<LookupTheoriticalAssesmentStatus>();
            };
        }

        #endregion

        #region Forms Sections

        #region Page 1 - Set Actual Dates

        #region Refresh Methods



        #endregion

        #region Populate Methods

        #endregion

        #region Control Events


        #endregion

        #region Logic Control Methods



        #endregion
        #endregion

        #region Page 2  - Update Stauses

        #region Refresh Methods
        private void refreshAvailableAssessmentModules()
        {

        }
        private void refreshLinkedAssessmentModules()
        {

        }
        #endregion
        #region Populate Methods
        private void populateAvailableAssessmentModules()
        {
            using (var Dbconnection = new MCDEntities())
            {

            };
        }
        private void poulateLinkedAssessmentModules()
        {
            using (var Dbconnection = new MCDEntities())
            {

            };
        }
        #endregion

        #region Controls Events

        #endregion

        #region Logical Control Metods


        #endregion



        #endregion



        #region Page 3 - Upload Facilitator Reports 

        #region Refresh Methods
        private void refreshDocumentUploads()
        {
            this.populatehDocumentUploads();
        }
        #endregion

        #region Populate Methods
        private void populatehDocumentUploads()
        {
            assessmentReportBindingSource.DataSource = AssessmentReportList.ToList<AssessmentReport>();
        }
        #endregion

        #region Control Events

        #endregion

        #region logical Control Methods

        #endregion

        #endregion

        #region Page 4 - Documentation Upload

        #region Refresh Methods

        #endregion

        #region Populate Methods

        #endregion

        #region Control Events


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
                    var lblObj = (MetroFramework.Controls.MetroLabel)Control;
                    if (Convert.ToInt32(lblObj.Tag.ToString()) == iCurrentPosition)
                    {
                        lblObj.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                        //lblObj.Font = new Font(lblObj.Font, FontStyle.Bold | FontStyle.Underline);
                    }
                    else
                    {
                        lblObj.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                        //lblObj.Font = new Font(lblObj.Font, FontStyle.Regular);
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
                    this.LoadupStep1();
                    break;
                case 1:
                    this.LoadupStep2();
                    break;
                case 2:
                    this.LoadupStep3();
                    break;
                case 3:
                    this.LoadupStep4();
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

                    break;
                case 2:
                    break;
                case 3:
                    bRtn = VerfiyFinalSelectionIsMade();
                    if (!bRtn)
                    {
                        MetroMessageBox.Show(this, "No Final Recommendation/Result has been Selected! Please Select before proceeding.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
        private Boolean VerfiyFinalSelectionIsMade()
        {
            foreach (Control Con in flowLayoutPanelFinalRecommendation.Controls)
            {
                if (Con is MetroRadioButton)
                {
                    if (((MetroRadioButton)Con).Checked)
                    {
                        return true;
                    }
                }
            }
            //if (radConculsionCompetent.Checked) { return true; }
            //else if (radConculsionCourseInComplete.Checked) { return true; }
            //else if (radConculsionNotYetCompetent.Checked) { return true; }
            return false;
        }
        #region "Each Wizard Page Loadup"
        private void LoadupStep1()
        {

            dtActualStartTime.Value = CurrentlySelectedAssessment.AssessmentDateStarted.Date;
            dtActualCompletionTime.Value = CurrentlySelectedAssessment.AssessmentDateCompleted.Date;
            setSummaryDates();

            if (CurrentlySelectedAssessment == null)
            {
                /*Load the dates from the Sechudles table and use the 
                 * Start and end Date of the Scheduled Times as the initial Sart and end time.
                 * *****************************************************************************/



            }
            else
            {
                /*Load the actual start and end time from the Currently Selected Assessment
                 **************************************************************************/

            }
        }
        private void LoadupStep2()
        {
            assessmentModuleBindingSource.DataSource = (from a in CurrentlySelectedAssessment.AssessmentModules
                                                        select a).ToList<AssessmentModule>();
        }
        private void LoadupStep3()
        {

            refreshDocumentUploads();

        }


        private void LoadupStep4()
        {

        }


        #endregion

        #endregion

        #endregion

        private void dgvAssessmentModules_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    AssessmentModule AssessmentModuleObj = (AssessmentModule)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colModuleName.Index].Value = AssessmentModuleObj.CurriculumCourseModule.Module.ModuleName.ToString();
                }
            }
        }

        private void btnUploadREportDocument_Click(object sender, EventArgs e)
        {
            List<Data.Models.File> UploadedFiles = Common.FileHandeling.FileHandeling.UploadFile(
               UseMultipleFileSelect: true,
               AutomaicallyAddFileToDatabase: false,
               ImagesOnly: false);



            //LookupEnrollentDocumentType EDT = null;
            //using (var Dbconnection = new MCDEntities())
            //{
            //    EDT = (from a in Dbconnection.LookupEnrollentDocumentTypes
            //           where a.LookupEnrollmentDocumentTypeID == (int)Common.Enum.EnumEnrollentDocumentTypes.Facilitator_Report
            //           select a).FirstOrDefault<LookupEnrollentDocumentType>();
            //};

            foreach (Data.Models.File f in UploadedFiles)
            {
                AssessmentReport AR = new AssessmentReport()
                {
                    AssessmentID = 0,
                    File = f,
                    ImageID = 0,
                    DateReportUploaded = DateTime.Now,
                    AssessmentReportID = 0
                };


                AssessmentReportList.Add(AR);
            }
            //foreach (EnrollmentDocument EDItem in CurrentEnrollment.EnrollmentDocuments)
            //{
            //    Dbconnection.EnrollmentDocuments.Attach(EDItem);
            //    Dbconnection.Entry(EDItem).Reference(a => a.LookupEnrollentDocumentType).Load();
            //}

            this.refreshDocumentUploads();
        }

        private void dgvFileUploads_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    AssessmentReport AssessmentReportObj = (AssessmentReport)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colFileName.Index].Value = AssessmentReportObj.File.FileName.ToString();
                    //row.Cells[colDocumentType.Index].Value = EnrollmentDocumentObj.LookupEnrollentDocumentType.EnrollmentDocumentType.ToString();
                    row.Cells[colFileExtension.Index].Value = AssessmentReportObj.File.FileExtension.ToString();
                }
            }
        }

        private void btnRemoveFacilitatorReport_Click(object sender, EventArgs e)
        {
            AssessmentReportList.RemoveAt(assessmentReportBindingSource.Position);
            this.refreshDocumentUploads();
        }

        private void dtActualStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtActualStartTime.Value.Date > dtActualCompletionTime.Value.Date)
            {
                dtActualCompletionTime.Value = dtActualStartTime.Value.Date;
            }
            CurrentlySelectedAssessment.AssessmentDateStarted = dtActualStartTime.Value.Date;
            setSummaryDates();
        }

        private void dtActualCompletionTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtActualCompletionTime.Value.Date < dtActualStartTime.Value.Date)
            {
                dtActualStartTime.Value = dtActualCompletionTime.Value.Date;
            }
            CurrentlySelectedAssessment.AssessmentDateCompleted = dtActualCompletionTime.Value.Date;
            setSummaryDates();
        }

        private void setSummaryDates()
        {
            txtSummaryActualStartTime.Text = dtActualStartTime.Value.Date.ToString("D");
            txtSummaryCompletionDate.Text = dtActualCompletionTime.Value.Date.ToString("D");
        }

        private void frmAddNewApprenticeshipAssessmentReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MustSaveItems)
            {
                this.SaveAssessment();
            }
        }
        private int GetRecommendation()
        {

            foreach (Control con in flowLayoutPanelFinalRecommendation.Controls)
            {
                if (con is MetroRadioButton)
                {
                    MetroRadioButton conObj = (MetroRadioButton)con;

                    if (((MetroRadioButton)con).Checked)
                    {
                        return (int)conObj.Tag;
                    }
                }
            }
            return 0;
        }
        private void SaveAssessment()
        {

            using (var Dbconnection = new MCDEntities())
            {
                using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                {
                    try
                    {
                        //CRUD Operations
                        List<AssessmentModule> AMList = CurrentlySelectedAssessment.AssessmentModules.ToList<AssessmentModule>();
                        CurrentlySelectedAssessment.AssessmentModules.Clear();
                        CurrentlySelectedAssessment.AssessmentRecommendationID = GetRecommendation();
                        //check the ObjectState property and mark appropriate EntityState 
                        if (CurrentlySelectedAssessment.ObjectState == EntityObjectState.Added)
                            Dbconnection.Entry(CurrentlySelectedAssessment).State = System.Data.Entity.EntityState.Added;
                        else if (CurrentlySelectedAssessment.ObjectState == EntityObjectState.Modified)
                            Dbconnection.Entry(CurrentlySelectedAssessment).State = System.Data.Entity.EntityState.Modified;
                        else if (CurrentlySelectedAssessment.ObjectState == EntityObjectState.Deleted)
                            Dbconnection.Entry(CurrentlySelectedAssessment).State = System.Data.Entity.EntityState.Deleted;
                        else
                            Dbconnection.Entry(CurrentlySelectedAssessment).State = System.Data.Entity.EntityState.Unchanged;

                        Dbconnection.SaveChanges();


                        foreach (AssessmentModule AssessmentModuleObj in AMList)
                        {
                            AssessmentModule AM = new AssessmentModule()
                            {
                                AssessmentModuleID = 0,
                                AssessmentID = CurrentlySelectedAssessment.AssessmentID,
                                CurriculumCourseModuleID = AssessmentModuleObj.CurriculumCourseModuleID,
                                ObjectState = EntityObjectState.Added,
                                PracticalAssessmentStatusID = AssessmentModuleObj.PracticalAssessmentStatusID,
                                TheoriticalAssessmentStatusID = AssessmentModuleObj.TheoriticalAssessmentStatusID,
                                Notes = AssessmentModuleObj.Notes
                            };
                            CurrentlySelectedAssessment.AssessmentModules.Add(AM);
                            //check the ObjectState property and mark appropriate EntityState 
                            if (AM.ObjectState == EntityObjectState.Added)
                                Dbconnection.Entry(AM).State = System.Data.Entity.EntityState.Added;
                            else if (AM.ObjectState == EntityObjectState.Modified)
                                Dbconnection.Entry(AM).State = System.Data.Entity.EntityState.Modified;
                            else if (AM.ObjectState == EntityObjectState.Deleted)
                                Dbconnection.Entry(AM).State = System.Data.Entity.EntityState.Deleted;
                            else
                                Dbconnection.Entry(AM).State = System.Data.Entity.EntityState.Unchanged;
                        }
                        ////saves all above operations within one transaction
                        Dbconnection.SaveChanges();

                        foreach (AssessmentReport AR in AssessmentReportList)
                        {
                            AR.AssessmentID = CurrentlySelectedAssessment.AssessmentID;
                            Dbconnection.AssessmentReports.Add(AR);
                        }
                        Dbconnection.SaveChanges();
                        //if (radConculsionCompetent.Checked)
                        //{
                        Dbconnection.CurriculumCourseEnrollments.Attach(CurrentlySelectedCurriculumCourseEnrollment);
                        CurrentlySelectedCurriculumCourseEnrollment.LookupEnrollmentProgressStateID = (int)Common.Enum.EnumEnrollmentProgressStates.Complete;
                        Dbconnection.SaveChanges();
                        Dbconnection.Entry(CurrentlySelectedCurriculumCourseEnrollment).Reference(a => a.LookupEnrollmentProgressState).Load();
                        //}
                        //else if (radConculsionNotYetCompetent.Checked)
                        //{

                        //}
                        //else if (radConculsionCourseInComplete.Checked)
                        //{

                        //}
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
}

