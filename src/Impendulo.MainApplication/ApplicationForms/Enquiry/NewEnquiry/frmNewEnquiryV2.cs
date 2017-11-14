using Impendulo.Common;
using Impendulo.Data.Models;
using Impendulo.Data.Models.DataGridViewStructures.Enquiry;
using Impendulo.Data.Models.Enum;
using Impendulo.Deployment.ContactDetails;
using Impendulo.Deployment.Email;
using Impendulo.Deployment.Enquiry.NewEnquiry.AddNewEnquiry;
using Impendulo.Deployment.Enrollment;
using Impendulo.Deployment.Students;
using MetroFramework;
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

namespace Impendulo.Deployment.Enquiry.NewEnquiry
{
    public partial class frmNewEnquiryV2 : MetroFramework.Forms.MetroForm
    {


        #region Enum Classes
        public enum enumEquiryOrigion : int
        {
            PrivateStudent = 1,
            Company = 2,
            GeneralEnquiry = 3
        }
        #endregion
        #region GLOBAL Variables
        #region Current Selected Options
        private enumEquiryOrigion CurrentlySelectedEquiryOrigion { get; set; }
        private Data.Models.Company CurrentlySelectedCompany { get; set; }
        private Individual CurrentlySelectedIndividual { get; set; }
        private ProgressFile CurrentlySelectedProgressFile { get; set; }

        private Boolean IsProgressComanyProgressFileEnquiry { get; set; }
        private Boolean IsStudentFileEnquiry { get; set; }
        #endregion
        #region Wizard Variables
        public int CurrentPosition { get; set; }
        #endregion
        #region User and Role
        public Employee CurrentEmployeeLoggedIn { get; set; }
        #endregion
        #endregion



        public frmNewEnquiryV2(Boolean IsProgressComanyProgressFileEnquiry = false, Boolean IsStudentFileEnquiry = false, Data.Models.Company CurrentlySelectedCompany = null, Individual CurrentlySelectedIndividual = null)
        {
            this.IsProgressComanyProgressFileEnquiry = IsProgressComanyProgressFileEnquiry;
            this.IsStudentFileEnquiry = IsStudentFileEnquiry;
            this.CurrentlySelectedCompany = CurrentlySelectedCompany;
            this.CurrentlySelectedIndividual = CurrentlySelectedIndividual;

            InitializeComponent();

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
        }

        private void frmNewEnquiryV2_Load(object sender, EventArgs e)
        {
            //CurrentEmployeeLoggedIn

            /*Loads the Wizard conponents and controls
             * **************************************/
            //this.CurrentPosition = 0;
            if (IsProgressComanyProgressFileEnquiry)
            {
                CurrentlySelectedEquiryOrigion = enumEquiryOrigion.Company;
                picSelectPrivateStudent.Visible = false;
                picSelectGeneralEnquiry.Visible = false;
                CurrentPosition = 2;
                int CompanyProgressFileID = Common.Verifiction.OfProgressFiles.VerifyCompanyProgressFile(this.CurrentlySelectedCompany.CompanyID);


                this.refreshCompanyName();
                this.refreshCompanyContact();
                this.refreshContactDetails();
            }
            else if (IsStudentFileEnquiry)
            {
                CurrentlySelectedEquiryOrigion = enumEquiryOrigion.PrivateStudent;
                picSelectCompany.Visible = false;
                picSelectGeneralEnquiry.Visible = false;
                CurrentPosition = 2;

                this.refreshStudent(CurrentlySelectedIndividual.IndividualID);

                refreshStudentContact();
                refreshContactDetails();
            }
            else
            {
                CurrentPosition = 0;
                CurrentlySelectedEquiryOrigion = enumEquiryOrigion.PrivateStudent;
            }

            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();
            /*End Loading Of Wizard Controls
            *******************************/

        }

        #region Wizard Page Sections

        #region Page 1 - Select Enquiry Origion

        #region Page Logic Methods
        private void resetAllInfoGathered()
        {
            contactDetailBindingSource.Clear();
            CurrentlySelectedIndividual = null;
            CurrentlySelectedCompany = null;
            CurrentlySelectedProgressFile = null;
        }

        private void resetEquiryOrigionSelectionButtons()
        {

            picSelectPrivateStudent.BackColor = Color.Transparent;
            picSelectCompany.BackColor = Color.Transparent;
            picSelectGeneralEnquiry.BackColor = Color.Transparent;


        }

        #endregion
        #region Control Methods
        private void picEnquiryOrigionSelection_Click(object sender, EventArgs e)
        {
            resetEquiryOrigionSelectionButtons();
            PictureBox PB = (PictureBox)sender;
            PB.BackColor = Color.Gainsboro;

            CurrentlySelectedEquiryOrigion = (enumEquiryOrigion)Convert.ToInt32(PB.Tag);
            setCurrentTabOrigion();


        }
        private void setCurrentTabOrigion()
        {
            switch (CurrentlySelectedEquiryOrigion)
            {
                case enumEquiryOrigion.Company:
                    lblCurentlySelectedEnquiryOrigionType.Text = "Company Related Enquiry";
                    this.resetAllInfoGathered();
                    break;
                case enumEquiryOrigion.GeneralEnquiry:
                    lblCurentlySelectedEnquiryOrigionType.Text = "General Enquiry";
                    this.resetAllInfoGathered();
                    break;
                case enumEquiryOrigion.PrivateStudent:
                    lblCurentlySelectedEnquiryOrigionType.Text = "Student Related Enquiry";
                    this.resetAllInfoGathered();
                    break;
            }
        }
        #endregion
        #endregion

