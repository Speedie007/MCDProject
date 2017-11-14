using Impendulo.Common.FileHandeling;
using Impendulo.Data;
using Impendulo.Data.Models;
using Impendulo.Data.Models.Enum;
using Impendulo.Development.Addresses;
using Impendulo.Development.Company;
using Impendulo.Development.Company.CompanyDetails;
using Impendulo.Development.ContactDetails;
using Impendulo.Development.Contacts;
using Impendulo.Development.Disablity;
using Impendulo.Development.NextOfKin;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Students
{
    public partial class frmStudentAddUpdate : MetroForm
    {

        public Student CurrentSelectedStudent { get; set; }
        public int CurrentStudentID { get; set; }
        public int CurrentPosition { get; set; }
        public Boolean IsSuccessfullySaved { get; set; }
        public Address address { get; set; }
        private List<Data.Models.File> StudentPictureToUploaded { get; set; }

        private Boolean IsClosingPrematurly { get; set; }

        public Employee CurrentEmployeeLoggedIn
        {
            get;
            set;
        }

        public frmStudentAddUpdate(int CurrentStudentID)
        {
            this.CurrentStudentID = CurrentStudentID;
            InitializeComponent();
            //CurrentStudentID = 15200;// 15189;// 15188;// 15187;// 15173;
            StudentPictureToUploaded = new List<Data.Models.File>();
            IsSuccessfullySaved = false;
            IsClosingPrematurly = true;

        }

        private void frmStudentAddUpdate_Load(object sender, EventArgs e)
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


            //Initialises the Student If None is provided before Loading the form.

            //PreLoads All Drop Down Controls
            this.refreshAllDropDownControls();
            //Loads the Wizard
            this.CurrentPosition = 0;
            this.setCenterDisplayPanels();
            this.setNavigationControls();
            this.loadupStep();

        }

        #region Refresh 

        private void refreshAllDropDownControls()
        {
            this.populateTitles();
            this.populateEthnicity();
            this.populateGenders();
            this.populateMaritalStatuis();
            this.populateHighestQualifications();
            this.populateAddresTypes();
            this.populateProvinces();
            this.populateCountries();
        }
        private void refreshStudentIDDocumnets()
        {
            populateStudentIDDocuments();
        }

        private void refreshContactDetails()
        {
            populateContactDetails();
        }
        private void refreshStudentAddressDetails()
        {
            populateStudentAddressDetails();
        }
        private void refreshStudentDisablities()
        {
            this.populateStudentDisablities();
        }
        private void refreshStudentCompany()
        {
            this.populateStudentCompany();
        }
        private void refreshNextOfKin()
        {
            this.populateNextOfKin();
        }

        #endregion
        #region Populate Methods
        private void populateContactDetails()
        {
            contactDetailsBindingSource.DataSource = (from a in CurrentSelectedStudent.Individual.ContactDetails
                                                      select a).ToList<ContactDetail>();
        }
        private void populateStudentAddressDetails()
        {
            addressesBindingSource.DataSource = (from a in CurrentSelectedStudent.Individual.Addresses
                                                 select a).ToList<Address>();
        }
        private void populateStudentDisablities()
        {
            studentDisabilitiesBindingSource.DataSource = (from a in CurrentSelectedStudent.StudentDisabilities
                                                           select a).ToList<StudentDisability>();
            studentDisabilitiesBindingSource.ResetBindings(true);
            if (studentDisabilitiesBindingSource.Count > 0)
            {
                btnRemoveStudentDisablity.Enabled = true;
                btnUpdateStudentDisablitiy.Enabled = true;
            }
            else
            {
                btnRemoveStudentDisablity.Enabled = false;
                btnUpdateStudentDisablitiy.Enabled = false;
            }
        }
        private void populateStudentCompany()
        {

            using (var Dbconnection = new MCDEntities())
            {
                this.studentAssociatedCompaniesBindingSource.DataSource =
                    (from a in CurrentSelectedStudent.StudentAssociatedCompanies
                     where a.StudentID == this.CurrentSelectedStudent.StudentID && a.IsCurrentCompany == true
                     select a).ToList<StudentAssociatedCompany>();

                if (studentAssociatedCompaniesBindingSource.Count > 0)
                {
                    //Link to company
                    Dbconnection.Students.Attach(CurrentSelectedStudent);
                    Dbconnection.Entry(CurrentSelectedStudent).Collection(a => a.StudentAssociatedCompanies).Load();

                    gbSelectCompanyDetails.Visible = true;
                    // gbSelectCompanyDetails.Visible = false;
                    picSelectCompany.BackColor = Color.Gainsboro;
                    picSelectPrivate.BackColor = Color.Transparent;

                    lblCurentlySelectedSiteType.Text = "Company";
                }
                else
                {
                    //Private
                    gbSelectCompanyDetails.Visible = false;
                    // gbSelectCompanyDetails.Visible = false;
                    picSelectCompany.BackColor = Color.Transparent;
                    picSelectPrivate.BackColor = Color.Gainsboro;

                    lblCurentlySelectedSiteType.Text = "Private";
                }

            };
        }
        private void populateNextOfKin()
        {
            nextOfKinsBindingSource.DataSource = (from a in CurrentSelectedStudent.NextOfKins
                                                  from b in a.Students
                                                  where b.StudentID == this.CurrentSelectedStudent.StudentID
                                                  select a).ToList<Data.Models.NextOfKin>();
            if (nextOfKinsBindingSource.Count > 0)
            {
                List<ContactDetail> CD = ((Data.Models.NextOfKin)nextOfKinsBindingSource.Current).Individual.ContactDetails.ToList<ContactDetail>();

                contactDetailsBindingSource.DataSource = CD;
            }

        }
        #endregion

        #region  PreLoad All Wizard DropDown Controls
        #region  Populate Student Form Drop Down Controls

        private void populateAddresTypes()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupAddressTypeBindingSource.DataSource = (from a in Dbconnection.LookupAddressTypes
                                                             select a).ToList<LookupAddressType>();
            };
        }
        private void populateProvinces()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupProvinceBindingSource.DataSource = (from a in Dbconnection.LookupProvinces
                                                          orderby a.Province
                                                          select a).ToList<LookupProvince>();
            };
        }
        private void populateCountries()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupCountryBindingSource.DataSource = (from a in Dbconnection.LookupCountries
                                                         select a).ToList<LookupCountry>();
            };
        }
        /// <summary>
        /// PreLoads the All Title Drop Down Controls.
        /// </summary>
        private void populateTitles()
        {

            using (var Dbconnection = new MCDEntities())
            {
                this.lookupTitleBindingSource.DataSource = (from a in Dbconnection.LookupTitles

                                                            select a).ToList<LookupTitle>();
            };
        }
        private void populateEthnicity()
        {

            using (var Dbconnection = new MCDEntities())
            {
                this.lookupEthnicityBindingSource.DataSource = (from a in Dbconnection.LookupEthnicities
                                                                orderby a.Ethnicity
                                                                select a).ToList<LookupEthnicity>();
            };
        }
        private void populateGenders()
        {

            using (var Dbconnection = new MCDEntities())
            {
                this.lookupGenderBindingSource.DataSource = (from a in Dbconnection.LookupGenders
                                                             orderby a.Gender
                                                             select a).ToList<LookupGender>();
            };
        }
        private void populateMaritalStatuis()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupMartialStatusBindingSource.DataSource = (from a in Dbconnection.LookupMartialStatuses
                                                               orderby a.MaritialStatus
                                                               select a).ToList<LookupMartialStatus>();
            };
        }
        private void populateHighestQualifications()
        {

            using (var Dbconnection = new MCDEntities())
            {
                lookupQualificationLevelBindingSource.DataSource = (from a in Dbconnection.LookupQualificationLevels
                                                                    select a).ToList<LookupQualificationLevel>();
            };
        }

        #endregion

        #endregion

        #region Wizard Page Sections

        #region Student Details Page

        #region Page Logic Methods
        private void LoadStudentPictureFromDataBase()
        {
            //Student StudentObj = (Student)studentBindingSource.Current;

            using (var Dbconnection = new MCDEntities())
            {
                StudentPictureToUploaded = (from a in Dbconnection.StudentPhotos
                                            where a.StudentID == CurrentStudentID
                                            select a.File).ToList<Data.Models.File>();
            };

        }
        private void switchStudentPictureButtons()
        {
            Student StudentObj = (Student)studentBindingSource.Current;

            if (StudentPictureToUploaded.Count > 0)
            {
                splitContainerAddupdateStudentPic.Panel1Collapsed = true;
                splitContainerAddupdateStudentPic.Panel2Collapsed = false;
                //this.btnStudentPictureAdd.Visible = false;
                //this.btnStudentPictureUpdate.Visible = true;

            }
            else
            {
                splitContainerAddupdateStudentPic.Panel1Collapsed = false;
                splitContainerAddupdateStudentPic.Panel2Collapsed = true;
                //this.btnStudentPictureAdd.Visible = true;
                //this.btnStudentPictureUpdate.Visible = false;
            }
        }
        private void ShowStudentPicture()
        {
            foreach (Data.Models.File f in StudentPictureToUploaded)
            {
                MemoryStream ms = new MemoryStream(f.FileImage);
                picStudentPicture.Image = new Bitmap(ms);
            }

        }

        #endregion
        #region Control Methods
        private void btnStudentPictureAdd_Click(object sender, EventArgs e)
        {
            StudentPictureToUploaded = FileHandeling.UploadFile(UseMultipleFileSelect: false, AutomaicallyAddFileToDatabase: false, ImagesOnly: true);

            if (StudentPictureToUploaded.Count > 0)
            {
                if (CurrentStudentID != 0)
                {

                    using (var Dbconnection = new MCDEntities())
                    {
                        Data.Models.File f = StudentPictureToUploaded.FirstOrDefault<Data.Models.File>();
                        Dbconnection.Files.Add(f);
                        Dbconnection.SaveChanges();

                        //Dbconnection.Files.Attach(f);
                        Dbconnection.StudentPhotos.Add(new StudentPhoto()
                        {
                            FileID = f.FileID,
                            StudentID = CurrentStudentID,
                            DateUpdated = DateTime.Now,
                            StudentPhotoID = 0
                        });
                        Dbconnection.SaveChanges();
                    };
                }
            }

            ShowStudentPicture();
            switchStudentPictureButtons();

        }
        private void btnStudentPictureUpdate_Click(object sender, EventArgs e)
        {
            List<Data.Models.File> FileToUpdate = FileHandeling.UploadFile(UseMultipleFileSelect: false, AutomaicallyAddFileToDatabase: false, ImagesOnly: true);


            Data.Models.File f = StudentPictureToUploaded.FirstOrDefault<Data.Models.File>();
            if (CurrentStudentID != 0)
            {

                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.Files.Attach(f);
                    Dbconnection.Entry(f).State = EntityState.Modified;
                    f.FileImage = FileToUpdate.First<Data.Models.File>().FileImage;
                    Dbconnection.SaveChanges();

                };
            }
            else
            {
                f.FileImage = FileToUpdate.First<Data.Models.File>().FileImage;
            }
            ShowStudentPicture();
        }
        #endregion
        #endregion

        #region Student ID Documents
        #region Populate Methods
        private void populateStudentIDDocuments()
        {
            using (var Dbconnection = new MCDEntities())
            {
                var StudentIDDocumentFiles = (from a in Dbconnection.StudentIDDocuments
                                              where a.StudentID == this.CurrentStudentID
                                              select new
                                              {
                                                  FileID = a.File.FileID,
                                                  FileName = a.File.FileName,
                                                  FileExtension = a.File.FileExtension,
                                                  DateCreated = a.File.DateCreated,
                                                  ContentType = a.File.ContentType
                                              }).ToList();
                List<Data.Models.File> AllStudentIDDocuments = new List<Data.Models.File>();
                foreach (var obj in StudentIDDocumentFiles)
                {
                    AllStudentIDDocuments.Add(new Data.Models.File()
                    {
                        FileID = obj.FileID,
                        FileName = obj.FileName,
                        ContentType = obj.ContentType,
                        DateCreated = obj.DateCreated,
                        FileExtension = obj.FileExtension
                    });
                }
                fileStudentIDDocumentBindingSource.DataSource = AllStudentIDDocuments;
            };
        }

        private void populateStudentAddresses()
        {

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



                    try
                    {
                        //ToDo - Catch All Verification and validation on fields before saving.
                        //check and adds the new student to the database or updates the exsiting one.


                        Student StudentObj = (Student)studentBindingSource.Current;
                        using (var Dbconnection = new MCDEntities())
                        {


                            if (StudentObj.ObjectState == EntityObjectState.Added)
                            {
                                Student StudentFound;


                                using (var DbconnectionInner = new MCDEntities())
                                {
                                    StudentFound = (from a in Dbconnection.Students
                                                    where a.StudentIDNumber.Contains(txtStudentIDNumber.Text)
                                                    select a).FirstOrDefault<Student>();
                                };

                                if (txtStudentIDNumber.Text.Length != 0 && StudentFound != null)
                                {
                                    //throw new DbEntityValidationException("(ID Number Invalid) - ID Number Already Exists in the System Please Re-Enter ID Number Or Search Again!");
                                    //MetroMessageBox.Show(this, "(ID Number Invalid) - ID Number Already Exists in the System Please Re-Enter ID Number Or Search Again!", "Error Message", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                    MessageBox.Show("(ID Number Invalid) - ID Number Already Exists in the System Please Re-Enter ID Number Or Search Again!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    bRtn = false;
                                    break;
                                }
                                else
                                {
                                    Student NewStudent = new Student()
                                    {
                                        StudentID = 0,
                                        StudentIDNumber = txtStudentIDNumber.Text.ToString(),
                                        StudentPassportNumber = txtStudentIDNumber.Text.ToString(),
                                        EthnicityID = Convert.ToInt32(cboStudentEthnicity.SelectedValue),
                                        GenderID = Convert.ToInt32(cboStudentGender.SelectedValue),
                                        MartialStatusID = Convert.ToInt32(cboStudentMartialStatus.SelectedValue),
                                        QualificationLevelID = Convert.ToInt32(cboStudentHighestQualificationLevel.SelectedValue),
                                        StudentCurrentPosition = "",
                                        StudentlInitialDate = DateTime.Now,
                                        Individual = new Individual()
                                        {
                                            IndividualID = 0,
                                            IndividualFirstName = txtStudentFirstName.Text,
                                            IndividualSecondName = txtStudentSecondName.Text,
                                            IndividualLastname = txtStudentLastname.Text,
                                            TitleID = Convert.ToInt32(cboStudentTitle.SelectedValue),

                                        }
                                    };

                                    Dbconnection.Students.Add(NewStudent);
                                    Dbconnection.SaveChanges();
                                    CurrentStudentID = NewStudent.StudentID;
                                    //StudentProgressFile newStudentProgressFile = new StudentProgressFile()
                                    //{
                                    //    StudentID = CurrentStudentID,
                                    //    DateLastUpdated = DateTime.Now,
                                    //    ProgressFile = new ProgressFile()
                                    //    {
                                    //        DateCreated = DateTime.Now,
                                    //        ProgressFileTypeID = (int)Common.Enum.EnumProgessFileTypes.Student
                                    //    }
                                    //};

                                    //Dbconnection.StudentProgressFiles.Add(newStudentProgressFile);
                                    //Dbconnection.SaveChanges();
                                    Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(NewStudent.StudentID);
                                    //CurrentSelectedStudent = NewStudent;

                                    if (StudentPictureToUploaded.Count > 0)
                                    {
                                        Data.Models.File f = StudentPictureToUploaded.FirstOrDefault<Data.Models.File>();
                                        Dbconnection.Files.Add(f);
                                        Dbconnection.SaveChanges();

                                        Dbconnection.StudentPhotos.Add(new StudentPhoto()
                                        {
                                            FileID = f.FileID,
                                            StudentID = CurrentStudentID,
                                            DateUpdated = DateTime.Now,
                                            StudentPhotoID = 0
                                        });
                                        Dbconnection.SaveChanges();
                                    }


                                    StudentObj = NewStudent;
                                    StudentObj.ObjectState = EntityObjectState.Modified;
                                    IsClosingPrematurly = false;
                                }

                            }

                            if (StudentObj.ObjectState == EntityObjectState.Modified)
                            {
                                Dbconnection.Students.Attach(StudentObj);
                                StudentObj.Individual.IndividualFirstName = txtStudentFirstName.Text;
                                StudentObj.Individual.IndividualSecondName = txtStudentSecondName.Text;
                                StudentObj.Individual.IndividualLastname = txtStudentLastname.Text;
                                StudentObj.Individual.TitleID = Convert.ToInt32(cboStudentTitle.SelectedValue);
                                StudentObj.StudentIDNumber = txtStudentIDNumber.Text.ToString();
                                StudentObj.EthnicityID = Convert.ToInt32(cboStudentEthnicity.SelectedValue);
                                StudentObj.GenderID = Convert.ToInt32(cboStudentGender.SelectedValue);
                                StudentObj.MartialStatusID = Convert.ToInt32(cboStudentMartialStatus.SelectedValue);
                                StudentObj.QualificationLevelID = Convert.ToInt32(cboStudentHighestQualificationLevel.SelectedValue);

                                Dbconnection.Entry(StudentObj).State = System.Data.Entity.EntityState.Modified;
                                Dbconnection.SaveChanges();
                            }

                            StudentObj.ObjectState = EntityObjectState.Modified;
                        };
                        CurrentSelectedStudent = StudentObj;
                        studentBindingSource.DataSource = StudentObj;
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                        {
                            foreach (DbValidationError error in entityErr.ValidationErrors)
                            {
                                MessageBox.Show(error.ErrorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        bRtn = false;
                    }
                    //studentBindingSource.ResetCurrentItem();
                    break;
                case 1:
                    if (dgvStudentIDDocuments.Rows.Count == 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "No ID Document Loaded for this Student, would you like to upload it now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        //DialogResult result = MessageBox.Show("Please Upload ID Document", "Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                        if (result == DialogResult.No)
                        {
                            this.loadupStepTwo();
                        }
                        else
                            if (result == DialogResult.Yes)
                        {
                            this.UploadFiles();
                            this.populateStudentIDDocuments();
                        }
                    }
                    break;
                case 2:
                    this.addStudentAddress();
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
            if (studentBindingSource.List.Count == 0)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    //Dbconnection.Configuration.ProxyCreationEnabled = false;
                    Student StudentObj = (from a in Dbconnection.Students
                                          where a.StudentID == CurrentStudentID
                                          select a)
                                          .Include(a => a.NextOfKins)
                                          .Include(a => a.NextOfKins.Select(b => b.Students))
                                          .Include(a => a.NextOfKins.Select(b => b.LookupTypeOfRelation))
                                          .Include(a => a.NextOfKins.Select(b => b.Individual))
                                          .Include(a => a.NextOfKins.Select(b => b.Individual.ContactDetails))
                                          .Include(a => a.StudentAssociatedCompanies)
                                          .Include(a => a.StudentAssociatedCompanies.Select(b => b.Company))
                                            .Include(a => a.Individual)
                                            .Include(a => a.Individual.ContactDetails)
                                            .Include(a => a.Individual.ContactDetails.Select(b => b.LookupContactType))
                                            .Include(a => a.Individual.Addresses)
                                            .Include(a => a.Individual.Addresses.Select(b => b.LookupCountry))
                                            .Include(a => a.Individual.Addresses.Select(b => b.LookupAddressType))
                                            .Include(a => a.Individual.Addresses.Select(b => b.LookupProvince))
                                            .Include(a => a.StudentDisabilities)
                                            .Include(a => a.StudentDisabilities.Select(b => b.LookupDisability))
                                            //LookupContactType
                                            .Include(a => a.Individual.Companies.Select(b => b.ContactDetails))
                                            .Include(a => a.StudentDisabilities)
                                            //.Include(a => a.StudentPhotos)
                                            .FirstOrDefault<Student>();
                    if (StudentObj == null)
                    {
                        StudentObj = new Student()
                        {
                            StudentID = 0,
                            ObjectState = EntityObjectState.Added,
                            Individual = new Individual()
                            {
                                IndividualID = 0,
                                ObjectState = EntityObjectState.Added
                            }
                        };
                    }
                    else
                    {
                        txtStudentFirstName.Text = StudentObj.Individual.IndividualFirstName;
                        txtStudentSecondName.Text = StudentObj.Individual.IndividualSecondName;
                        txtStudentLastname.Text = StudentObj.Individual.IndividualLastname;
                        txtStudentIDNumber.Text = StudentObj.StudentIDNumber.ToString();
                        txtPassportNumber.Text = StudentObj.StudentPassportNumber.ToString();
                        cboStudentTitle.SelectedValue = StudentObj.Individual.TitleID;
                        cboStudentEthnicity.SelectedValue = StudentObj.EthnicityID;
                        cboStudentGender.SelectedValue = StudentObj.GenderID;
                        cboStudentMartialStatus.SelectedValue = StudentObj.MartialStatusID;
                        cboStudentHighestQualificationLevel.SelectedValue = StudentObj.QualificationLevelID;
                        StudentObj.ObjectState = EntityObjectState.Modified;
                        LoadStudentPictureFromDataBase();
                        ShowStudentPicture();
                        switchStudentPictureButtons();
                        IsClosingPrematurly = false;
                        this.CurrentSelectedStudent = StudentObj;
                    }
                    studentBindingSource.DataSource = StudentObj;

                };
                switchStudentPictureButtons();

                txtStudentNumber.Focus();
            }
        }

        private void loadupStepTwo()
        {
            refreshStudentIDDocumnets();
        }

        private void loadupStepThree()
        {
            this.refreshStudentAddressDetails();
        }


        private void loadupStepFour()
        {
            refreshContactDetails();
        }
        private void loadupStepFive()
        {
            refreshStudentDisablities();

        }
        private void loadupStepSix()
        {
            refreshStudentCompany();
        }
        private void loadupStepSeven()
        {
            //Student Next Of Kin
            this.refreshNextOfKin();
        }
        private void loadupStepEight()
        {
            //Summary
        }

        #endregion

        #endregion

        #endregion

        private void frmStudentAddUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClosingPrematurly)
            {
                DialogResult Rtn = MessageBox.Show("Process incomplete, do you wish to save the change made so far?", "Warning Form Closing", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (Rtn == DialogResult.Yes)
                {
                    if (!this.ValidateStep())
                    {
                        e.Cancel = true;
                    }
                }
                if (Rtn == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

        }

        private void UploadFiles()
        {
            List<Data.Models.File> StudentIDDocumentsToUploaded = FileHandeling.UploadFile(
                UseMultipleFileSelect: true,
                AutomaicallyAddFileToDatabase: false,
                ImagesOnly: false);

            if (StudentIDDocumentsToUploaded.Count > 0)
            {

                using (var Dbconnection = new MCDEntities())
                {

                    foreach (Data.Models.File f in StudentIDDocumentsToUploaded)
                    {
                        Dbconnection.StudentIDDocuments.Add(new StudentIDDocument()
                        {
                            FileID = f.FileID,
                            StudentID = this.CurrentStudentID,
                            File = f
                        });
                        Dbconnection.SaveChanges();
                    }
                };
                this.refreshStudentIDDocumnets();

            }
        }

        private void btnAddStudentIDDocuments_Click(object sender, EventArgs e)
        {
            this.UploadFiles();
        }

        private void btnRemoveStudentIDDocuments_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You Sure You Wish To Remove This File?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Rtn == DialogResult.Yes)
            {
                Data.Models.File FileToRemove = (Data.Models.File)fileStudentIDDocumentBindingSource.Current;

                using (var Dbconnection = new MCDEntities())
                {
                    //Data.Models.File x = (from a in Dbconnection.Files
                    //                      where a.)
                    List<StudentIDDocument> x = (from a in Dbconnection.StudentIDDocuments
                                                 where a.FileID == FileToRemove.FileID
                                                 select a).ToList<StudentIDDocument>();
                    foreach (StudentIDDocument doc in x)
                    {
                        Dbconnection.Entry(doc).State = EntityState.Deleted;
                    }

                    Dbconnection.SaveChanges();
                };
                this.refreshStudentIDDocumnets();
            }

        }

        private void dgvStudentIDDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {

                case 0:


                    var FileObj = fileStudentIDDocumentBindingSource.Current;
                    Data.Models.File x = new Data.Models.File();
                    //loop through the properties of the object you want to covert:          
                    foreach (PropertyInfo pi in FileObj.GetType().GetProperties())
                    {
                        try
                        {
                            //get the value of property and try 
                            //to assign it to the property of T type object:
                            x.GetType().GetProperty(pi.Name).SetValue(x, pi.GetValue(FileObj, null), null);
                        }
                        catch { }
                    }

                    folderBrowserDialogForDownloading.ShowDialog();

                    if (folderBrowserDialogForDownloading.SelectedPath.Length > 0)
                    {
                        try
                        {
                            Data.Models.File CurrentFile = FileHandeling.GetFile(x.FileID);
                            string path = folderBrowserDialogForDownloading.SelectedPath + "\\" + x.FileName;
                            System.IO.File.WriteAllBytes(path, CurrentFile.FileImage);
                            MessageBox.Show(x.FileName + ", Successfully Saved to: " + path, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    break;
            }
        }
        private void populateAddressDataGridView()
        {
            using (var Dbconnection = new MCDEntities())
            {
                addressesBindingSource.DataSource = (from a in Dbconnection.Individuals
                                                     from b in a.Addresses
                                                     where a.IndividualID == this.CurrentStudentID
                                                     select b).ToList<Address>();
            }

        }

        private void addStudentAddress()
        {

        }

        private void btnAddAddress_Click(object sender, EventArgs e)
        {
            this.addStudentAddress();
            //this.populateAddressDataGridView();
        }

        private void dgvStudentAddress_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    Address AddressObj = (Address)(row.DataBoundItem);
                    //var _Provinces = TDCETMD.LookupProvince;

                    row.Cells[colAddressType.Index].Value = AddressObj.LookupAddressType.AddressType.ToString();
                    //row.Cells[addressLineTwoDataGridViewTextBoxColumn.Index].Value = _AddresslineTwo.ToString();
                    //row.Cells[addressTownDataGridViewTextBoxColumn.Index].Value = _Town.ToString();
                    //row.Cells[addressAreaCodeDataGridViewTextBoxColumn.Index].Value = _Code.ToString();

                }
            }
        }



        private void dgvStudentContactDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var ContactDetailObj = (ContactDetail)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colStudentContactType.Index].Value = ContactDetailObj.LookupContactType.ContactType.ToString();

                }
            }
        }



        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(((ContactDetail)contactDetailsBindingSource.Current).ContactDetailID))
            {
                if (CurrentSelectedStudent != null)
                {

                    // Individual CurrentContact = CurrentSelectedStudent.Individual;
                    // ContactDetail CurrentDetail = ;
                    frm.CurrentDetail = (ContactDetail)contactDetailsBindingSource.Current;
                    frm.ShowDialog();
                    if (frm.CurrentDetail != null)
                    {
                        using (var Dbconnection = new MCDEntities())
                        {

                            //Dbconnection.ContactDetails.Attach(frm.CurrentDetail);

                            //Dbconnection.Entry(frm.CurrentDetail).State = EntityState.Modified;
                            //Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                            //Dbconnection.SaveChanges();


                        };
                        this.refreshContactDetails();
                    }
                }
            }
        }

        private void btnAddStudentContactDetails_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateContactDetails frm = new frmAddUpdateContactDetails(0))
            {
                if (CurrentSelectedStudent != null)
                {

                    Individual CurrentContact = CurrentSelectedStudent.Individual;

                    frm.ShowDialog();
                    if (frm.CurrentDetail != null)
                    {
                        using (var Dbconnection = new MCDEntities())
                        {
                            Dbconnection.Individuals.Attach(CurrentContact);

                            Dbconnection.ContactDetails.Attach(frm.CurrentDetail);

                            CurrentContact.ContactDetails.Add(frm.CurrentDetail);

                            Dbconnection.SaveChanges();

                            Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                        };
                        this.refreshContactDetails();
                    }
                }
            }
        }



        private void btnRemoveContactDetails_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Rtn == DialogResult.Yes)
            {
                using (var Dbconnection = new MCDEntities())
                {
                    Individual CurrentContact = CurrentSelectedStudent.Individual;
                    ContactDetail CD = (ContactDetail)contactDetailsBindingSource.Current;

                    Dbconnection.Individuals.Attach(CurrentContact);
                    Dbconnection.ContactDetails.Attach(CD);

                    Dbconnection.Entry(CD).State = EntityState.Deleted;
                    //Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                    Dbconnection.SaveChanges();



                    this.refreshContactDetails();

                };
            }

        }

        private void btnAddStudentNewAddress_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateAddresses frm = new frmAddUpdateAddresses(0))
            {
                frm.ShowDialog();
                if (frm.CurrentAddress != null && frm.IsSuccessfullyUpdated)
                {
                    if (CurrentSelectedStudent != null)
                    {

                        Individual CurrentContact = CurrentSelectedStudent.Individual;

                        if (frm.CurrentAddress != null)
                        {
                            using (var Dbconnection = new MCDEntities())
                            {
                                Dbconnection.Individuals.Attach(CurrentContact);

                                Dbconnection.Addresses.Attach(frm.CurrentAddress);

                                CurrentContact.Addresses.Add(frm.CurrentAddress);

                                Dbconnection.SaveChanges();

                                if (frm.CurrentAddress.AddressIsDefault == true)
                                {
                                    foreach (Address add in CurrentSelectedStudent.Individual.Addresses)
                                    {
                                        add.AddressIsDefault = false;
                                        Dbconnection.Addresses.Attach(add);
                                        Dbconnection.Entry(add).State = EntityState.Modified;
                                    }
                                    frm.CurrentAddress.AddressIsDefault = true;
                                    Dbconnection.SaveChanges();
                                }

                                Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupAddressType).Load();
                                Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupProvince).Load();
                                Dbconnection.Entry(frm.CurrentAddress).Reference(a => a.LookupCountry).Load();
                            };
                            this.refreshStudentAddressDetails();
                        }
                    }

                }
            }




        }
        private void btnRemoveStudentAddress_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Rtn == DialogResult.Yes)
            {
                if (addressesBindingSource.Count > 0)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Individual CurrentContact = CurrentSelectedStudent.Individual;
                        Address A = (Address)addressesBindingSource.Current;

                        Dbconnection.Individuals.Attach(CurrentContact);
                        Dbconnection.Addresses.Attach(A);

                        Dbconnection.Entry(A).State = EntityState.Deleted;
                        //Dbconnection.Entry(frm.CurrentDetail).Reference("LookupContactType").Load();
                        Dbconnection.SaveChanges();


                        this.refreshStudentAddressDetails();
                        if (A.AddressIsDefault && addressesBindingSource.Count > 0)
                        {
                            addressesBindingSource.MoveFirst();
                            Address B = (Address)addressesBindingSource.Current;
                            Dbconnection.Addresses.Attach(B);
                            B.AddressIsDefault = true;
                            Dbconnection.Entry(B).State = EntityState.Modified;
                            Dbconnection.SaveChanges();
                            this.refreshStudentAddressDetails();
                        }
                    };
                }
            }
        }
        private void btnUpdateStudentAddress_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateAddresses frm = new frmAddUpdateAddresses(((Address)addressesBindingSource.Current).AddressID))
            {

                if (CurrentSelectedStudent != null && addressesBindingSource.Count > 0)
                {
                    frm.CurrentAddress = (Address)addressesBindingSource.Current;


                    frm.ShowDialog();
                    if (frm.CurrentAddress != null && frm.IsSuccessfullyUpdated)
                    {
                        if (frm.CurrentAddress.AddressIsDefault == true)
                        {

                            using (var Dbconnection = new MCDEntities())
                            {
                                foreach (Address add in CurrentSelectedStudent.Individual.Addresses)
                                {
                                    add.AddressIsDefault = false;
                                    Dbconnection.Addresses.Attach(add);
                                    Dbconnection.Entry(add).State = EntityState.Modified;
                                }
                                frm.CurrentAddress.AddressIsDefault = true;
                                Dbconnection.SaveChanges();
                            };

                        }
                        this.refreshStudentAddressDetails();
                    }
                }
            }
        }



        private void btnUpdateStudentDisablitiy_Click(object sender, EventArgs e)
        {
            StudentDisability SD = (StudentDisability)studentDisabilitiesBindingSource.Current;

            using (frmAddUpdateDisability frm = new frmAddUpdateDisability(CurrentSelectedStudent.StudentID, SD.DisabilityID))
            {
                frm.CurrentStudentDisablity = SD;
                frm.ShowDialog();
                this.refreshStudentDisablities();
            }
        }

        private void btnAddStudentDisablity_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateDisability frm = new frmAddUpdateDisability(CurrentSelectedStudent.StudentID, 0))
            {
                frm.ShowDialog();
                if (frm.CurrentStudentDisablity != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        //Dbconnection.Students.Attach(CurrentSelectedStudent);
                        Dbconnection.StudentDisabilities.Attach(frm.CurrentStudentDisablity);
                        Dbconnection.Entry(frm.CurrentStudentDisablity).Reference(a => a.LookupDisability).Load();
                        CurrentSelectedStudent.StudentDisabilities.Add(frm.CurrentStudentDisablity);
                    };


                }
                this.refreshStudentDisablities();
            }
        }

        private void btnRemoveStudentDisablity_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Rtn == DialogResult.Yes)
            {
                StudentDisability SD = (StudentDisability)studentDisabilitiesBindingSource.Current;
                using (var Dbconnection = new MCDEntities())
                {
                    Dbconnection.Students.Attach(CurrentSelectedStudent);
                    Dbconnection.StudentDisabilities.Attach(SD);
                    Dbconnection.Entry(SD).State = EntityState.Deleted;
                    // Dbconnection.StudentDisabilities.Remove(SD);
                    Dbconnection.SaveChanges();
                    this.refreshStudentDisablities();
                };
            }

        }



        private void picSelectCompany_Click(object sender, EventArgs e)
        {
            gbSelectCompanyDetails.Visible = true;
            // gbSelectCompanyDetails.Visible = false;
            picSelectCompany.BackColor = Color.Gainsboro;
            picSelectPrivate.BackColor = Color.Transparent;

            lblCurentlySelectedSiteType.Text = "Company";


        }

        private void picSelectPrivate_Click(object sender, EventArgs e)
        {
            gbSelectCompanyDetails.Visible = false;
            //gbSelectCompanyDetails.Visible = true;
            picSelectCompany.BackColor = Color.Transparent;
            picSelectPrivate.BackColor = Color.Gainsboro;
            lblCurentlySelectedSiteType.Text = "Private";

            using (var Dbconnection = new MCDEntities())
            {
                foreach (StudentAssociatedCompany SAC in CurrentSelectedStudent.StudentAssociatedCompanies)
                {
                    Dbconnection.StudentAssociatedCompanies.Attach(SAC);
                    SAC.IsCurrentCompany = false;
                    Dbconnection.Entry(SAC).State = EntityState.Modified;
                }
                Dbconnection.SaveChanges();
                int SPFID = Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(CurrentSelectedStudent.StudentID);
                List<CompanyStudentProgressFile> CSPFList = Dbconnection.CompanyStudentProgressFiles.Where(a => a.StudentProgressFileID == SPFID).ToList<CompanyStudentProgressFile>();
                Dbconnection.CompanyStudentProgressFiles.RemoveRange(CSPFList);
                Dbconnection.SaveChanges();
            };


            this.refreshStudentCompany();
        }

        private void btnSearchForCompany_Click(object sender, EventArgs e)
        {
            using (frmCompanySearch frm = new frmCompanySearch())
            {
                frm.ShowDialog();

                if (frm.CurrentCompany != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.Students.Attach(CurrentSelectedStudent);

                        Boolean PreviouslyAssociatedWithCompany = false;
                        if (CurrentSelectedStudent.StudentAssociatedCompanies.Count > 0)
                        {
                            foreach (StudentAssociatedCompany CompanyStatusToUpdate in CurrentSelectedStudent.StudentAssociatedCompanies)
                            {
                                Dbconnection.StudentAssociatedCompanies.Attach(CompanyStatusToUpdate);
                                CompanyStatusToUpdate.IsCurrentCompany = false;
                                if (CompanyStatusToUpdate.StudentID == this.CurrentSelectedStudent.StudentID
                                    && CompanyStatusToUpdate.CompanyID == frm.CurrentCompany.CompanyID)
                                {
                                    PreviouslyAssociatedWithCompany = true;
                                    CompanyStatusToUpdate.IsCurrentCompany = true;

                                }
                                Dbconnection.Entry(CompanyStatusToUpdate).State = EntityState.Modified;
                            }
                        }

                        if (PreviouslyAssociatedWithCompany)
                        {
                            Dbconnection.SaveChanges();
                        }
                        else
                        {
                            StudentAssociatedCompany SAC = new StudentAssociatedCompany()
                            {
                                CompanyID = frm.CurrentCompany.CompanyID,
                                StudentID = CurrentSelectedStudent.StudentID,
                                IsCurrentCompany = true


                            };
                            CurrentSelectedStudent.StudentAssociatedCompanies.Add(SAC);
                            Dbconnection.SaveChanges();
                            Dbconnection.Entry(SAC).Reference(A => A.Company).Load();
                        }
                        int SPFID = Common.Verifiction.OfProgressFiles.VerifyStudentProgressFile(CurrentStudentID);
                        List<CompanyStudentProgressFile> CSPFList = Dbconnection.CompanyStudentProgressFiles.Where(a => a.StudentProgressFileID == SPFID).ToList<CompanyStudentProgressFile>();
                        Dbconnection.CompanyStudentProgressFiles.RemoveRange(CSPFList);
                        Dbconnection.SaveChanges();
                        Common.Verifiction.OfProgressFiles.VerifyCompanyStudentProgressFile(CurrentStudentID, frm.CurrentCompany.CompanyID);


                    };
                    this.refreshStudentCompany();
                }
            }

        }

        private void btnReViewCompanyDetails_Click(object sender, EventArgs e)
        {
            if (studentAssociatedCompaniesBindingSource.Count > 0)
            {
                using (ViewEditCompanyDetails frm = new ViewEditCompanyDetails(((Data.Models.StudentAssociatedCompany)studentAssociatedCompaniesBindingSource.Current).Company.CompanyID)) 
                {
                   
                    frm.ShowDialog();
                    // this.refreshStudentCompany();
                };

            }
        }

        private void dgvStudentDisablity_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var StudentDisabilityObj = (StudentDisability)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colStudentDisability.Index].Value = StudentDisabilityObj.LookupDisability.Disability.ToString();

                }
            }
        }

        private void bindingNavigator2_RefreshItems(object sender, EventArgs e)
        {

        }

        private void btnAddNewNextOfKin_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateNextOfKin frm = new frmAddUpdateNextOfKin(0))
            {
                frm.ShowDialog();
                if (frm.CurrentNextOfKin != null)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.NextOfKins.Attach(frm.CurrentNextOfKin);

                        Dbconnection.Students.Attach(CurrentSelectedStudent);

                        CurrentSelectedStudent.NextOfKins.Add(frm.CurrentNextOfKin);
                        Dbconnection.Entry(frm.CurrentNextOfKin).Reference(a => a.LookupTypeOfRelation).Load();
                        Dbconnection.Entry(frm.CurrentNextOfKin).Collection(a => a.Students).Load();
                        // Dbconnection.Entry(frm.CurrentNextOfKin).Collection(a => a.Individual.ContactDetails).Load();
                        Dbconnection.SaveChanges();
                    };
                    this.refreshNextOfKin();
                }
            }
        }

        private void btnRemoveNextOfKin_Click(object sender, EventArgs e)
        {
            DialogResult Rtn = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Rtn == DialogResult.Yes)
            {
                Data.Models.NextOfKin NOK = (Data.Models.NextOfKin)nextOfKinsBindingSource.Current;

                if (nextOfKinsBindingSource.Count > 0)
                {
                    using (var Dbconnection = new MCDEntities())
                    {
                        Dbconnection.NextOfKins.Attach(NOK);
                        Dbconnection.Entry(NOK).State = EntityState.Deleted;
                        Dbconnection.SaveChanges();
                    }
                    this.refreshNextOfKin();
                }
            }


        }

        private void btnUpdateNextOfKin_Click(object sender, EventArgs e)
        {
            Data.Models.NextOfKin NOK = (Data.Models.NextOfKin)nextOfKinsBindingSource.Current;

            using (frmAddUpdateNextOfKin frm = new frmAddUpdateNextOfKin(NOK.IndividualID))
            {
                frm.CurrentNextOfKin = NOK;
                frm.ShowDialog();

                this.refreshNextOfKin();
            }
        }

        private void btnAddNextOfKinContactDertails_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveNextOfKinContactDertails_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateNextOfKinContactDertails_Click(object sender, EventArgs e)
        {

        }

        private void dgvNextOfKin_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var gridView = (DataGridView)sender;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var NextOfKinObj = (Data.Models.NextOfKin)(row.DataBoundItem);
                    //var IndividualObj = ContactDetailObj.Individual;

                    row.Cells[colNextOfKinContactType.Index].Value = NextOfKinObj.LookupTypeOfRelation.TypeOfRelation.ToString();
                    row.Cells[colNextOfKinContactName.Index].Value = NextOfKinObj.Individual.FullName.ToString();

                }
            }
        }
    }
}
