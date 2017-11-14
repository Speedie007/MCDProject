﻿using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using Impendulo.Deployment.ContactDetails;
using Impendulo.Deployment.Email;
using Impendulo.Deployment.Enquiry;
using Impendulo.Deployment.Enrollment;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Deployment.Enquiry
{
    public partial class frmClientEnquiry : MetroFramework.Forms.MetroForm
    {

        //Form Properties
        public Employee CurrentEmployeeLoggedIn { get; set; }

        private int _CurrentSelectedEnquiryID = 0;
        private int CurrentSelectedEnquiryID
        {
            get { return _CurrentSelectedEnquiryID; }

        }
        public frmClientEnquiry(int CurrentSelectedEnquiryID)
        {
            InitializeComponent();
            //Remove before deploying
            _CurrentSelectedEnquiryID = CurrentSelectedEnquiryID;
        }

        private void frmClientEnquiryV3_Load(object sender, EventArgs e)
        {
            if (CurrentEmployeeLoggedIn == null)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    CurrentEmployeeLoggedIn = (from a in Dbconnection.Employees
                                               select a)
                                               .Include("LookupDepartments")
                                               .FirstOrDefault<Employee>();
                };
            }
            //Loads up the Currently Selected Equiry - Is Blank.
            this.refreshInProgressEnquiry(_CurrentSelectedEnquiryID);
            this.disableFormControlsIfNoEnquiryLoaded();
        }

        #region Inprogress Enquiry

        #region Refresh Methods
        private void refreshInProgressEnquiry(int EnquiryID)
        {
            this.populateInprogressEnquiry(EnquiryID);
        }
        #endregion

        #region Populate Methods

        private void populateInprogressEnquiry(int _EnquiryID)
        {
            using (var Dbconnection = new MCDEntities())
            {
                Data.Models.Enquiry x = (from a in Dbconnection.Enquiries
                                         where a.EnquiryID == _EnquiryID
                                         && a.EnquiryStatusID != (int)EnumEnquiryStatuses.Enquiry_Closed
                                         select a)
                                         .Include(a => a.Individuals)
                                         .Include(a => a.Individuals.Select(b => b.ContactDetails))
                                         .Include(a => a.Individuals.Select(b => b.ContactDetails.Select(c => c.LookupContactType)))
                                         //.Include(a => a.Enrollments)
                                         .Include(a => a.Curriculum)
                                         .Include(a => a.Companies)
                                          .FirstOrDefault<Data.Models.Enquiry>();



                if (x != null)
                {
                    ////filter out closed enquiries.
                    //List<Data.Models.Enquiry> DataSourceList;

                    ////Filters the Curriculum Enquiry for the current Loggin Employee (Only The One THat they May View).
                    //DataSourceList = new List<Data.Models.Enquiry>();
                    
                    //    if (DetermineIfPartOfDepartment(x.Curriculum.DepartmentID))
                    //    {
                    //        //if (CE.EnquiryStatusID != (int)EnumEnquiryStatuses.Enquiry_Closed)
                    //        //{
                    //        DataSourceList.Add(x);
                    //        //}
                    //    }
                    
                    //x.CurriculumEnquiries.Clear();
                    //foreach (CurriculumEnquiry CE in DataSourceList)
                    //{
                    //    x.CurriculumEnquiries.Add(CE);
                    //}
                    _CurrentSelectedEnquiryID = x.EnquiryID;
                    //curriculumEnquiryInprogressBindingSource
                    enquiryInprogressBindingSource.DataSource = x;
                }
                else
                {
                    enquiryInprogressBindingSource.Clear();
                    contactDetailsInprogressBindingSource.Clear();
                    curriculumEnquiryInprogressBindingSource.Clear();
                }

            };
        }
        #endregion
        #region Control Event Methods
        private void btnAddContactDetail_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                Individual CurrentContact = (Individual)((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Individuals.FirstOrDefault<Individual>();

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
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(((ContactDetail)contactDetailsInprogressBindingSource.Current).ContactDetailID))
            {
                frm.CurrentDetail = (ContactDetail)contactDetailsInprogressBindingSource.Current;
                frm.ShowDialog();
                if (frm.CurrentDetail != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.ContactDetails.Attach(frm.CurrentDetail);
                        Dbconnection.Entry(frm.CurrentDetail).State = System.Data.Entity.EntityState.Modified;
                        Dbconnection.SaveChanges();
                    };

                    contactDetailsInprogressBindingSource.ResetCurrentItem();
                }


                // refreshInProgressEnquiry(CurrentSelectedEnquiryID);

            }
        }
        private void enquiryInprogressBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            Data.Models.Enquiry EnquiryObj = (Data.Models.Enquiry)(enquiryInprogressBindingSource.Current);
            if (EnquiryObj != null)
            {

                if (EnquiryObj.Companies.Count == 0)
                {
                    txtInprogressCompanyName.Text = "NA - Private Enquiry";
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


        private void btnSearchForEnquiry_Click(object sender, EventArgs e)
        {
            using (frmSearchForSelectedEquiry frm = new frmSearchForSelectedEquiry())
            {
                frm.ShowDialog();
                this.refreshInProgressEnquiry(frm.SelectedEnquiryID);
                if (enquiryInprogressBindingSource.Count > 0)
                {
                    this.enableFormControlsIfNoEnquiryLoaded();
                }
                else
                {
                    this.disableFormControlsIfNoEnquiryLoaded();
                }
            }
        }

        private void dgvInprogressContactDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;

            if (e.ColumnIndex == 2)
            {
                var ContactDetailObj = (ContactDetail)(gridView.Rows[e.RowIndex].DataBoundItem);
                if (ContactDetailObj.ContactTypeID == (int)Common.Enum.EnumContactTypes.Email_Address)
                {
                    using (frmEmailMessageV2 frm = new frmEmailMessageV2())
                    {
                        frm.txtMessageSubject.Text = "MCD Communication - Follow On Enquiry - " + txtEnquiryInProgressEnquiryID.Text;
                        frm.AddToEmailContact(new List<Individual>() { (Individual)individualsInprogressBindingSource.Current });
                        frm.txtMessageSubject.ReadOnly = true;
                        frm.ShowDialog();


                    }
                }
            }
        }
        private void btnInitialConsultationConfirmation_Click(object sender, EventArgs e)
        {
            if (!(((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).InitialConsultationComplete))
            {
                using (frmEnquiryInitialConsultation frm = new frmEnquiryInitialConsultation((Data.Models.Enquiry)enquiryInprogressBindingSource.Current))
                {
                    frm.EmployeeID = CurrentEmployeeLoggedIn.EmployeeID;
                  
                    frm.ShowDialog();
                    this.enquiryInprogressBindingSource.ResetCurrentItem();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Initial Consultaion is completed, See History for details.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void enquiryDateTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox x = (TextBox)sender;
            if (x.Text.Length > 0) { x.Text = Convert.ToDateTime(x.Text).ToString("D"); }

        }
        #endregion

        #region Form Logic Control Flow Methods
        private Boolean DetermineIfPartOfDepartment(int DepartmentIdToCheck)
        {
            Boolean IsPart = false;
            foreach (LookupDepartment EmployDep in CurrentEmployeeLoggedIn.LookupDepartments)
            {
                if (EmployDep.DepartmentID == DepartmentIdToCheck)
                {
                    IsPart = true;
                }
            }

            return IsPart;

        }
        private void disableFormControlsIfNoEnquiryLoaded()
        {
            btnViewProgressInProgressSections.Visible = false;
            btnInitialConsultationConfirmationInProgressSection.Visible = false;
            btnCloseInprogressEnquiry.Visible = false;
            gbInprogressContactNameAndCompanyName.Enabled = false;
            gbInProgressContactDetails.Enabled = false;
            gbInProgressEnquiryEnrrolmentQueries.Enabled = false;
        }
        private void enableFormControlsIfNoEnquiryLoaded()
        {
            btnViewProgressInProgressSections.Visible = true;
            btnInitialConsultationConfirmationInProgressSection.Visible = true;
            btnCloseInprogressEnquiry.Visible = true;
            gbInprogressContactNameAndCompanyName.Enabled = true;
            gbInProgressContactDetails.Enabled = true;
            //if (((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).InitialConsultationComplete)
            //{
            gbInProgressEnquiryEnrrolmentQueries.Enabled = true;
            //}
            //else
            //{
            //    gbInProgressEnquiryEnrrolmentQueries.Enabled = false;
            //}
        }







        #endregion

        #endregion

        private void btnViewProgressInProgressSections_Click(object sender, EventArgs e)
        {
            using (frmEquiryViewHistory frm = new frmEquiryViewHistory(Convert.ToInt32(txtEnquiryInProgressEnquiryID.Text)))
            {
                frm.ShowDialog();
            }
        }

        private void btnInProgressSwitchBetweenCompanyANdPrivate_Click(object sender, EventArgs e)
        {
            if (((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Companies.Count == 0)
            {
                using (frmSelectCompanyContact frm = new frmSelectCompanyContact())
                {
                    Data.Models.Enquiry CurrentEnquiry = (Data.Models.Enquiry)enquiryInprogressBindingSource.Current;
                    Individual CurrentContact = CurrentEnquiry.Individuals.FirstOrDefault<Individual>();
                    //frm.CurrentCompany = CurrentEnquiry.Companies.FirstOrDefault<Data.Models.Company>();
                    //frm.SelectedIndividual = CurrentEnquiry.Individuals.FirstOrDefault<Individual>();
                    frm.ShowDialog();


                    if (frm.CurrentCompany != null)
                    {
                        using (var Dbconnection = new MCDEntities())
                        {
                            Dbconnection.Enquiries.Attach(CurrentEnquiry);
                            //REmova Current Contact from tthe Enquiry
                            Dbconnection.Enquiries.Attach(CurrentEnquiry);

                            CurrentEnquiry.Individuals.Clear();
                            CurrentEnquiry.Companies.Clear();
                            Dbconnection.SaveChanges();
                        }
                        //Link Company That Is responible for the Enquiry
                        using (var Dbconnection = new MCDEntities())
                        {
                            Dbconnection.Enquiries.Attach(CurrentEnquiry);
                            Data.Models.Company CompanyToLink = new Data.Models.Company
                            {
                                CompanyID = frm.CurrentCompany.CompanyID
                            };
                            Dbconnection.Companies.Attach(CompanyToLink);
                            CurrentEnquiry.Companies.Add(CompanyToLink);
                            Dbconnection.SaveChanges();
                        };

                        using (var Dbconnection = new MCDEntities())
                        {
                            Dbconnection.Enquiries.Attach(CurrentEnquiry);
                            Individual NewContact = new Individual
                            {
                                IndividualID = frm.SelectedIndividual.IndividualID
                            };
                            Dbconnection.Individuals.Attach(NewContact);
                            CurrentEnquiry.Individuals.Add(NewContact);
                            Dbconnection.SaveChanges();
                        };

                        //Link the Copmpany Contact
                        // Dbconnection.Individuals.Add(frm.SelectedIndividual);


                        //Remove the current Contact

                        //Dbconnection.Entry(CompanyToLink).Reload();
                        //CurrentEnquiry.Companies.Add(frm.CurrentCompany);



                    }

                }
                refreshInProgressEnquiry(CurrentSelectedEnquiryID);
            }
            else
            {
                //Selects From List Of All Students from the Custom Student Search Form
                using (frmSelectIndividualContact frm = new frmSelectIndividualContact())
                {
                    frm.ShowDialog();
                    //get the Individual Selected Which represents the Student Selected.

                    if (frm.SelectedIndividual.IndividualID != 0)
                    {

                        using (var Dbconnection = new MCDEntities())
                        {
                            /*1. Removes the current Slected Company Contact*/
                            Data.Models.Enquiry CurrentEnquiry = ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current);

                            Dbconnection.Enquiries.Attach(CurrentEnquiry);

                            List<Individual> temp = ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Individuals.ToList<Individual>();
                            List<Data.Models.Company> tempCompanies = ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Companies.ToList<Data.Models.Company>();
                            foreach (Individual individ in temp)
                            {
                                ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Individuals.Remove(individ);
                            }
                            foreach (Data.Models.Company comp in tempCompanies)
                            {
                                ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Companies.Remove(comp);
                            }
                            // Dbconnection.Entry(CurrentEnquiry).State = EntityState.Modified;
                            Dbconnection.SaveChanges();
                            /*1. End Removal of Current Enquiry Contact*/


                            /*2. Adds The Cuurentlty Selected Student Contact to be linked with the Enquiry for future contacting*/
                            Individual IndividualToLink = (from a in Dbconnection.Individuals
                                                           where a.IndividualID == frm.SelectedIndividual.IndividualID
                                                           select a).FirstOrDefault<Individual>();
                            CurrentEnquiry.Individuals.Add(IndividualToLink);
                            Dbconnection.SaveChanges();
                            /*2. End Adding the Student Contact Details To the Enquiry.*/
                        };
                    }
                    this.refreshInProgressEnquiry(CurrentSelectedEnquiryID);


                }
            }
        }

        private void dgvInProgressCurriculumEnquiries_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    //var CurriculumEnquiryObj = (CurriculumEnquiry)(row.DataBoundItem);
                    if (true)//CurriculumEnquiryObj.EnquiryStatusID == (int)EnumEnquiryStatuses.Enquiry_Closed)
                    {
                        row.Cells[colInProgressEnquiryCloseCurriculumEnquiry.Index].Value = "[ Reinstate ]";
                    }
                    else
                    {
                        row.Cells[colInProgressEnquiryCloseCurriculumEnquiry.Index].Value = "[ Close ]";
                    }
                    //row.Cells[colInProgressCurriculumDepartment.Index].Value = CurriculumEnquiryObj.Curriculum.LookupDepartment.DepartmentName.ToString();
                    //row.Cells[colInProgressCurriculumName.Index].Value = CurriculumEnquiryObj.Curriculum.CurriculumName.ToString();
                    ////row.Cells[colInProgressCurriculumEnquiryStatus.Index].Value = CurriculumEnquiryObj.LookupEnquiryStatus.EnquiryStatus.ToString();
                    //row.Cells[colInProgressEnquiryQuantityCurrentlyEnrolled.Index].Value = CurriculumEnquiryObj.Enrollments.Where(a => a.EnrolmentParentID == 0).Count();
                    if (true)//CurriculumEnquiryObj.EnrollmentQuanity == CurriculumEnquiryObj.Enrollments.Count)
                    {
                        row.Cells[colInProgressProcessEnrollment.Index].Value = "";
                    }
                    else
                    {
                        row.Cells[colInProgressProcessEnrollment.Index].Value = "[ Add Enrollment ]";
                    }


                    //
                    if (true)//CurriculumEnquiryObj.Enrollments.Count > 0)
                    {
                        row.Cells[colInProgressViewCurrentEnrollment.Index].Value = "[ Edit Enrollments ]";
                    }
                    else
                    {
                        row.Cells[colInProgressViewCurrentEnrollment.Index].Value = "";
                    }
                }
            }
        }

        private void dgvInProgressCurriculumEnquiries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Data.Models.Enquiry CurrentEnquiry = (Data.Models.Enquiry)enquiryInprogressBindingSource.Current;
            //CurriculumEnquiry CE = (CurriculumEnquiry)dgvInProgressCurriculumEnquiries.Rows[e.RowIndex].DataBoundItem;
            switch (e.ColumnIndex)
            {
                case 1:
                    if (true)//CE.EnquiryStatusID != (int)EnumEnquiryStatuses.Enquiry_Closed)
                    {
                        DialogResult Rtn = MessageBox.Show("Are you sure that you wish to Close this Enquiry Item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (Rtn == DialogResult.Yes)
                        {
                            using (var Dbconnection = new MCDEntities())
                            {
                                //Dbconnection.CurriculumEnquiries.Attach(CE);
                                //CE.EnquiryStatusID = (int)EnumEnquiryStatuses.Enquiry_Closed;
                                //CE.LastUpdated = DateTime.Now;
                                //Dbconnection.Entry<CurriculumEnquiry>(CE).State = System.Data.Entity.EntityState.Modified;
                                Dbconnection.SaveChanges();
                                EquiryHistory hist = new EquiryHistory
                                {
                                    EnquiryID = CurrentEnquiry.EnquiryID,
                                    EmployeeID = this.CurrentEmployeeLoggedIn.EmployeeID,
                                    LookupEquiyHistoryTypeID = (int)EnumEquiryHistoryTypes.Curriculum_Enquiry_Item_Closed,
                                    DateEnquiryUpdated = DateTime.Now,
                                    //EnquiryNotes = "Curriculum Enquiry Line Item Closed, Item Removed - " + CE.Curriculum.CurriculumName + "- For Enquiry Ref: " + CurrentEnquiry.EnquiryID
                                };

                                Dbconnection.EquiryHistories.Add(hist);
                                int IsSaved = Dbconnection.SaveChanges();

                                refreshInProgressEnquiry(CurrentSelectedEnquiryID);
                            };
                        }
                    }
                    else
                    {
                        DialogResult Rtn = MessageBox.Show("Are you sure that you wish to Re-Instate this Enquiry Item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (Rtn == DialogResult.Yes)
                        {
                            using (var Dbconnection = new MCDEntities())
                            {
                                //Dbconnection.CurriculumEnquiries.Attach(CE);
                                //CE.EnquiryStatusID = (int)EnumEnquiryStatuses.Enrollment_In_Progress;
                                //CE.LastUpdated = DateTime.Now;
                                //Dbconnection.Entry<CurriculumEnquiry>(CE).State = System.Data.Entity.EntityState.Modified;
                                Dbconnection.SaveChanges();
                                EquiryHistory hist = new EquiryHistory
                                {
                                    EnquiryID = CurrentEnquiry.EnquiryID,
                                    EmployeeID = this.CurrentEmployeeLoggedIn.EmployeeID,
                                    LookupEquiyHistoryTypeID = (int)EnumEquiryHistoryTypes.Curriculum_Enquiry_Item_Reinstated,
                                    DateEnquiryUpdated = DateTime.Now,
                                    //EnquiryNotes = "Curriculum Enquiry Line Item Reinstated, Item Reinstated - " + CE.Curriculum.CurriculumName + "- For Enquiry Ref: " + CurrentEnquiry.EnquiryID
                                };

                                Dbconnection.EquiryHistories.Add(hist);
                                int IsSaved = Dbconnection.SaveChanges();

                                refreshInProgressEnquiry(CurrentSelectedEnquiryID);
                            };
                        }
                    }


                    break;
                case 6:
                    //if (CE.Curriculum.DepartmentID == (int)EnumDepartments.Apprenticeship)
                    //{
                    if (((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).InitialConsultationComplete)
                    {
                        //gbInProgressEnquiryEnrrolmentQueries.Enabled = true;
                        DialogResult Rtn = MessageBox.Show("Do you have a copy of the individuals ID document or relevant details, These details are rquired to process initial enrollment! Else Select No and send an email Notification to the contact requesting these details.", "ID Document Requirement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                            if (true)//CE.EnrollmentQuanity > CE.Enrollments.Where(a => a.EnrolmentParentID == 0).Count())
                            {
                                using (frmApprenticeshipEnrollmentFormV2 frm = new frmApprenticeshipEnrollmentFormV2(null,false))
                                {
                                    //frm.CurrentCurriculumEnquiry = CE;
                                    frm.ShowDialog();
                                    //CE.Enrollments.Add(frm.CurrentEnrollment);
                                    curriculumEnquiryInprogressBindingSource.ResetCurrentItem();
                                    //this.refreshInProgressEnquiry(CurrentSelectedEnquiryID);

                                    if (frm.IsSuccessfullySaved)
                                    {
                                        DialogResult Rtn1 = MessageBox.Show("Do you wish to process this new enrollment now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                        if (Rtn1 == DialogResult.Yes)
                                        {
                                            using (frmEnrollmentProgress innerFrm = new frmEnrollmentProgress(frm.CurrentEnrollment.EnrollmentID))
                                            {
                                                innerFrm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                                                innerFrm.CurrentEquiryID = this.CurrentSelectedEnquiryID;
                                                //innerFrm.CurrentSelectedDepartment = (Common.Enum.EnumDepartments)CE.Curriculum.DepartmentID;
                                                innerFrm.ShowDialog();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Initial Consultation Is Not Yet Completed, Please complete before proceeding with the enrollment!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //gbInProgressEnquiryEnrrolmentQueries.Enabled = false;
                    }

                    //}
                    break;
                case 7:
                    //ensure that the Enrollments are refershed
                    using (var Dbconnection = new MCDEntities())
                    {
                        //Dbconnection.CurriculumEnquiries.Attach(CE);
                        //if (!(Dbconnection.Entry(CE).Collection(a => a.Enrollments).IsLoaded))
                        //{
                        //    Dbconnection.Entry(CE).Collection(a => a.Enrollments).Load();
                        //}
                    };
                    //IF any enrollments exists then open Selection list else Do Nothing.
                    if (true)//CE.Enrollments.Count > 0)
                    {
                        //Open thje list of linked Enrollments that are in progress
                        using (frmEnrollmentSelectionForEquiry frm = new frmEnrollmentSelectionForEquiry(new Data.Models.Enquiry()))
                        {
                            frm.ShowDialog();
                            if (frm.SelectedEnrollmentID != 0)
                            {
                                using (frmEnrollmentProgress innerFrm = new frmEnrollmentProgress(frm.SelectedEnrollmentID))
                                {
                                    innerFrm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                                    innerFrm.CurrentEquiryID = this.CurrentSelectedEnquiryID;
                                    //innerFrm.CurrentSelectedDepartment = (Common.Enum.EnumDepartments)CE.Curriculum.DepartmentID;
                                    innerFrm.ShowDialog();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("There are currently no enrollments for this enquiry.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void btnInProgressChangeContact_Click(object sender, EventArgs e)
        {
            if (((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Companies.Count > 0)
            {
                using (frmSelectCompanyContact frm = new frmSelectCompanyContact())
                {
                    Data.Models.Enquiry _CurentSelectedEnquiry = (Data.Models.Enquiry)enquiryInprogressBindingSource.Current;
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Enquiries.Attach(_CurentSelectedEnquiry);
                        Dbconnection.Entry(_CurentSelectedEnquiry).Collection(a => a.Companies).Load();

                        frm.CurrentCompany = ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Companies.FirstOrDefault<Data.Models.Company>();
                        frm.ShowDialog();
                        if (frm.SelectedIndividual != null)
                        {


                            Individual CurrentEnquiryContact = ((Data.Models.Enquiry)enquiryInprogressBindingSource.Current).Individuals.FirstOrDefault<Individual>();

                            //REmove C=urrent Contact
                            Dbconnection.Enquiries.Attach(_CurentSelectedEnquiry);

                            _CurentSelectedEnquiry.Individuals.Remove(CurrentEnquiryContact);

                            Dbconnection.SaveChanges();

                            //Link Selected Contact

                            Dbconnection.Individuals.Attach(frm.SelectedIndividual);

                            _CurentSelectedEnquiry.Individuals.Add(frm.SelectedIndividual);

                            Dbconnection.SaveChanges();



                        };
                        //REfresh Enquiry
                        this.refreshInProgressEnquiry(CurrentSelectedEnquiryID);

                    }

                }
            }
            else
            {
                MetroMessageBox.Show(this, "Can Only Change Company Contacts, Not Private Contacts", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCloseInprogressEnquiry_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You sure?\nThis will close this enquiry and all the associated Line Items.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Rtn == DialogResult.Yes)
            {

                using (frmCloseEnquiryConfirmation frm = new frmCloseEnquiryConfirmation())
                {
                    frm.CurrentSelectedEnquiry = (Data.Models.Enquiry)enquiryInprogressBindingSource.Current;
                    frm.CurrentEmployeeLoggedIn = this.CurrentEmployeeLoggedIn;
                    frm.ShowDialog();
                    if (frm.IsSuccessfullyClosedOff)
                    {
                        this.refreshInProgressEnquiry(0);
                    }

                }
            }
        }

        private void btrnInProgressSendInitialDocumentation_Click(object sender, EventArgs e)
        {
            using (frmEmailMessageV2 frm = new frmEmailMessageV2())
            {
                frm.txtMessageSubject.Text = "MCD Communication - Follow On Enquiry - " + txtEnquiryInProgressEnquiryID.Text;
                frm.AddToEmailContact(new List<Individual>() { (Individual)individualsInprogressBindingSource.Current });
                frm.txtMessageSubject.ReadOnly = true;

                using (var Dbconnection = new MCDEntities())
                {

                    List<MessageTemplate> MT = (from a in Dbconnection.MessageTemplates
                                                from b in a.Files
                                                where a.ProcessStepID == (int)EnumProcessSteps.Enquiry__Apprenticeship__Step_1__Documentation_To_Send
                                                select a).ToList<MessageTemplate>();

                    MessageTemplate CurrentMessageTemplate = MT.FirstOrDefault<MessageTemplate>();
                    string Mess = "Good Day " + fullNameTextBox.Text + "\n \n";
                    frm.txtMessageBody.Text = "Please Reference the Following Line Equiry Number when returning any documentation: \n" +
                                                "Enquiry No " + txtEnquiryInProgressEnquiryID.Text + "\n\n" + Mess + CurrentMessageTemplate.Message;
                    foreach (MessageTemplate MTObj in MT)
                    {
                        foreach (Data.Models.File FileObj in MTObj.Files)
                        {
                            frm.addDatabaseAttachment(FileObj.FileID);
                        }
                    }
                    //frm.refreshAttachments();

                };
                frm.ShowDialog();


            }
            //  frm1.txtTestSubject.Text = "Enquiry No: ( " + CurrentEnquiryObj.EnquiryID + "-" + CE.CurriculumEnquiryID + " ) Enquiry Feed Back";


        }

        private void btnUpdateCurriculumEnquiryItemEnrollmentQty_Click(object sender, EventArgs e)
        {
            using (frmUpdateSelectedCurriculumEnrollQty frm = new frmUpdateSelectedCurriculumEnrollQty())
            {
                //CurriculumEnquiry CE = (CurriculumEnquiry)curriculumEnquiryInprogressBindingSource.Current;

                using (var Dbconnection = new MCDEntities())
                {
                    Data.Models.Enquiry EnquiryObj = (Data.Models.Enquiry)enquiryInprogressBindingSource.Current;
                    Dbconnection.Enquiries.Attach(EnquiryObj);

                    //Dbconnection.Entry(CE).Collection(a => a.Enrollments).Load();

                };
                //int EnrollomentQty = CE.Enrollments.Where(a => a.EnrolmentParentID == 0).Count() + 1;
                //frm.nudQtyToEnroll.Minimum = EnrollomentQty;
                //frm.nudQtyToEnroll.Value = EnrollomentQty;
                //frm.CurrentCurriculumEnquiry = CE;
                frm.ShowDialog();
                //any change to the Qty will be saved.
                using (var Dbconnection = new MCDEntities())
                {
                    //Dbconnection.CurriculumEnquiries.Attach(CE);
                    //Dbconnection.Entry(CE).State = EntityState.Modified;
                    Dbconnection.SaveChanges();
                };
                this.curriculumEnquiryInprogressBindingSource.ResetCurrentItem();
                //this.refreshInProgressEnquiry(CurrentSelectedEnquiryID);
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
