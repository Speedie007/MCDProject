using Impendulo.Common.Enum;
using Impendulo.Data.Models;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.Development.Contacts
{
    public partial class frmContactsAddUpdateContacts : MetroForm
    {

        public Boolean IsStudent { get; set; }
        public Individual CurrentContact { get; set; }
        private int IndividualID { get; set; }

        public frmContactsAddUpdateContacts(int IndividualID, Individual CurrentContact = null)
        {
            this.CurrentContact = CurrentContact;
            this.IndividualID = IndividualID;
            IsStudent = false;
            InitializeComponent();
        }

        private void frmContactsV2_Load(object sender, EventArgs e)
        {
            this.populateTitles();
            if (IsStudent)
            {
                txtIDNumber.Visible = true;
                lblIDNumber.Visible = true;
            }
            else
            {
                txtIDNumber.Visible = false;
                lblIDNumber.Visible = false;
            }
            if (IndividualID != 0)
            {
                this.setControls();
            }
            this.setContactAddUpdateButtons();
        }
        private void populateTitles()
        {
            using (var Dbconnection = new MCDEntities())
            {
                lookupTitleBindingSource.DataSource = (from a in Dbconnection.LookupTitles
                                                       select a).ToList<LookupTitle>();
            };
        }
        private void setControls()
        {
            //create individual with properties set
            Individual ContactObj;
            using (var Dbconnection = new MCDEntities())
            {
                ContactObj = (from a in Dbconnection.Individuals
                              where a.IndividualID == IndividualID
                              select a).FirstOrDefault<Individual>();
            };
            this.cboIndividualTitle.SelectedValue = ContactObj.TitleID;
            this.txtFirstName.Text = ContactObj.IndividualFirstName;
            this.txtSecondName.Text = ContactObj.IndividualSecondName;
            this.txtLastName.Text = ContactObj.IndividualLastname;
        }
        private void setContactAddUpdateButtons()
        {
            if (IndividualID == 0)
            {
                btnAddContact.Visible = true;
                btnUpdateContact.Visible = false;
            }
            else
            {
                btnAddContact.Visible = false;
                btnUpdateContact.Visible = true;
            }
        }
        private void setContactControls()
        {
            if (IndividualID == 0)
            {
                this.txtFirstName.Clear();
                this.txtSecondName.Clear();
                this.txtLastName.Clear();
            }
            else
            {
                this.txtFirstName.Text = CurrentContact.IndividualFirstName;
                this.txtSecondName.Text = CurrentContact.IndividualSecondName;
                this.txtLastName.Text = CurrentContact.IndividualLastname;
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            if (IsStudent)
            {
                Student StudentObj = new Student()
                {
                    EthnicityID = (int)EnumEthnicities.Other_Unspecified,
                    GenderID = (int)EnumGenders.Male,
                    MartialStatusID = (int)EnumMartialStatuses.Single,
                    QualificationLevelID = (int)EnumQualificationLevels.NQF_1_Grade_9_National_Certificate,
                    StudentlInitialDate = DateTime.Today,
                    StudentIDNumber = txtIDNumber.Text,
                    StudentCurrentPosition = "",
                    Individual = new Individual()
                    {
                        // IndividualID = 0,
                        TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue),
                        IndividualFirstName = txtFirstName.Text.ToString(),
                        IndividualSecondName = txtSecondName.Text.ToString(),
                        IndividualLastname = txtLastName.Text.ToString()

                    }
                };

                using (MCDEntities DbConnection = new MCDEntities())
                {
                    //We are saving a new student into the Student Collection
                    DbConnection.Students.Add(StudentObj);
                    DbConnection.SaveChanges();

                }
                CurrentContact = StudentObj.Individual;
            }
            else
            {

                using (var Dbconnection = new MCDEntities())
                {
                    using (System.Data.Entity.DbContextTransaction dbTran = Dbconnection.Database.BeginTransaction())
                    {
                        try
                        {
                            //CRUD Operations
                            CurrentContact = new Individual
                            {
                                TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue),
                                IndividualFirstName = txtFirstName.Text.ToString(),
                                IndividualSecondName = txtSecondName.Text.ToString(),
                                IndividualLastname = txtLastName.Text.ToString()
                            };
                            Dbconnection.Individuals.Add(CurrentContact);
                            ////saves all above operations within one transaction
                            Dbconnection.SaveChanges();

                            //commit transaction
                            dbTran.Commit();
                            this.Close();
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

        private void btnUpdateContact_Click(object sender, EventArgs e)
        {
            Individual IndividualToUpdate = null;
            using (var Dbconnection = new MCDEntities())
            {
                 IndividualToUpdate = (from a in Dbconnection.Individuals
                                                 where a.IndividualID == IndividualID
                                                 select a).FirstOrDefault<Individual>();
                IndividualToUpdate.TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue);
                IndividualToUpdate.IndividualFirstName = txtFirstName.Text.ToString();
                IndividualToUpdate.IndividualSecondName = txtSecondName.Text.ToString();
                IndividualToUpdate.IndividualLastname = txtLastName.Text.ToString();
                Dbconnection.SaveChanges();
                Dbconnection.Entry(IndividualToUpdate).Reference(a => a.LookupTitle).Load();
            };
            if (CurrentContact != null)
            {
                CurrentContact.TitleID = Convert.ToInt32(cboIndividualTitle.SelectedValue);
                CurrentContact.IndividualFirstName = txtFirstName.Text.ToString();
                CurrentContact.IndividualSecondName = txtSecondName.Text.ToString();
                CurrentContact.IndividualLastname = txtLastName.Text.ToString();
                
                CurrentContact.LookupTitle = IndividualToUpdate.LookupTitle;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