        #region Page 2 - Contact Selection


        #region Control Methods 
        private void picSelectComany_Click(object sender, EventArgs e)
        {
            using (frmSelectCompanyContact frm = new frmSelectCompanyContact())
            {
                frm.CurrentCompany = CurrentlySelectedCompany;
                frm.SelectedIndividual = CurrentlySelectedIndividual;
                frm.ShowDialog();
                if (frm.CurrentCompany != null)
                {
                    this.CurrentlySelectedCompany = frm.CurrentCompany;
                    if (frm.SelectedIndividual != null)
                    {
                        this.CurrentlySelectedIndividual = frm.SelectedIndividual;
                        int CompanyProgressFileID = Common.Verifiction.OfProgressFiles.VerifyCompanyProgressFile(this.CurrentlySelectedCompany.CompanyID);
                        //Gets the Slected Copmpany Progress File
                        //using (var Dbconnection = new MCDEntities())
                        //{
                        //    CurrentlySelectedProgressFile = (from a in Dbconnection.ProgressFiles
                        //                                     where a.ProgressFileID == CompanyProgressFileID
                        //                                     select a)
                        //                                     .Include(a => a.CompanyProgressFile)
                        //                                     .FirstOrDefault<ProgressFile>();
                        //};
                    }
                }

            };

            this.refreshCompanyName();
            this.refreshCompanyContact();
            this.refreshContactDetails();
        }

        private void picSelectStudent_Click(object sender, EventArgs e)
        {

            using (frmStudentSearchForStudent frm = new frmStudentSearchForStudent(bMustSearchAllStudents: true))
            {

                //Set the list of student that are already Enrolled for this Enquiry Item.
                //frm.StudentExpceptionList = StudentExcemptionList;
                frm.ShowDialog();
                if (frm.CurrentSelectedStudent != null)
                {
                    this.refreshStudent(frm.CurrentSelectedStudent.StudentID);
                }
                else
                {
                    CurrentlySelectedIndividual = null;
                }
                refreshStudentContact();
                refreshContactDetails();
            };
        }

        private void btnpicUpdateStudent_Click(object sender, EventArgs e)
        {

            using (frmStudentAddUpdate frm = new frmStudentAddUpdate(CurrentlySelectedIndividual.IndividualID))
            {
                frm.ShowDialog();
                this.refreshStudent(CurrentlySelectedIndividual.IndividualID);
                refreshStudentContact();
                refreshContactDetails();
            };
            //frm.CurrentStudentID = 0;




        }
        private void picSelectConact_Click(object sender, EventArgs e)
        {
            using (frmSelectIndividualContact frm = new frmSelectIndividualContact())
            {
                frm.SelectedIndividual = CurrentlySelectedIndividual;
                frm.ShowDialog();


                if (frm.SelectedIndividual != null)
                {
                    CurrentlySelectedIndividual = frm.SelectedIndividual;
                }
                else
                {
                    CurrentlySelectedIndividual = null;
                }
                refreshGeneralContact();
                refreshContactDetails();
            };

        }
        #endregion
        #region Page Logic Methods
        private void setCorrectContactSelectionPanel()
        {
            switch (CurrentlySelectedEquiryOrigion)
            {
                case enumEquiryOrigion.PrivateStudent:
                    tableLayoutPanelForCompanySelection.Visible = false;
                    tableLayoutPanelForStudentSelection.Visible = true;
                    tableLayoutPanelForGeneralSelection.Visible = false;

                    tableLayoutPanelForStudentSelection.Width = flowLayoutPanelSelectContactPerson.Width;
                    tableLayoutPanelForStudentSelection.Height = flowLayoutPanelSelectContactPerson.Height;
                    break;
                case enumEquiryOrigion.Company:
                    tableLayoutPanelForCompanySelection.Visible = true;
                    tableLayoutPanelForGeneralSelection.Visible = false;
                    tableLayoutPanelForStudentSelection.Visible = false;

                    tableLayoutPanelForCompanySelection.Width = flowLayoutPanelSelectContactPerson.Width;
                    tableLayoutPanelForCompanySelection.Height = flowLayoutPanelSelectContactPerson.Height;
                    break;
                case enumEquiryOrigion.GeneralEnquiry:
                    tableLayoutPanelForCompanySelection.Visible = false;
                    tableLayoutPanelForGeneralSelection.Visible = true;
                    tableLayoutPanelForStudentSelection.Visible = false;

                    tableLayoutPanelForGeneralSelection.Width = flowLayoutPanelSelectContactPerson.Width;
                    tableLayoutPanelForGeneralSelection.Height = flowLayoutPanelSelectContactPerson.Height;
                    break;
            }
        }
        private void clearSelectedCompanies()
        {
            CurrentlySelectedCompany = null;
            this.refreshCompanyName();
            this.refreshCompanyContact();
        }

        private void clearSelectedIndividuals()
        {
            //CurrentEnquiry.Individuals.Clear();
            this.refreshContactDetails();
        }

        #endregion
        #region Refresh Methods

        private void refreshStudent(int StudentID)
        {
            populateStudent(StudentID);
        }
        private void refreshStudentContact()
        {
            if (CurrentlySelectedIndividual != null)
            {
                this.populateStudentContact();
            }
            else
            {
                txtStudentFristName.Clear();
                txtStudentID_PassportNumber.Clear();
                txtStudentLastName.Clear();
                txtStudentNumber.Clear();

            }
        }
        private void refreshCompanyName()
        {
            if (CurrentlySelectedCompany != null)
            {
                this.populateCompanyName();
            }
            else
            {
                txtContactInformationCompanyName.Clear();
                //txtSummaryCompanyName.Text = "NA - Private Enquiry";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void refreshCompanyContact()
        {
            if (CurrentlySelectedIndividual != null)
            {
                this.populateCompanyContact();
            }
            else
            {
                txtContactInformationCompanyContact.Clear();
                //txtSummaryContactName.Clear();
            }
        }
        private void refreshContactDetails()
        {

            if (CurrentlySelectedIndividual != null)
            {
                this.populateContactDetails();
            }
            else
            {
                contactDetailBindingSource.DataSource = null;
            }

        }

        private void refreshGeneralContact()
        {
            if (CurrentlySelectedIndividual != null)
            {
                populateGeneralContact();
            }
            else
            {
                txtGeneralContactFirstName.Clear();
                txtGeneralContactLastName.Clear();
            }
        }
        #endregion

        #region Populate Methods
        private void populateGeneralContact()
        {
            if (CurrentlySelectedIndividual != null)
            {
                txtGeneralContactFirstName.Text = CurrentlySelectedIndividual.IndividualFirstName.ToString();
                txtGeneralContactLastName.Text = CurrentlySelectedIndividual.IndividualLastname.ToString();
            }
            
        }
        private void populateStudent(int StudentID)
        {
            using (var Dbconnection = new MCDEntities())
            {
                this.CurrentlySelectedIndividual = (from a in Dbconnection.Individuals
                                                    where a.IndividualID == StudentID
                                                    select a)
                                                    .Include(a => a.Student)
                                                    .Include(a => a.ContactDetails)
                                                    .Include(a => a.ContactDetails.Select(b => b.LookupContactType))
                                                    .FirstOrDefault<Individual>();

                /*Verfiy to determine if Student Progress Files Exists
                *****************************************************/
                int StudentProgressFileID = Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(CurrentlySelectedIndividual.IndividualID);

                //CurrentlySelectedProgressFile = (from a in Dbconnection.ProgressFiles
                //                                 where a.ProgressFileID == StudentProgressFileID
                //                                 select a)
                //                                 .Include(a => a.StudentProgressFile)
                //                                 .FirstOrDefault<ProgressFile>();
            };
        }
        private void populateStudentContact()
        {
            txtStudentFristName.Text = CurrentlySelectedIndividual.IndividualFirstName;
            txtStudentID_PassportNumber.Text = CurrentlySelectedIndividual.Student.StudentIDNumber;
            if (CurrentlySelectedIndividual.Student.StudentPassportNumber.Length > 0)
            {
                txtStudentID_PassportNumber.Text += " " + CurrentlySelectedIndividual.Student.StudentPassportNumber;
            }
            txtStudentLastName.Text = CurrentlySelectedIndividual.IndividualLastname;
            txtStudentNumber.Text = CurrentlySelectedIndividual.Student.StudentID.ToString();
        }
        private void populateContactDetails()
        {
            using (var Dbconnection = new MCDEntities())
            {
                contactDetailBindingSource.DataSource = (from a in Dbconnection.ContactDetails
                                                         from b in a.Individuals
                                                         where b.IndividualID == CurrentlySelectedIndividual.IndividualID
                                                         select a).ToList<ContactDetail>();
            };

        }
        private void populateCompanyContact()
        {
            txtContactInformationCompanyContact.Text = CurrentlySelectedIndividual.FullName;
        }
        private void populateCompanyName()
        {

            /*populate Company Text Box
             * ****************************/
            txtContactInformationCompanyName.Text = CurrentlySelectedCompany.CompanyName;
            //txtSummaryCompanyName.Text = CompanyObj.CompanyName;

        }



        #endregion
        #endregion

        #region Page 3 - Comany and Student Enquiry

        #region Refresh Methods
        private void refreshProgressFile()
        {
            switch (CurrentlySelectedEquiryOrigion)
            {
                case enumEquiryOrigion.Company:

                    using (var Dbconnection = new MCDEntities())
                    {
                        CurrentlySelectedProgressFile = (from a in Dbconnection.ProgressFiles
                                                         where a.CompanyProgressFile.CompanyID == CurrentlySelectedCompany.CompanyID
                                                         select a)
                                                                .Include(a => a.CompanyProgressFile)
                                                                .Include(a => a.CompanyProgressFile.CompanyStudentProgressFiles)
                                                                .Include(a => a.Enquiries)
                                                                .Include(a => a.Enquiries.Select(b => b.Curriculum))
                                                                .Include(a => a.Enquiries.Select(b => b.Curriculum.LookupDepartment))
                                                                .Include(a => a.Enquiries.Select(b => b.Enrollments))
                                                                .Include(a => a.Enquiries.Select(b => b.LookupEnquiryStatus))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals.Select(c => c.ContactDetails)))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals.Select(c => c.ContactDetails.Select(d => d.LookupContactType))))
                                                                .FirstOrDefault<ProgressFile>();
                    };
                    txtProgressFileID.Text = CurrentlySelectedProgressFile.ProgressFileID.ToString();
                    refreshEnquiries();
                    break;
                case enumEquiryOrigion.PrivateStudent:
                    using (var Dbconnection = new MCDEntities())
                    {
                        CurrentlySelectedProgressFile = (from a in Dbconnection.ProgressFiles
                                                         where a.StudentProgressFile.StudentID == CurrentlySelectedIndividual.IndividualID
                                                         select a)
                                                                .Include(a => a.StudentProgressFile)
                                                                .Include(a => a.Enquiries)
                                                                .Include(a => a.Enquiries.Select(b => b.Curriculum))
                                                                .Include(a => a.Enquiries.Select(b => b.Curriculum.LookupDepartment))
                                                                .Include(a => a.Enquiries.Select(b => b.Enrollments))
                                                                .Include(a => a.Enquiries.Select(b => b.LookupEnquiryStatus))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals.Select(c => c.ContactDetails)))
                                                                .Include(a => a.Enquiries.Select(b => b.Individuals.Select(c => c.ContactDetails.Select(d => d.LookupContactType))))
                                                                .FirstOrDefault<ProgressFile>();
                    };
                    txtProgressFileID.Text = CurrentlySelectedProgressFile.ProgressFileID.ToString();
                    refreshEnquiries();
                    break;

            }
            txtCurrentContactPerson.Text = CurrentlySelectedIndividual.FullName;
        }
        private void refreshEnquiries()
        {
            populateCompanyOrStudentEnquiry();
        }
        private void refreshAllCompnayEnrollments()
        {

        }

        private void refreshStudentFile()
        {

        }
        #endregion

        #region Populate Methods

        private void populateCompanyOrStudentEnquiry()
        {
            this.enquiriesBindingSource.Clear();
            //CurrentEnquiries
            enquiriesBindingSource.DataSource = new CustomSortableBindingList<CurrentEnquiries>((from a in CurrentlySelectedProgressFile.Enquiries
                                                                                                 select new CurrentEnquiries()
                                                                                                 {
                                                                                                     EnquiryID = a.EnquiryID,
                                                                                                     Curriculum = a.Curriculum.CurriculumName,
                                                                                                     Department = a.Curriculum.LookupDepartment.DepartmentName,
                                                                                                     InitialConsultationComplete = a.InitialConsultationComplete,
                                                                                                     InitialCurriculumEnquiryDocumentationSent = a.InitialCurriculumEnquiryDocumentationSent,
                                                                                                     LastUpdated = a.LastUpdated,
                                                                                                     QuanityToEnroll = a.EnrollmentQuanity,
                                                                                                     QuantityCurrentlyEnrolled = a.Enrollments.Where(b => b.EnrolmentParentID == 0).Count(),
                                                                                                     Status = a.LookupEnquiryStatus.EnquiryStatus
                                                                                                 }).ToList<CurrentEnquiries>());
        }
        private void populateCompnayFile()
        {

        }
        private void populateStuidentFile()
        {

        }
        #endregion
        #region Logical Methods

        #endregion
        #region Control Event Methods
        private void btnInProgressSwitchEnquiryContact_Click(object sender, EventArgs e)
        {
            if (enquiriesBindingSource.Count > 0 && CurrentlySelectedEquiryOrigion == enumEquiryOrigion.Company)
            {
                CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
                Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

                using (var Dbconnection = new MCDEntities())
                {

                    // return one instance each entity by primary key
                    //var EnquiryToRemove = Dbconnection.Enquiries.Include(a => a.Individuals).FirstOrDefault(p => p.EnquiryID == E.EnquiryID);

                    Dbconnection.Enquiries.Attach(E);
                    var IndividualsToRemove = E.Individuals.ToList<Individual>();
                    IndividualsToRemove.ForEach(c => E.Individuals.Remove(c));

                    //var IndividualToRemove = Dbconnection.Individuals.FirstOrDefault(s => s.IndividualID == E.Individuals..IndividualID);

                    // call Remove method from navigation property for any instance
                    // supplier.Product.Remove(product);
                    // also works
                    // EnquiryToRemove.Individuals.Remove(IndividualToRemove);

                    // call SaveChanges from context
                    Dbconnection.SaveChanges();
                    //Dbconnection.Individuals.Attach(CurrentlySelectedIndividual);




                };

                using (var Dbconnection = new MCDEntities())
                {

                    // 1
                    Data.Models.Enquiry Ee = new Data.Models.Enquiry { EnquiryID = E.EnquiryID };
                    // 2
                    Dbconnection.Enquiries.Add(Ee);
                    // 3
                    Dbconnection.Enquiries.Attach(Ee);

                    // 1
                    Individual I = new Individual { IndividualID = CurrentlySelectedIndividual.IndividualID };
                    // 2
                    Dbconnection.Individuals.Add(I);
                    // 3
                    Dbconnection.Individuals.Attach(I);

                    // like previous method add instance to navigation property
                    Ee.Individuals.Add(I);
                    Dbconnection.SaveChanges();
                    //Refresh Current enquiry Line Item

                    //enquiriesBindingSource.ResetCurrentItem();
                    //Dbconnection.Entry(E).Reference(a => a.Individuals.Select(b => b.ContactDetails.Select(c => c.LookupContactType))).Load();
                    //.Include(a => a.Enquiries.Select(b => b.Individuals.Select(c => c.ContactDetails.Select(d => d.LookupContactType))))

                };

                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.Enquiries.Attach(E);
                    Dbconnection.Entry(E).Collection(a => a.Individuals).Load();
                    foreach (Individual Ind in E.Individuals.ToList())
                    {
                        Dbconnection.Entry(Ind).Collection(a => a.ContactDetails).Load();
                        foreach (ContactDetail CD in Ind.ContactDetails)
                        {
                            Dbconnection.Entry(CD).Reference(a => a.LookupContactType).Load();
                        }
                    }
                };
                //refreshEnquiries();
                enquiriesBindingSource.ResetCurrentItem();
            }
            else
            {
                MetroMessageBox.Show(this, "Can not change the contact details(Studnet Is the Only Contact Person).", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void enquiriesBindingSource_PositionChanged(object sender, EventArgs e)
        {
            if (enquiriesBindingSource.Count > 0)
            {
                CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
                Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

                switch (CurrentlySelectedEquiryOrigion)
                {
                    case enumEquiryOrigion.Company:
                        if (E.Individuals.Count > 0)
                        {
                            contactDetailBindingSource.DataSource = E.Individuals.FirstOrDefault<Individual>().ContactDetails;
                            Individual individ = E.Individuals.FirstOrDefault<Individual>();

                            fullNameTextBox.Text = individ.FullName;

                        }
                        txtInprogressCompanyName.Text = CurrentlySelectedCompany.CompanyName;
                        break;

                    case enumEquiryOrigion.PrivateStudent:
                        //txtInprogressCompanyName
                        if (CurrentlySelectedIndividual.ContactDetails != null)
                        {
                            contactDetailBindingSource.DataSource = CurrentlySelectedIndividual.ContactDetails;
                        }
                        fullNameTextBox.Text = CurrentlySelectedIndividual.FullName;
                        txtInprogressCompanyName.Text = "NA - Private Student";
                        break;
                }


            }

        }
        private void dgvCurrentEnquiry_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    //var EnquiryObj = (Data.Models.Enquiry)(row.DataBoundItem);
                    //row.Cells[colEnquiryStatus.Index].Value = EnquiryObj.LookupEnquiryStatus.EnquiryStatus.ToString();
                    //row.Cells[colEnquiryCurriculum.Index].Value = EnquiryObj.Curriculum.CurriculumName.ToString();
                    //row.Cells[colEnquiryDepartment.Index].Value = EnquiryObj.Curriculum.LookupDepartment.DepartmentName.ToString();
                    //row.Cells[ColQtyEmrolled.Index].Value = EnquiryObj.Enrollments.Where(a => a.EnrolmentParentID == 0).Count();


                }
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
                    if (ContactDetailObj.ContactTypeID == (int)Common.Enum.EnumContactTypes.Email_Address)
                    {
                        row.Cells[colInProgressContactDetailSendOption.Index].Value = "[ Send Email ]";
                    }
                    if (ContactDetailObj.ContactTypeID == (int)Common.Enum.EnumContactTypes.Cell_Number)
                    {
                        row.Cells[colInProgressContactDetailSendOption.Index].Value = "[ Send SMS ]";
                    }
                }
            }
        }
        private void btnAddContactDetail_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                Individual CurrentContact = (Individual)((Data.Models.Enquiry)enquiriesBindingSource.Current).Individuals.FirstOrDefault<Individual>();

                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {

                        Dbconnection.Individuals.Attach(CurrentContact);

                        ContactDetail Con = new ContactDetail
                        {
                            ContactTypeID = frm.CurrentDetail.ContactTypeID,
                            ContactDetailValue = frm.CurrentDetail.ContactDetailValue
                        };

                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);

                        Dbconnection.Entry(frm.CurrentDetail).Reference(a => a.LookupContactType).Load();
                        CurrentContact.ContactDetails.Add(frm.CurrentDetail);

                        Dbconnection.SaveChanges();



                    };
                    // refreshInProgressEnquiry(CurrentSelectedEnquiryID);
                }
            }
        }

        private void btnRemoveContactDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnuUpdateContactDetails_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(((ContactDetail)contactDetailBindingSource.Current).ContactDetailID))
            {
                frm.CurrentDetail = (ContactDetail)contactDetailBindingSource.Current;
                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                        Dbconnection.Entry(frm.CurrentDetail).State = System.Data.Entity.EntityState.Modified;
                        Dbconnection.SaveChanges();
                    };

                    contactDetailBindingSource.ResetCurrentItem();
                }


                // refreshInProgressEnquiry(CurrentSelectedEnquiryID);

            }
        }
        private void dgvContactDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);


                    row.Cells[colContactType.Index].Value = ContactDetailObj.LookupContactType.ContactType.ToString();


                }
            }
        }
        private void btnAddEnrollment_Click(object sender, EventArgs e)
        {
            if (enquiriesBindingSource.Count > 0)
            {
                CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
                Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

                DialogResult Rtn = MetroMessageBox.Show(this, "Do you have a copy of the individuals ID document or relevant details, These details are rquired to process initial enrollment! Else Select No and send an email Notification to the contact requesting these details.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //DialogResult Rtn = MessageBox.Show("Do you have a copy of the individuals ID document or relevant details, These details are rquired to process initial enrollment! Else Select No and send an email Notification to the contact requesting these details.", "ID Document Requirement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Rtn == DialogResult.Yes)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        //Dbconnection.CurriculumEnquiries.Attach(CE);
                        //if (!(Dbconnection.Entry(CE).Collection(a => a.Enrollments).IsLoaded))
                        //{
                        //    Dbconnection.Entry(CE).Collection(a => a.Enrollments).Load();
                        //}
                    };
                    if (true)//E.EnrollmentQuanity > E.Enrollments.Where(a => a.EnrolmentParentID == 0).Count())
                    {
                        using (frmApprenticeshipEnrollmentFormV2 frm = new frmApprenticeshipEnrollmentFormV2(null, false))
                        {
                            //frm.CurrentCurriculumEnquiry = E;
                            frm.ShowDialog();
                            //E.Enrollments.Add(frm.CurrentEnrollment);
                            // curriculumEnquiryInprogressBindingSource.ResetCurrentItem();
                            refreshProgressFile();
                            //this.refreshInProgressEnquiry(E.EnquiryID);

                            if (frm.IsSuccessfullySaved)
                            {
                                DialogResult Rtn1 = MessageBox.Show("Do you wish to process this new enrollment now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (Rtn1 == DialogResult.Yes)
                                {
                                    using (frmEnrollmentProgress innerFrm = new frmEnrollmentProgress(frm.CurrentEnrollment.EnrollmentID))
                                    {
                                        innerFrm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                                        innerFrm.CurrentEquiryID = E.EnquiryID;
                                        innerFrm.CurrentSelectedDepartment = (Common.Enum.EnumDepartments)E.Curriculum.DepartmentID;
                                        innerFrm.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dgvInprogressContactDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;

            CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
            Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

            //Data.Models.Enquiry E = (Data.Models.Enquiry)enquiriesBindingSource.Current;
            if (e.ColumnIndex == 2)
            {
                var ContactDetailObj = (ContactDetail)(gridView.Rows[e.RowIndex].DataBoundItem);
                if (ContactDetailObj != null)
                {
                    if (ContactDetailObj.ContactTypeID == (int)Common.Enum.EnumContactTypes.Email_Address)
                    {
                        using (frmEmailMessageV2 frm = new frmEmailMessageV2())
                        {
                            frm.txtMessageSubject.Text = "MCD Communication - Follow On Enquiry " + E.EnquiryID + " - (" + E.Curriculum.CurriculumName + ")";
                            frm.AddToEmailContact(new List<Individual>() { CurrentlySelectedIndividual });
                            frm.txtMessageSubject.ReadOnly = true;
                            frm.ShowDialog();
                        }
                    }
                }
            }
        }
        private void btnAddNewEnquiry_Click(object sender, EventArgs e)
        {
            //ProgressFile Pf = (ProgressFile)progressFileBindingSource.Current;
            using (frmAddNewEnquiry frm = new frmAddNewEnquiry(CurrentlySelectedProgressFile.ProgressFileID))
            {
                if (CurrentlySelectedEquiryOrigion == enumEquiryOrigion.PrivateStudent)
                {
                    frm.nudQtyToEnroll.Enabled = false;
                }
                frm.ShowDialog();

                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            /*If there exists and enquiry(relating the the selected Curriculum) that belongs to the company and was generated by the current contact person then:
                                * 1. Get the old enquiry
                                * 2. update the status to InProgress
                                * {Set the following Properies:
                                *      Initial Cosultation Completed = true:
                                *      InitialDocumentation Sent: true:
                                *      Last updated = (TIme of enquiry)
                                * 3.add the enrollment quantity to the existing enroillment quantity.
                                * 4.Update the Existing Enquiry!
                                */
                            List<Data.Models.Enquiry> NewEnquiryToAdd = new List<Data.Models.Enquiry>();
                            foreach (Data.Models.Enquiry E in frm.EL)
                            {

                                Data.Models.Enquiry EnquiryCheck = (from a in Dbconnection.Enquiries
                                                                    from b in a.Individuals
                                                                    where
                                                                        a.CurriculumID == E.CurriculumID
                                                                        && b.IndividualID == this.CurrentlySelectedIndividual.IndividualID
                                                                        && a.ProgressFileID == this.CurrentlySelectedProgressFile.ProgressFileID
                                                                    select a)
                                                                    //.Include(a => a.Curriculum)
                                                                    .FirstOrDefault<Data.Models.Enquiry>();
                                if (EnquiryCheck != null)
                                {
                                    if (CurrentlySelectedEquiryOrigion == enumEquiryOrigion.Company)
                                    {
                                        //Update the current Enquiry
                                        EnquiryCheck.EnrollmentQuanity = EnquiryCheck.EnrollmentQuanity + E.EnrollmentQuanity;
                                        EnquiryCheck.LastUpdated = DateTime.Now;
                                        //EnquiryCheck.InitialConsultationComplete = true;
                                        // EnquiryCheck.InitialCurriculumEnquiryDocumentationSent = true;
                                        EnquiryCheck.EnquiryStatusID = (int)Common.Enum.EnumEnquiryStatuses.Enrollment_In_Progress;
                                        //Update the Enquiry.
                                        Dbconnection.Entry(EnquiryCheck).State = EntityState.Modified;
                                        Dbconnection.SaveChanges();
                                        MetroMessageBox.Show(this, "There currently exists an Enquiry for " + EnquiryCheck.Curriculum.CurriculumName + ",(Enquiry No. " + EnquiryCheck.EnquiryID + " -  it will be updated and the consultant will be notified.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    if (CurrentlySelectedEquiryOrigion == enumEquiryOrigion.PrivateStudent)
                                    {
                                        MetroMessageBox.Show(this, "There currently exists an Enquiry for " + EnquiryCheck.Curriculum.CurriculumName + ",(Enquiry No. " + EnquiryCheck.EnquiryID + " -  Generate a New Notification to follow up with the enquiry with ta consultant.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    /***************************************************************************************************************************
                                     * NB!!!!!!!
                                    /*TO-DO : Must Creat a ew notification that will be sent the person responsiable for handeling this form of curriulum enquiry.
                                     * ************************************************/
                                }
                                else
                                {
                                    //Adds the selected enquiry Based on a selected Curriculum into the list 
                                    //so that is can be saved as new as it currently no Enqiry exist a Enquiry Based 
                                    //on the Selected curriculum, Origanting from the contact person from this company.
                                    Dbconnection.Individuals.Attach(CurrentlySelectedIndividual);
                                    E.Individuals.Add(CurrentlySelectedIndividual);
                                    NewEnquiryToAdd.Add(E);
                                }

                            }
                            if (NewEnquiryToAdd.Count > 0)
                            {
                                Dbconnection.Enquiries.AddRange(NewEnquiryToAdd);
                                ////saves all above operations within one transaction
                                Dbconnection.SaveChanges();
                                MetroMessageBox.Show(this, "Enquiry Successfully Completed: A notification will the forwarded to the Client and the consultant will be notified.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }
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
                refreshProgressFile();
            };
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (enquiriesBindingSource.Count > 0)
            {
                CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
                Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

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
                                innerFrm.CurrentSelectedDepartment = (Common.Enum.EnumDepartments)E.Curriculum.DepartmentID;
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
        }


        #endregion
        #endregion

        #endregion


        #region Wizard Comopnents
        #region "Navigation Controls"
        private void navigateForward()
        {
            if (ValidateStep())
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
                        //this.mustSaveItems = true;
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
            if (CurrentPosition - 1 >= 0)
            {
                CurrentPosition--;
            }
            else
            {

                //CurrentPosition = 5;
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
                    //Student ID Documents
                    this.loadupStepTwo();
                    break;
                case 2:
                    //Student Address Details
                    this.loadupStepThree();
                    break;
                case 3:
                    //Student Contact Ddetails
                    this.loadupStepFour();
                    break;
                case 4:
                    //Student Disablity
                    this.loadupStepFive();
                    break;
                case 5:
                    //Company Selection
                    this.loadupStepSix();
                    break;
                case 6:
                    //Next Of Kin
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
        private Boolean ValidateStep()
        {

            Boolean bRtn = true;
            switch (CurrentPosition)
            {
                case 0:

                    //if (!radEquiryOrigionIsExistingConactYES.Checked && !radEquiryOrigionIsExistingConactNO.Checked)
                    //{
                    //    MetroMessageBox.Show(this, "Please verify if existing contact by seelecting Yes ot No!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    bRtn = false;
                    //}
                    break;
                case 1:
                    switch (CurrentlySelectedEquiryOrigion)
                    {
                        case enumEquiryOrigion.Company:
                            if (CurrentlySelectedCompany == null)
                            {
                                MetroMessageBox.Show(this, "Please Select a Company before proceeding!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            if (CurrentlySelectedIndividual == null)
                            {
                                MetroMessageBox.Show(this, "Please Select a valid Company Contact Person before proceeding!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                        case enumEquiryOrigion.PrivateStudent:
                            if (CurrentlySelectedIndividual == null)
                            {
                                MetroMessageBox.Show(this, "Please Select a valid Student Contact before proceeding!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            //Verify that the Current Studnet Not Linked To Another company
                            CompanyProgressFile CPF;
                            using (var Dbconnection = new MCDEntities())
                            {
                                CPF = (from a in Dbconnection.CompanyStudentProgressFiles
                                       where a.StudentProgressFile.StudentID == CurrentlySelectedIndividual.IndividualID
                                       select a.CompanyProgressFile)
                                                          .Include(a => a.Company)
                                                          .FirstOrDefault<CompanyProgressFile>();

                            };
                            if (CPF != null)
                            {
                                DialogResult Rtn = MetroMessageBox.Show(this, "Selected Student is currently Linked to the Following Company:" + CPF.Company.CompanyName + "\n Either Swith the Student to private by editing the Student Details OR\nWould like to switch to the Company Enquiries View to follow up the Student related Queries?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (Rtn == DialogResult.Yes)
                                {
                                    CurrentlySelectedEquiryOrigion = enumEquiryOrigion.Company;
                                    setCurrentTabOrigion();
                                    this.setCenterDisplayPanels();
                                    this.setNavigationControls();
                                    this.loadupStep();
                                    return false;
                                }
                                else
                                {
                                    //TO=DO Switch to General Enuiry View
                                }
                            }
                            break;
                        case enumEquiryOrigion.GeneralEnquiry:
                            if (CurrentlySelectedIndividual == null)
                            {
                                MetroMessageBox.Show(this, "Please Select a valid Contact before proceeding!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            else
                            {
                                CurrentPosition = 3;
                                this.setCenterDisplayPanels();
                                this.setNavigationControls();
                                this.loadupStep();
                            }
                            break;
                    }
                    break;
                case 2:

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
            this.setCorrectContactSelectionPanel();
            switch (CurrentlySelectedEquiryOrigion)
            {
                case enumEquiryOrigion.Company:
                    refreshCompanyName();
                    refreshCompanyContact();
                    refreshContactDetails();
                    break;
                case enumEquiryOrigion.GeneralEnquiry:


                    refreshContactDetails();
                    break;
                case enumEquiryOrigion.PrivateStudent:
                    refreshStudentContact();
                    refreshContactDetails();
                    break;
            }
        }

        private void loadupStepThree()
        {
            refreshProgressFile();
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
            //Summary
        }






        #endregion

        #endregion

        #endregion




        private void enquiriesBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (enquiriesBindingSource.Count > 0)
            {

                SetContactDetailsForEnquiry();

            }
        }

        private void SetContactDetailsForEnquiry()
        {
            CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
            Data.Models.Enquiry E = (Data.Models.Enquiry)CurrentlySelectedProgressFile.Enquiries.Where(a => a.EnquiryID == CE.EnquiryID).FirstOrDefault<Data.Models.Enquiry>();

            switch (CurrentlySelectedEquiryOrigion)
            {
                case enumEquiryOrigion.Company:
                    if (E.Individuals.Count > 0)
                    {
                        contactDetailBindingSource.DataSource = E.Individuals.FirstOrDefault<Individual>().ContactDetails.ToList<ContactDetail>();
                        Individual individ = E.Individuals.FirstOrDefault<Individual>();

                        fullNameTextBox.Text = individ.FullName;
                    }
                    txtInprogressCompanyName.Text = CurrentlySelectedCompany.CompanyName;
                    break;

                case enumEquiryOrigion.PrivateStudent:
                    //txtInprogressCompanyName
                    if (CurrentlySelectedIndividual.ContactDetails != null)
                    {
                        contactDetailBindingSource.DataSource = CurrentlySelectedIndividual.ContactDetails.ToList<ContactDetail>();
                    }
                    fullNameTextBox.Text = CurrentlySelectedIndividual.FullName;
                    txtInprogressCompanyName.Text = "NA - Private Student";
                    break;
            }
        }



        private void enquiriesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            SetContactDetailsForEnquiry();
        }

        private void btnViewEnquiryHistory_Click(object sender, EventArgs e)
        {
            if (enquiriesBindingSource.Count > 0)
            {
                CurrentEnquiries CE = (CurrentEnquiries)enquiriesBindingSource.Current;
                using (frmEquiryViewHistory frm = new frmEquiryViewHistory(CE.EnquiryID))
                {
                    frm.ShowDialog();
                }

            }
        }

        private void btnmGenerateEnquiryNotification_Click(object sender, EventArgs e)
        {

        }

        private void dgvCurrentEnquiry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }
    }
}
